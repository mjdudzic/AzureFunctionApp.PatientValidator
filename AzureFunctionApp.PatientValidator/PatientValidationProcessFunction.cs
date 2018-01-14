using System;
using System.Threading.Tasks;
using AzureFunctionApp.PatientValidator.Services.ExternalApiServices;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;
using AzureFunctionApp.PatientValidator.ValueObjects;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace AzureFunctionApp.PatientValidator
{
	public static class PatientValidationProcessFunction
	{
		[FunctionName("PatientValidationProcessFunction")]
		public static async Task Run(
			[QueueTrigger("patient-validation-request", Connection = "")]string myQueueItem,
			[Table("PatientValidationProcessFunctionLog")]ICollector<PatientValidationProcessFunctionLog> tableBinding,
			ExecutionContext context,
			TraceWriter log)
		{
			var instanceId = Environment.GetEnvironmentVariable(
				"WEBSITE_INSTANCE_ID",
				EnvironmentVariableTarget.Process);

			if (string.IsNullOrWhiteSpace(instanceId))
			{
				instanceId = context.InvocationId.ToString("N");
			}

			var validationRequest = JsonConvert.DeserializeObject<SmsMessagePatientValidationRequest>(myQueueItem);
			if (validationRequest == null)
			{
				log.Error($"Queue trigger function cannot process. Data cannot be deserialized: {myQueueItem}");
				return;
			}

			var validationResult = await ValidatePatient(validationRequest.Body);

			tableBinding.Add(new PatientValidationProcessFunctionLog
			{
				PartitionKey = Guid.NewGuid().ToString("N"),
				RowKey = instanceId,
				CreateDate = DateTime.Now,
				RequestJson = myQueueItem,
				ResponseJson = JsonConvert.SerializeObject(validationResult)
			});

			if (validationResult.Status != PatientApiValidationResultStatus.Verificated)
			{
				log.Error($"Queue trigger function cannot process. SMS message cannot be sent");
				return;
			}

			await SendSms(validationRequest.Originator);
		}

		public static async Task<PatientApiValidationResult> ValidatePatient(string patientNumber)
		{
			var patientApiService = new PatientApiService(new PatientApiConfiguration
			{
				PatientApiAuthenticationUrl = Environment.GetEnvironmentVariable("PatientApiAuthenticationUrl"),
				PatientApiValidationRequestUrl = Environment.GetEnvironmentVariable("PatientApiValidationRequestUrl"),
				PatientApiLogin = Environment.GetEnvironmentVariable("PatientApiLogin"),
				PatientApiPassword = Environment.GetEnvironmentVariable("PatientApiPassword")
			});

			var token = await patientApiService.GetAuthenticationToken();
			if (token == null || string.IsNullOrWhiteSpace(token.AccessToken))
			{
				return new PatientApiValidationResult
				{
					Status = PatientApiValidationResultStatus.CommunicationError,
					Error = new PatientApiValidationResponseError
					{
						Message = "Token not specified"
					}
				};
			}

			return await patientApiService.Verify(token.AccessToken, new PatientValidationRequest
			{
				MemberNumber = patientNumber,
				RefDate = DateTime.Now.Date.ToString("yyyy-MM-dd")
			});
		}

		private static async Task SendSms(string recipientNumber)
		{
			var smsApiService = new SmsApiService(new SmsApiConfiguration
			{
				SmsApiAccessKey = Environment.GetEnvironmentVariable("SmsApiAccessKey"),
				SmsApiOriginator = Environment.GetEnvironmentVariable("SmsApiOriginator"),
				SmsApiUrl = Environment.GetEnvironmentVariable("SmsApiUrl")
			});

			if (!Int64.TryParse(recipientNumber, out long number))
			{
				return;
			}

			await smsApiService.SendMessage(new SmsMessagePatientValidationResponse
			{
				Originator = Environment.GetEnvironmentVariable("SmsApiOriginator"),
				Recipients = new[] { number },
				Body = $"todo"
			}, number);
		}
	}
}
