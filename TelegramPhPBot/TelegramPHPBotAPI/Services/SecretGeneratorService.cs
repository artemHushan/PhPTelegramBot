namespace TelegramPHPBotAPI.Services
{
    public class SecretGeneratorService
    {
        public string GenerateSecret(int length = 12)
        {
            return GenerateRandomString(length);
        }

        public string GenerateSecretKeyParam(int length = 32)
        {
            return GenerateRandomString(length);
        }

        private static readonly char[] ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private static readonly Random Random = new Random();
        private string GenerateRandomString(int length)
        {
            var stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                stringChars[i] = ValidChars[Random.Next(ValidChars.Length)];
            }

            return new String(stringChars);
        }
    }
}
