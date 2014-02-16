using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCMP.Models;
using GCMP.ViewModels;
using GCMP.ViewModels.CardViewModel;

namespace GCMP.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult GetAllCards()
        //{
        //    var cardModel = new CardModel();
        //    var cardViewModels = cardModel.ListAllCards().Select(card => new CardViewModel
        //    {
        //        CardId = card.Id, 
        //        CardName = card.CardName, 
        //        DateAdded = card.DateAdded, 
        //        DateExpired = card.CardInfo.DateExpired, 
        //        Status = card.CardInfo.Status, 
        //        Description = card.Description
        //    }).ToList();
        //    return View(cardViewModels);
        //}
	}
}