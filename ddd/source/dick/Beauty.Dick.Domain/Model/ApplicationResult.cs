using System.Collections.Generic;

namespace Beauty.Dick.Domain.Model
{
    public class ApplicationResult<TEntity> where TEntity:class
    {
        public bool IsValid { get; set; }   
        public TEntity Entity { get; set; }

        public List<ErrorMessage> ErrorMessages { get; set; }

        public ApplicationResult()
        {
            ErrorMessages = new List<ErrorMessage>();
        }
    }
}
