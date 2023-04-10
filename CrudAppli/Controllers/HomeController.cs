using CrudAppli.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudAppli.Controllers
{
    public class HomeController : Controller
    {
        EmployeeContext db=new EmployeeContext();
        // GET: Home
        public ActionResult Index()
        {
            var data=db.Employees.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee e)
        {
            if (ModelState.IsValid==true)
            {
                db.Employees.Add(e);
                int a = db.SaveChanges();
                if (a>0)
                {
                    //ViewBag.InsertMessage="<script>alert('Data Inserted!!!')</script>";
                    //TempData["InsertMessage"]="<script>alert('Data Inserted!!!')</script>";

                    TempData["InsertMessage"]="Data Inserted !!";
                    return RedirectToAction("Index");
                    //  ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage="<script>alert('Data  not Inserted!!!')</script>";

                }
            }

            return View();
        }
        public ActionResult Edit(int id)
        {
            var row=db.Employees.Where(model=>model.Id==id).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Employee e)
        {
            if(ModelState.IsValid==true)
            {
                db.Entry(e).State=EntityState.Modified;
                int a = db.SaveChanges();
                if (a>0)
                {
                    //ViewBag.UpdateMessage="<script>alert('Data  updated!!!')</script>";
                    TempData["UpdateMessage"]="Data  updated";
                    return RedirectToAction("Index");
                    // ModelState.Clear();
                }
                else
                {
                    ViewBag.UpdateMessage="<script>alert('Data not updated!!!')</script>";

                }
            }
            
            return View();
        }
        public ActionResult Delete(int id)
        {
            var EmpIdRow=db.Employees.Where(model=>model.Id==id).FirstOrDefault();
            return View(EmpIdRow);

        }
        [HttpPost]
        public ActionResult Delete(Employee e)
        {
            db.Entry(e).State=EntityState.Deleted;
            int a=db.SaveChanges();
            if(a>0)
            {
               TempData["DeleteMessage"]="<script>alert('Data deleted!!!')</script>";
            }
            else
            {
                TempData["DeleteMessage"]="<script>alert('Data not deleted!!!')</script>";
            }
            return RedirectToAction("Index");
        }
    }
}