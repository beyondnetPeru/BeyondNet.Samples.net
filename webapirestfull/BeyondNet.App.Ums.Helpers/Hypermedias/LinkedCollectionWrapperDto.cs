using System.Collections.Generic;

namespace BeyondNet.App.Ums.Helpers.Hypermedias
{
    public class LinkedCollectionWrapperDto<T> : AbstractLinkDto where T : AbstractLinkDto
    {
        public IEnumerable<T> Value { get; set; }

        public LinkedCollectionWrapperDto(IEnumerable<T> value)
        {
            Value = value;
        }
    }
}
