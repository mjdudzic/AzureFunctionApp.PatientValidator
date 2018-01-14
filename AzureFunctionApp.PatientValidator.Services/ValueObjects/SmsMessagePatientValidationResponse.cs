using Newtonsoft.Json;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class SmsMessagePatientValidationResponse
	{
		public const string TYPE_SMS = "sms";
		public const string ENCODING_PLAIN = "plain";
		public const int CLASS_1 = 1;

		public SmsMessagePatientValidationResponse()
		{
			Type = TYPE_SMS;
			Encoding = ENCODING_PLAIN;
			Class = CLASS_1;
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