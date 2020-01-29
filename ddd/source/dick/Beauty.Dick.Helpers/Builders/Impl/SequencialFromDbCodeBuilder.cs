using Jal.Monads;

namespace Beauty.Dick.Helpers.Builders.Interface.Impl
{
    public class SequencialFromDbCodeBuilder : ICodeBuilder
    {
        private readonly ICodeBuilderRepository _codeBuilderRepository;

        public SequencialFromDbCodeBuilder(ICodeBuilderRepository codeBuilderRepository)
        {
            _codeBuilderRepository = codeBuilderRepository;
        }

        public Result<string> Build(string key, string prefix)
        {
            return _codeBuilderRepository.Get(key)
                .OnSuccess(resultSequence => {
                    return $"{prefix}{resultSequence.ToString().PadLeft(8,'0')}".ToResult();
                });
        }
    }
}
