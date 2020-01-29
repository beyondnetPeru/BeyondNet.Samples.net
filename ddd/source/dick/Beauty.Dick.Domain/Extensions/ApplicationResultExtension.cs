using Beauty.Dick.Domain.Model;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Beauty.Dick.Domain.Extensions
{
    public static class ApplicationResultExtension
    {
        public static void AddMessage(this List<ErrorMessage> errorMessages, ValidationResult validationResult)
        {
            foreach (var errorItem in validationResult.Errors)
            {
                errorMessages.Add(new ErrorMessage() { Code = errorItem.ErrorCode, Message = errorItem.ErrorMessage });
            }
        }
    }
}
