# Patient Validator Application

This project presents example of Azure Functions Application. Is the solution that allows to validate patient data in external service triggered by SMS message. 

## Requirements
Application is built as Azure Function Application in Visual Studio 2017 (15.5.3). All external services are mocked by using Azure Functions Proxy.
Following is the example of `local.settings.json` file content that must be added to the solution on local machine:

```sh
{
	"IsEncrypted": false,
	"Values": {
		"AzureWebJobsStorage": "UseDevelopmentStorage=true",
		"AzureWebJobsDashboard": "UseDevelopmentStorage=true",
		"PROXY_SMS_KEY": "2BE77147B3134B2A9A729FBCFCF5FD05",
		"PROXY_PATIENT_API": "E8FB7B387816498C8B322D0CB4C7E8BC",
		"PatientApiAuthenticationUrl": "http://localhost:7071/mock/patientvalidation/tokens/E8FB7B387816498C8B322D0CB4C7E8BC",
		"PatientApiValidationRequestUrl": "http://localhost:7071/mock/patientvalidation/requests/E8FB7B387816498C8B322D0CB4C7E8BC",
		"PatientApiLogin": "username",
		"PatientApiPassword": "pass",
		"SmsApiUrl": "http://localhost:7071/mock/sms/messages/2BE77147B3134B2A9A729FBCFCF5FD05",
		"SmsApiAccessKey": "2BE77147B3134B2A9A729FBCFCF5FD05",
		"SmsApiOriginator": "Pat.Valid."
	}
}
```
