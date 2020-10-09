using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SATApp.DATA.EF;

namespace SATApp.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin, Scheduling")]
    public class StudentsController : Controller
    {
        private SAT_DatabaseEntities db = new SAT_DatabaseEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
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
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoURL,SSID")] Student student, HttpPostedFileBase photoURL)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                //use a default image if none is provided
                string imgName = "No-Image-Available.png";

                if (photoURL != null)
                {
                    //get image and assign to variable
                    imgName = photoURL.FileName;

                    //declare and assign extension value
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    //declare a list of valid extenstion
                    string[] goodexts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the ext variable against goodexts
                    if (goodexts.Contains(ext.ToLower()) && (photoURL.ContentLength <= 4194304))
                    {
                        //if it is in the list, rename it using a guid
                        imgName = Guid.NewGuid() + ext;

                        //save to the webserver
                        photoURL.SaveAs(Server.MapPath("~/Content/assets/img/" + imgName));
                    }
                    else
                    {
                        imgName = "No-Image-Available.png";
                    }
                }
                //no matter what add the imageName to the object
                student.PhotoURL = imgName;

                #endregion





                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
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
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoURL,SSID")] Student student, HttpPostedFileBase PhotoURL)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                if (PhotoURL != null)
                {
                    //get image and assign to variable
                    string imgName = PhotoURL.FileName;

                    //declare and assign ext value
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    //declare and assign list of good extensions
                    string[] goodexts = { ".jpg", ".jpeg", ".gif", ".png" };

                    //Compare file extension against list
                    if (goodexts.Contains(ext.ToLower()) && (PhotoURL.ContentLength <= 4194304))
                    {
                        //if it's good, rename using a guid
                        imgName = Guid.NewGuid() + ext;

                        //save to the web server
                        PhotoURL.SaveAs(Server.MapPath("~/Content/assets/img/" + imgName));

                        //Make sure to not delete default image
                        if (student.PhotoURL != null && student.PhotoURL != "No-Image-Available.png")
                        {
                            //remove the original file from the Edit view
                            //System.IO.File.Delete(Server.MapPath("~/Content/assets/img" + Session["currentImage"].ToString()));
                        }
                        //only save if the image meets criteria imgageName to the object
                        student.PhotoURL = imgName;
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
        public ActionResult DeleteConfirmed(int id)
        {
            //Student student = db.Students.Find(id);
            //db.Students.Remove(student);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            Student student = db.Students.Find(id);
            if (student.SSID != 7)
            {
                student.SSID = 7;
            }
            else
            {
                student.SSID = 2;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Withdrawn()
        {
            var students = db.Students.Where(x => x.SSID == 7);
            return View(students);
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
