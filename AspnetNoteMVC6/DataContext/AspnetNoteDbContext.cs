using AspnetNoteMVC6.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetNoteMVC6.DataContext
{
    public class AspnetNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=AspnetnoteDb;User Id=sa;Password=sa1234;");
        }
    }
}
