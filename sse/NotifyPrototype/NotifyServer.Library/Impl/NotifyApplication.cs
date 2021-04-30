using NotifyServer.Library.Interface;
using NotifyServer.Library.Model;
using NotifyServer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NotifyServer.Library.Impl
{
    public class NotifyApplication : INotifyApplication
    {
        private readonly INotificationSender _notificationSender;

        private readonly INotifyFormatter _formatter;

        private readonly INotifyRepository _repository;


        public NotifyApplication(INotificationSender notificationSender, INotifyFormatter formatter, INotifyRepository repository)
        {
            _notificationSender = notificationSender;
            
            _formatter = formatter;
            _repository = repository;

            _repository = repository;
        }

        public ResponseMessage GetTopMessagesByProfile(RequestCommand requestCommand)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(RequestCommand requestCommand)
        {
            if (string.IsNullOrEmpty(requestCommand.NotificationType) ||
                requestCommand.NotificationType.ToLower() != "prv" &&
                requestCommand.NotificationType.ToLower() != "pub")
                return false;

            if (string.IsNullOrEmpty(requestCommand.Message))
                return false;

            if (string.IsNullOrEmpty(requestCommand.UserName))
                return false;

            return true;
        }

        public async Task<ResponseMessage> PushMessage(RequestCommand requestCommand)
        {
            var formattedValue = _formatter.Format(new InValue()
            {
                ProfileId = requestCommand.ProfileId,
                Message = requestCommand.Message,
                Hyperlink = requestCommand.Hyperlink,
                Read = requestCommand.Read,
                NotificationType = requestCommand.NotificationType,
                Parameters = requestCommand.Parameters,
                User = requestCommand.UserName,
            });

            if (requestCommand.NotificationType.ToLower().Contains("pub"))
                await _notificationSender.Send("PublicNotificationPush", formattedValue.FormattedValue);
            else
                await _notificationSender.Send("PrivateNotificationPush", formattedValue.FormattedValue, requestCommand.UserName);

            var recordBuilt = BuildRecord(requestCommand);

             await _repository.Insert(recordBuilt);

            return BuildResponseMessage(recordBuilt);
        }

        private ResponseMessage BuildResponseMessage(NotifyAdd notifyAdd)
        {
            return new ResponseMessage()
            {
                Code = 200,
                Data = new Dictionary<string, object>(){{"Message", JsonConvert.SerializeObject(notifyAdd) } },
                Errors = null    
            };
        }

        private NotifyAdd BuildRecord(RequestCommand requestCommand)
        {
           return new NotifyAdd()
            {
                Message = requestCommand.Message,
                UserName = requestCommand.UserName,
                Hyperlink = requestCommand.Hyperlink,
                DateTime = DateTime.Now,
                Read = requestCommand.Read,
                ProfileId = requestCommand.ProfileId,
                NotificationType = requestCommand.NotificationType,
                Parameters = requestCommand.Parameters,
                RawMessages = JsonConvert.SerializeObject(requestCommand)
            };
        }
    }
}
