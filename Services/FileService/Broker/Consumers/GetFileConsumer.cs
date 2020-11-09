using Studfolio.Broker.Requests;
using Studfolio.Broker.Responses;
using Studfolio.FileService.Data.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Studfolio.FileService.Broker.Consumers
{
    public class GetFileConsumer : IConsumer<IGetFileRequest>
    {
        private readonly IFileRepository repository;

        public GetFileConsumer([FromServices] IFileRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<IGetFileRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetFile, context.Message);

            await context.RespondAsync<IOperationResult<IFileResponse>>(response);
        }

        private object GetFile(IGetFileRequest request)
        {
            var dbFile = repository.GetFileById(request.FileId);

            return new
            {
                Content = Convert.ToBase64String(dbFile.Content),
                Extension = dbFile.Extension,
                Name = dbFile.Name
            };
        }
    }
}