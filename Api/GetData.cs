using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public class GetData
    {
        //static async Task CallWebAPIAsyncGet(string username, string Password, string ApiKey)
        //{
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("https://platform.mydomain.com/");
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client.DefaultRequestHeaders.Add("x-trackbox-username", username);
        //            client.DefaultRequestHeaders.Add("x-trackbox-password", Password);
        //            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        //            //GET Method
        //            HttpResponseMessage response = await client.GetAsync("api/unknown/2");
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string test = await response.Content.ReadAsStringAsync();
        //                Console.WriteLine("Id:{0}\tName:");
        //                Console.WriteLine(test);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Internal server Error");
        //            }
        //        }
        //        Console.Read();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //}
        public static async Task<GetModel> CallWebAPIAsyncPost(string BaseUrl, string postUrl, string username, string Password, string ApiKey)
        {
            try
            {
                if (BaseUrl != null & postUrl != null & username != null & Password != null & ApiKey != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("x-trackbox-username", username);
                        client.DefaultRequestHeaders.Add("x-trackbox-password", Password);
                        client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
                        //POST Method
                        SendModel model = new SendModel { from = DateTime.Now.ToString("yyyy-MM-dd  00:00:00"), to = DateTime.Now.ToString("yyyy-MM-dd  23:59:59"), type = "3" };
                        HttpResponseMessage responsePost = await client.PostAsJsonAsync(postUrl, model);
                        if (responsePost.IsSuccessStatusCode)
                        {
                            // Get the URI of the created resource.
                            Uri returnUrl = responsePost.Headers.Location;
                            GetModel data = await responsePost.Content.ReadAsAsync<GetModel>();
                            return data;
                        }
                        return null;
                    }
                }
                else
                {
                    return null;
                }
                   
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
