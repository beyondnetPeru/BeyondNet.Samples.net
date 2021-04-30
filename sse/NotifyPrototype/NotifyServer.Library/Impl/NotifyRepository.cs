using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NotifyServer.Library.Interface;
using NotifyServer.Library.Model.Entities;
using NotifyServer.Model;

namespace NotifyServer.Library.Impl
{
    public class NotifyRepository : INotifyRepository
    {
        private readonly NotificationDbContext _context;

        public NotifyRepository(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task Insert(NotifyAdd notifyAdd)
        {
            try
            {
                var notification =  new Notification()
                {
                    Id = Guid.NewGuid().ToString(),
                    User = notifyAdd.UserName,
                    DateTime = notifyAdd.DateTime,
                    NotificationType = notifyAdd.NotificationType,
                    Hyperlink = notifyAdd.Hyperlink,
                    Message = notifyAdd.Message,
                    ProfileId = notifyAdd.ProfileId,
                    Parameters = JsonConvert.SerializeObject(notifyAdd.Parameters),
                    RawMessage = JsonConvert.SerializeObject(notifyAdd),
                    Read = false,
                    Status = 1
                };

                await _context.Notifications.AddAsync(notification);

                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
            
        }
    }
}
