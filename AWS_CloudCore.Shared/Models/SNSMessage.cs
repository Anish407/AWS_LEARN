using System.Text.Json;
using System.Text.Json.Serialization;

namespace AWS_CloudCore.Shared.Models
{
    public class SNSMessage
    {
        [JsonPropertyName("default")]
        public string DefaultContent { get; }

        [JsonPropertyName("email")]
        public string EmailContent { get; set; }

        [JsonPropertyName("email-json")]
        public string EmailJsonContent { get; set; }

        [JsonPropertyName("sqs")]
        public string SQSContent { get; set; }

        [JsonPropertyName("lambda")]
        public string LambdaContent { get; set; }

        [JsonPropertyName("http")]
        public string HttpContent { get; set; }

        [JsonPropertyName("https")]
        public string HttpsContent { get; set; }

        [JsonPropertyName("sms")]
        public string SMSContent { get; set; }

        [JsonPropertyName("APNS")]
        public string APNSContent { get; set; }

        [JsonPropertyName("APNS_SANDBOX")]
        public string APNSSandboxContent { get; set; }

        [JsonPropertyName("MACOS")]
        public string MacOsContent { get; set; }

        [JsonPropertyName("MACOS_SANDBOX")]
        public string MacOsSandboxContent { get; set; }

        [JsonPropertyName("GCM")]
        public string FCMContent { get; set; }

        [JsonPropertyName("ADM")]
        public string ADMContent { get; set; }

        [JsonPropertyName("BAIDU")]
        public string BaiduContent { get; set; }

        [JsonPropertyName("MPNS")]
        public string MPNSContent { get; set; }

        [JsonPropertyName("WNS")]
        public string WNSContent { get; set; }

        public SNSMessage(string defaultContent)
        {
            DefaultContent = defaultContent;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
