using Beauty.Dick.Helpers.Builders.Interface;
using Jal.Monads;
using System;

namespace Beauty.Barry.Infrastructure.DaoEF.CodeBuilder
{
    public class CodeBuilderDao : ICodeBuilderRepository
    {
        public Result<int> Get(string key)
        {
            return new Random().Next(100).ToResult();
        }
    }
}
