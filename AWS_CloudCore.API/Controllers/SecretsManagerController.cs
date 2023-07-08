using AWS_CloudCore.Core.Models.Configs;
using AWS_CloudCore.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AWS_CloudCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsManagerController : ControllerBase
    {
        private readonly ISecretsMangerService _secretsMangerService;

        public SecretsManagerController(IOptionsSnapshot<SNSConfig> options, ISecretsMangerService secretsMangerService)
        {
            _secretsMangerService = secretsMangerService;
            // secret name format: {env}_ProjectName_Key 
            //eg:  Production_AWS_CloudCore.API_SNSConfig__Endpoint
        }

        [HttpGet("GetSecret")]
        public async Task<string> GetSecret()
        {
            string secretName = "Production_AWS_CloudCore.API_SNSConfig__Endpoint";
            string region = "eu-north-1";
            string secretVersion = "AWSCURRENT";

            return await _secretsMangerService.GetSecret(new Core.Models.SecretRequestDto
            {
                Region = region,
                SecretName = secretName,
                Version = secretVersion
            });
        }
    }
}
