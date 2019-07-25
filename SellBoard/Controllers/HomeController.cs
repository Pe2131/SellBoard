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
                //ViewModel_ShowData model = await GetDataFromServerAsync();
                return View(null);
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
        public async Task<ViewModel_ShowData> GetDataFromServerAsync()
        {
            try
            {
                var apisetting = Db.ApiRepository.Get().FirstOrDefault();
                SendModel todayType2Send = new SendModel { from = DateTime.Now.ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "2" };
                SendModel todayType4Send = new SendModel { from = DateTime.Now.ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "4" };
                SendModel weeklyType2Send = new SendModel { from = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "2" };
                SendModel weeklyType4Send = new SendModel { from = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "4" };
                var todayType2Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, todayType2Send);
                var todayType4Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, todayType4Send);
                var weeklyType2Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, weeklyType2Send);
                var weeklyType4Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, weeklyType4Send);
                ViewModel_ShowData model = new ViewModel_ShowData();
                model.TodayType2 = todayType2Get != null ? todayType2Get.data.Count : 0;
                model.TodayType4 = todayType4Get != null ? todayType4Get.data.Count : 0;
                model.WeeklyType2 = weeklyType2Get != null ? weeklyType2Get.data.Count : 0;
                model.WeeklyType4 = weeklyType4Get != null ? weeklyType4Get.data.Count : 0;
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<string> ShowDataForView()
        {
            try
            {
                ViewModel_ShowData model = await GetDataFromServerAsync();
               return Newtonsoft.Json.JsonConvert.SerializeObject(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
