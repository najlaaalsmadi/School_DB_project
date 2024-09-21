using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School_DB_project.Models;

namespace School_DB_project.Controllers
{
    public class CoursesController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Courses
        public ActionResult Index()
        {
            string currentTeacherName = Session["UserName"]?.ToString();

            if (string.IsNullOrEmpty(currentTeacherName))
            {
                return RedirectToAction("Login"); // Redirect to login if not authenticated
            }

            // Filter courses where the teacher's name matches the session's user name
            var courses = db.Courses.Where(c => c.Teacher.Name == currentTeacherName).ToList();

            return View(courses);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            string currentTeacherName = Session["UserName"]?.ToString();

            if (string.IsNullOrEmpty(currentTeacherName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Teacher not found in session.");
            }

            // Retrieve all teachers with the matching name
            var teachers = db.Teachers.Where(t => t.Name == currentTeacherName).ToList();

            // Handle the case where there are multiple teachers with the same name
            if (teachers.Count > 1)
            {
                return Content("Multiple teachers found with the same name. Please refine your search criteria.");
            }

            // Handle the case where no teachers are found
            var teacher = teachers.FirstOrDefault();
            if (teacher == null)
            {
                return HttpNotFound("Teacher not found.");
            }

            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", teacher.TeacherId);
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,Name,TeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", course.TeacherId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", course.TeacherId);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Name,TeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", course.TeacherId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
