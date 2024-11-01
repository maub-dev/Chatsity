using BlazorApp.Client.DTO;
using BlazorApp.Data;
using BlazorApp.Models;
using Chatsity.Infra;
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
            var chatMessagesToAdd = new List<ChatMessage>
            {
                new()
                {
                    DateSent = DateTime.Now,
                    Message = userMessage.Message,
                    Room = room,
                    SentBy = user,
                    Username = user.UserName
                }
            };
            if (userMessage.Message.StartsWith("/stock=", StringComparison.InvariantCultureIgnoreCase))
            {
                var stockCode = userMessage.Message.Replace("/stock=", "", StringComparison.InvariantCultureIgnoreCase);
                var stockData = await StooqService.GetStockDataAsync(stockCode);
                if (stockData is not null)
                {

                    chatMessagesToAdd.Add(new()
                    {
                        Message = $"{stockCode} quote is ${stockData.Close} per share",
                        DateSent = DateTime.Now,
                        Room = room,
                        Username = "StooqBot"
                    });
                }
            }
            
            await _applicationDbContext.ChatMessage.AddRangeAsync(chatMessagesToAdd);
            await _applicationDbContext.SaveChangesAsync();

            var chatMessages = _applicationDbContext.ChatMessage.OrderByDescending(x => x.DateSent).Take(50).ToList();

            await Clients.All.SendAsync("ReceiveMessage", chatMessages.Select(x => new UserMessage
            {
                ChatroomId = x.Room.Id,
                DateSent = x.DateSent,
                Message = x.Message,
                Username = x.Username
            }).OrderBy(x => x.DateSent));
        }
    }
}
