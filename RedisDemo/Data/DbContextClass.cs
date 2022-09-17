using Microsoft.EntityFrameworkCore;
using RedisDemo.Model;

namespace RedisDemo.Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
