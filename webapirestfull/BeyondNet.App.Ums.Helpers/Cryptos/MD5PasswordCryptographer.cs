namespace BeyondNet.App.Ums.Helpers.Cryptos
{
    public class Md5PasswordCryptographer : IPasswordCryptographer
    {
        public string Encrypt(string password)
        {
            return password;
        }

        public string Decrypt(string password)
        {
            return password;
        }
    }
}
