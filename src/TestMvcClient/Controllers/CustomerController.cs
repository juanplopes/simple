using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Simple.Tests.SampleServer;
using Simple;

namespace TestMvcClient.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View(Customer.ListAll());
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(string id)
        {
            return View(Customer.Load(id));
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Customer/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Customer customer)
        {
            try
            {
                customer.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Customer/Edit/5
 
        public ActionResult Edit(string id)
        {
            return View(Customer.Load(id));
        }

        //
        // POST: /Customer/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Customer c)
        {
            try
            {
                c.Update();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
