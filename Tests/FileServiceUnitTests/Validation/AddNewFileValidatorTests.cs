using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Studfolio.FileService.Models.Dto;

namespace Studfolio.FileService.Validation.UnitTests
{
    public class AddNewFileRequestValidatorTests
    {
        private IValidator<FileCreateRequest> validator;

        private FileCreateRequest fileRequest;

        [SetUp]
        public void SetUp()
        {
            fileRequest = new FileCreateRequest
            {
                Content = "RGlnaXRhbCBPZmA5Y2U=",
                Extension = ".txt",
                Name = "DigitalOfficeTestFile"
            };

            validator = new AddNewFileValidator();
        }

        [Test]
        public void ShouldNotHaveAnyValidationErrorsWhenFileIsValid()
        {
            validator.TestValidate(fileRequest).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void ShouldHaveValidationErrorWhenContentIsInWrongEncoding()
        {
            fileRequest.Content = "T 1 ! * & ? Z :C ; _____";

            var fileValidationResult = validator.TestValidate(fileRequest);

            fileValidationResult.ShouldHaveValidationErrorFor(f => f.Content);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenNameIsTooLong()
        {
            fileRequest.Name += fileRequest.Name.PadLeft(244);

            var fileValidationResult = validator.TestValidate(fileRequest);

            fileValidationResult.ShouldHaveValidationErrorFor(f => f.Name);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenNameIsEmpty()
        {
            fileRequest.Name = "";

            validator.TestValidate(fileRequest).ShouldHaveValidationErrorFor(request => request.Name);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenContentIsNull()
        {
            fileRequest.Content = null;

            validator.TestValidate(fileRequest).ShouldHaveValidationErrorFor(request => request.Content);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenNameDoesNotMatchRegularExpression()
        {
            fileRequest.Name = "???'";

            validator.TestValidate(fileRequest).ShouldHaveValidationErrorFor(request => request.Name);
        }
    }
}
