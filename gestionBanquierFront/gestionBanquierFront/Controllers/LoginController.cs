using gestionBanquierFront.Models;
using gestionBanquierFront.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace gestionBanquierFront.Controllers
{
    public class LoginController : Controller

    {
        HttpClient httpClient;
        public LoginController()
        {
            httpClient = HttpClientAccessor.HttpClient;

          
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Login/LoginUser
        [HttpPost]
        public async Task<ActionResult> LoginUser(FormCollection collection)
        {
            try
            {

                var requestModel = new LoginRequestModel {
                    username = collection[0],
                    password =  collection[1]
                };
                var response = await httpClient.PostAsJsonAsync("auth/login", requestModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                           var resultObject = JsonConvert.DeserializeObject<UserLoginResultModel>(result.Result);
                           Session["username"] = resultObject.username;
                    Session["accessToken"] = resultObject.accessToken;
                    Session["role"] = resultObject.roles[0];
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }


            }
            catch
            {
                return View();
            }
        }
    }
}
