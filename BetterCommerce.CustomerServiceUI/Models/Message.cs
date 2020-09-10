using System;

namespace BetterCommerce.CustomerServiceUI.Models
{
    public class Message
    {

        public int RoomId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}