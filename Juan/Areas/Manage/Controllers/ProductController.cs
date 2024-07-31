using Juan.Data;
using Juan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Juan.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly JuanDbContext _context;

        public ProductController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products =await _context.Products
                .AsNoTracking()
                .Include(p=>p.ProductCategories)
                .Include(p=>p.ProductColors)
                .Include(p => p.ProductSizes)
                .Where(p=> !p.IsDelete)
                .ToListAsync();
      
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories
                .Where(p => !p.IsDelete)
                .ToListAsync();

            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Product product, int[] selectedCategories)
        {
            ViewBag.Categories = await _context.Categories
                .Where(p => !p.IsDelete)
                .ToListAsync();

            if (ModelState.IsValid)
            {
                return View(product);
            }

            var imageFile = product.ImageFile;
            if (imageFile == null)
            {
                ModelState.AddModelError("", "Photo is required.");
                return View(product);
            }

            if (!imageFile.IsImage())
            {
                ModelState.AddModelError("", "Invalid photo format.");
                return View(product);
            }

            if (!imageFile.IsCorrectSize(5120)) // 5 MB = 5120 KB
            {
                ModelState.AddModelError("", "Photo size too large. Maximum allowed size is 5 MB.");
                return View(product);
            }

            string savedImagePath;
            try
            {
                savedImagePath = await imageFile.SaveFileAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving the photo: {ex.Message}");
                return View(product);
            }

            // Ürünü oluştur
            var newProduct = new Product
            {
                ProductName = product.ProductName.Trim(),
                Desc = product.Desc,
                CreatedDate = DateTime.Now,
                Img = savedImagePath,
                // Diğer alanları doldur
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync(); // Ürünü veritabanına kaydet

            // Ürün ID'sini al
            var productId = newProduct.ProductId;

            // Kategorileri ekle
            if (selectedCategories != null)
            {
                var productCategories = selectedCategories.Select(categoryId => new ProductCategory
                {
                    ProductId = productId,
                    CategoryId = categoryId
                }).ToList();

                await _context.ProductCategories.AddRangeAsync(productCategories);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }



[HttpGet]
public async Task<IActionResult> Update(int id)
{
    var product = await _context.Products
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.ProductId == id);

    if (product == null)
    {
        return NotFound();
    }

    return View(product);
}


[HttpPost]
[AutoValidateAntiforgeryToken]
public async Task<IActionResult> Update(Product updatedProduct)
{
    if (!ModelState.IsValid)
    {
        return View(updatedProduct);
    }

    var product = await _context.Products
        .FirstOrDefaultAsync(p => p.ProductId == updatedProduct.ProductId);

    if (product == null)
    {
        return NotFound();
    }

    product.ProductName = updatedProduct.ProductName.Trim();
    product.Desc = updatedProduct.Desc;
    product.Price = updatedProduct.Price;
    product.OldPrice = updatedProduct.OldPrice;
    product.IsDelete = updatedProduct.IsDelete;


    var imageFile = updatedProduct.ImageFile;
    if (imageFile != null)
    {
        if (!imageFile.IsImage())
        {
            ModelState.AddModelError("", "Invalid photo format.");
            return View(updatedProduct);
        }

        if (!imageFile.IsCorrectSize(5120)) // 5 MB = 5120 KB
        {
            ModelState.AddModelError("", "Photo size too large. Maximum allowed size is 5 MB.");
            return View(updatedProduct);
        }

        string savedImagePath;
        try
        {
            savedImagePath = await imageFile.SaveFileAsync();
            product.Img = savedImagePath;
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error saving the photo: {ex.Message}");
            return View(updatedProduct);
        }
    }

   
    _context.Products.Update(product);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

    }
}
