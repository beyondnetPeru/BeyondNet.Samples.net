using BeyondNet.Demo.Polly.App.Interfaces;
using Newtonsoft.Json;

namespace BeyondNet.Demo.Polly.App.Impl
{
    public class TableRuleResponseConverter : ITableRuleResponseConverter
    {

        public T[] Convert<T>(string result)
        {
            if (IsArray(result))
            {
                var arrayresult = JsonConvert.DeserializeObject<T[]>(result);

                return arrayresult;
            }
            if (IsOne(result))
            {
                var singleresult = JsonConvert.DeserializeObject<T>(result);

                return new[] { singleresult };
            }

            return new[] { (T)System.Convert.ChangeType(result, typeof(T)) };
        }

        private static bool IsOne(string result)
        {
            return result.IndexOf('{') >= 0;
        }

        private static bool IsArray(string result)
        {
            return result.IndexOf('[') == 0;
        }
    }
}
