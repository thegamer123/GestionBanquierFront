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
    public class UserController : Controller
    {

        HttpClient httpClient;

        public UserController()
        {
            httpClient = HttpClientAccessor.HttpClient;
            //httpClient.BaseAddress = new Uri("http://localhost:8082/api/auth/");
            // httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Session["accessToken"]);

            ViewBag.userList = new SelectList(new List<SelectListItem>());
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)Session["accessToken"]);
        }

        // GET: User
        public ActionResult Index()
        {


            var userListResponse = httpClient.GetAsync("auth/users").Result;

            if (userListResponse.IsSuccessStatusCode)
            {
                var response = userListResponse.Content.ReadAsAsync<List<User>>().Result;
                return View(response);
            }

            return View();
        }

        // GET: User/PDF/5

        public ActionResult ExportPDF()
        {
            var user = httpClient.GetAsync("auth/users/export/pdf").Result;
               
                var response = user.Content.ReadAsStringAsync().Result;
            return File(response, "application/pdf", "report.pdf");
            

            
        }
        public ActionResult ExportCSV()
        {
            var user = httpClient.GetAsync("auth/users/export/csv").Result;
            
                var response = user.Content.ReadAsStringAsync().Result;
                //string csv = ListToCSV(response);

                return File(new System.Text.UTF8Encoding().GetBytes(response), "text/csv", "report.csv");
            
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
                var user = new CreateUserModel
                {

                    nom = collection["nom"],
                    prenom = collection["prenom"],

                    email = collection["email"],

                    username = collection["username"],

                    password = collection["password"],

                };



                //string jsonString = JsonConvert.SerializeObject(account);

                var response = await httpClient.PostAsJsonAsync("auth/register/", user);

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

        // POST: Account/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userListResponse = httpClient.GetAsync("auth/users/{id}"+id).Result;

            if (userListResponse.IsSuccessStatusCode)
            {
                var response = userListResponse.Content.ReadAsAsync<User>().Result;
                return View(response);
            }

            return View();
        }

        // GET: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            ViewBag.userList = new SelectList(Enumerable.Empty<User>());
            var userList = httpClient.GetAsync("auth/users/{id}"+id).Result;
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

            return View(userList);

        }
        // GET: Uerr/Details/5
        public ActionResult Details(int id)
        {

            var userListResponse = httpClient.GetAsync("auth/users/"+id).Result;

            if (userListResponse.IsSuccessStatusCode)
            {
                var response = userListResponse.Content.ReadAsAsync<List<User>>().Result;
                return View(response);
            }

            return View();
        }



        // POST: Account/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            {
                try
                {

                    var response = await httpClient.PostAsync("auth/deleteusers/{id}" + id, null);

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
}

