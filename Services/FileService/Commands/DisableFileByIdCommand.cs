using Studfolio.FileService.Business.Interfaces;
using Studfolio.FileService.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Studfolio.FileService.Business
{
    public class DisableFileByIdCommand : IDisableFileByIdCommand
    {
        private readonly IFileRepository repository;

        public DisableFileByIdCommand([FromServices] IFileRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(Guid fileId)
        {
            repository.DisableFileById(fileId);
        }
    }
}
