using System.Data;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak
    
    public class NorthwindContext:DbContext
    {
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseMySQL(@"server=localhost;user=root;password=;database=northwindmysql;");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Owner> Owners { get; set; }

        
    }   
}