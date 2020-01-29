using Jal.Monads;

namespace Beauty.Dick.Helpers.Builders.Interface
{
    public interface ICodeBuilderRepository
    {
        Result<int> Get(string key);
    }
}
