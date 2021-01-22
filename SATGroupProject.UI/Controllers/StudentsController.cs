using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SATGroupProject.DATA.EF;
using SATGroupProject.UI.Utilities;

namespace SATGroupProject.UI.Controllers
{
    public class StudentsController : Controller
    {
        private SATGroupProjectEntities db = new SATGroupProjectEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        public ActionResult Booted()
        {
            return View(db.Students.Where(p => p.SSID == 6).ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentPhoto)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "no-image.jpg";

                if (studentPhoto != null)
                {
                    file = studentPhoto.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext))
                    {
                        if (studentPhoto.ContentLength <= 4194304)
                        {
                            file = Guid.NewGuid() + ext;

                            #region Resize
                            string savePath = Server.MapPath("~/Content/img/StudentImages/");

                            Image convertedImage = Image.FromStream(studentPhoto.InputStream);

                            int maxImageSize = 500;
                            int maxThumbSize = 100;

                            ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                            #endregion
                        }
                    
                    student.PhotoUrl = file;
                    }
                }
                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentPhoto)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "no-image.jpg";

                if (studentPhoto != null)
                {
                    file = studentPhoto.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext))
                    {
                        if (studentPhoto.ContentLength <= 4194304)
                        {
                            file = Guid.NewGuid() + ext;

                            #region Resize
                            string savePath = Server.MapPath("~/Content/img/StudentImages/");

                            Image convertedImage = Image.FromStream(studentPhoto.InputStream);

                            int maxImageSize = 500;
                            int maxThumbSize = 100;

                            ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                            #endregion
                        }

                        student.PhotoUrl = file;
                    }
                }
                #endregion

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Soft Delete (status change)
            Student stu = db.Students.Find(id);
            
            if (stu.SSID == 2)
            {
                //check to see if they are currently enrolled
                if (stu.Enrollments.ToList().Count > 0)
                {
                    //Add some text
                    ViewBag.Message = "Please unenroll the student before moving to booted.";
                    return RedirectToAction("Index", "Enrollments");
                } else //If they arent currently enrolled
                {
                    //go from current student to booted
                    stu.SSID = 6;
                    db.SaveChanges();
                    return RedirectToAction("Booted");
                }
            } else if (stu.SSID == 6)
            {
                //go from booted to current student
                stu.SSID = 2;
                db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                db.Students.Find(id);
                //redirect to edit page
                //NEED TO PUT ID HERE
                //GOOGLE REDIRECTOACTION(EDIT ID)
                //return RedirectToAction("ActionName", "Controller", new { arg = updatedResultsDocument });
                return RedirectToAction("Edit", "Students", new { id = id });
            }

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
