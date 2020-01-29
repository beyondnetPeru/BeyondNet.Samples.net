using System.Linq;
using System.Reflection;

namespace BeyondNet.App.Ums.Helpers.Binders
{
    public class TypeHelperService : ITypeHelperService
    {
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            return fieldsAfterSplit.Select(field => field.Trim()).Select(propertyName => typeof(T).GetProperty(propertyName, 
                                           BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance))
                                           .All(propertyInfo => propertyInfo != null);
        }
    }
}
