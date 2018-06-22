using Microsoft.EntityFrameworkCore;


namespace Linq.Model {
    public class TESTDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ReturnVisitTask> ReturnVisitTasks { get; set; }
        public DbSet<VisitRecord> VisitRecords { get; set; }

        public TESTDbContext(DbContextOptions<TESTDbContext> options)
            : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Customer>().HasKey(c=>c.Id);
            modelBuilder.Entity<Consultant>().HasKey(c => c.Id);
            modelBuilder.Entity<ReturnVisitTask>().HasKey(c => c.Id);
            modelBuilder.Entity<VisitRecord>().HasKey(c => c.Id);
        }
    }
}
