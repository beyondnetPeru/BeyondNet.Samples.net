using System.Threading.Tasks;
using NotifyServer.Model;

namespace NotifyServer.Library.Interface
{
    public interface INotifyApplication
    {
        bool IsValid(RequestCommand requestCommand);

        Task<ResponseMessage> PushMessage(RequestCommand requestCommand);

        ResponseMessage GetTopMessagesByProfile(RequestCommand requestCommand);
    }
}
