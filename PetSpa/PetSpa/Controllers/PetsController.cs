using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpa.DAL;
using PetSpa.DAL.Entities;
using PetSpa.Helpers;

namespace PetSpa.Controllers
{
    public class PetsController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;
        private readonly IUserHelper _userHelper;

        public PetsController(DataBaseContext context, IDropDownListHelper dropDownListHelper, IUserHelper userHelper)
        {
            _context = context;
            _dropDownListHelper = dropDownListHelper;
            _userHelper = userHelper;

        }

        private string GetUserId()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.Id.ToString())
                .FirstOrDefault();
        }

        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }

       


        public async Task<IActionResult> Index()
        {
            ViewBag.UserFullName = GetUserFullName();

            
            ViewBag.UserId = GetUserId();


            return View(await _context.Pets
                .Include(o => o.Owner)
                .Include(o => o.Service)
                .ToListAsync());
            Problem("Entity set 'DataBaseContext.UserTrainings'  is null.");

        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Pets == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PetDetailsId,Id,CreatedDate,ModifiedDate")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                pet.Id = Guid.NewGuid();
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Pets == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,PetDetailsId,Id,CreatedDate,ModifiedDate")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
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
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Pets == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Pets == null)
            {
                return Problem("Entity set 'DataBaseContext.Pets'  is null.");
            }
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(Guid id)
        {
          return (_context.Pets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
