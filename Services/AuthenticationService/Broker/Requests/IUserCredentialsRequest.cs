namespace Studfolio.Broker.Requests
{
    /// <summary>
    /// The model is a binding the request internal model of consumer for RabbitMQ.
    /// </summary>
    public interface IUserCredentialsRequest
    {
        string LoginData { get; }

        static object CreateObj(string loginData)
        {
            return new
            {
                LoginData = loginData
            };
        }
    }
}
