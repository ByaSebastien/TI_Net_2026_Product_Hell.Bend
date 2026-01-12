using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TI_Net_2026_Product_Hell.Bend.Contexts;
using TI_Net_2026_Product_Hell.Bend.Entities;
using TI_Net_2026_Product_Hell.Bend.Models;

namespace TI_Net_2026_Product_Hell.Bend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductHellContext context) : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Product>> Index()
        {
            List<Product> products = context.Products.ToList();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProductFormDto form, [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            var imagesPath = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var fileExtension = Path.GetExtension(image.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";

            var filePath = Path.Combine(imagesPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            Product p = new Product
            {
                Name = form.Name,
                Description = form.Description,
                Price = form.Price,
                Image = $"/images/{fileName}",
            };

            context.Products.Add(p);

            context.SaveChanges();

            return Created();
        }
    }
}
