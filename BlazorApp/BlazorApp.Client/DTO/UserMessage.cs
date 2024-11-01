using System;

namespace BlazorApp.Client.DTO
{
    public class UserMessage
    {
        public Guid ChatroomId { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public DateTime DateSent { get; set; }
    }
}
