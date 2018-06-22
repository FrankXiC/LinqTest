using Microsoft.EntityFrameworkCore;

namespace TestDataBaseEx.Model {
    public static class DbContextOptionsConfigurer {
        public static void Configure(
            DbContextOptionsBuilder<TESTDbContext> dbContextOptions,
            string connectionString
        ) {
            /* This is the single point to configure DbContextOptions for TESTABPDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}