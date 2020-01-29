using System;
using Microsoft.AspNetCore.Mvc;

namespace BeyondNet.App.Ums.Helpers.Hypermedias
{
    public sealed class HypermediaVerbs
    {
        public const string Get = "GET";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Patch = "PATCH";
        public const string Delete = "DELETE";
    }

    public class HypermediaBuilder : IHypermediaBuilder
    {
        private readonly IUrlHelper _urlHelper;

        public HypermediaBuilder(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public void CreateLink<T>(T entity, string methodName, Guid identifier, string rel, string verb) where T : AbstractLinkDto
        {
            var obj = (AbstractLinkDto)entity;

            var url = _urlHelper.Link(methodName, new {id = identifier});

            obj.Links.Add(new LinkDto(url, rel, verb));
        }

        public void CreateCollectionLink<T>(LinkedCollectionWrapperDto<T> collection, string methodName, string rel, string verb) where T : AbstractLinkDto
        {
            collection.Links.Add(new LinkDto(_urlHelper.Link(methodName, new { }), rel, verb));
        }
    }
}
