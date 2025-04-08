using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionWeb.Models;
using System.Linq;

public class ProductController : Controller
{
    private readonly AuctionDbContext _context;

    public ProductController(AuctionDbContext context)
    {
        _context = context;
    }

    // Hiển thị sản phẩm với tìm kiếm và lọc category
    public async Task<IActionResult> Index(string search, int? categoryId)
    {
        ViewBag.Categories = await _context.ProductCategories.ToListAsync();

        var products = from p in _context.Products.Include(p => p.ProductCategory)
                       select p;

        if (!string.IsNullOrEmpty(search))
        {
            products = products.Where(p => p.Product_Name.Contains(search));
        }

        if (categoryId.HasValue && categoryId != 0)
        {
            products = products.Where(p => p.Product_Cate_ID == categoryId);
        }

        return View(await products.ToListAsync());
    }
}
