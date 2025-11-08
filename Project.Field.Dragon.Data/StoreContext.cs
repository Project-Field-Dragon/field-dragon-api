using Project.Field.Dragon.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Project.Field.Dragon.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }

        public DbSet<Item> Items { get; set; }

        // 確保這段 OnModelCreating 程式碼「一定」在檔案裡！
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DbInitializer.Initialize(builder);
        }
    }
}