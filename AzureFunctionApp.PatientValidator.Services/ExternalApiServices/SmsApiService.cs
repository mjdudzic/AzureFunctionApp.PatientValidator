using AzureFunctionApp.PatientValidator.Services.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureFunctionApp.PatientValidator.Services.Interfaces;

namespace AzureFunctionApp.PatientValidator.Services.ExternalApiServices
{
	public class SmsApiService : ISmsApiService
	{
		private static readonly HttpClient HttpClient;
		private readonly SmsApiConfiguration _apiConfiguration;

		static SmsApiService()
		{
			HttpClient = new HttpClient();
		}

		public SmsApiService(SmsApiConfiguration configuration)
		{
			_apiConfiguration = configuration;
		}

		public async Task<string> SendMessage(SmsMessagePatientValidationResponse message, params long[] recipients)
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(_apiConfiguration.SmsApiUrl),
				Method = HttpMethod.Post,
				Content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json")
			};

			request.Headers.Add("Accept", "application/json");
			request.Headers.Add("Cache-Control", "no-cache");
			request.Headers.Add("Authorization", $"AccessKey {_apiConfiguration.SmsApiAccessKey}");

			var response = await HttpClient.SendAsync(request);
			var result = await response.Content.ReadAsStringAsync();

			return result;
		}
	}
}
