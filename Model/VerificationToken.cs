using System;
using System.Text.Json.Serialization;

namespace TrustComponent.TrustCaptcha.Model;

public class VerificationToken
{
    [JsonPropertyName("apiEndpoint")]
    public string ApiEndpoint { get; set; }

    [JsonPropertyName("verificationId")]
    public Guid VerificationId { get; set; }
}