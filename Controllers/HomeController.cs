using BFTFLoan.Models;
using BFTFLoan.Models.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BFTFLoan.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}