using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Model;
using DAL.Model.Tables;
using Microsoft.AspNetCore.Authorization;

namespace SellBoard.Controllers
{
    [Authorize]
    public class ApiSettingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiSettingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApiSetting
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_ApiSettings.ToListAsync());
        }

        // GET: ApiSetting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_ApiSetting = await _context.tbl_ApiSettings
                .FirstOrDefaultAsync(m => m.id == id);
            if (tbl_ApiSetting == null)
            {
                return NotFound();
            }

            return View(tbl_ApiSetting);
        }

        // GET: ApiSetting/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApiSetting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,BaseUrl,PostUrl,UserName,Password,ApiKey")] Tbl_ApiSetting tbl_ApiSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_ApiSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_ApiSetting);
        }

        // GET: ApiSetting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_ApiSetting = await _context.tbl_ApiSettings.FindAsync(id);
            if (tbl_ApiSetting == null)
            {
                return NotFound();
            }
            return View(tbl_ApiSetting);
        }

        // POST: ApiSetting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,BaseUrl,PostUrl,UserName,Password,ApiKey")] Tbl_ApiSetting tbl_ApiSetting)
        {
            if (id != tbl_ApiSetting.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_ApiSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tbl_ApiSettingExists(tbl_ApiSetting.id))
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
            return View(tbl_ApiSetting);
        }

        // GET: ApiSetting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_ApiSetting = await _context.tbl_ApiSettings
                .FirstOrDefaultAsync(m => m.id == id);
            if (tbl_ApiSetting == null)
            {
                return NotFound();
            }

            return View(tbl_ApiSetting);
        }

        // POST: ApiSetting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_ApiSetting = await _context.tbl_ApiSettings.FindAsync(id);
            _context.tbl_ApiSettings.Remove(tbl_ApiSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Tbl_ApiSettingExists(int id)
        {
            return _context.tbl_ApiSettings.Any(e => e.id == id);
        }
    }
}
