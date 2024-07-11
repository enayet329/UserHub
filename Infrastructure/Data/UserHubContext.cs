using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserHubContext : DbContext
    {
        public UserHubContext(DbContextOptions<UserHubContext> options) : base(options)
        {
            
        }

        public DbSet<User> UserHub { get; set; }
    }
}
