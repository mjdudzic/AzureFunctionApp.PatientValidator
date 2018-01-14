using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionApp.PatientValidator.ValueObjects
{
	public class PatientValidationProcessFunctionLog
	{
		public string PartitionKey { get; set; }

		public string RowKey { get; set; }

		public DateTime CreateDate { get; set; }

		public string RequestJson { get; set; }

		public string ResponseJson { get; set; }
	}
}
