using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NotifyServer.Library.Hubs;
using NotifyServer.Library.Interface;
using NotifyServer.Library.Model;

namespace NotifyServer.Library.Impl
{
    public class NotificationSender:INotificationSender
    {
        private readonly IHubContext<NotifyHub> _notificationContext;

        public NotificationSender(IHubContext<NotifyHub> notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task<Result> Send(string clientMethod, string message)
        {
            try
            {
                await _notificationContext.Clients.All.SendAsync(clientMethod, message);

                return new Result()
                {
                    ResponseCode = 200, 
                    Data = new Dictionary<string, object>(){{"X200", $"Message sent: {message}"
                }}};
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    ResponseCode = 500,
                    Data = new Dictionary<string, object>(){{"X500", $"Error Message: {ex.Message}"
                    }}
                };
            }
        }

        public async Task<Result> Send(string clientMethod, string message, string userName)
        {
            try
            {
                //await _notificationContext.Clients.User(userName).SendAsync(clientMethod, message);

                await _notificationContext.Clients.All.SendAsync(clientMethod, message);

                return new Result()
                {
                    ResponseCode = 200,
                    Data = new Dictionary<string, object>(){{"X200", $"Message sent: {message}"
                    }}
                };
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    ResponseCode = 500,
                    Data = new Dictionary<string, object>(){{"X500", $"Error Message: {ex.Message}"
                    }}
                };
            }
        }
    }
}
