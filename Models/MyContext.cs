using Microsoft.EntityFrameworkCore;
 
namespace CRUDelicious.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Dish> dishes { get; set; }

    }
}