using Newtonsoft.Json;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class SmsMessagePatientValidationResponse
	{
		public const string TypeSms = "sms";
		public const string EncodingPlain = "plain";
		public const int Class1 = 1;

		public SmsMessagePatientValidationResponse()
		{
			Type = TypeSms;
			Encoding = EncodingPlain;
			Class = Class1;
		}

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("originator")]
		public string Originator { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("datacoding")]
		public string Encoding { get; set; }

		[JsonProperty("mclass")]
		public int Class { get; set; }

		[JsonProperty("recipients")]
		public long[] Recipients { get; set; }
	}
}