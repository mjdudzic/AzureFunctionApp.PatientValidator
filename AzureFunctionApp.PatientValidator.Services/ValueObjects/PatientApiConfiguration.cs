using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientApiConfiguration
	{
		public string PatientApiAuthenticationUrl { get; set; }

		public string PatientApiValidationRequestUrl { get; set; }

		public string PatientApiLogin { get; set; }

		public string PatientApiPassword { get; set; }

		public TimeSpan PatientApiTimeout { get; set; }
	}
}
