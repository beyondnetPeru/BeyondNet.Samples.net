using Jal.Monads;

namespace BeyondNet.Demo.Jal.Library.Helper
{
    public interface IFileManager
    {
        Result Write(string message);
    }
}
