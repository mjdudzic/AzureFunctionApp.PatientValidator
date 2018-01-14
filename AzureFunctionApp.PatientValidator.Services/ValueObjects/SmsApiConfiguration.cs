namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class SmsApiConfiguration
	{
		public string SmsApiUrl { get; set; }

		public string SmsApiAccessKey { get; set; }

		public string SmsApiOriginator { get; set; }
	}
}
