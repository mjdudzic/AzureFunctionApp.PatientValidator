using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;
using System;
using AzureFunctionApp.PatientValidator.ValueObjects;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace AzureFunctionApp.PatientValidator
{
	public static class PatientValidationRequestFunction
	{
		[FunctionName("PatientValidationRequestFunction")]
		public static async Task<HttpResponseMessage> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req,
			[Queue("patient-validation-request")]ICollector<string> memberQueueItem,
			[Table("PatientValidationRequestFunctionLog")]ICollector<PatientValidationRequestFunctionLog> tableBinding,
			ExecutionContext context,
			TraceWriter log)
		{
			log.Info("C# HTTP trigger function processed a request.");

			var instanceId = Environment.GetEnvironmentVariable(
				"WEBSITE_INSTANCE_ID",
				EnvironmentVariableTarget.Process);

			if (string.IsNullOrWhiteSpace(instanceId))
			{
				instanceId = context.InvocationId.ToString("N");
			}

			var data = await req.Content.ReadAsAsync<SmsMessagePatientValidationRequest>();

			memberQueueItem.Add(JsonConvert.SerializeObject(data));

			tableBinding.Add(new PatientValidationRequestFunctionLog
			{
				PartitionKey = Guid.NewGuid().ToString("N"),
				RowKey = instanceId
			});

			return req.CreateResponse(HttpStatusCode.OK, data, "application/json");
		}
	}
}
