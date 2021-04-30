using System.Threading.Tasks;
using NotifyServer.Model;

namespace NotifyServer.Library.Interface
{
    public interface INotifyRepository
    {
        Task Insert(NotifyAdd notifyAdd);
    }
}
