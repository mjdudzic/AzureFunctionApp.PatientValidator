using AzureFunctionApp.PatientValidator.Services.Interfaces;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionApp.PatientValidator.Services.ExternalApiServices
{
	public class PatientApiService : IPatientApiService
	{
		private static readonly HttpClient HttpClient;
		private readonly PatientApiConfiguration _apiConfiguration;

		static PatientApiService()
		{
			HttpClient = new HttpClient();
		}

		public PatientApiService(PatientApiConfiguration configuration)
		{
			_apiConfiguration = configuration;
		}

		public async Task<PatientApiAuthenticationToken> GetAuthenticationToken()
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(_apiConfiguration.PatientApiAuthenticationUrl),
				Method = HttpMethod.Post,
				Content =
					new StringContent(
						$"username={_apiConfiguration.PatientApiLogin}&password={_apiConfiguration.PatientApiPassword}&grant_type=password",
						Encoding.UTF8,
						"application/x-www-form-urlencoded")
			};

			request.Headers.Add("Accept", "application/json");
			request.Headers.Add("Cache-Control", "no-cache");

			var response = await HttpClient.SendAsync(request);
			var result = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			return JsonConvert.DeserializeObject<PatientApiAuthenticationToken>(result);
		}

		public async Task<PatientApiValidationResult> Verify(string accessToken, params PatientValidationRequest[] requests)
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(_apiConfiguration.PatientApiValidationRequestUrl),
				Method = HttpMethod.Post,
				Content = new StringContent(JsonConvert.SerializeObject(requests), Encoding.UTF8, "application/json")
			};

			request.Headers.Add("Accept", "application/json");
			request.Headers.Add("Cache-Control", "no-cache");
			request.Headers.Add("Authorization", $"Bearer {accessToken}");

			string result;

			try
			{
				var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

				result = await response.Content.ReadAsStringAsync();
			}
			catch (Exception ex)
			{
				return new PatientApiValidationResult
				{
					Status = PatientApiValidationResultStatus.CommunicationError,
					Error = new PatientApiValidationResponseError
					{
						Message = ex.Message
					}
				};
			}

			try
			{
				return new PatientApiValidationResult
				{
					Status = PatientApiValidationResultStatus.Verificated,
					Responses = JsonConvert.DeserializeObject<PatientApiValidationResponse[]>(result)
				};
			}
			catch
			{
				return new PatientApiValidationResult
				{
					Status = PatientApiValidationResultStatus.ResponseError,
					Error = JsonConvert.DeserializeObject<PatientApiValidationResponseError>(result)
				};
			}
		}
	}
}
