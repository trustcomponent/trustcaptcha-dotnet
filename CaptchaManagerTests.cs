using System;
using System.Threading.Tasks;
using Xunit;
using TrustComponent.TrustCaptcha;
using TrustComponent.TrustCaptcha.Model;

namespace TrustComponent.TrustCaptcha.Tests
{
    public class CaptchaManagerTests
    {
        private const string ValidToken = "eyJhcGlFbmRwb2ludCI6Imh0dHBzOi8vYXBpLnRydXN0Y29tcG9uZW50LmNvbSIsInZlcmlmaWNhdGlvbklkIjoiMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAwIiwiZW5jcnlwdGVkQWNjZXNzVG9rZW4iOiJ0b2tlbiJ9";
        private const string NotFoundToken = "eyJhcGlFbmRwb2ludCI6Imh0dHBzOi8vYXBpLnRydXN0Y29tcG9uZW50LmNvbSIsInZlcmlmaWNhdGlvbklkIjoiMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAxIiwiZW5jcnlwdGVkQWNjZXNzVG9rZW4iOiJ0b2tlbiJ9";
        private const string LockedToken = "eyJhcGlFbmRwb2ludCI6Imh0dHBzOi8vYXBpLnRydXN0Y29tcG9uZW50LmNvbSIsInZlcmlmaWNhdGlvbklkIjoiMDAwMDAwMDAtMDAwMC0wMDAwLTAwMDAtMDAwMDAwMDAwMDAyIiwiZW5jcnlwdGVkQWNjZXNzVG9rZW4iOiJ0b2tlbiJ9";

        [Fact]
        public async Task TestSuccessfulVerification()
        {
            VerificationResult result = await CaptchaManager.GetVerificationResult("secret-key", ValidToken);
            
            Assert.NotNull(result);
            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000000"), result.VerificationId);
        }

        [Fact]
        public async Task TestVerificationTokenInvalid()
        {
            var exception = await Assert.ThrowsAsync<VerificationTokenInvalidException>(
                async () => await CaptchaManager.GetVerificationResult("secret-key", "invalid-token")
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task TestVerificationNotFound()
        {
            var exception = await Assert.ThrowsAsync<VerificationNotFoundException>(
                async () => await CaptchaManager.GetVerificationResult("secret-key", NotFoundToken)
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task TestSecretKeyInvalid()
        {
            var exception = await Assert.ThrowsAsync<SecretKeyInvalidException>(
                async () => await CaptchaManager.GetVerificationResult("invalid-key", ValidToken)
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task TestVerificationNotFinished()
        {
            var exception = await Assert.ThrowsAsync<VerificationNotFinishedException>(
                async () => await CaptchaManager.GetVerificationResult("secret-key", LockedToken)
            );

            Assert.NotNull(exception);
        }
    }
}
