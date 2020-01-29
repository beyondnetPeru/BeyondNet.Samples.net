using System;

namespace BeyondNet.App.Ums.Helpers.Hypermedias
{
    public interface IHypermediaBuilder
    {
        void CreateLink<T>(T entity, string methodName, Guid identifier, string rel, string verb) where T: AbstractLinkDto;

        void CreateCollectionLink<T>(LinkedCollectionWrapperDto<T> collection, string methodName, string rel, string verb) where T : AbstractLinkDto;
    }
}