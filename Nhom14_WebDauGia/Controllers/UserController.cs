using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionWeb.Models;
using System.Threading.Tasks;
using System.Linq;


namespace AuctionWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly AuctionDbContext _context;

        public UserController(AuctionDbContext context)
        {
            _context = context;
        }

        // Trang Home cho user
        public async Task<IActionResult> UserHome()
        {
            var auctions = await _context.Auctions
                .Include(a => a.Product)
                .Where(a => a.Aut_End_Date > DateTime.Now)
                .ToListAsync();
            return View(auctions);
        }


        // Danh sách đã đấu giá
        public async Task<IActionResult> MyBids()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            var bids = await _context.AuctionBids
                .Include(b => b.Auction).ThenInclude(a => a.Product)
                .Where(b => b.User_ID == userId)
                .OrderByDescending(b => b.Bid_Time)
                .ToListAsync();

            return View(bids);
        }

        // Danh sách sản phẩm đã thắng
        public async Task<IActionResult> WonAuctions()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            var user = await _context.UserAccounts.FindAsync(userId);
            var auctions = await _context.Auctions
                .Include(a => a.Product)
                .Where(a => a.Aut_Winner_FullName == user.User_FullName)
                .ToListAsync();

            return View(auctions);
        }

        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _context.UserAccounts.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                _context.UserAccounts.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserHome");
            }
            return View(user);
        }

        public async Task<IActionResult> Pay(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.Product)
                .FirstOrDefaultAsync(a => a.Auction_ID == id);

            if (auction == null)
            {
                return NotFound();
            }

            var paymentMethod = new PaymentMethod
            {
                Pay_Method_Description = "Tiền Mặt",
                Auction_ID = id
            };

            ViewBag.PaymentMethods = paymentMethod;
            return View(auction);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(int id, int paymentMethodId)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            auction.IsPaid = true;
            auction.Aut_Payment_Date = DateTime.Now;

            var shipment = new Shipment
            {
                Shipment_Planned_Date = DateTime.Now.AddDays(3),
                Shipment_Item_ID = auction.Auc_Item_ID,
                Shipment_Fee = 50000
            };

            var paymentMethod = new PaymentMethod
            {
                Pay_Method_Description = "Tiền Mặt",
                Auction_ID = id
            };

            _context.PaymentMethods.Add(paymentMethod);
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Payment successful." });
        }

        public async Task<IActionResult> Shipments(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var shipments = await _context.Shipments
                .Include(s => s.Product)
                .Where(s => s.Product.Product_ID == id)
                .ToListAsync();

            if (shipments == null || !shipments.Any())
            {
                return NotFound();
            }

            return View(shipments);
        }
    }
}

