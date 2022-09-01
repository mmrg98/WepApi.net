using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class WebAppContext :IdentityDbContext
    {
        private readonly IConfiguration _configuration;
        public WebAppContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Employee> Employees { get; set; } = null;
        public DbSet<Department> Department { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EmployeeAppCon"));
        }
    }
}
