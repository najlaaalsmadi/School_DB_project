using School_DB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace School_DB_project.Controllers
{
    public class AccountController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // تشفير كلمة المرور المدخلة
                string hashedPassword = HashPassword(model.Password);

                // البحث عن المستخدم بناءً على الاسم وكلمة المرور المشفرة
                var user = db.Teachers.FirstOrDefault(t => t.Name == model.Name && t.Password == hashedPassword);

                if (user != null)
                {
                    // تحقق إضافي لتأمين الجلسة
                    if (Session != null)
                    {
                        // تخزين معلومات المستخدم في الجلسة (Session)
                        Session["UserName"] = user.Name;
                        Session["UserId"] = user.TeacherId;

                        // تخصيص وقت انتهاء الجلسة
                        Session.Timeout = 30;

                        // إعادة التوجيه إلى الصفحة الرئيسية بعد تسجيل الدخول
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Session error. Please try again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View(model);
        }

        // Logout action
        public ActionResult Logout()
        {
            // إزالة بيانات الجلسة
            Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Teacher model)
        {
            if (ModelState.IsValid)
            {
                // تحقق من أن اسم المستخدم غير مستخدم مسبقًا
                if (db.Teachers.Any(t => t.Name == model.Name))
                {
                    ModelState.AddModelError("", "This username is already taken.");
                    return View(model);
                }

                // تشفير كلمة المرور
                model.Password = HashPassword(model.Password);

                // إضافة المعلم إلى قاعدة البيانات
                db.Teachers.Add(model);
                db.SaveChanges();
                TempData["SuccessMessage"] = "تم التسجيل بنجاح!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // دالة لتشفير كلمة المرور
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
