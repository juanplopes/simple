using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Example.Project.Domain;
using Example.Project.Web.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;

namespace Example.Project.Web.Controllers
{
    [SimpleValidationFilter]
    public class AuthorsController : Controller
    { 
        public ActionResult Index()
        {
            return View(Author.ListAll());
        }

        public ActionResult Details(int id)
        {
            return View(Author.Load(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(object diff)
        {
            var model = new Author()
                .BindWith(TryUpdateModel).Save();
            
            return RedirectToAction("Index")
                .NotifySuccess("Success!");
        }

        public ActionResult Edit(int id)
        {
            return View(Author.Load(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, object diff)
        {
            Author.Load(id)
                .BindWith(TryUpdateModel).Update();

            return RedirectToAction("Index")
                .NotifySuccess("Success!");
                
        }
        
        public virtual ActionResult Delete(int id)
        {
            return this.DeleteView(Author.Load(id));
        }

        [HttpPost]
        public virtual ActionResult Delete(int id, object diff)
        {
            Author.Delete(id);
            return RedirectToAction("Index")
                .NotifySuccess("Success!");
        }
    }
}
