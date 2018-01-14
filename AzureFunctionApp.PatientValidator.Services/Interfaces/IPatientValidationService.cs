using System.Threading.Tasks;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;

namespace AzureFunctionApp.PatientValidator.Services.Interfaces
{
	public interface IPatientValidationService
	{
		Task<PatientValidationRequestResult> ValidatePatient(PatientValidationRequest request);
	}
}