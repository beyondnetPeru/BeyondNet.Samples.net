using System;
using System.Collections.Generic;
using BeyondNet.App.Ums.Domain.User.Dto;
using BeyondNet.App.Ums.Helpers.Paginations;

namespace BeyondNet.App.Ums.Domain.User
{
    public interface IUserApplicationService
    {
        ApplicationResult<PageList<UserInfoDto>> GetAll(PaginationParameters paginationParameters);
        ApplicationResult<IEnumerable<UserInfoDto>> GetCollection(IEnumerable<Guid> userIds);
        ApplicationResult<UserDto> Get(Guid userId);
        ApplicationResult<bool> UserNameExists(string userName);
        ApplicationResult<bool> EmailExists(string email);
        ApplicationResult<UserDto> Create(UserCreationDto userDto);
        void ChangeEmail(Guid id, string newEmail);
        void ChangeUserName(Guid id, string userName);
        ApplicationResult<UserDto> Update(UserUpdateDto userDto);
        void Delete(Guid userId);
        ApplicationResult<IEnumerable<KeyInfoDto>> GetKeys(Guid userId);
        ApplicationResult<KeyInfoDto> GetKey(Guid userId, Guid keyId);
        ApplicationResult<KeyDto> CreateKey(Guid userId, KeyCreationDto keyDto);
        ApplicationResult<KeyDto> UpdateKey(Guid userId, KeyUpdateDto keyDto);
        void DeleteKey(Guid userId, Guid keyId);
        
    }
}