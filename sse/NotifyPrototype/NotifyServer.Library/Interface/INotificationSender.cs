using System.Threading.Tasks;
using NotifyServer.Library.Model;

namespace NotifyServer.Library.Interface
{
    public interface INotificationSender
    {
        Task<Result> Send(string clientMethod, string message);

        Task<Result> Send(string clientMethod, string message, string userName);
    }
}
