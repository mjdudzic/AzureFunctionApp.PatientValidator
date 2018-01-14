using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientValidationRequest
	{
		public string MemberNumber { get; set; }

		public string RefDate { get; set; }
	}
}
