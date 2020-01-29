using System.Collections.Generic;
using FluentValidation.Results;

namespace BeyondNet.App.Ums.Domain
{
    public class ApplicationResult<TEntity>  
    {
        public TEntity Data { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsFailure { get; set; }
        public List<ValidationFailure> Errors { get; set; }

        public ApplicationResult()
        {
            IsFailure = false;
            IsSuccessful = false;
            Errors = new List<ValidationFailure>();
        }
    }
}