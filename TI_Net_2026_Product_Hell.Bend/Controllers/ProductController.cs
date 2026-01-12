using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TI_Net_2026_Product_Hell.Bend.Contexts;
using TI_Net_2026_Product_Hell.Bend.Entities;
using TI_Net_2026_Product_Hell.Bend.Models;

namespace TI_Net_2026_Product_Hell.Bend.Controllers
{
    [Route("[controller]")]
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
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Post([FromForm] PostProductDto dto)
        {
            IFormFile image = dto.Image;

            if (image == null || image.Length == 0 || dto.Form == null)
                return BadRequest();

            Console.WriteLine(image.ContentType);

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

            ProductFormDto form = JsonSerializer.Deserialize<ProductFormDto>(
                                    dto.Form,
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    })!;

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
