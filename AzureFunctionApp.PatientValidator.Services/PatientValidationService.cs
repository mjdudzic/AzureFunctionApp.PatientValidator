using AzureFunctionApp.PatientValidator.Services.Interfaces;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctionApp.PatientValidator.Services
{
	public class PatientValidationService : IPatientValidationService
	{
		private readonly IPatientApiService _apiService;

		public PatientValidationService(IPatientApiService apiService)
		{
			_apiService = apiService;
		}

		public async Task<PatientValidationRequestResult> ValidatePatient(PatientValidationRequest request)
		{
			var token = await _apiService.GetAuthenticationToken();
			if (token == null || string.IsNullOrWhiteSpace(token.AccessToken))
			{
				return new PatientValidationRequestResult
				{
					ErrorMessage = "Authorization failed"
				};
			}

			var result = await _apiService.Verify(token.AccessToken, request);
			
			if (result.Response == null)
			{
				return new PatientValidationRequestResult
				{
					ErrorMessage = "No response"
				};
			}

			var response = result.Response;

			return new PatientValidationRequestResult
			{
				MemberName = result.Response.MemberName,
				MemberNumber = result.Response.MemberNumber,
				Status = result.Response.Status,
				ErrorMessage = result.Error == null || string.IsNullOrWhiteSpace(result.Error.Message)
					? null
					: result.Error.Message
			};
		}
	}
}
