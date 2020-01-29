using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BeyondNet.App.Ums.Domain.Common.Impl;
using BeyondNet.App.Ums.Domain.Common.Interface;
using BeyondNet.App.Ums.Domain.User.Dto;
using BeyondNet.App.Ums.Domain.User.Key;
using BeyondNet.App.Ums.Domain.User.Specifications;
using BeyondNet.App.Ums.Helpers.Cryptos;
using BeyondNet.App.Ums.Helpers.Paginations;

namespace BeyondNet.App.Ums.Domain.User
{
    public class UserApplicationService : AbstractApplicationService, IUserApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordCryptographer _passwordCryptographer;

        public UserApplicationService(IPasswordCryptographer passwordCryptographer, 
                                      IUserRepository userRepository, 
                                      IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _userRepository = userRepository;
            _passwordCryptographer = passwordCryptographer;
        }

        public ApplicationResult<UserDto> Get(Guid userId)
        {
            var userEdit = _userRepository.Get(userId);

            return new ApplicationResult<UserDto>() {Data = Mapper.Map<UserDto>(userEdit), IsSuccessful = true};
        }

        public UserDto Get(string userName)
        {
            var spec =  new UserGetByUserNameSpec(userName);

            var userEdit = _userRepository.FindOne(spec);

            return Mapper.Map<UserDto>(userEdit);
        }

        public ApplicationResult<PageList<UserInfoDto>> GetAll(PaginationParameters paginationParameters)
        {
            var userListInfoReadModel = _userRepository.GetAll(paginationParameters);

            var userListMapped = Mapper.Map<IEnumerable<UserInfoDto>>(userListInfoReadModel);

            var userPagedList = PageList<UserInfoDto>.Create(userListMapped.AsQueryable(), 
                                                      paginationParameters.PageNumber, paginationParameters.PageSize);

            return new ApplicationResult<PageList<UserInfoDto>>()
            {
                Data = userPagedList,
                IsSuccessful = true
            };
        }

        public ApplicationResult<IEnumerable<UserInfoDto>> GetCollection(IEnumerable<Guid> userIds)
        {
            var ids = userIds.ToList();

            var userEdits = _userRepository.GetCollection(ids);

            return new ApplicationResult<IEnumerable<UserInfoDto>>()
            {
                Data = Mapper.Map<IEnumerable<UserInfoDto>>(userEdits),
                IsSuccessful = true
            };
        }

        public ApplicationResult<UserDto> Create(UserCreationDto userDto)
        {
            var userEdit = UserEdit.Create(userDto.ExternalId,userDto.UserName, userDto.FullName, userDto.Email);

            var brokenRules = userEdit.GetBrokenValidationRules();

            if (!brokenRules.IsValid)
                return new ApplicationResult<UserDto> {Errors = brokenRules.Errors.ToList(),IsSuccessful = false,IsFailure = true};

            userEdit = _userRepository.Create(userEdit);

            if (userDto.Keys.Any())
            {
                foreach (var key in userDto.Keys)
                {
                    var keyEdit = KeyEdit.Create(userEdit, key.Password);

                    _userRepository.CreateKey(userEdit.Id, keyEdit);
                }
            }

            UnitOfWork.Commit();

            return new ApplicationResult<UserDto>(){Data = Mapper.Map<UserDto>(userEdit), IsSuccessful = true, IsFailure = false};

        }

        public ApplicationResult<bool> UserNameExists(string userName)
        {
            var spec = new UserExistsByUserNameSpec(userName);

            var userExists = _userRepository.Exists(spec);

            return new ApplicationResult<bool>() {IsSuccessful = userExists };
        }

        public ApplicationResult<bool> EmailExists(string email)
        {
            var spec = new UserExistsByEmailSpec(email);

            var emailExists = _userRepository.Exists(spec);

            return new ApplicationResult<bool>() {IsSuccessful = emailExists };
        }

        public void ChangeEmail(Guid id, string newEmail)
        {
            throw new NotImplementedException();
        }

        public void ChangeUserName(Guid id, string userName)
        {
            throw new NotImplementedException();
        }

        public ApplicationResult<UserDto> Update(UserUpdateDto userUpdateDto)
        {
            var userEdit = Mapper.Map<UserEdit>(userUpdateDto);

            userEdit.Update(userUpdateDto.ExternalId, userUpdateDto.UserName, userUpdateDto.FullName, userUpdateDto.Email);

            var brokenRules = userEdit.GetBrokenValidationRules();

            if (!brokenRules.IsValid)
                return new ApplicationResult<UserDto> { Errors = brokenRules.Errors.ToList(), IsFailure = true };

            userEdit = _userRepository.Update(userEdit);

            UnitOfWork.Commit();

            return new ApplicationResult<UserDto>() {Data = Mapper.Map<UserDto>(userEdit), IsSuccessful = true};
        }

        public void Delete(Guid userId)
        {
            var userEdit = _userRepository.Get(userId);

            _userRepository.Delete(userEdit.Id);

            UnitOfWork.Commit();
        }

        public ApplicationResult<IEnumerable<KeyInfoDto>> GetKeys(Guid userId)
        {
            var keyInfoReadModel = _userRepository.GetKeys(userId);

            return new ApplicationResult<IEnumerable<KeyInfoDto>>()
            {
                Data = Mapper.Map<IEnumerable<KeyInfoDto>>(keyInfoReadModel),
                IsSuccessful = true
            };
        }

        public ApplicationResult<KeyInfoDto> GetKey(Guid userId, Guid keyId)
        {
            var keyEdit = _userRepository.GetKey(userId, keyId);

            var brokenRules = keyEdit.GetBrokenValidationRules();

            if (!brokenRules.IsValid)
                return new ApplicationResult<KeyInfoDto>(){Errors = brokenRules.Errors.ToList(), IsFailure = true };

            return new ApplicationResult<KeyInfoDto>() {Data = Mapper.Map<KeyInfoDto>(keyEdit), IsSuccessful = true};
        }

        public ApplicationResult<KeyDto> CreateKey(Guid userId, KeyCreationDto keyDto)
        {
            var userEdit = _userRepository.Get(userId);

            var passwordEncrypted = _passwordCryptographer.Encrypt(keyDto.Password);

            var keyEdit = KeyEdit.Create(userEdit, passwordEncrypted);

            var brokenRules = keyEdit.GetBrokenValidationRules();

            if (!brokenRules.IsValid)
                return new ApplicationResult<KeyDto>() { Errors = brokenRules.Errors.ToList(), IsFailure = true };

            userEdit.CreateKey(keyEdit);

            _userRepository.CreateKey(keyEdit.Id, keyEdit);

            UnitOfWork.Commit();

            return new ApplicationResult<KeyDto>() {Data = Mapper.Map<KeyDto>(keyEdit), IsSuccessful = true};
        }

        public ApplicationResult<KeyDto> UpdateKey(Guid userId, KeyUpdateDto keyDto)
        {
            var userFound = _userRepository.Get(userId);

            var keyEdit = Mapper.Map<KeyEdit>(keyDto);

            var passwordEncrypted = _passwordCryptographer.Encrypt(keyDto.Password);

            keyEdit.ChangePassword(passwordEncrypted);

            var brokenRules = keyEdit.GetBrokenValidationRules();

            if (!brokenRules.IsValid)
                return new ApplicationResult<KeyDto>() { Errors = brokenRules.Errors.ToList(), IsFailure = true };

            _userRepository.UpdateKey(userId, keyEdit);

            UnitOfWork.Commit();

            return new ApplicationResult<KeyDto>() {Data = Mapper.Map<KeyDto>(keyEdit), IsSuccessful = true};
        }

        public void DeleteKey(Guid userId, Guid keyId)
        {
            var userEdit = _userRepository.Get(userId);

            var keyEdit = userEdit.Keys.First(x => x.Id == keyId);

            userEdit.DeleteKey(keyEdit);

            _userRepository.DeleteKey(userEdit.Id, keyEdit.Id);

            UnitOfWork.Commit();
        }
    }
}
