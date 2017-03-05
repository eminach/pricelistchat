using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Naut.Controllers
{
    public class SimpleViewController : Controller
    {
        // GET: SimpleView
        public ActionResult Index()
        {
            return View();
        }
    }
}