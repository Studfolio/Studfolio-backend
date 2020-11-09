using Studfolio.AuthenticationService.Broker.Consumers;
using Studfolio.AuthenticationService.Token.Interfaces;
using Studfolio.Broker.Requests;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;

namespace Studfolio.AuthenticationService.Broker.UnitTests.Consumers
{
    class UserJwtConsumerTests
    {
        private Mock<IJwtValidator> jwtValidationMock;

        private InMemoryTestHarness harness;
        private string userJwt;
        private ConsumerTestHarness<JwtConsumer> consumerTestHarness;

        #region Setup
        [SetUp]
        public void SetUp()
        {
            userJwt = "Example_jwt";

            harness = new InMemoryTestHarness();
            jwtValidationMock = new Mock<IJwtValidator>();

            consumerTestHarness = harness.Consumer(() => new JwtConsumer(jwtValidationMock.Object));
        }
        #endregion

        #region Response to broker
        [Test]
        public async Task ShouldSendResponseToBrokerWhenUserJwtIsValid()
        {
            object expectedErrors = null;
            var expectedBody = true;

            await harness.Start();

            try
            {
                var requestClient = await harness.ConnectRequestClient<ICheckTokenRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserJwt = userJwt
                });

                Assert.That(response.Message.IsSuccess, Is.True);
                Assert.AreEqual(expectedErrors, response.Message.Errors);
                Assert.AreEqual(expectedBody, response.Message.Body);
                Assert.That(consumerTestHarness.Consumed.Select<ICheckTokenRequest>().Any(), Is.True);
                Assert.That(harness.Sent.Select<IOperationResult<bool>>().Any(), Is.True);
            }
            finally
            {
                await harness.Stop();
            }
        }
        #endregion

        #region Throw exception
        [Test]
        public async Task ShouldExceptioWhenUserJwtIsNotValid()
        {
            string expectedErrors = "Token failed validation";
            bool expectedBody = false;

            jwtValidationMock
                .Setup(x => x.ValidateAndThrow(It.IsAny<string>()))
                .Throws(new Exception("Token failed validation"));

            await harness.Start();

            try
            {
                var requestClient = await harness.ConnectRequestClient<ICheckTokenRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserJwt = userJwt
                });

                Assert.That(response.Message.IsSuccess, Is.False);
                Assert.AreEqual(expectedBody, response.Message.Body);
                Assert.AreEqual(expectedErrors, String.Join(", ", response.Message.Errors));
                Assert.That(consumerTestHarness.Consumed.Select<ICheckTokenRequest>().Any(), Is.True);
                Assert.That(harness.Sent.Select<IOperationResult<bool>>().Any(), Is.True);
            }
            finally
            {
                await harness.Stop();
            }
        }
        #endregion
    }
}