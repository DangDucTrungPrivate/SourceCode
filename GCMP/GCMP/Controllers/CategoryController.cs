using System.Collections;
using GCMP.Models;
using GCMP.Models.Entities;
using GCMP.ViewModels.CategoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;

namespace GCMP.Controllers
{
    public class CategoryController : Controller
    {
        // trungdd- view card in a category
        public ActionResult ViewCateogry(int id)
        {
            var cardModel = new CardModel();

            var cardsList = cardModel.GetCardsInCategory(id);

            IList<Card> list = cardsList.ToPagedList(1, 1);

            return View(list);
        }

        //
        // GET: /Category/
        public ActionResult Index()
        {
            var catModel = new CategoryModel();
            var listCategoryViewModel = new List<CategoryViewModel>();
            foreach (var cte in catModel.ListCategory())
            {
                var categoryViewModel = new CategoryViewModel();
                categoryViewModel.CategoryName = cte.CategoryName;
                categoryViewModel.Status = cte.Status;
                categoryViewModel.Id = cte.Id;
                listCategoryViewModel.Add(categoryViewModel);
            }
            return View(listCategoryViewModel);
        }

        //
        //GET: /Category/CategoryCreate
        public ActionResult AddCategory()
        {
            List<SelectListItem> listStatus = new List<SelectListItem>();
            listStatus.Add(new SelectListItem() { Text = "Đang Hoạt Động", Value = "Available", Selected = true });
            listStatus.Add(new SelectListItem() { Text = "Ngưng Hoạt Động", Value = "Not Available", Selected = false });
            ViewBag.Status = listStatus;
            return View();
        }

        [HttpPost]
        public ActionResult CheckExistCategories(string category)
        {
            CategoryModel categoryModel = new CategoryModel();
            if (categoryModel.CheckExistedCategory(category))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel categoryViewModel)
        {
            CategoryModel cateModel = new CategoryModel();
            if (cateModel.AddCategory(categoryViewModel.CategoryName, categoryViewModel.Status) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        //Get: /Category/CategoryEdit
        public ActionResult UpdateCategory(int id)
        {
            var _catModel = new CategoryModel();
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            foreach (var cate in _catModel.FindCategory(id))
            {
                categoryViewModel.CategoryName = cate.CategoryName;
                categoryViewModel.Status = cate.Status;
            }

            List<SelectListItem> listStatus = new List<SelectListItem>();
            listStatus.Add(new SelectListItem() { Text = "Đang Hoạt Động", Value = "Available", Selected = true });
            listStatus.Add(new SelectListItem() { Text = "Ngưng Hoạt Động", Value = "Not Available", Selected = false });
            ViewBag.Status = listStatus;

            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var _catModel = new CategoryModel();
            if (_catModel.Update(categoryViewModel.Id, categoryViewModel.CategoryName, categoryViewModel.Status) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("UpdateCategory/" + categoryViewModel.Id);
            }
        }

    }
}