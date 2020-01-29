using Beauty.Dick.Domain.Interface;
using Common.Logging;
using Jal.Monads;

namespace Beauty.Dick.Domain.Impl
{
    public class AbstractApplicationService : IApplicationService
    {
        private readonly ILog _log;

        public AbstractApplicationService(ILog log)
        {
            _log = log;
        }
    }
}
