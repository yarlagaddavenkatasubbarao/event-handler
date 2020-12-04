using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISC567Web.DAL;
using ISC567Web.Models;

namespace ISC567Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly ISC567WebContext _context;

        public EventsController(ISC567WebContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var iSC567WebContext = _context.Events.Include(m => m.Advertiser);
            return View(await iSC567WebContext.ToListAsync());
        }

        // GET: Events
        [Route("EventCategories/Data/{id}")]
        public async Task<IActionResult> IndexCustomized(int id)
        {
            var iSC567WebContext = _context.Events.Include(m => m.Advertiser);
            var data = await iSC567WebContext.ToListAsync();
            return View(data.Where(x => x.EventCategoryID == id));
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(m => m.Advertiser)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            //ViewData["AdvertiserID"] = new SelectList(_context.Advertisers, "AdvertiserID", "AdvertiserID");
            PopulateCompanyDropDownList();
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,EventName,EventLocation,Date,Description,EventLogo,Promotional,EventCategoryID,AdvertiserID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexCustomized", "Events", new { id = @event.EventCategoryID });
            }
            //ViewData["AdvertiserID"] = new SelectList(_context.Advertisers, "AdvertiserID", "AdvertiserID", @event.AdvertiserID);
            PopulateCompanyDropDownList(@event.AdvertiserID);
            return View(@event);
        }

        private void PopulateCompanyDropDownList(object selectedCompany = null)
        {
            var companyData = from d in _context.Companies
                              orderby d.CompanyName
                              select d;
            ViewBag.CompanyID = new SelectList(companyData, "CompanyID", "CompanyName", selectedCompany);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["AdvertiserID"] = new SelectList(_context.Advertisers, "AdvertiserID", "AdvertiserID", @event.AdvertiserID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,EventName,EventLocation,Date,Description,EventLogo,Promotional,EventCategoryID,AdvertiserID")] Event @event)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //if (@event.EventCategoryID.HasValue)
                //{
                //    int CategoryID = @event.EventCategoryID.Value;
                //    return RedirectToAction("IndexCustomized","Events", new { id = CategoryID });
                //}
                //else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["AdvertiserID"] = new SelectList(_context.Advertisers, "AdvertiserID", "AdvertiserID", @event.AdvertiserID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(m => m.Advertiser)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
}
