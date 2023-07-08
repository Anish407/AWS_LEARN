namespace AWS_CloudCore.Core.Services.Interfaces
{
    public interface IMessageQueueService
    {
        Task PublishMessage<T>(T message) where T : class;
    }
}
