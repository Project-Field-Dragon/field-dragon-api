using Microsoft.AspNetCore.Mvc;
using Project.Field.Dragon.Domain.Catalog; 
using System.Collections.Generic;
using Project.Field.Dragon.Data; // <-- 1. 匯入 (using) 你的 Data 專案

namespace Project.Field.Dragon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class CatalogController : ControllerBase
    {
        // 2. 建立一個私有變數來存放資料庫內容
        private readonly StoreContext _db;

        // 3. 建立 Constructor (建構函式)
        // ASP.NET 會自動將 'StoreContext' (來自 Startup.cs) 傳入這裡
        public CatalogController(StoreContext db)
        {
            _db = db;
        }

        // 4. GetItems (取得所有產品) - 現在從資料庫讀取
        [HttpGet]
        public IActionResult GetItems()
        {
            // 我們不再回傳假資料，而是回傳資料庫中的 Items 表格
            return Ok(_db.Items);
        }

        // --- (我們暫時保留舊的假資料方法，稍後會一一更新) ---

        // GetItem (依 Id 取得單一產品)
        [HttpGet("{id:int}")]
        public ActionResult GetItem(int id)
        {
            var item = new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m);
            item.Id = id; 
            return Ok(item);
        }

        // Post (建立新產品)
        [HttpPost]
        public IActionResult Post(Item item)
        {
            return Created("/catalog/42", item); 
        }

        // PostRating (為產品新增評價)
        [HttpPost("{id:int}/ratings")]
        public ActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m);
            item.Id = id;
            item.AddRating(rating);
            return Ok(item);
        }

        // Put (更新產品)
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Item item)
        {
            return NoContent();
        }

        // Delete (刪除產品)
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}