using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotifyServer.Library.Model.Entities
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public string Id { get; set; }
        public string NotificationType { get; set; }
        public string ProfileId { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public string Hyperlink { get; set; }
        public DateTime DateTime { get; set; }
        public bool Read { get; set; }
        public string Parameters { get; set; }
        public string RawMessage { get; set; }
        public int Status { get; set; }
    }
}
