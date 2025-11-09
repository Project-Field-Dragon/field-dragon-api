using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// 1. 修正 Namespace
namespace Project.Field.Dragon.Data
{
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

            // 2. 修正資料庫路徑 (和 Startup.cs 裡的一樣)
            optionsBuilder.UseSqlite("Data Source=../Registrar.sqlite");

            return new StoreContext(optionsBuilder.Options);
        }
    }
}