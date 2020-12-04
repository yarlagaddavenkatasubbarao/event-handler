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
    public class AdvertisersController : Controller
    {
        private readonly ISC567WebContext _context;

        public AdvertisersController(ISC567WebContext context)
        {
            _context = context;
        }

        // GET: Advertisers
        public async Task<IActionResult> Index()
        {
            var iSC567WebContext = _context.Advertisers.Include(a => a.Company);
            return View(await iSC567WebContext.ToListAsync());
        }

        // GET: Advertisers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertiser = await _context.Advertisers
                .Include(a => a.Company)
                .FirstOrDefaultAsync(m => m.AdvertiserID == id);
            if (advertiser == null)
            {
                return NotFound();
            }

            return View(advertiser);
        }

        // GET: Advertisers/Create
        public IActionResult Create()
        {
            PopulateCompanyDropDownList();
           // ViewData["CompanyID"] = new SelectList(_context.Companies, "CompanyID", "CompanyID");
            return View();
        }

        // POST: Advertisers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvertiserID,FirstName,LastName,PhoneNumber,EmailAddress,CompanyID")] Advertiser advertiser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertiser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["CompanyID"] = new SelectList(_context.Companies, "CompanyID", "CompanyID", advertiser.CompanyID);
            PopulateCompanyDropDownList(advertiser.CompanyID);
            return View(advertiser);
        }

        // GET: Companies/Details/5
        public async Task<Company> GetCompanyName(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            return company;
        }

        private void PopulateCompanyDropDownList(object selectedCompany = null)
        {
            var companyData = from d in _context.Companies
                                   orderby d.CompanyName
                                   select d;
            ViewBag.CompanyID = new SelectList(companyData, "CompanyID", "CompanyName", selectedCompany);
        }

        // GET: Advertisers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertiser = await _context.Advertisers.FindAsync(id);
            if (advertiser == null)
            {
                return NotFound();
            }

            ////var company = await GetCompanyName(advertiser.CompanyID);
            ////var companyName = company != null ? company.CompanyName : null;
            // ViewData["CompanyID"] = new SelectList(_context.Companies, "CompanyID", "CompanyID", advertiser.CompanyID);
            PopulateCompanyDropDownList(advertiser.CompanyID);
            return View(advertiser);
        }

        // POST: Advertisers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertiserID,FirstName,LastName,PhoneNumber,EmailAddress,CompanyID")] Advertiser advertiser)
        {
            if (id != advertiser.AdvertiserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertiser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertiserExists(advertiser.AdvertiserID))
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
            //ViewData["CompanyID"] = new SelectList(_context.Companies, "CompanyID", "CompanyID", advertiser.CompanyID);
            PopulateCompanyDropDownList(advertiser.CompanyID);
            return View(advertiser);
        }

        // GET: Advertisers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertiser = await _context.Advertisers
                .Include(a => a.Company)
                .FirstOrDefaultAsync(m => m.AdvertiserID == id);
            if (advertiser == null)
            {
                return NotFound();
            }
            PopulateCompanyDropDownList(advertiser.CompanyID);
            return View(advertiser);
        }

        // POST: Advertisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertiser = await _context.Advertisers.FindAsync(id);
            _context.Advertisers.Remove(advertiser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertiserExists(int id)
        {
            return _context.Advertisers.Any(e => e.AdvertiserID == id);
        }
    }
}
