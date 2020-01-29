using System;
using System.Collections.Generic;
using System.Linq;
using BeyondNet.App.Ums.Domain.User;
using BeyondNet.App.Ums.Helpers.Binders;

namespace BeyondNet.App.Ums.Api.Helpers
{
    public class UserPropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _authorPropertyMapping = 
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
               { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
               { "ExternalId", new PropertyMappingValue(new List<string>() { "ExternalId" } )},
               { "UserName", new PropertyMappingValue(new List<string>() { "UserName" } , true) },
               { "FullName", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) }
           };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public UserPropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<UserEdit, UserInfoReadModel>(_authorPropertyMapping));
        }
        public Dictionary<string, PropertyMappingValue>  GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<UserEdit, UserInfoReadModel>>().ToList();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            return (from field in fieldsAfterSplit
                         select field.Trim() into trimmedField
                            let indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal)
                                select indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace))
                                        .All(propertyName => propertyMapping.ContainsKey(propertyName));
        }

    }
}
