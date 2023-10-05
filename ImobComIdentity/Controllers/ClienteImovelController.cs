using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImobComIdentity.Data;
using ImobComIdentity.Models;
using Microsoft.AspNetCore.Authorization;

namespace ImobComIdentity.Controllers
{
    [Authorize]
    public class ClienteImovelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteImovelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClienteImovel
        public async Task<IActionResult> Index()
        {
              return _context.CLienteImoveis != null ? 
                          View(await _context.CLienteImoveis.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CLienteImoveis'  is null.");
        }

        // GET: ClienteImovel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CLienteImoveis == null)
            {
                return NotFound();
            }

            var clienteImovel = await _context.CLienteImoveis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteImovel == null)
            {
                return NotFound();
            }

            return View(clienteImovel);
        }

        // GET: ClienteImovel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteImovel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cliente,Email,Celular,Imovel,Referencia,Valor,Permuta,CriadoEm")] ClienteImovel clienteImovel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteImovel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteImovel);
        }

        // GET: ClienteImovel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CLienteImoveis == null)
            {
                return NotFound();
            }

            var clienteImovel = await _context.CLienteImoveis.FindAsync(id);
            if (clienteImovel == null)
            {
                return NotFound();
            }
            return View(clienteImovel);
        }

        // POST: ClienteImovel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cliente,Email,Celular,Imovel,Referencia,Valor,Permuta,CriadoEm")] ClienteImovel clienteImovel)
        {
            if (id != clienteImovel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteImovel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteImovelExists(clienteImovel.Id))
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
            return View(clienteImovel);
        }

        // GET: ClienteImovel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CLienteImoveis == null)
            {
                return NotFound();
            }

            var clienteImovel = await _context.CLienteImoveis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteImovel == null)
            {
                return NotFound();
            }

            return View(clienteImovel);
        }

        // POST: ClienteImovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CLienteImoveis == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CLienteImoveis'  is null.");
            }
            var clienteImovel = await _context.CLienteImoveis.FindAsync(id);
            if (clienteImovel != null)
            {
                _context.CLienteImoveis.Remove(clienteImovel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteImovelExists(int id)
        {
          return (_context.CLienteImoveis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
