#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransparenciaFinanciera.Models;

namespace TransparenciaFinanciera.Controllers
{
    public class EgresosController : Controller
    {
        private readonly TransparenciaFinancieraContext _context;

        public EgresosController(TransparenciaFinancieraContext context)
        {
            _context = context;
        }

        // GET: Egresos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Egreso.ToListAsync());
        }

        // GET: Egresos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egreso = await _context.Egreso
                .FirstOrDefaultAsync(m => m.Id == id);
            if (egreso == null)
            {
                return NotFound();
            }

            return View(egreso);
        }

        // GET: Egresos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Egresos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha")] Egreso egreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(egreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(egreso);
        }

        // GET: Egresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egreso = await _context.Egreso.FindAsync(id);
            if (egreso == null)
            {
                return NotFound();
            }
            return View(egreso);
        }

        // POST: Egresos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha")] Egreso egreso)
        {
            if (id != egreso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(egreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EgresoExists(egreso.Id))
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
            return View(egreso);
        }

        // GET: Egresos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egreso = await _context.Egreso
                .FirstOrDefaultAsync(m => m.Id == id);
            if (egreso == null)
            {
                return NotFound();
            }

            return View(egreso);
        }

        // POST: Egresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var egreso = await _context.Egreso.FindAsync(id);
            _context.Egreso.Remove(egreso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EgresoExists(int id)
        {
            return _context.Egreso.Any(e => e.Id == id);
        }
    }
}
