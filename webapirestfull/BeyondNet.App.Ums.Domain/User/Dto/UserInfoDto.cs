using System;
using BeyondNet.App.Ums.Helpers.Hypermedias;

namespace BeyondNet.App.Ums.Domain.User.Dto
{
    public class UserInfoDto : AbstractLinkDto
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
    }
}
