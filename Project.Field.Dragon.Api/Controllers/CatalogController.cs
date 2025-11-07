using Microsoft.AspNetCore.Mvc;
using Project.Field.Dragon.Domain.Catalog; 
using System.Collections.Generic;

namespace Project.Field.Dragon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class CatalogController : ControllerBase
    {
        // 1. GetItems (取得所有產品)
        [HttpGet]
        public IActionResult GetItems()
        {
            var items = new List<Item>()
            {
                new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m),
                new Item("Shorts", "Ohio State shorts.", "Nike", 44.99m)
            };
            return Ok(items);
        }

        // 2. GetItem (依 Id 取得單一產品)
        [HttpGet("{id:int}")]
        public ActionResult GetItem(int id)
        {
            var item = new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m);
            item.Id = id; 
            return Ok(item);
        }

        // 3. Post (建立新產品)
        [HttpPost]
        public IActionResult Post(Item item)
        {
            return Created("/catalog/42", item); 
        }

        // 4. PostRating (為產品新增評價)
        [HttpPost("{id:int}/ratings")]
        public ActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m);
            item.Id = id;
            item.AddRating(rating);
            return Ok(item);
        }

        // 5. Put (更新產品) (Lab 4 第 14 頁)
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Item item)
        {
            return NoContent();
        }

        // 6. Delete (刪除產品) (Lab 4 第 15 頁)
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}