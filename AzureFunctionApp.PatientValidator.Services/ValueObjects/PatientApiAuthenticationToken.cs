using Newtonsoft.Json;
using System;

namespace AzureFunctionApp.PatientValidator.Services.ValueObjects
{
	public class PatientApiAuthenticationToken
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonProperty(".expires")]
		public string Expires { get; set; }

		[JsonIgnore]
		public DateTime? ExpiresDate
		{
			get
			{
				if (DateTime.TryParse(Expires, out DateTime date))
				{
					return date;
				}

				return null;
			}
		}
	}
}
