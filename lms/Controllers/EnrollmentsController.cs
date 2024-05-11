using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lms.Models;
using Microsoft.AspNetCore.Identity;

namespace lms.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly LmsDBContext _context;
        private UserManager<IdentityUser> _userManager;

        public EnrollmentsController(LmsDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var Userid = user.Id;
            var enrollments = _context.Enrollment.Include(c => c.Course).Include(c => c.IdentityUser).Join(_context.Course,e => e.CourseId, o => o.Id, (e,o) => o);

            return View(enrollments);

        }

        private bool EnrollmentExists(int id)
        {
          return (_context.Enrollment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
