using gestionBanquierFront.Models;
using gestionBanquierFront.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace gestionBanquierFront.Controllers
{
    public class AccountController : Controller
    {
        
        HttpClient httpClient;

        public AccountController()
        {
            httpClient = HttpClientAccessor.HttpClient;
            //httpClient.BaseAddress = new Uri("http://localhost:8082/api/account/");
            // httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Session["accessToken"]);

            ViewBag.userList = new SelectList(new List<SelectListItem>());
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Session["accessToken"]);
        }

        // GET: Account
        public ActionResult Index()
        {


            var accountListResponse = httpClient.GetAsync("account/retrieve-all-Accounts").Result;

            if (accountListResponse.IsSuccessStatusCode)
            {
                var response = accountListResponse.Content.ReadAsAsync<List<Account>>().Result;
                return View(response);
            }

            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            var account = httpClient.GetAsync("account/" + id).Result;
            if (account.IsSuccessStatusCode)
            {
                var response = account.Content.ReadAsAsync<Account>().Result;
                return View(response);
            }

            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            ViewBag.userList = new SelectList(Enumerable.Empty<User>());
            var userList = httpClient.GetAsync("auth/users").Result;
            if (userList.IsSuccessStatusCode)
            {
                var response = userList.Content.ReadAsAsync<List<User>>().Result;
                var itemsList = new List<SelectListItem>();
                response.ForEach(x =>
                {
                    itemsList.Add(new SelectListItem
                    {
                        Text = x.nom + " " + x.prenom,
                        Value = x.id.ToString()
                    });
                });
                ViewBag.userList = itemsList;
            }
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                var provider = CultureInfo.InvariantCulture;
                var format = "yyyy-mm-dd";
                var userId = collection["SelectedUser"];
                var dateSplitted = DateTime.ParseExact(collection["dateCreation"], format, provider).ToString().Split(" ".ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries)[0].Split("/".ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);
                var account = new CreateAccountRequestModel
                {

                    numCompte = collection["numCompte"],
                    solde = collection["solde"],

                    activated = collection["activated"].Equals("true") ? 1 : 0,

                    dateCreation = dateSplitted[2]+"-"+ dateSplitted[1]+"-"+ dateSplitted[0],
                    maxAmountToBorrow = collection["maxAmountToBorrow"]

                };



                //string jsonString = JsonConvert.SerializeObject(account);

                var response = await httpClient.PostAsJsonAsync("account/add-Account/" + userId, account);

                if (response.IsSuccessStatusCode)

                {
                    var result = response.Content.ReadAsStringAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    return View();
                }





            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return View("Index");
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public async Task<ActionResult>  Delete(int id)
        {
            try
            {

                var response = await httpClient.PostAsync("account/deleteAccount/" + id, null);

                if (response.IsSuccessStatusCode)

                {
                    var result = response.Content.ReadAsStringAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    return View();
                }


            }
            catch
            {
                return View();
            }
        }

        // POST: Account/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {

                var response = await httpClient.PostAsync("account/deleteAccount/{account-id}" + id,null);

                if (response.IsSuccessStatusCode)

                {
                    var result = response.Content.ReadAsStringAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    response.EnsureSuccessStatusCode();
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
