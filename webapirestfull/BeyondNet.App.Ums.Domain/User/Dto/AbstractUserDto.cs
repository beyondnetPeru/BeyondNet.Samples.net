using System.ComponentModel.DataAnnotations;

namespace BeyondNet.App.Ums.Domain.User.Dto
{
    public abstract class AbstractUserDto
    {
        public string ExternalId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        public virtual string FullName { get; set; }
        [Required]
        [MaxLength(300)]
        public string Email { get; set; }
    }
}
