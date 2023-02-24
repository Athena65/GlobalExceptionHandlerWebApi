using GlobalExceptionHandlerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandlerWebApi.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; } 
    }
}
