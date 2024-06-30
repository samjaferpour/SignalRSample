using Backend.Contexts;
using Backend.Dtos;
using Backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SignalRDbContexxt _dbContexxt;

        public CategoryController(SignalRDbContexxt dbContexxt)
        {
            _dbContexxt = dbContexxt;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var response = await _dbContexxt.Categories.ToListAsync();
            return Ok(response.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
            }));
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllChildren(Guid categoryId)
        {
            var response = await _dbContexxt.Categories
                                            .Where(x => x.Id == categoryId)
                                            .FirstOrDefaultAsync();
            
            var result = response.Products.ToList();

            return Ok(result.Select(x => new ProductDto
            {
                Id= x.Id,
                Name= x.Name,
                CategoryId= categoryId,
                Price = x.Price,
            }));
        }
        [HttpPost]
        public async Task<IActionResult> AddNew(CategoryDto request)
        {
            var category = new Category
            {
                Name = request.Name,
            };

            _dbContexxt.Categories.Add(category);
            await _dbContexxt.SaveChangesAsync();

            return Ok(category.Id);
        }
    }
}
