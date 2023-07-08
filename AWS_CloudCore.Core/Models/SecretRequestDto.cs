namespace AWS_CloudCore.Core.Models
{
    public class SecretRequestDto
    {
        public string SecretName { get; set; }
        public string Region { get; set; }
        public string Version { get; set; }
    }
}
