using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentMang.Models;
using StudentMang.Services;

namespace StudentMang.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _repo;
        public StudentsController(IStudentRepository repo)
        {
            _repo = repo;
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View(_repo.Students);
        }
        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            return View(_repo.GetStudent(id));
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("StudentID,LastName,FirstMidName")] Student student)
        {
            try
            {
                _repo.Add(student);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_repo.GetStudent(id));
        }
        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("StudentID,LastName,FirstMidName")] Student student)
        {
            try
            {
                // TODO: Add update logic here
                _repo.Update(id, student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_repo.GetStudent(id));
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Student collection)
        {
            try
            {
                // TODO: Add delete logic here
                _repo.DeleteStudent(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private ActionResult ErrorView(Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unknown Error");
            return View();
        }
    }
}
