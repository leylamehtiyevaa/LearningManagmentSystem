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
    public class InstructorCourseController : Controller
    {
        private readonly LmsDBContext _context;
        private readonly LmsDBContext _context2;
        private UserManager<IdentityUser> _userManager;

        public InstructorCourseController(LmsDBContext context, UserManager<IdentityUser> userManager, LmsDBContext context2)
        {
            _context = context;
            _userManager = userManager;
            _context2 = context2;
        }

        // GET: InstructorCourse
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            var lmsDBContext = _context.Course.Include(c => c.Category).Include(c => c.IdentityUser).Where(c => c.InstructorId == Userid);
            return View(await lmsDBContext.ToListAsync());
        }

        // GET: InstructorCourse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            var course = await _context.Course
                .Include(c => c.Category)
                .Include(c => c.IdentityUser)
                .Where(c => c.InstructorId == Userid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: InstructorCourse/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: InstructorCourse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,InstructorId, Author,imageURL")] Course course)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            course.InstructorId = Userid;

            if (ModelState.IsValid)
            {
                
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // GET: InstructorCourse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;

            var course = await _context.Course.FindAsync(id);
            if (course == null || course.InstructorId != Userid)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // POST: InstructorCourse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,InstructorId,Author,imageURL")] Course course)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            var course2 = await _context2.Course.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            course.InstructorId = Userid;
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(course2.InstructorId == Userid)
                    {
                        _context.Update(course);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // GET: InstructorCourse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;

            var course = await _context.Course
                .Include(c => c.Category)
                .Include(c => c.IdentityUser)
                .Where(c => c.InstructorId == Userid)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: InstructorCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            if (_context.Course == null)
            {
                return Problem("Entity set 'LmsDBContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                if(course.InstructorId == Userid)
                {
                    _context.Course.Remove(course);
                }
                else
                {
                    return NotFound();
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.Course?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
