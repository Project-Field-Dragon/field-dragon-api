using Microsoft.AspNetCore.Mvc;
using Project.Field.Dragon.Domain.Catalog; 
using System.Collections.Generic;
using Project.Field.Dragon.Data; 
using Microsoft.EntityFrameworkCore; // 確保 'Include' 可以被識別

namespace Project.Field.Dragon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class CatalogController : ControllerBase
    {
        private readonly StoreContext _db;

        public CatalogController(StoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(_db.Items);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetItem(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return Created($"/catalog/{item.Id}", item);
        }

        [HttpPost("{id:int}/ratings")]
        public ActionResult PostRating(int id, [FromBody] Rating rating)
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

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var itemFromDb = _db.Items.Find(id);

            if (itemFromDb == null)
            {
                return NotFound();
            }

            itemFromDb.Name = item.Name;
            itemFromDb.Description = item.Description;
            itemFromDb.Brand = item.Brand;
            itemFromDb.Price = item.Price;

            _db.SaveChanges();

            return NoContent();
        }

        // 6. Delete (已修正 - 包含 Ratings)
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = _db.Items.Include(i => i.Ratings)
                .FirstOrDefault(i => i.Id == id);

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