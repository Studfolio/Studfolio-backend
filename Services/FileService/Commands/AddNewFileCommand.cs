using FluentValidation;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using Microsoft.AspNetCore.Mvc;
using Studfolio.FileService.Business.Interfaces;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database.Entities;
using Studfolio.FileService.Mappers.Interfaces;
using Studfolio.FileService.Models.Dto;
using System;

namespace Studfolio.FileService.Business
{
    /// <inheritdoc cref="IAddNewFileCommand"/>
    public class AddNewFileCommand : IAddNewFileCommand
    {
        private readonly IFileRepository repository;
        private readonly IValidator<FileCreateRequest> validator;
        private readonly IMapper<FileCreateRequest, DbFile> mapper;

        public AddNewFileCommand(
            [FromServices] IFileRepository repository,
            [FromServices] IValidator<FileCreateRequest> validator,
            [FromServices] IMapper<FileCreateRequest, DbFile> mapper)
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public Guid Execute(FileCreateRequest request)
        {
            validator.ValidateAndThrowCustom(request);

            var newFile = mapper.Map(request);

            return repository.AddNewFile(newFile);
        }
    }
}
