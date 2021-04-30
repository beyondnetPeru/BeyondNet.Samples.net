using Newtonsoft.Json;
using NotifyServer.Library.Interface;
using NotifyServer.Library.Model;

namespace NotifyServer.Library.Impl
{
    public class NotifyJsonFormatter : INotifyFormatter
    {
        public OutValue Format(InValue inValue)
        {
            var formatValue = JsonConvert.SerializeObject(inValue);

            var result = new OutValue()
            {
                FormattedValue = formatValue
            };

            return result;
        }
    }
}
