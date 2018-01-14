using Newtonsoft.Json;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientApiValidationResponseError
	{
		[JsonProperty("Message")]
		public string Message { get; set; }
	}
}