using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AuctionWeb.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly AuctionDbContext _context;

        public AdminController(AuctionDbContext context)
        {
            _context = context;
        }

        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }

        //Hi?n th? danh sách ng??i dùng
        public async Task<IActionResult> UserAccounts()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            return View(await _context.UserAccounts.ToListAsync());
        }

        //Xóa UserAccount
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.UserAccounts.FindAsync(id);
            if (user != null)
            {
                _context.UserAccounts.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("UserAccounts");
        }

        //S?a UserAccount
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.UserAccounts.FindAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                _context.UserAccounts.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserAccounts");
            }
            return View(user);
        }
        // Display products
        public async Task<IActionResult> Products()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var products = _context.Products.Include(p => p.ProductCategory);
            return View(await products.ToListAsync());
        }

        // Delete product
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Products");
        }

        // Add product
        public IActionResult CreateProduct()
        {
            ViewBag.Categories = new SelectList(_context.ProductCategories, "Product_Cate_ID", "Product_Cate_Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // ??m b?o Seller_ID ???c set
                    product.Seller_ID = 1; // Ho?c l?y ID t? session n?u c?n

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Products");
                }
                // N?u ModelState không h?p l?, in ra các l?i ?? debug
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log l?i ?? debug
                System.Diagnostics.Debug.WriteLine(ex.Message);
                ModelState.AddModelError("", "Có l?i x?y ra khi thêm s?n ph?m");
            }

            // N?u có l?i, load l?i Categories và tr? v? view
            ViewBag.Categories = new SelectList(_context.ProductCategories, "Product_Cate_ID", "Product_Cate_Name");
            return View(product);
        }


        // GET: Admin/EditProduct/5
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_context.ProductCategories, "Product_Cate_ID", "Product_Cate_Name", product.Product_Cate_ID);
            return View(product);
        }

        // POST: Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.Product_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // ??m b?o Seller_ID ???c gi? nguyên
                    var existingProduct = await _context.Products.AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Product_ID == id);
                    if (existingProduct != null)
                    {
                        product.Seller_ID = existingProduct.Seller_ID;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Products));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Product_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Log l?i
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sản phẩm: " + ex.Message);
                }
            }

            // N?u có l?i, load l?i Categories và tr? v? view
            ViewBag.Categories = new SelectList(_context.ProductCategories, "Product_Cate_ID", "Product_Cate_Name", product.Product_Cate_ID);
            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Product_ID == id);
        }

        //Hi?n th? th? lo?i s?n ph?m
        public async Task<IActionResult> ProductCategories()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            return View(await _context.ProductCategories.ToListAsync());
        }

        //Xóa th? lo?i s?n ph?m
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category != null)
            {
                _context.ProductCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ProductCategories");
        }

        //Thêm th? lo?i s?n ph?m
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.ProductCategories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductCategories");
            }
            return View(category);
        }

        //S?a th? lo?i s?n ph?m
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.ProductCategories.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProductCategories");
            }
            return View(category);
        }
        //Hi?n th? các phiên ??u giá
        public async Task<IActionResult> Auctions()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var auctions = _context.Auctions.Include(a => a.Product);
            return View(await auctions.ToListAsync());
        }

        //Xóa phiên ??u giá
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction != null)
            {
                _context.Auctions.Remove(auction);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Auctions");
        }

        [HttpGet]
        public IActionResult CreateAuction()
        {
            ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuction([Bind("Auc_Item_ID,Aut_Start_Date,Aut_End_Date,Aut_Reserve_Price,Min_Bid_Increment")] Auction auction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Ki?m tra ngày
                    if (auction.Aut_End_Date <= auction.Aut_Start_Date)
                    {
                        ModelState.AddModelError("Aut_End_Date", "Ngày kết thúc phải sau ngày bắt đầu");
                        ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name", auction.Auc_Item_ID);
                        return View(auction);
                    }

                    // Ki?m tra s?n ph?m t?n t?i
                    var product = await _context.Products.FindAsync(auction.Auc_Item_ID);
                    if (product == null)
                    {
                        ModelState.AddModelError("Auc_Item_ID", "Sản phẩm không tồn tại");
                        ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name", auction.Auc_Item_ID);
                        return View(auction);
                    }

                    auction.Payment_Deadline = auction.Aut_End_Date.AddDays(7);

                    _context.Auctions.Add(auction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Auctions));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                    // Log the exception for debugging
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name", auction.Auc_Item_ID);
            return View(auction);
        }



        // S?a phiên ??u giá
        public async Task<IActionResult> EditAuction(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name", auction.Auc_Item_ID);
            return View(auction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuction(int id, [Bind("Auction_ID,Auc_Item_ID,Aut_Start_Date,Aut_End_Date,Aut_Reserve_Price,Min_Bid_Increment,Aut_Payment_Date,Aut_Winner_FullName,Aut_Winner_Amount")] Auction auction)
        {
            if (id != auction.Auction_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Ki?m tra ngày
                    if (auction.Aut_End_Date <= auction.Aut_Start_Date)
                    {
                        ModelState.AddModelError("Aut_End_Date", "Ngày kết thúc phải sau ngày bắt đầu");
                        ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name", auction.Auc_Item_ID);
                        return View(auction);
                    }

                    // Ki?m tra s?n ph?m t?n t?i
                    var product = await _context.Products.FindAsync(auction.Auc_Item_ID);
                    if (product == null)
                    {
                        ModelState.AddModelError("Auc_Item_ID", "Sản phẩm không tồn tại");
                        ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name", auction.Auc_Item_ID);
                        return View(auction);
                    }

                    _context.Update(auction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Auctions));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.Auction_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                    // Log the exception for debugging
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            ViewBag.Products = new SelectList(_context.Products, "Product_ID", "Product_Name", auction.Auc_Item_ID);
            return View(auction);
        }
        private bool AuctionExists(int id)
        {
            return _context.Auctions.Any(e => e.Auction_ID == id);
        }



    }



}
