namespace AzureFunctionApp.PatientValidator.ValueObjects
{
	public class PatientValidationRequestFunctionLog
	{
		public string PartitionKey { get; set; }

		public string RowKey { get; set; }
	}
}
