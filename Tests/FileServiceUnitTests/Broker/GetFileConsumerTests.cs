using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.UnitTestKernel;
using MassTransit;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;
using Studfolio.Broker.Requests;
using Studfolio.Broker.Responses;
using Studfolio.FileService.Broker.Consumers;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Studfolio.FileService.Broker.UnitTests
{
    internal class GetFileConsumerTests
    {
        private static byte[] contentByte;
        private string contentString;
        private string extension;
        private string name;
        private Guid fileId;
        private ConsumerTestHarness<GetFileConsumer> consumerTestHarness;

        private InMemoryTestHarness harness;
        private Mock<IFileRepository> repository;
        private IRequestClient<IGetFileRequest> requestClient;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IFileRepository>();

            harness = new InMemoryTestHarness();
            consumerTestHarness = harness.Consumer(() =>
                new GetFileConsumer(repository.Object));

            fileId = Guid.NewGuid();
            contentByte = Convert.FromBase64String("RGlnaXRhbCBPZmA5Y2U=");
            contentString = Convert.ToBase64String(contentByte);
            extension = ".jpg";
            name = "File";
        }

        [Test]
        public async Task ShouldResponseGetFileResponse()
        {
            await harness.Start();

            repository
                .Setup(x => x.GetFileById(It.IsAny<Guid>()))
                .Returns(new DbFile
                {
                    Content = contentByte,
                    Extension = extension,
                    Name = name
                });

            try
            {
                requestClient = await harness.ConnectRequestClient<IGetFileRequest>();

                var response = await requestClient.GetResponse<IOperationResult<IFileResponse>>(new
                {
                    FileId = fileId
                });

                var expected = new
                {
                    IsSuccess = true,
                    Errors = null as List<string>,
                    Body = new
                    {
                        Content = contentString,
                        Extension = extension,
                        Name = name
                    }
                };

                SerializerAssert.AreEqual(expected, response.Message);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldResponseIOperationResultWithExceptionWhenRepositoryNotFoundFile()
        {
            await harness.Start();

            repository
                .Setup(x => x.GetFileById(It.IsAny<Guid>()))
                .Throws(new Exception("File with this id was not found."));

            try
            {
                requestClient = await harness.ConnectRequestClient<IGetFileRequest>();

                var response = await requestClient.GetResponse<IOperationResult<IFileResponse>>(new
                {
                    FileId = fileId
                });

                var expected = new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "File with this id was not found." },
                    Body = null as object
                };

                SerializerAssert.AreEqual(expected, response.Message);
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}