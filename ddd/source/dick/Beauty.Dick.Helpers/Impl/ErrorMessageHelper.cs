using Beauty.Dick.Domain.Model;

namespace Beauty.Dick.Helpers.Impl
{
    public static class ErrorMessageHelper
    {
        public static ErrorMessage BuildSystemErrorMessage(string[] errors)
        {
            return new ErrorMessage() { Code = "SYSTEM_ERROR", Message = string.Join(",", errors) };
        }
    }
}
