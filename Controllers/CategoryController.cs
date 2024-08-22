using DotnetStockAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DotnetStockAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[EnableCors("MultipleOrigins")]
public class CategoryController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<category> GetCategories()
    {
        var categories = _context.categories.ToList(); // select * from category

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public ActionResult<category> GetCategory(int id)
    {
        // LINQ สำหรับการดึงข้อมูลจากตาราง Categories ตาม ID
        var category = _context.categories.Find(id); // select * from category where id = 1

        if(category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult<category> AddCategory([FromBody] category category)
    {
        _context.categories.Add(category); // insert into category values (...)
        _context.SaveChanges(); // commit

        return Ok(category);
    }

    [HttpPut("{id}")]
    public ActionResult<category> UpdateCategory(int id, [FromBody] category category)
    {
        var cat = _context.categories.Find(id); // select * from category where id = 1

        if(cat == null)
        {
            return NotFound();
        }

        cat.categoryname = category.categoryname; 
        cat.categorystatus = category.categorystatus;

        _context.SaveChanges();

        return Ok(cat);
    }


    [HttpDelete("{id}")]
    public ActionResult<category> DeleteCategory(int id)
    {
        var cat = _context.categories.Find(id);

        if(cat == null)
        {
            return NotFound();
        }

        _context.categories.Remove(cat); 
        _context.SaveChanges(); 

        return Ok(cat);
    }

}