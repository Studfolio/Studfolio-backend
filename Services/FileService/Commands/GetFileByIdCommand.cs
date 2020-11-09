using Microsoft.AspNetCore.Mvc;
using Studfolio.FileService.Business.Interfaces;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database.Entities;
using Studfolio.FileService.Mappers.Interfaces;
using Studfolio.FileService.Models.Dto;
using System;

namespace Studfolio.FileService.Business
{
    /// <inheritdoc cref="IGetFileByIdCommand"/>
    public class GetFileByIdCommand : IGetFileByIdCommand
    {
        private readonly IFileRepository repository;
        private readonly IMapper<DbFile, File> mapper;

        public GetFileByIdCommand(
            [FromServices] IFileRepository repository,
            [FromServices] IMapper<DbFile, File> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public File Execute(Guid fileId)
        {
            return mapper.Map(repository.GetFileById(fileId));
        }
    }
}