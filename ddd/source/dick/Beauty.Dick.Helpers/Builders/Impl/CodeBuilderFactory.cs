using Beauty.Dick.Helpers.Builders.Interface;
using Beauty.Dick.Helpers.Builders.Model;
using Jal.Factory.Interface;
using Jal.Monads;
using System;
using System.Linq;

namespace Beauty.Dick.Helpers.Builders.Impl
{
    public class CodeBuilderFactory : ICodeBuilderFactory
    {
        private readonly IObjectFactory _objectFactory;

        public CodeBuilderFactory(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public Result<ICodeBuilder> Create(EnumCodeBuilder type)
        {
            try
            {
                return _objectFactory.Create<EnumCodeBuilder, ICodeBuilder>(type).First().ToResult();
            }
            catch (Exception ex)
            {
                return Result.Failure<ICodeBuilder>(new[] { $"No logger type filter set up. Error: {ex.Message}" });
            }
        }
    }
}
