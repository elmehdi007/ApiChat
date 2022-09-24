using chat.Models;
using Microsoft.EntityFrameworkCore;

namespace chat.Context
{
    public class ChatDbContext : DbContext
    {

        public ChatDbContext(DbContextOptions<ChatDbContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
    }
}
