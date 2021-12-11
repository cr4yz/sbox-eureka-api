using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EurekaApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace EurekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumItemsController : ControllerBase
    {
        private readonly ForumContext _context;

        public ForumItemsController(ForumContext context)
        {
            _context = context;
        }

        // GET: api/ForumItems
        [HttpGet]
        [ServiceFilter(typeof(AdminCheckActionFilter))]
        public async Task<ActionResult<IEnumerable<ForumItem>>> GetForumItems()
        {
            return await _context.ForumItems.ToListAsync();
        }

        // GET: api/ForumItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumItem>> GetForumItem(long id)
        {
            var forumItem = await _context.ForumItems.FindAsync(id);

            if (forumItem == null)
            {
                return NotFound();
            }

            return forumItem;
        }

        // PUT: api/ForumItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AdminCheckActionFilter))]
        public async Task<IActionResult> PutForumItem(long id, ForumItem forumItem)
        {
            if (id != forumItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(forumItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ForumItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ServiceFilter(typeof(AdminCheckActionFilter))]
        public async Task<ActionResult<ForumItem>> PostForumItem(ForumItem forumItem)
        {
            _context.ForumItems.Add(forumItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction( nameof(GetForumItem), new { id = forumItem.Id }, forumItem);
        }

        // DELETE: api/ForumItems/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AdminCheckActionFilter))]
        public async Task<IActionResult> DeleteForumItem(long id)
        {
            var forumItem = await _context.ForumItems.FindAsync(id);
            if (forumItem == null)
            {
                return NotFound();
            }

            _context.ForumItems.Remove(forumItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumItemExists(long id)
        {
            return _context.ForumItems.Any(e => e.Id == id);
        }
    }
}
