using System.Collections.Generic;

namespace BeyondNet.App.Ums.Domain.User.Dto
{
    public class UserCreationDto : AbstractUserDto
    {
        public ICollection<KeyCreationDto> Keys { get; set; } = new List<KeyCreationDto>();
    }
}
