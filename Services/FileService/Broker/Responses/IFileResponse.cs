namespace Studfolio.Broker.Responses
{
    /// <summary>
    /// DTO for dispatch file information through a message broker.
    /// </summary>
    public interface IFileResponse
    {
        public string Content { get; }
        public string Extension { get; }
        public string Name { get; }
    }
}