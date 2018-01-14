using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionApp.PatientValidator.ValueObjects
{
	public class PatientValidationRequestFunctionLog
	{
		public string PartitionKey { get; set; }

		public string RowKey { get; set; }
	}
}
