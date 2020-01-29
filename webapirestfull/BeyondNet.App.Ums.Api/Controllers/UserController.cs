using System;
using System.Linq;
using BeyondNet.App.Ums.Domain.User;
using BeyondNet.App.Ums.Domain.User.Dto;
using BeyondNet.App.Ums.Helpers.Binders;
using BeyondNet.App.Ums.Helpers.Hypermedias;
using BeyondNet.App.Ums.Helpers.Logging;
using BeyondNet.App.Ums.Helpers.Paginations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UMS.Infrastructure.Helpers.Paginations;

namespace BeyondNet.App.Ums.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Users", Name = "API Users")]
    public class UserController : Controller
    {
        private readonly IUserApplicationService _userApplication;
        private readonly ILog _logger;
        private readonly IPaginationHelper _paginationHelper;
        private readonly IHypermediaBuilder _hypermediaBuilder;

        public UserController(IUserApplicationService userApplication, 
                              ILog logger, 
                              IPaginationHelper paginationHelper,
                              IHypermediaBuilder hypermediaBuilder)
        {
            _userApplication = userApplication;
            _logger = logger;
            _paginationHelper = paginationHelper;
            _hypermediaBuilder = hypermediaBuilder;
        }

        [HttpGet(Name = "GetAllUsers")]
        public IActionResult GetAll(PaginationParameters paginationParameters)
        {
            _logger.Info($"Event: {LoggingEvents.UserGetAll}, Method:{nameof(GetUser)}, Message: Get all users. Paging: {JsonConvert.SerializeObject(paginationParameters)}");

            var userInfoListDto = _userApplication.GetAll(paginationParameters).Data;

            var previousPageLink = userInfoListDto.HasPrevious ?
                _paginationHelper.CreateUsersResourceUrl("GetAllUsers", paginationParameters, ResourceUriType.PreviousPage) : null;

            var nextPageLink = userInfoListDto.HasNext ?
                _paginationHelper.CreateUsersResourceUrl("GetAllUsers", paginationParameters, ResourceUriType.NextPage) : null;

            Response.Headers.Add("X-Pagination", _paginationHelper.GetMetadata(userInfoListDto, previousPageLink, nextPageLink));

            var newUserListAsQueryable = userInfoListDto.Select(user => 
            {
                user = CreateLink(user);
                return user;
            }).AsQueryable();
            
            var wrapper = new LinkedCollectionWrapperDto<UserInfoDto>(newUserListAsQueryable);

            return Ok(CreateLink(wrapper));
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(Guid userId)
        {
            _logger.Info($"Event:{LoggingEvents.UserGet},Method:{nameof(GetUser)}, Message: find user with ID: {userId}");

            var userFound = _userApplication.Get(userId).Data;

            if (userFound == null)
            {
                _logger.Warn($"Event:{LoggingEvents.UserGet},Method:{nameof(GetUser)}, Error: user with Id {userId} cannot be found.");
                return NotFound();
            }

            _logger.Info($"Event:{LoggingEvents.UserGet},Method:{nameof(GetUser)}, Message: user was found. PayLoad: {JsonConvert.SerializeObject(userFound)}");

            return Ok(CreateLink(userFound));
        }
        
        [HttpPost(Name="CreateUser")]
        public IActionResult CreateUser([FromBody] UserCreationDto user)
        {
            _logger.Info($"Event:{LoggingEvents.UserCreate},Method:{nameof(CreateUser)}, Message: trying create a new user. Payload: {JsonConvert.SerializeObject(user)}");

            if (user == null)
            {
                _logger.Warn($"Event:{LoggingEvents.UserCreate},Method:{nameof(CreateUser)}, Error: model is null.");
                return BadRequest();
            }

            var userNameFound = _userApplication.UserNameExists(user.UserName);

            if (userNameFound.IsSuccessful)
            {
                _logger.Warn($"Event:{LoggingEvents.UserCreate},Method:{nameof(CreateUser)}, Error: username {user.UserName} exists, try using another one.");
                return BadRequest();
            }

            var emailFound = _userApplication.EmailExists(user.Email);

            if (emailFound.IsSuccessful)
            {
                _logger.Warn($"Event:{LoggingEvents.UserCreate},Method:{nameof(CreateUser)}, Error: email {user.Email} exists, try using another one.");
                return BadRequest();
            }

            var userCreated = _userApplication.Create(user);

            if (userCreated.IsFailure)
            {
                _logger.Warn($"Event:{LoggingEvents.UserCreate},Method:{nameof(CreateUser)}, Error: the following validations rules were broken: {JsonConvert.SerializeObject(userCreated.Errors)}");
            }

            _logger.Info($"Event:{LoggingEvents.UserCreate},Method:{nameof(CreateUser)}, Message: user was created successfully.");

            return CreatedAtRoute("GetUser", new {userId = userCreated.Data.Id}, CreateLink(userCreated.Data));
        }

        [HttpPut("{userId}", Name = "UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserUpdateDto user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var userNameFound = _userApplication.UserNameExists(user.UserName);

            if (userNameFound.Data)
            {
                return BadRequest();
            }

            var emailFound = _userApplication.EmailExists(user.Email);

            if (emailFound.Data)
            {
                return BadRequest();
            }

            var userUpdated = _userApplication.Update(user).Data;

            return Ok(userUpdated);
        }

        [HttpDelete("{userId}", Name = "DeleteUser")]
        public IActionResult DeleteUser(Guid userId)
        {
            var userFound = _userApplication.Get(userId).Data;

            if (userFound == null)
                return NotFound();
           
            _userApplication.Delete(userId);

            return NoContent();
        }

        #region Hypermedia

        private UserDto CreateLink(UserDto userDto)
        {
            _hypermediaBuilder.CreateLink(userDto, "GetUser", userDto.Id, "self", HypermediaVerbs.Get);
            _hypermediaBuilder.CreateLink(userDto, "CreateUser", userDto.Id, "create_user", HypermediaVerbs.Post);
            _hypermediaBuilder.CreateLink(userDto, "UpdateUser", userDto.Id, "update_user", HypermediaVerbs.Put);
            _hypermediaBuilder.CreateLink(userDto, "DeleteUser", userDto.Id, "delete_user", HypermediaVerbs.Delete);

            return userDto;
        }

        private UserInfoDto CreateLink(UserInfoDto userInfoDto)
        {
            _hypermediaBuilder.CreateLink(userInfoDto, "GetAllUsers", userInfoDto.Id, "self", HypermediaVerbs.Get);

            return userInfoDto;
        }

        private LinkedCollectionWrapperDto<UserInfoDto> CreateLink(LinkedCollectionWrapperDto<UserInfoDto> usersWrapper)
        {
            _hypermediaBuilder.CreateCollectionLink(usersWrapper, "GetAllUsers", "self", HypermediaVerbs.Get);

            return usersWrapper;
        }

        #endregion  
    }
}
