using Studfolio.FileService.Business.Interfaces;
using Studfolio.FileService.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Studfolio.FileService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost("addNewFile")]
        public Guid AddNewFile(
            [FromBody] FileCreateRequest request,
            [FromServices] IAddNewFileCommand command)
        {
            return command.Execute(request);
        }

        [HttpGet("getFileById")]
        public File GetFileById([FromServices] IGetFileByIdCommand command, [FromQuery] Guid fileId)
        {
            return command.Execute(fileId);
        }

        [HttpDelete("disableFileById")]
        public void DisableFileById(
            [FromServices] IDisableFileByIdCommand command,
            [FromQuery] Guid fileId)
        {
            command.Execute(fileId);
        }
    }
}