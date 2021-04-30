using System.Threading.Tasks;
using NotifyServer.Library.Model;

namespace NotifyServer.Library.Interface
{
    public interface INotifyFormatter
    {
        OutValue Format(InValue inValue);
    }
}
