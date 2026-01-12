using Microsoft.AspNetCore.Mvc;

namespace TI_Net_2026_Product_Hell.Bend.Models
{
    public record PostProductDto(
         [FromForm(Name = "form")]
         string Form,
         [FromForm(Name = "image")]
         IFormFile Image
        );
}
