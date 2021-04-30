using System.Threading.Tasks;
using NotifyServer.Library.Model;

namespace NotifyServer.Library.Interface
{
    public interface INotifyHub
    {
        Task<Result> Send(string message);
    }
}
