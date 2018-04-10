using System;
using System.Threading.Tasks;
using AzureFunctionApp.PatientValidator.Services.Interfaces;
using AzureFunctionApp.PatientValidator.Services.ValueObjects;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AzureFunctionApp.PatientValidator.Services.Tests
{
	public class PatientValidationServiceTests
	{
		readonly IPatientApiService _patientApiService;

		public PatientValidationServiceTests()
		{
			_patientApiService = Substitute.For<IPatientApiService>();
		}

		[Fact]
		public void ValidatePatientMethod_Returns_Authorization_Failed_Error_Message_If_Request_For_Token_Returns_Null()
		{
			//Arrange
			_patientApiService
				.GetAuthenticationToken()
				.Returns(Task.FromResult<PatientApiAuthenticationToken>(null));

			var service = new PatientValidationService(_patientApiService);

			//Act
			var result = service.ValidatePatient(new PatientValidationRequest
			{
				MemberNumber = Guid.NewGuid().ToString("N"),
				RefDate = DateTime.Now.Date.ToString()
			}).Result;

			//Assert
			result.ErrorMessage.Should().NotBeNullOrWhiteSpace();
			result.ErrorMessage.Should().Be("Authorization failed");
		}

		[Fact]
		public void ValidatePatientMethod_Returns_No_Responses_Error_Message_If_Patient_Verify_Request_Returns_Object_With_Empty_Responses_List()
		{
			//Arrange
			_patientApiService
				.GetAuthenticationToken()
				.Returns(Task.FromResult(new PatientApiAuthenticationToken
				{
					AccessToken = Guid.NewGuid().ToString("N"),
					ExpiresIn = 86399
				}));

			_patientApiService
				.Verify(Arg.Any<string>(), Arg.Any<PatientValidationRequest>())
				.Returns(Task.FromResult(new PatientApiValidationResult
				{
					Response = null
				}));

			var service = new PatientValidationService(_patientApiService);

			//Act
			var result = service.ValidatePatient(new PatientValidationRequest
			{
				MemberNumber = Guid.NewGuid().ToString("N"),
				RefDate = DateTime.Now.Date.ToString()
			}).Result;

			//Assert
			result.ErrorMessage.Should().NotBeNullOrWhiteSpace();
			result.ErrorMessage.Should().Be("No response");
		}

		[Fact]
		public void ValidatePatientMethod_Returns_Validation_Result_If_Request_Correct()
		{
			//Arrange
			var memberName = Guid.NewGuid().ToString("N");
			var memberNumber = Guid.NewGuid().ToString("N");

			_patientApiService
				.GetAuthenticationToken()
				.Returns(Task.FromResult(new PatientApiAuthenticationToken
				{
					AccessToken = Guid.NewGuid().ToString("N"),
					ExpiresIn = 86399
				}));

			_patientApiService
				.Verify(Arg.Any<string>(), Arg.Any<PatientValidationRequest>())
				.Returns(Task.FromResult(new PatientApiValidationResult
				{
					Status = PatientApiValidationResultStatus.Verificated,
					Response = new PatientApiValidationResponse
					{
						MemberName = memberName,
						MemberNumber = memberNumber
					}
				}));

			var service = new PatientValidationService(_patientApiService);

			//Act
			var result = service.ValidatePatient(new PatientValidationRequest
			{
				MemberNumber = Guid.NewGuid().ToString("N"),
				RefDate = DateTime.Now.Date.ToString()
			}).Result;

			//Assert
			result.ErrorMessage.Should().BeNull();
			result.MemberName.Should().Be(memberName);
			result.MemberNumber.Should().Be(memberNumber);
		}
	}
}
