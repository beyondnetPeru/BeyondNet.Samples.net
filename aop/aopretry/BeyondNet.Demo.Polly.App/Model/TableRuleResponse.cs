using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondNet.Demo.Polly.App.Model
{
    public class TableRuleResponse
    {
        public TableRuleResponseStatus Status { get; set; }
        public List<string> Messages { get; set; }

        public TableRuleResponse()
        {
            Messages = new List<string>();

            Status = TableRuleResponseStatus.None;
        }
    }

    public class TableRuleResponse<T> : TableRuleResponse
    {
        public T[] Data { get; set; }
    }
}
