using Microsoft.EntityFrameworkCore;
using SwaggerTest.Models;

namespace SwaggerTest.Data
{
    public class DatabaseDBContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("default"));
        }
        public DbSet<UserIM> userIMs { get; set; }
        public DbSet<UserVM> userVMs { get; set; }
        public DbSet<UserToDoModelIM> userToDoModelsim { get; set;}
        public DbSet<UserToDoModelVM> userToDoModelsvm { get; set;}
    }
}
