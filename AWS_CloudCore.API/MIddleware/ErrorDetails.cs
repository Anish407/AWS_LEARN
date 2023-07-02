using System.Text.Json;

namespace AWS_CloudCore.API.MIddleware
{
    internal class ErrorDetails
    {
        public ErrorDetails()
        {
        }

        public string Message { get; set; }

        public string Serialize() => JsonSerializer.Serialize(this);
    }
}