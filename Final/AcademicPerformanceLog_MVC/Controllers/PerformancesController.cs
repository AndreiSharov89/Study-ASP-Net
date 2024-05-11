using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcademicPerformanceLog_MVC.Data;
using AcademicPerformanceLog_MVC.Models;
using System.Text;
using ClosedXML.Excel;
using Microsoft.JSInterop;
using System.Text.Unicode;
using DocumentFormat.OpenXml.Drawing;



namespace AcademicPerformanceLog_MVC.Controllers
{
    public class PerformancesController : Controller
    {
        private readonly PerformanceLogContext _context;

        public PerformancesController(PerformanceLogContext context)
        {
            _context = context;
        }

        // GET: Performances
        public async Task<IActionResult> Index()
        {
            ViewBag.Students = students();
            ViewBag.Disciplines = disciplines();
            foreach (var student in ViewBag.Students) { 
                    double i = 0;
                    int count = 0;
                    string rez = "";
                    foreach (var item in _context.Performances)
                    {
                        if (student.ID == item.StudentID)
                        {
                                i += item.Mark;
                            count++;
                        }
                    }
                    double res = i / count;
                    rez = $"{res:F2}";
                    student.Score = rez;
                _context.Update(student);
                _context.SaveChanges();
            }
            return View(await _context.Performances.ToListAsync());
        }

        public IActionResult Excel()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");
            var currentRow = 1;
            var currentCell = 3;

            worksheet.Cell(currentRow, 1).Value = "Id";
            worksheet.Cell(currentRow, 2).Value = "Имя";
            worksheet.Cell(currentRow, 3).Value = "Фамилия";
            foreach (var disc in disciplines())
            {
                currentCell++;
                worksheet.Cell(currentRow, currentCell).Value = disc.Name;
            }
            worksheet.Cell(currentRow, currentCell+1).Value = "Rating";
            foreach (var user in students())
            {
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = user.ID;
                worksheet.Cell(currentRow, 2).Value = user.Firstname;
                worksheet.Cell(currentRow, 3).Value = user.Lastname;
                currentCell = 3;
                foreach (var item in _context.Performances) {
                    if (user.ID == item.StudentID) {
                        currentCell++;
                        worksheet.Cell(currentRow, currentCell).Value = item.Mark;
                    }
                    worksheet.Cell(currentRow, currentCell + 1).Value = user.Score;
                }
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Список студентов.xlsx");
        }

        // GET: Performances/Edit/5
        public async Task<IActionResult> Edit(int? studentId, int? disciplineId)
        {
            if (studentId == null || disciplineId == null)
            {
                return NotFound();
            }

            var performance = await _context.Performances.FindAsync(disciplineId, studentId);
            if (performance == null)
            {
                return NotFound();
            }
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int studentId, int disciplineId, [Bind("StudentID,DisciplineID,Mark")] Performance performance)
        {
            if (disciplineId != performance.DisciplineID && studentId != performance.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceExists(performance.DisciplineID))
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
            return View(performance);
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performances.Any(e => e.DisciplineID == id);
        }

        private List<Student> students() => _context.Students.ToList();

        private List<Discipline> disciplines() => _context.Disciplines.ToList();

        //Не выходит создать строку htmlTable
        //public FileResult Export(string htmlTable)
        //{
        //    return File(Encoding.ASCII.GetBytes(htmlTable), "application/vnd.ms-excel");
        //}

    }
}
