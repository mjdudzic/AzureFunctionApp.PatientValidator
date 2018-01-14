namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientApiValidationResult
	{
		public PatientApiValidationResponseError Error { get; set; }

		public PatientApiValidationResponse[] Responses { get; set; }

		public PatientApiValidationResultStatus Status { get; set; }
	}
}