
using Beauty.Dick.Helpers.Builders.Model;
using Jal.Monads;

namespace Beauty.Dick.Helpers.Builders.Interface
{
    public interface ICodeBuilderFactory
    {
        Result<ICodeBuilder> Create(EnumCodeBuilder type);
    }
}
