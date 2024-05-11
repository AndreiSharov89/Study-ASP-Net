using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcademicPerformanceLog_MVC.Data;
using AcademicPerformanceLog_MVC.Models;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace AcademicPerformanceLog_MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly PerformanceLogContext _context;

        public StudentsController(PerformanceLogContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Disciplines = _context.Disciplines;
            ViewBag.Performances = _context.Performances.Where(e => e.StudentID == id); ;

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Firstname,Lastname")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                try
                {
                    List<Performance> diary = Diary(student);
                    foreach (var d in diary)
                    {
                        _context.Performances.Add(d);
                    }
                    await _context.SaveChangesAsync();
                }
                catch 
                {
                    return View();
                }
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Firstname,Lastname")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            return View(student);
        }

        public async Task<IActionResult> EditScore(int id, string score)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            student.Score = score;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }

        private List<Performance> Diary(Student student)
        { 
            List<Performance> result = new List<Performance>();

            List<Discipline> disciplines = _context.Disciplines.ToList();

            foreach (var discipline in disciplines)
            {
                result.Add(new Performance
                {
                    StudentID = student.ID,
                    DisciplineID = discipline.ID,
                });
            }

            return result;
        }
        public ActionResult GoodStudents(int Id)
        {
            var allStudents = _context.Students.OrderBy(x => x.Score).ToList();
            var goodStudets = allStudents.TakeLast(5);
            return PartialView(goodStudets);
        }
        public ActionResult BadStudents(int Id)
        {
            var allStudents = _context.Students.OrderBy(x => x.Score).ToList();
            var badStudets = allStudents.Take(5);
            return PartialView(badStudets);
        }
    }
}
