using AWS_CloudCore.Core.Models;

namespace AWS_CloudCore.Core.Services.Interfaces
{
    public interface ISecretsMangerService
    {
        Task<string> GetSecret(SecretRequestDto secretRequestDto);
    }
}
