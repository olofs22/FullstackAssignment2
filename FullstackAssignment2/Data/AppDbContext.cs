using FullstackAssignment2.Models;
using Microsoft.EntityFrameworkCore;
    
namespace FullstackAssignment2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Car { get; set; } 
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }
    }
}
