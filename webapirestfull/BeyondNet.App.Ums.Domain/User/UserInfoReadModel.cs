using System;

namespace BeyondNet.App.Ums.Domain.User
{
    public class UserInfoReadModel
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
    }
}
