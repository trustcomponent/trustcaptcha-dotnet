using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrustComponent.TrustCaptcha.Model;

namespace TrustComponent.TrustCaptcha
{
    public static class CaptchaManager
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<VerificationResult> GetVerificationResult(string secretKey, string base64VerificationToken)
        {
            var verificationToken = GetVerificationToken(base64VerificationToken);

            string url = $"{verificationToken.ApiEndpoint}/verifications/{verificationToken.VerificationId}/assessments";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("tc-authorization", secretKey);
            request.Headers.Add("tc-library-language", "dotnet");
            request.Headers.Add("tc-library-version", "1.2");

            try
            {
                var response = await httpClient.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new VerificationNotFoundException();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new SecretKeyInvalidException();
                }
                if ((int)response.StatusCode == 423)
                {
                    throw new VerificationNotFinishedException();
                }
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<VerificationResult>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to retrieve verification result", ex);
            }
        }

        private static VerificationToken GetVerificationToken(string verificationToken)
        {
            try
            {
                string decodedVerificationToken = Encoding.UTF8.GetString(Convert.FromBase64String(verificationToken));
                return JsonSerializer.Deserialize<VerificationToken>(decodedVerificationToken);
            }
            catch (Exception)
            {
                throw new VerificationTokenInvalidException();
            }
        }
    }

    // Custom Exceptions
    public class VerificationNotFoundException : Exception { }
    public class VerificationTokenInvalidException : Exception { }
    public class SecretKeyInvalidException : Exception { }
    public class VerificationNotFinishedException : Exception { }
}
