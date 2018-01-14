using Newtonsoft.Json;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class SmsMessagePatientValidationRequest
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("recipient")]
		public string Recipient { get; set; }

		[JsonProperty("originator")]
		public string Originator { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("createdDatetime")]
		public string CreatedDatetime { get; set; }
	}
}