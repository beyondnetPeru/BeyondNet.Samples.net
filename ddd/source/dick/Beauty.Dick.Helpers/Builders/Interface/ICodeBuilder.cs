using Jal.Monads;

namespace Beauty.Dick.Helpers.Builders.Interface
{
    public interface ICodeBuilder 
    {
        Result<string> Build(string key, string prefix);
    }
}
