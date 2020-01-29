using BeyondNet.App.Ums.Domain.User;
using Microsoft.AspNetCore.Mvc;

namespace BeyondNet.App.Ums.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/UserCollection")]
    public class UserCollectionController : Controller
    {
        private readonly IUserApplicationService _userApplication;

        public UserCollectionController(IUserApplicationService userApplication)
        {
            _userApplication = userApplication;
        }


        //[HttpPost]
        //public IActionResult CreateUserWithKeys([FromBody] UserCollectionCreationDto user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }

        //    var userCreated = _userApplication.Create(user);

        //    return CreatedAtRoute("GetUser", new { userId = userCreated.Id }, userCreated);
        //}

        //[HttpPost]
        //public IActionResult CreateUserCollection([FromBody] IEnumerable<UserCreationDto> userCollection)
        //{
        //    if (userCollection == null)
        //    {
        //        return BadRequest();
        //    }

        //    var userDtoCollection = _userApplication.CreateCollection(userCollection);

        //    var idsAsStrings = string.Join(",", userDtoCollection.Select(x => x.Id));

        //    return CreatedAtRoute("GetUserCollection", new {userIds = idsAsStrings}, userDtoCollection);
        //}

        //[HttpGet("{{userIds}}", Name = "GetUserCollection")]
        //public IActionResult GetUserGuids([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> userIds)
        //{
        //    if (userIds == null) return BadRequest();

        //    var ids = userIds.ToList();

        //    var userDtos = _userApplication.GetAll();

        //    if (ids.Count != userDtos.Count()) return NotFound();
            
        //    return Ok(userDtos);
        //}
    }
}