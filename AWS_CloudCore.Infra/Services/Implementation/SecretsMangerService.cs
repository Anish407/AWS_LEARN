using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using AWS_CloudCore.Core.Models;

namespace AWS_CloudCore.Core.Services.Interfaces
{
    public class SecretsMangerService: ISecretsMangerService
    {
        public async Task<string> GetSecret(SecretRequestDto secretRequestDto)
        {
            string secretName = secretRequestDto.SecretName;
            string region = secretRequestDto.Region;

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = secretRequestDto.Version, // VersionStage defaults to AWSCURRENT if unspecified.
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
