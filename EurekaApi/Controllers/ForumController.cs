using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EurekaApi.Models;

namespace EurekaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly EurekaDbContext _context;

        public ForumController(EurekaDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // GET: api/ForumItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forum>>> GetForumItems()
        {
            return await _context.Forums.ToListAsync();
        }

        // GET: api/ForumItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Forum>> GetForumItem(long id)
        {
            var forumItem = await _context.Forums.FindAsync(id);

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
        public async Task<IActionResult> PutForumItem(long id, Forum forumItem)
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
        public async Task<ActionResult<Forum>> PostForumItem(Forum forumItem)
        {
            _context.Forums.Add(forumItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction( nameof(GetForumItem), new { id = forumItem.Id }, forumItem);
        }

        // DELETE: api/ForumItems/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AdminCheckActionFilter))]
        public async Task<IActionResult> DeleteForumItem(long id)
        {
            var forumItem = await _context.Forums.FindAsync(id);
            if (forumItem == null)
            {
                return NotFound();
            }

            _context.Forums.Remove(forumItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumItemExists(long id)
        {
            return _context.Forums.Any(e => e.Id == id);
        }
    }
}
