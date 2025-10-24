using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentGradingSystem.Data;
using StudentGradingSystem.Models;

namespace StudentGradingSystem.Controllers
{
    public class StudentGradesController : Controller
    {
        private readonly StudentGradeDbContext _context;
        public StudentGradesController(StudentGradeDbContext context)
        {
            _context = context;
        }


        //GET
        public async Task<IActionResult> Index(string search, string filter = "All")
        {
            var query = _context.Students

                .Include(sg => sg.Subject)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(sg => sg.StudentName.Contains(search));
            }

            if (filter == "PASS")
            {
                query = query.Where(sg => sg.Grade >= 75);
            }

            else if (filter == "FAIL")
            {
                query = query.Where(sg => sg.Grade < 75);
            }

            var list = await query
                .Select(sg => new StudentGradeViewModel
                {
                    StudentId = sg.StudentId,
                    StudentName = sg.StudentName,
                    SubjectName = sg.Subject!.Name,
                    Grade = sg.Grade,
                    Remarks = sg.Grade >= 75 ? "PASS" : "FAIL"
                }).ToListAsync();

            ViewData["CurrentSearch"] = search;
            ViewData["CurrentFilter"] = filter;

            return View(list);
        }

        //Get :Create
        public IActionResult Create()
        {

            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name");
            return View();
        }

        // POST:Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentName,SubjectId,Grade")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", studentGrade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", student.SubjectId);
            return View(student);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sg = await _context.Students.FindAsync(id);
            if (sg == null) return NotFound();
            //ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", sg.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", sg.SubjectId);
            return View(sg);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,SubjectId,Grade")] Student student)
        {
            if (id != student.StudentId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(e => e.StudentId == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Name", studentGrade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", student.SubjectId);
            return View(student);
        }

        //GET:Delete View
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var sg = await _context.Students
                
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(s => s.StudentId == id);
            if (sg == null) return NotFound();

            return View(sg);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            var sg = _context.Students.Find(id);
            if (sg == null)
            {
                return NotFound();
            }

            _context.Students.Remove(sg);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
