using System;
using Newtonsoft.Json;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientApiValidationResponse
	{
		[JsonProperty("MemberName")]
		public string MemberName { get; set; }

		[JsonProperty("MemberNumber")]
		public string MemberNumber { get; set; }

		[JsonProperty("DateOfBirth")]
		public string DateOfBirthValue { get; set; }

		[JsonIgnore]
		public DateTime? DateOfBirth
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(DateOfBirthValue, out date))
					return date;

				return null;
			}
		}

		[JsonProperty("EligibilityStartDate")]
		public string EligibilityStartDateValue { get; set; }

		[JsonIgnore]
		public DateTime? EligibilityStartDate
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(EligibilityStartDateValue, out date))
					return date;

				return null;
			}
		}

		[JsonProperty("EligibilityEndDate")]
		public string EligibilityEndDateValue { get; set; }

		[JsonIgnore]
		public DateTime? EligibilityEndDate
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(EligibilityEndDateValue, out date))
					return date;

				return null;
			}
		}

		[JsonProperty("Status")]
		public string Status { get; set; }

		[JsonProperty("Gender")]
		public string Gender { get; set; }
	}
}