using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Naut.Controllers
{
    public class TemplatesController : Controller
    {
        public ActionResult Footer()
        {
            return PartialView();
        }
        public ActionResult LayerSearch()
        {
            return PartialView();
        }
        public ActionResult Sidebar()
        {
            return PartialView();
        }
        public ActionResult Settings()
        {
            return PartialView();
        }
        public ActionResult TopNavbar()
        {
            return PartialView();
        }
        public ActionResult TopNavbarDock()
        {
            return PartialView();
        }
    }
}