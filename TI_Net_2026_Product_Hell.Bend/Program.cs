using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TI_Net_2026_Product_Hell.Bend.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c => c.AddDefaultPolicy(p => 
    p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
));

builder.Services.AddDbContext<ProductHellContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activer les fichiers statiques (wwwroot par défaut)
app.UseStaticFiles();

// Servir un dossier personnalisé, par exemple "Images" à la racine du projet
var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
if (!Directory.Exists(imagePath))
{
    Directory.CreateDirectory(imagePath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagePath),
    RequestPath = "/images"
});

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
