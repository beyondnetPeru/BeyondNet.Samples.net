using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotifyServer.Library.Interface;
using NotifyServer.Library.Model.Entities;
using NotifyServer.Model;

namespace NotifyServer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;
        
        private readonly INotifyApplication _notifyApplication;

        private readonly NotificationDbContext _context;


        public NotifyController(ILogger<NotifyController> logger,
                                INotifyApplication notifyApplication, NotificationDbContext context)
        {
            _logger = logger;
         
            _notifyApplication = notifyApplication;

            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Send(RequestCommand requestCommand)
        {
            if (!_notifyApplication.IsValid(requestCommand))
                return BadRequest(new ResponseMessage(){Code = 400, Errors = new Dictionary<string, object>(){{"X404","Request is not valid"}}});

            var responseMessage = await _notifyApplication.PushMessage(requestCommand);

            if (responseMessage.Errors != null && responseMessage.Errors.Any())
                return BadRequest(new ResponseMessage()
                    {Code = responseMessage.Code, Errors = responseMessage.Errors});

            return Ok(responseMessage);
        }
    }
}
