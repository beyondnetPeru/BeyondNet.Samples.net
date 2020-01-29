using System.ComponentModel.DataAnnotations;

namespace BeyondNet.App.Ums.Domain.User.Dto
{
    public class UserUpdateDto : AbstractUserDto
    {
        [Required]
        public override string FullName { get; set; }
    }
}
