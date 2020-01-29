using System;
using System.Linq;
using BeyondNet.App.Ums.Domain.User;
using BeyondNet.App.Ums.Domain.User.Dto;
using BeyondNet.App.Ums.Helpers.Hypermedias;
using Microsoft.AspNetCore.Mvc;

namespace BeyondNet.App.Ums.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/users/{userId}/keys")]
    public class KeyController : Controller
    {
        private readonly IUserApplicationService _userApplication;
        private readonly IHypermediaBuilder _hypermediaBuilder;

        public KeyController(IUserApplicationService userApplication, IHypermediaBuilder hypermediaBuilder)
        {
            _userApplication = userApplication;
            _hypermediaBuilder = hypermediaBuilder;
        }

        [HttpGet(Name = "GetKeysByUser")]
        public IActionResult GetKeysByUser(Guid userId)
        {
            var userFound = _userApplication.Get(userId).Data;

            if (userFound == null)
            {
                return NotFound();
            }

            var keyInfoList = _userApplication.GetKeys(userId).Data;

            keyInfoList = keyInfoList.Select(key =>
            {
                key = CreateLink(key);
                return key;
            });

            var wrapper = new LinkedCollectionWrapperDto<KeyInfoDto>(keyInfoList);

            return Ok(CreateLink(wrapper));
        }

        [HttpGet("{keyId}", Name = "GetKeyByUser")]
        public IActionResult GetKeyByUser(Guid userId, Guid keyId)
        {
            var userFound = _userApplication.Get(userId).Data;

            if (userFound == null) return NotFound();

            var keyDto = _userApplication.GetKey(userId, keyId).Data;

            if (keyDto == null) return NotFound();

            return Ok(CreateLink(keyDto));
        }

        [HttpPost()]
        public IActionResult CreateKeyByUser(Guid userId, [FromBody] KeyCreationDto key)
        {
            if (key == null) return BadRequest();

            var userFound = _userApplication.Get(userId).Data;

            if (userFound == null) return NotFound();

            var keyDto = _userApplication.CreateKey(userId, key).Data;

            return CreatedAtRoute("GetKeyForUser", new {userId = keyDto.UserId, keyId = keyDto.Id}, CreateLink(keyDto));
        }

        [HttpPut("{keyId}", Name = "UpdateKeyByUser")]
        public IActionResult UpdateKeyForUser(Guid userId, Guid keyId, [FromBody] KeyUpdateDto key)
        {
            if (key == null) return BadRequest();

            var userFound = _userApplication.Get(userId).Data;

            if (userFound == null) return NotFound();

            var keyFound = _userApplication.GetKey(userId, keyId).Data;

            if (keyFound == null) return NotFound();

            var keyUpdated = _userApplication.UpdateKey(userId, key).Data;

            return Ok(CreateLink(keyUpdated));
        }

        [HttpDelete("keyId", Name = "DeleteKeyByUser")]
        public IActionResult DeleteKeyByUser(Guid userId, Guid keyId)
        {
            _userApplication.DeleteKey(userId,keyId);

            return NoContent();
        }

        #region Hypermedia

        private KeyDto CreateLink(KeyDto keyDto)
        {
            _hypermediaBuilder.CreateLink(keyDto, "GetKeyByUser", keyDto.Id, "selft", HypermediaVerbs.Get);
            _hypermediaBuilder.CreateLink(keyDto, "DeleteKeyByUser", keyDto.Id, "delete_key", HypermediaVerbs.Delete);
            _hypermediaBuilder.CreateLink(keyDto, "UpdateKeyByUser", keyDto.Id, "update_key", HypermediaVerbs.Put);

            return keyDto;
        }

        private KeyInfoDto CreateLink(KeyInfoDto keyInfoDto)
        {
            _hypermediaBuilder.CreateLink(keyInfoDto, "GetKeyByUser", keyInfoDto.Id, "selft", HypermediaVerbs.Get);

            return keyInfoDto;
        }

        private LinkedCollectionWrapperDto<KeyInfoDto> CreateLink(LinkedCollectionWrapperDto<KeyInfoDto> keysWrapper)
        {
            _hypermediaBuilder.CreateCollectionLink(keysWrapper, "GetKeysByUser","self",HypermediaVerbs.Get);

            return keysWrapper;
        }

        #endregion
    }
}