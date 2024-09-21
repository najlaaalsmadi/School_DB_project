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
    public class StudentClassesController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: StudentClasses
        public ActionResult Index(int? classId)
        {
            // Fetching student classes with related data
            var studentClasses = db.StudentClasses.Include(s => s.Class).Include(s => s.Student).AsQueryable();

            // Filtering by classId if provided
            if (classId.HasValue)
            {
                studentClasses = studentClasses.Where(s => s.ClassId == classId.Value);
            }

            // Converting to list for rendering in the view
            return View(studentClasses.ToList());
        }
    


        // GET: StudentClasses/Create
        public ActionResult Create(int classId)
        {
            // عرض الصف المحدد في العنوان وفي الـ ViewBag
            var selectedClass = db.Classes.FirstOrDefault(c => c.ClassId == classId);
            ViewBag.ClassName = selectedClass != null ? selectedClass.Name : "Unknown Class";

            // عرض قائمة الطلاب
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name");

            // إنشاء StudentClass مع تحديد الـ ClassId
            var studentClass = new StudentClass { ClassId = classId };
            return View(studentClass);
        }

        // POST: StudentClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                db.StudentClasses.Add(studentClass);
                db.SaveChanges();
                return RedirectToAction("Index", new { classId = studentClass.ClassId });
            }

            // إعادة تحميل البيانات في حال فشل التحقق من البيانات
            var selectedClass = db.Classes.FirstOrDefault(c => c.ClassId == studentClass.ClassId);
            ViewBag.ClassName = selectedClass != null ? selectedClass.Name : "Unknown Class";
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", studentClass.StudentId);

            return View(studentClass);
        }
        // GET: StudentClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentClass studentClass = db.StudentClasses.Find(id);
            if (studentClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "Name", studentClass.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", studentClass.StudentId);
            return View(studentClass);
        }

        // POST: StudentClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentClassId,StudentId,ClassId")] StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "Name", studentClass.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", studentClass.StudentId);
            return View(studentClass);
        }

        // GET: StudentClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentClass studentClass = db.StudentClasses.Find(id);
            if (studentClass == null)
            {
                return HttpNotFound();
            }
            return View(studentClass);
        }

        // POST: StudentClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentClass studentClass = db.StudentClasses.Find(id);
            db.StudentClasses.Remove(studentClass);
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
