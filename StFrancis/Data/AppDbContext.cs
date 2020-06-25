using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StFrancis.Models;


namespace StFrancis.Data
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option): base(option)
        {

        }

        public DbSet<Event> Events { get; set; }
    }
}
