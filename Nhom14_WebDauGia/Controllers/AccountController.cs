using Microsoft.AspNetCore.Mvc;
using AuctionWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


public class AccountController : Controller
{
    private readonly AuctionDbContext _context;

    public AccountController(AuctionDbContext context)
    {
        _context = context;
    }

    // Hiển thị form đăng ký
    public IActionResult Register()
    {
        return View();
    }

    // Xử lý form đăng ký
    [HttpPost]
    public async Task<IActionResult> Register(UserAccount user)
    {
        if (ModelState.IsValid)
        {
            var check = await _context.UserAccounts.FirstOrDefaultAsync(u => u.User_Email == user.User_Email);
            if (check == null)
            {
                user.User_Admin_ID = 1;
                user.Password = HashPassword(user.Password);
                _context.UserAccounts.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Email đã tồn tại");
        }
        return View(user);
    }

    private string HashPassword(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }

    // Hiển thị form đăng nhập
    public IActionResult Login()
    {
        return View();
    }

    // Xử lý đăng nhập
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Tìm trong User trước
        var user = await _context.UserAccounts.SingleOrDefaultAsync(u => u.User_Email == email);
        if (user != null && VerifyPassword(user.Password, password))
        {
            HttpContext.Session.SetString("UserRole", "User");
            HttpContext.Session.SetInt32("UserID", user.User_ID);
            return RedirectToAction("UserHome", "User");
        }

        // Tìm trong Admin
        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Admin_Email == email && a.Admin_Phone.ToString() == password);
        if (admin != null)
        {
            HttpContext.Session.SetString("UserRole", "Admin");
            HttpContext.Session.SetInt32("AdminID", admin.Admin_ID);
            return RedirectToAction("AdminDashboard", "Admin");
        }

        ViewBag.Error = "Sai email hoặc mật khẩu";
        return View();
    }

    private bool VerifyPassword(string hashedPassword, string password)
    {
        var parts = hashedPassword.Split('.');
        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = parts[1];

        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hash == storedHash;
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
