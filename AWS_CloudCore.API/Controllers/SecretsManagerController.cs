using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using AWS_CloudCore.Core.Models.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Amazon;
using System.Net;

namespace AWS_CloudCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsManagerController : ControllerBase
    {
        public SecretsManagerController(IOptionsSnapshot<SNSConfig> options)
        {
            // secret name format: {env}_ProjectName_Key 
            //eg:  Production_AWS_CloudCore.API_SNSConfig__Endpoint
        }

        // change to accept a model and move code to a class 
        [HttpGet]
        public async Task<string> Get()
        {
            string secretName = "Production_AWS_CloudCore.API_SNSConfig__Endpoint";
            string region = "eu-north-1";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
                return response.SecretString;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
