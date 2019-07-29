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
                ViewModel_ShowData model = await GetDataFromServerAsync();
                return View(model);
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
                SendModel todayType2Send = new SendModel { from = DateTime.Now.ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "2", offset = "0" };
                SendModel todayType4Send = new SendModel { from = DateTime.Now.ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "4", offset = "0" };
                SendModel weeklyType2Send = new SendModel { from = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "2", offset = "0" };
                SendModel weeklyType4Send = new SendModel { from = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "4", offset = "0" };
                DateTime now = DateTime.Now;
                SendModel MonthType2Send = new SendModel { from = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "2", offset = "0" };
                SendModel MonthType4Send = new SendModel { from = new DateTime(now.Year, now.Month, 1).ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "4", offset = "0" };
                //var todayType2Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, todayType2Send);
                //var todayType4Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, todayType4Send);
                //var weeklyType2Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, weeklyType2Send);
                //var weeklyType4Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, weeklyType4Send);
                //var MonthType2Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, MonthType2Send);
                //var MonthType4Get = await GetData.CallWebAPIAsyncPost(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, MonthType4Send);
                ViewModel_ShowData model = new ViewModel_ShowData();
                model.TodayType2 = await CallApi(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, todayType2Send);
                model.TodayType4 = await CallApi(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, todayType4Send);
                model.WeeklyType2 = await CallApi(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, weeklyType2Send);
                model.WeeklyType4 = await CallApi(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, weeklyType4Send);
                model.MonthType2 = await CallApi(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, MonthType2Send);
                model.MonthType4 = await CallApi(apisetting.BaseUrl, apisetting.PostUrl, apisetting.UserName, apisetting.Password, apisetting.ApiKey, MonthType4Send);
                model.TodayCR = model.TodayType2 != 0 ? Math.Round((double)(decimal.Divide((model.TodayType4 * 100), model.TodayType2)), 2) : 0;
                model.WeekCR = model.WeeklyType2 != 0 ? Math.Round((double)(decimal.Divide((model.WeeklyType4 * 100), model.WeeklyType2)), 2) : 0;
                model.MonthCR = model.MonthType2 != 0 ? Math.Round((double)(decimal.Divide((model.MonthType4*100),model.MonthType2)), 2) : 0;
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<int> CallApi(string BaseUrl, string PostUrl, string UserName, string Password, string ApiKey, SendModel sendModel)
        {
            int count = 0;
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    var dataFRomApi = await GetData.CallWebAPIAsyncPost(BaseUrl, PostUrl, UserName, Password, ApiKey, sendModel);
                    count += dataFRomApi.data.Count;
                    if (count >= 500 && count % 500 == 0)
                    {
                        sendModel.offset = (Convert.ToInt32(sendModel.offset) + 500).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
                return count;
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
