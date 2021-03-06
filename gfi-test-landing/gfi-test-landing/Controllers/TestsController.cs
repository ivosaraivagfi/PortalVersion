﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using gfi_test_landing;

namespace gfi_test_landing.Models
{
    public class TestsController : Controller
    {
        private void changeLanguage(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        }

        private testLandingEntities db = new testLandingEntities();

        // GET: Tests
        public ActionResult Index(string language)
        {
            changeLanguage(language);
            var test = db.Test.Include(t => t.Project);
            return View(test.ToList());
        }

        // GET: Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Test.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            gfi_test_landing.Models.TestViewModel tvm = new TestViewModel();
            tvm.test = test;
            return View(tvm);
        }

        // GET: Tests/Create
        public ActionResult Create(string language)
        {
            changeLanguage(language);
            ViewBag.id_project = new SelectList(db.Project, "id", "name");
            gfi_test_landing.Models.TestViewModel tvm = new TestViewModel();
            tvm.actionList = db.Step.Include(s => s.Action).Include(s => s.Object);
            tvm.actionList.ToList();
            return View(tvm);
        }

        // POST: Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description,id_project,broswer")] Test test, string language)
        {
            changeLanguage(language);
            if (test.name == null)
            {
                ViewBag.testname_error_message = "Test Name has an invalid format. Test name must have at least 3 characters and a max of 64.";
                if (test.description == null)
                {
                    ViewBag.testdesc_error_message = "Test Description has an invalid format. Descriptions must have at least 3 characters and a max of 120.";
                }

                ViewBag.id_project = new SelectList(db.Project, "id", "name", test.id_project);
                gfi_test_landing.Models.TestViewModel tvm = new TestViewModel();
                tvm.test = test;
                tvm.actionList = db.Step.Include(s => s.Action).Include(s => s.Object);
                tvm.actionList.ToList();
                return View(tvm);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    test.creation_date = DateTime.Now.ToString();
                    db.Test.Add(test);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.id_project = new SelectList(db.Project, "id", "name", test.id_project);
                gfi_test_landing.Models.TestViewModel tvm = new TestViewModel();
                tvm.test = test;
                return View(tvm);
            }
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Test.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_project = new SelectList(db.Project, "id", "name", test.id_project);
            gfi_test_landing.Models.TestViewModel tvm = new TestViewModel();
            tvm.test = test;
            return View(tvm);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description,creation_date,id_project,broswer")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_project = new SelectList(db.Project, "id", "name", test.id_project);
            gfi_test_landing.Models.TestViewModel tvm = new TestViewModel();
            tvm.test = test;
            return View(tvm);
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Test.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            gfi_test_landing.Models.TestViewModel tvm = new TestViewModel();
            tvm.test = test;
            return View(tvm);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = db.Test.Find(id);
            db.Test.Remove(test);
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
