﻿using gestionBanquierFront.Models;
using gestionBanquierFront.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace gestionBanquierFront.Controllers
{
    public class CreditController : Controller
    {

        HttpClient httpClient;
        public CreditController()
        {
            httpClient = HttpClientAccessor.HttpClient;
            // httpClient.BaseAddress = new Uri("http://localhost:8082/api/credits/");
            //httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer",
    "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJiZWNocGlkZXZAZ21haWwuY29tIiwiaWF0IjoxNjI3MjUzNjg1LCJleHAiOjE2MjczNDAwODV9.AvANLMmFDvd6SkkfJ8f3rtsnjU4_X5p5FCuShr7GMOR7AV4EMF0QgMBhlq9FK3esXS5Bul6S8u7Cm_qSYLicAw");
        }
        /*
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Session["accessToken"]);
        }*/

        // GET: Credit
        public ActionResult Index()
        {

            var creditList = httpClient.GetAsync("credits/list").Result;

            if (creditList.IsSuccessStatusCode)
            {
                var response = creditList.Content.ReadAsAsync<List<Credit>>().Result;
                return View(response);
            }

            return View();
        }

        // GET: Credit/Details/1
        public ActionResult Details(int id)
        {
                 var creditList = httpClient.GetAsync("credits/"+id).Result;

            if (creditList.IsSuccessStatusCode)
            {
                var response = creditList.Content.ReadAsAsync<Credit>().Result;
                return View(response);
            }
            return View();
        }

        // GET: Credit/Create
        public ActionResult Create()
        {
            return View();
        }


        private ActionResult RedirectToIndex(string v)
        {
            throw new NotImplementedException();
        }

        // POST: Credit/Create
        [HttpPost]
        public ActionResult Create(Credit credit)
        {

            try
            {
                var response = httpClient.PostAsJsonAsync("credits/new", credit).ContinueWith(task =>
                {
                    if (task.Result.IsSuccessStatusCode)
                    {
                        var result = task.Result.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        task.Result.EnsureSuccessStatusCode();
                    }
                });
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }
        }

        // GET: Credit/Edit/5
        public ActionResult Edit(int id)
        {

            var creditList = httpClient.GetAsync("credits/" + id).Result;

            if (creditList.IsSuccessStatusCode)
            {
                var response = creditList.Content.ReadAsAsync<Credit>().Result;
                return View(response);
            }
            return View();
        }

        // POST: Credit/Edit/5
        [HttpPost]
        public ActionResult EditAsync(Credit credit)
        {
            try
            {
     
                {
                   var response = httpClient.PostAsJsonAsync("credits/update", credit);
                   
                }

               /* var creditList = httpClient.PostAsync("credits/update", credit).Result;

                if (creditList.IsSuccessStatusCode)
                {
                    var response = creditList.Content.ReadAsAsync<Credit>().Result;
                    return View(response);
                }*/
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Credit/Delete/5
        public ActionResult Delete(int id)
        {
            var creditList = httpClient.GetAsync("credits/" + id).Result;

            if (creditList.IsSuccessStatusCode)
            {
                var response = creditList.Content.ReadAsAsync<Credit>().Result;
                return View(response);
            }
            return View();
        }
                // POST: Credit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {


            var creditList = httpClient.DeleteAsync("credits/" + id + "/delete").Result;

            if (creditList.IsSuccessStatusCode)
            {
                var response = creditList.Content.ReadAsAsync<List<Credit>>().Result;
                return View(response);
            }


            return RedirectToAction("Index");


        }

        // POST: Credit/CalculScore/5
        [HttpPost]
        public ActionResult CaldulScore(int id)
        {


            var creditList = httpClient.DeleteAsync("credits/" + id + "/score").Result;

            if (creditList.IsSuccessStatusCode)
            {
                var response = creditList.Content.ReadAsAsync<List<Credit>>().Result;
                return View(response);
            }


            return RedirectToAction("Index");


        }

    }
}
