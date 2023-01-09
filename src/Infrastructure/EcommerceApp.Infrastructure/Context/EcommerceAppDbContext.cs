using EcommerceApp.Domain.Entities;
using EcommerceApp.Infrastructure.EntityTypeConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Infrastructure.Context
{
    public class EcommerceAppDbContext:DbContext
    {
        public EcommerceAppDbContext(DbContextOptions<EcommerceAppDbContext>options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfig())
                .ApplyConfiguration(new MallConfig())
                .ApplyConfiguration(new ProductConfig())
                .ApplyConfiguration(new CategoryConfig());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Mall> Malls { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
