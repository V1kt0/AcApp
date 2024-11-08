using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcApp.Data;
using AcApp.Models;

namespace AcApp.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderItems.Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItems
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItems == null)
            {
                return NotFound();
            }

            return View(orderItems);
        }

        // GET: OrderItems/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,Price,OrderId,ProductID,Id")] OrderItems orderItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Id", orderItems.ProductID);
            return View(orderItems);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItems.FindAsync(id);
            if (orderItems == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Id", orderItems.ProductID);
            return View(orderItems);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Quantity,Price,OrderId,ProductID,Id")] OrderItems orderItems)
        {
            if (id != orderItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemsExists(orderItems.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Id", orderItems.ProductID);
            return View(orderItems);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItems
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItems == null)
            {
                return NotFound();
            }

            return View(orderItems);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderItems'  is null.");
            }
            var orderItems = await _context.OrderItems.FindAsync(id);
            if (orderItems != null)
            {
                _context.OrderItems.Remove(orderItems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemsExists(int id)
        {
          return (_context.OrderItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
