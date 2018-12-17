using Microsoft.EntityFrameworkCore;

namespace loginreg.Models
{
    public class LoginRegContext : DbContext
    {
        public LoginRegContext(DbContextOptions<LoginRegContext> options) : base(options) {}
        public  DbSet<User> Users { get; set;}
    }
}