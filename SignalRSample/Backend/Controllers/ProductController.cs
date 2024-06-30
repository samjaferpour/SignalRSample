using Backend.Contexts;
using Backend.Dtos;
using Backend.Entities;
using Backend.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SignalRDbContexxt _dbContexxt;
        private readonly IHubContext<SignalRHub> _hubContext;

        public ProductController(SignalRDbContexxt dbContexxt,
                                 IHubContext<SignalRHub> hubContext)
        {
            _dbContexxt = dbContexxt;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var response = await _dbContexxt.Products.ToListAsync();
            return Ok(response.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Price = x.Price,
            }));
        }
        [HttpPost]
        public async Task<IActionResult> AddNew(ProductDto request)
        {
            var product = new Product
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price,
            };

            _dbContexxt.Products.Add(product);
            await _dbContexxt.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "one product added succssfully! ");
            await _hubContext.Clients.All.SendAsync("ReceiveProductAdd", request);

            return Ok(product.Id);
        }
    }
}
