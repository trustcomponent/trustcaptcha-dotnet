using System;
using System.Text.Json.Serialization;

namespace TrustcaptchaCSharp.Lib.Model;

public class VerificationResult
{
    [JsonPropertyName("captchaId")]
    public Guid CaptchaId { get; set; }

    [JsonPropertyName("verificationId")]
    public Guid VerificationId { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("reason")]
    public string Reason { get; set; }

    [JsonPropertyName("mode")]
    public string Mode { get; set; }

    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("ipAddress")]
    public string IpAddress { get; set; }

    [JsonPropertyName("deviceFamily")]
    public string DeviceFamily { get; set; }

    [JsonPropertyName("operatingSystem")]
    public string OperatingSystem { get; set; }

    [JsonPropertyName("browser")]
    public string Browser { get; set; }

    [JsonPropertyName("creationTimestamp")]
    public string CreationTimestamp { get; set; }

    [JsonPropertyName("releaseTimestamp")]
    public string ReleaseTimestamp { get; set; }

    [JsonPropertyName("retrievalTimestamp")]
    public string RetrievalTimestamp { get; set; }

    [JsonPropertyName("verificationPassed")]
    public bool VerificationPassed { get; set; }
}