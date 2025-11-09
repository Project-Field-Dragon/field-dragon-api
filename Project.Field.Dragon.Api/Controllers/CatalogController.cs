using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Field.Dragon.Domain.Catalog;
using Project.Field.Dragon.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Field.Dragon.Api.Controllers
{
    // 路由屬性，讓它映射到 /catalog
    [ApiController]
    [Route("[controller]")] 
    public class CatalogController : ControllerBase
    {
        // 依賴注入 (Dependency Injection)
        private readonly StoreContext _db;

        public CatalogController(StoreContext db)
        {
            _db = db;
        }

        // 1. GET: /catalog (獲取所有 Item)
        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(_db.Items);
        }

        // 2. GET: /catalog/{id} (根據 ID 獲取單個 Item)
        [HttpGet("{id:int}")]
        public ActionResult GetItem(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // 3. POST: /catalog (新增 Item)
        [HttpPost]
        public IActionResult Post(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return Created($"/catalog/{item.Id}", item);
        }

        // 4. POST: /catalog/{id}/ratings (新增 Rating)
        [HttpPost("{id:int}/ratings")]
        public IActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.AddRating(rating);
            _db.SaveChanges();
            return Ok(item);
        }
        
        // 5. PUT: /catalog/{id} (更新 Item)
        [HttpPut("{id:int}")]
        public ActionResult PutItem(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (_db.Items.Find(id) == null)
            {
                return NotFound();
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return NoContent();
        }
        
        // 6. DELETE: /catalog/{id} (刪除 Item)
        [HttpDelete("{id:int}")]
        public ActionResult DeleteItem(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            
            _db.Items.Remove(item);
            _db.SaveChanges();
            return Ok();
        }
    }
}