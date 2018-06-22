using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestDataBaseEx.Model;

namespace TestDataBaseEx.Model {
    public class TESTDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ReturnVisitTask> ReturnVisitTasks { get; set; }
        public DbSet<VisitRecord> VisitRecords { get; set; }

        public string conn;
        public TESTDbContext(string option) {
            conn = option;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(conn);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Customer>().HasKey(c=>c.Id);
            modelBuilder.Entity<Consultant>().HasKey(c => c.Id);
            modelBuilder.Entity<ReturnVisitTask>().HasKey(c => c.Id);
            modelBuilder.Entity<VisitRecord>().HasKey(c => c.Id);
        }
    }
}
