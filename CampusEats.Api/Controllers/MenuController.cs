using CampusEats.Api.Data;
using CampusEats.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusEats.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController(AppDbContext db, ILogger<MenuController> logger) : ControllerBase
{
    // Public endpoint used by the frontend and for deployment testing
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItem>>> GetAll()
    {
        var items = await db.MenuItems.Where(m => m.Available).OrderBy(m => m.Id).ToListAsync();
        logger.LogInformation("GET /api/menu returned {Count} items", items.Count);
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MenuItem>> GetById(int id)
    {
        var item = await db.MenuItems.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // Protected by JWT (auth from Lab 04)
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<MenuItem>> Create(MenuItem item)
    {
        db.MenuItems.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }
}
