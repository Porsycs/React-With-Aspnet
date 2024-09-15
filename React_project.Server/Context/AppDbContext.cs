using Microsoft.EntityFrameworkCore;
using React_project.Server.Models;

namespace React_project.Server.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Client> Clients { get; set; }
    }
}
