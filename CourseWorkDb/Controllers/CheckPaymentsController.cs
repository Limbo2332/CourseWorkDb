using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWorkDb.Models;

namespace CourseWorkDb.Controllers
{
    public class CheckPaymentsController : Controller
    {
        private readonly CourseWorkContext _context;

        public CheckPaymentsController(CourseWorkContext context)
        {
            _context = context;
        }

        // GET: CheckPayments
        public async Task<IActionResult> Index()
        {
            var courseWorkContext = _context.CheckPayments.Include(c => c.Apartment).Include(c => c.Client);
            return View(await courseWorkContext.ToListAsync());
        }

        // GET: CheckPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CheckPayments == null)
            {
                return NotFound();
            }

            var checkPayment = await _context.CheckPayments
                .Include(c => c.Apartment)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.CheckPaymentId == id);
            if (checkPayment == null)
            {
                return NotFound();
            }

            return View(checkPayment);
        }

        // GET: CheckPayments/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId");
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            return View();
        }

        // POST: CheckPayments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckPaymentId,DateOfCreation,DateOfSettlement,DateOfEviction,CheckState,TypeOfPladge,ClientId,ApartmentId")] CheckPayment checkPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", checkPayment.ApartmentId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", checkPayment.ClientId);
            return View(checkPayment);
        }

        // GET: CheckPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CheckPayments == null)
            {
                return NotFound();
            }

            var checkPayment = await _context.CheckPayments.FindAsync(id);
            if (checkPayment == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", checkPayment.ApartmentId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", checkPayment.ClientId);
            return View(checkPayment);
        }

        // POST: CheckPayments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckPaymentId,DateOfCreation,DateOfSettlement,DateOfEviction,CheckState,TypeOfPladge,ClientId,ApartmentId")] CheckPayment checkPayment)
        {
            if (id != checkPayment.CheckPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckPaymentExists(checkPayment.CheckPaymentId))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "ApartmentId", "ApartmentId", checkPayment.ApartmentId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", checkPayment.ClientId);
            return View(checkPayment);
        }

        // GET: CheckPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CheckPayments == null)
            {
                return NotFound();
            }

            var checkPayment = await _context.CheckPayments
                .Include(c => c.Apartment)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.CheckPaymentId == id);
            if (checkPayment == null)
            {
                return NotFound();
            }

            return View(checkPayment);
        }

        // POST: CheckPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CheckPayments == null)
            {
                return Problem("Entity set 'CourseWorkContext.CheckPayments'  is null.");
            }
            var checkPayment = await _context.CheckPayments.FindAsync(id);
            if (checkPayment != null)
            {
                _context.CheckPayments.Remove(checkPayment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckPaymentExists(int id)
        {
          return (_context.CheckPayments?.Any(e => e.CheckPaymentId == id)).GetValueOrDefault();
        }
    }
}
