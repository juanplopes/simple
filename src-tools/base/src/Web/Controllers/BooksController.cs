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
    public class BooksController : Controller
    { 
        public ActionResult Index()
        {
            return View(Book.ListAll());
        }

        public ActionResult Details(int id)
        {
            return View(Book.Load(id));
        }

        public ActionResult Create()
        {
            ViewBag.Authors = Author.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View();
        }

        [HttpPost]
        public ActionResult Create(object diff)
        {
            var model = new Book()
                .BindWith(TryUpdateModel).Save();
            
            return RedirectToAction("Index")
                .NotifySuccess("Success!");
        }

        public ActionResult Edit(int id)
        {
            return View(Book.Load(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, object diff)
        {
            Book.Load(id)
                .BindWith(TryUpdateModel).Update();

            return RedirectToAction("Index")
                .NotifySuccess("Success!");
                
        }
        
        public virtual ActionResult Delete(int id)
        {
            return this.DeleteView(Book.Load(id));
        }

        [HttpPost]
        public virtual ActionResult Delete(int id, object diff)
        {
            Book.Delete(id);
            return RedirectToAction("Index")
                .NotifySuccess("Success!");
        }
    }
}
