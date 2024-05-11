﻿using Azure.Core;
using lms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Linq;
using System.Text.Json;

namespace lms.Controllers
{
    public class CourseContentController : Controller
    {
        private readonly LmsDBContext _context;
        private UserManager<IdentityUser> _userManager;

        public CourseContentController(LmsDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var Userid = user?.Id;
            

            var course = await _context.Course
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            CourseEnrollement courseEnroll = new CourseEnrollement(course);

            var enrollement = await _context.Enrollment
                .Where(m => m.CourseId == id && m.UserId == Userid)
                .FirstOrDefaultAsync();

            if (enrollement == null)
            {
                courseEnroll.enrolled = false;
            }
            else
            {
                courseEnroll.enrolled = true;
                var materials = _context.Material
                    .Where(m => m.CourseId == id).ToList<Material>();
                courseEnroll.materials = materials;
                courseEnroll.currentStep = enrollement.step;
                courseEnroll.enrollementID = enrollement.Id;
                courseEnroll.stepLength = materials.Count();
            }
            return View(courseEnroll);
        }



        [HttpPost]
        public async Task<IActionResult> EditStep(int id,int step)
        {

            var enrollment = await _context.Enrollment.FindAsync(id);



            if (enrollment == null)
            {
                return NotFound();
            }

            // Update the step's state based on the isChecked parameter
            enrollment.step = step;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index), new { id = enrollment.CourseId });
                    }
                }
                return RedirectToAction(nameof(Index), new { id = enrollment.CourseId });
            }
            return RedirectToAction(nameof(Index), new { id = enrollment.CourseId });
        }

        private bool EnrollmentExists(int id)
        {
            return (_context.Enrollment?.Any(e => e.Id == id)).GetValueOrDefault();
        }

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

                bool exist = _context.Enrollment.Any(e => e.CourseId == id && e.UserId == Userid);
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
            return RedirectToAction(nameof(Index), new { id = id });
        }

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
                    if(entryToDelete != null)
                    {
                        _context.Enrollment.Remove(entryToDelete);
                    }
                    
                }
                else
                {
                    return Problem("Not Enrolled");
                }


            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = id });
        }
    }
}
