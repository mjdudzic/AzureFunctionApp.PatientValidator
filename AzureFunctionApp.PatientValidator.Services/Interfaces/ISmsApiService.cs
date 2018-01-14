using System.Threading.Tasks;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;

namespace AzureFunctionApp.PatientValidator.Services.ExternalApiServices
{
	public interface ISmsApiService
	{
		Task<string> SendMessage(SmsMessagePatientValidationResponse message, params long[] recipients);
	}
}