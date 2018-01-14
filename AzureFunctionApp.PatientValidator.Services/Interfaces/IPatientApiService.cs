using System.Threading.Tasks;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;

namespace AzureFunctionApp.PatientValidator.Services.Interfaces
{
	public interface IPatientApiService
	{
		Task<PatientApiAuthenticationToken> GetAuthenticationToken();
		Task<PatientApiValidationResult> Verify(string accessToken, params PatientValidationRequest[] requests);
	}
}