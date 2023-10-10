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
    public class CoursesController : Controller
    {
        private readonly LmsDBContext _context;
        private UserManager<IdentityUser> _userManager;


        public CoursesController(LmsDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var lmsDBContext = _context.Course.Include(c => c.Category);
            return View(await lmsDBContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            bool exist = _context.Enrollment.Any(e => e.CourseId == id && e.UserId == Userid);
            

            var course = await _context.Course
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            CourseEnrollmentViewModel courseEnrollmentViewModel = new CourseEnrollmentViewModel();
            courseEnrollmentViewModel.course = course;
            courseEnrollmentViewModel.isEnrolled = exist;
            

            return View(courseEnrollmentViewModel);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,Author,imageURL")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,Author,imageURL")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
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

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'LmsDBContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


















































        // GET: Courses/Enroll/5
        public async Task<IActionResult> Enroll(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Enroll/5
        [HttpPost, ActionName("Enroll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnrollConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'LmsDBContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                Enrollment enrollment = new Enrollment();
                enrollment.CourseId = id;

                var user = await _userManager.GetUserAsync(User);
                var Userid = user.Id;
                enrollment.UserId = Userid;

                bool exist = _context.Enrollment.Any(e => e.CourseId == id && e.UserId == Userid );
                if (!exist)
                {
                    _context.Enrollment.Add(enrollment);
                }
                else
                {
                    return Problem("Already Enrolled");
                }
                
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // POST: Courses/UnEnroll/5
        [HttpPost, ActionName("UnEnroll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnEnroll(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'LmsDBContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                Enrollment enrollment = new Enrollment();
                enrollment.CourseId = id;

                var user = await _userManager.GetUserAsync(User);
                var Userid = user.Id;
                enrollment.UserId = Userid;


                bool exist = _context.Enrollment.Any(e => e.CourseId == id && e.UserId == Userid);
                if (exist)
                {
                    // Find the entry
                    var entryToDelete = await _context.Enrollment.FirstOrDefaultAsync(e => e.CourseId == id && e.UserId == Userid);
                    _context.Enrollment.Remove(entryToDelete);
                }
                else
                {
                    return Problem("Not Enrolled");
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
