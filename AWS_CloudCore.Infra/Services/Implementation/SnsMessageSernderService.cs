using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using AWS_CloudCore.Core.Common.Exceptions;
using AWS_CloudCore.Core.Models.Configs;
using AWS_CloudCore.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AWS_CloudCore.Infra.Services.Implementation
{
    public class SnsMessageSenderService: IMessageQueueService
    {
        private readonly SNSConfig _snsConfig;
        private readonly IAmazonSimpleNotificationService _amazonSimpleNotificationService;

        public SnsMessageSenderService(IOptionsSnapshot<SNSConfig> snsConfig, IAmazonSimpleNotificationService amazonSimpleNotificationService )
        {
            _snsConfig = snsConfig.Value;
            _amazonSimpleNotificationService = amazonSimpleNotificationService;
        }
        public async Task PublishMessage<T>(T message) where T : class 
        {
            try
            {
                string publishMessage= JsonSerializer.Serialize(message);
                var request = new PublishRequest
                {
                    TopicArn = _snsConfig.Endpoint,
                    Message = publishMessage,
                };

                var response = await _amazonSimpleNotificationService.PublishAsync(request);
            }
            catch (Exception ex)
            {
                throw new ReturnExceptionMessageToCallerException($"Failed to publish message {ex.Message}");
            }
        }

    }
}
