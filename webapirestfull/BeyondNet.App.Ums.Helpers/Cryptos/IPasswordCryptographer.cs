namespace BeyondNet.App.Ums.Helpers.Cryptos
{
    public interface IPasswordCryptographer
    {
        string Encrypt(string password);
        string Decrypt(string password);
    }
}
