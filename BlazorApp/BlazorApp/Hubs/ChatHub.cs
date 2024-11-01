using BlazorApp.Client.DTO;
using BlazorApp.Data;
using BlazorApp.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ChatHub(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SendMessage(UserMessage userMessage)
        {
            var room = _applicationDbContext.Chatroom.First(x => x.Id == userMessage.ChatroomId);
            var user = _applicationDbContext.Users.First(x => x.Id == Context.UserIdentifier);
            var chatMessage = new ChatMessage
            {
                DateSent = DateTime.Now,
                Message = userMessage.Message,
                Room = room,
                SentBy = user
            };
            
            await _applicationDbContext.ChatMessage.AddAsync(chatMessage);
            await _applicationDbContext.SaveChangesAsync();

            var chatMessages = _applicationDbContext.ChatMessage.Select(x => x).Include(x => x.SentBy).OrderByDescending(x => x.DateSent).Take(50).ToList();

            await Clients.All.SendAsync("ReceiveMessage", chatMessages.Select(x => new UserMessage
            {
                ChatroomId = x.Room.Id,
                DateSent = x.DateSent,
                Message = x.Message,
                Username = x.SentBy.UserName
            }).OrderBy(x => x.DateSent));
        }
    }
}
