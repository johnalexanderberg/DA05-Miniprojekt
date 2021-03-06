using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DA05_Miniprojekt
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
            options.UseSqlServer(@"Data Source=den1.mssql7.gear.host;Initial Catalog=consoleapp;User Id=consoleapp;Password={}; Trusted_Connection=false; MultipleActiveResultSets=true;");
        }
    }


}


