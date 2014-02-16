using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GCMP.Models;
using GCMP.Models.Entities;
using GCMP.ViewModels;
using GCMP.ViewModels.CategoryViewModel;

namespace GCMP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult CategoriesMenu()
        {
            var catModel = new CategoryModel();

            var citems = catModel.ListCategory();

            var cmenu = citems.Select(category => new CategoryMenuViewModel
            {
                Id = category.Id, ImangePath = category.Image, Name = category.CategoryName
            }).ToList();

            return PartialView(cmenu);

        }

        [ChildActionOnly]
        public ActionResult CategoryBar()
        {

            return PartialView();
        }

    }
}