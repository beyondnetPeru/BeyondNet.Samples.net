using System.Collections.Generic;

namespace BeyondNet.App.Ums.Helpers.Hypermedias
{
    public abstract class AbstractLinkDto
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
