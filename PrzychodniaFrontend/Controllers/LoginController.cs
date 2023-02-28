using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PrzychodniaFrontend.Models;
using PrzychodniaFrontend.Services;

namespace PrzychodniaFrontend.Controllers
{
    public class LoginController : Controller
    {
        public CrudService _crudService;
        public string Message { get; set; }
        public LoginController()
        {
            _crudService = new();
        }

        public IActionResult Index()
        {
            var response = new LoginModel
            {
                Message = ""
            };
            ViewBag.Admin = 1;
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("Email, Password")] LoginModel model)
        {
            if (model.Email == null && model.Password == null || model.Email == null || model.Password == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string loginUrl = "https://localhost:44391/login";
            string content = JsonConvert.SerializeObject(model);
            var response = await _crudService.Logging(loginUrl, content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                var authResult = JsonConvert.DeserializeObject<AuthResult>(result);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMinutes(20), 
                    Domain = loginUrl,
                    Path = "/"
                };

                HttpContext.Response.Cookies.Append("jwt", authResult.Token);

                Response.Cookies.Append("jwt", authResult.Token, cookieOptions);

                return RedirectToAction("Index", "Home");
            }

            else
            {
                var result = await response.Content.ReadAsStringAsync();
                ViewData["Message"] = $"{result}";
                Response.Cookies.Delete("jwt");
                ViewBag.Admin = 1;

                return View();
            }
        }
    }
}
