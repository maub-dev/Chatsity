using BlazorApp.Data;

namespace BlazorApp.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }

        public ApplicationUser SentBy { get; set; }
        public Chatroom Room { get; set; }
    }
}
