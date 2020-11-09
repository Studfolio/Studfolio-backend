using System;

namespace Studfolio.Broker.Responses
{
    /// <summary>
    /// The model is a binding the response internal model of sender for RabbitMQ.
    /// </summary>
    public interface IUserCredentialsResponse
    {
        Guid UserId { get; }
        string PasswordHash { get; }
        string Salt { get; }
        string UserLogin { get; }

        static object CreateObj(Guid userId, string passwordHash, string salt, string userLogin)
        {
            return new
            {
                UserId = userId,
                PasswordHash = passwordHash,
                Salt = salt,
                UserLogin = userLogin
            };
        }
    }
}