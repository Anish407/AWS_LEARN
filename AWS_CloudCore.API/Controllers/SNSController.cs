using AWS_CloudCore.Core.Models.Common;
using AWS_CloudCore.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AWS_CloudCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNSController : ControllerBase
    {
        /// <summary>
        /// Publishes a message to SNS. I have added an email id as a subscriber to the SNS topic
        /// </summary>
        /// <param name="messageQueueService"></param>
        /// <returns></returns>
        [HttpPost("PushMessage")]
        public async Task<IActionResult> PushMesage(
            [FromServices] IMessageQueueService messageQueueService)
        {
            await messageQueueService.PublishMessage(
                new MessageDto
                {
                    Message = "Dummy Message"
                });
            return Ok(); 
        }
    }
}
