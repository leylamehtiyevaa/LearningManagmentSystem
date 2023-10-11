using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace lms.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorMaterialController : Controller
    {
        private readonly LmsDBContext _context;
        private readonly LmsDBContext _context2;
        private UserManager<IdentityUser> _userManager;

        public InstructorMaterialController(LmsDBContext context, UserManager<IdentityUser> userManager, LmsDBContext context2)
        {
            _context = context;
            _userManager = userManager;
            _context2 = context2;
        }

        // GET: InstructorMaterial
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            var lmsDBContext = _context.Material.Include(m => m.Course).Include(m => m.IdentityUser).Where(c => c.InstructorId == Userid); ;
            return View(await lmsDBContext.ToListAsync());
        }

        // GET: InstructorMaterial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            var material = await _context.Material
                .Include(m => m.Course)
                .Include(m => m.IdentityUser)
                .Where(c => c.InstructorId == Userid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: InstructorMaterial/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            return View();
        }

        // POST: InstructorMaterial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,VideoURL,ImageURL,Author,CourseId,InstructorId")] Material material)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            material.InstructorId = Userid;

            var course2 = _context2.Course
                .Where(c => c.InstructorId == Userid).ToList<Course>();

            var flag = false;
            foreach (Course course in course2)
            //(int i = 0; i < students.Count; i++)
            {
                if(material.CourseId == course.Id)
                {
                    flag = false;
                    break;
                }
                flag = true;
            }

            if(flag == true)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
               
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course.Where(c => c.InstructorId == Userid), "Id", "Name", material.CourseId);
            return View(material);
        }

        // GET: InstructorMaterial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
 
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }

            var material = await _context.Material.FindAsync(id);
            if (material == null || material.InstructorId != Userid)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", material.CourseId);
            return View(material);
        }

        // POST: InstructorMaterial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,VideoURL,ImageURL,Author,CourseId,InstructorId")] Material material)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            var material2 = await _context2.Material.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            material.InstructorId = Userid;
            if (id != material.Id || material2.InstructorId != Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", material.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Users, "Id", "Id", material.InstructorId);
            return View(material);
        }

        // GET: InstructorMaterial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            if (id == null || _context.Material == null)
            {
                return NotFound();
            }

            var material = await _context.Material
                .Include(m => m.Course)
                .Include(m => m.IdentityUser)
                .Where(c => c.InstructorId == Userid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: InstructorMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            if (_context.Material == null)
            {
                return Problem("Entity set 'LmsDBContext.Material'  is null.");
            }
            var material = await _context.Material.FindAsync(id);
            if (material != null)
            {
                if (material.InstructorId == Userid)
                {
                    _context.Material.Remove(material);
                }
                else
                {
                    return NotFound();
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
          return (_context.Material?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
