using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL.Model;
using Api;
using Microsoft.AspNetCore.Mvc;
using SellBoard.Models;
using SellBoard.ViewModel;

namespace SellBoard.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork Db = new UnitOfWork();
        public async Task<IActionResult> Index()
        {
            try
            {
                var apisetting = Db.ApiRepository.Get().FirstOrDefault();
                var dataFRomApi = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey);
                if (dataFRomApi != null)
                {
                    ViewBag.count = dataFRomApi.data.Count();
                }
                return View();
            }
            catch (Exception e)
            {
                ViewModel_Error error = new ViewModel_Error();
                error.ErrorTitle = "Error";
                error.ErrorMassage = e.Message;
                return View("Error", error);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
