using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PrzychodniaFrontend.Models;
using PrzychodniaFrontend.Services;

namespace PrzychodniaFrontend.Controllers
{
    public class UserController : Controller
    {
        private CrudService _crudService;
        private readonly string _url = "https://localhost:44391/users";

        public UserController()
        {
            _crudService = new();
        }

        public IActionResult NoAuthorize()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel model)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            model.FirstName = Request.Form["Name"];
            model.LastName = Request.Form["Surname"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(nameof(NoAuthorize));
            }

            var response = await _crudService.Create(token, _url, model);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                ViewData["Message"] = $"{result}";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["jwt"];
            var result = await _crudService.GetAllWithToken<UserModel>(_url, token);
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(nameof(NoAuthorize));
            }

            await _crudService.Delete(_url, token, id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            var result = await _crudService.GetById<UserModel>(_url, id, token);
            ViewData["Message"] = "";
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModel(int id, UserModel model)
        {
            model.FirstName = Request.Form["Name"];
            model.LastName = Request.Form["Surname"];

            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(nameof(NoAuthorize));
            }

            var response = await _crudService.Edit(_url, id, token, model);

            return RedirectToAction(nameof(Index));
        }
    }
}
