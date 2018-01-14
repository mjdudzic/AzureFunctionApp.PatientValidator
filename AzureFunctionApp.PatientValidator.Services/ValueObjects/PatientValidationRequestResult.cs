namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientValidationRequestResult
	{
		public string MemberName { get; set; }

		public string MemberNumber { get; set; }

		public string Status { get; set; }

		public string ErrorMessage { get; set; }
	}
}