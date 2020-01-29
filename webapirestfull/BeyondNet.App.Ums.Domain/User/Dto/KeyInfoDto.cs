using System;
using BeyondNet.App.Ums.Helpers.Hypermedias;

namespace BeyondNet.App.Ums.Domain.User.Dto
{
    public class KeyInfoDto : AbstractLinkDto
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public DateTime LastSignOn { get; set; }
        public int Status { get; set; }
    }
}
