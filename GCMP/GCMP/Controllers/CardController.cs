using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCMP.Models;
using GCMP.ViewModels;

namespace GCMP.Controllers
{
    public class CardController : Controller
    {
        //
        // GET: /Card/
        public ActionResult Index()
        {
            return View();
        }
	}
}