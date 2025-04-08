using AuctionWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

public class AuctionController : Controller
{
    private readonly AuctionDbContext _context;

    public AuctionController(AuctionDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Details(int id)
    {
        var auction = await _context.Auctions
            .Include(a => a.Product)
            .FirstOrDefaultAsync(a => a.Auction_ID == id);

        if (auction == null)
        {
            return NotFound();
        }

        var bids = await _context.AuctionBids
            .Include(b => b.UserAccount)
            .Where(b => b.Auction_ID == id)
            .OrderByDescending(b => b.Bid_Amount)
            .ToListAsync();

        var highestBid = bids.FirstOrDefault()?.Bid_Amount ?? 0;
        var highestBidder = bids.FirstOrDefault()?.UserAccount?.User_FullName;
        var bidTime = bids.FirstOrDefault()?.Bid_Time;

        ViewBag.Bids = bids;
        ViewBag.HighestBid = highestBid;
        ViewBag.HighestBidder = highestBidder;
        ViewBag.BidTime = bidTime;

        bool isAuctionClosed = auction.Aut_End_Date <= DateTime.Now;
        ViewBag.IsAuctionClosed = isAuctionClosed;

        return View(auction);
    }

    [HttpPost]
    public IActionResult PlaceBid(int auctionId, decimal bidAmount)
    {
        var userId = HttpContext.Session.GetInt32("UserID");
        var auction = _context.Auctions.Find(auctionId);
        if (auction == null || auction.Aut_End_Date <= DateTime.Now)
        {
            return Json(new { success = false, message = "Phiên đấu giá không tồn tại hoặc đã kết thúc." });
        }

        if (bidAmount < auction.Aut_Reserve_Price)
        {
            return Json(new { success = false, message = "Giá đấu phải cao hơn giá khởi điểm." });
        }

        if (bidAmount <= auction.Aut_Reserve_Price + auction.Min_Bid_Increment)
        {
            return Json(new { success = false, message = $"Giá đấu phải cao tối thiểu {auction.Min_Bid_Increment.ToString("N0")} VNĐ so với giá khởi điểm" });
        }

        var highestBid = _context.AuctionBids
            .Where(b => b.Auction_ID == auctionId)
            .OrderByDescending(b => b.Bid_Amount)
            .FirstOrDefault();

        if (highestBid != null && bidAmount <= highestBid.Bid_Amount)
        {
            return Json(new { success = false, message = "Giá đấu phải cao hơn giá cao nhất hiện tại." });
        }

        if (highestBid != null && bidAmount <= highestBid.Bid_Amount + auction.Min_Bid_Increment)
        {
            return Json(new { success = false, message = $"Giá đấu phải cao tối thiểu {auction.Min_Bid_Increment.ToString("N0")} VNĐ so với giá hiện tại" });
        }

        var newBid = new AuctionBid
        {
            Auction_ID = auctionId,
            User_ID = userId.Value,
            Bid_Amount = bidAmount,
            Bid_Time = DateTime.Now
        };

        auction.Aut_Winner_FullName = _context.UserAccounts.Find(userId)?.User_FullName;
        auction.Aut_Winner_Amount = bidAmount;

        _context.AuctionBids.Add(newBid);
        _context.SaveChanges();

        return Json(new { success = true, message = "Đấu giá thành công." });
    }

    public async Task<IActionResult> ClosedAuctions()
    {
        var closedAuctions = await _context.Auctions
            .Where(a => a.Aut_End_Date <= DateTime.Now)
            .Include(a => a.Product)
            .ToListAsync();

        return View(closedAuctions);
    }
}
