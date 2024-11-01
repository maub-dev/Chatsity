using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Models;

namespace BlazorApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<BlazorApp.Models.Chatroom> Chatroom { get; set; } = default!;
        public DbSet<BlazorApp.Models.ChatMessage> ChatMessage { get; set; } = default!;
    }
}
