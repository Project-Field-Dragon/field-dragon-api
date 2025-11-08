// 1. 匯入 (using) 你的 Domain 類別和 EF Core
using Project.Field.Dragon.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

// 2. 修正 Namespace
namespace Project.Field.Dragon.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }

        // 3. 告訴 EF Core，我們的資料庫裡有一個 "Items" 表格
        public DbSet<Item> Items { get; set; }
    }
}