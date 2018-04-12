namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientValidationRequest
	{
		public string MemberNumber { get; set; }

		public string RefDate { get; set; }
	}
}
