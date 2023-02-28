using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrzychodniaFrontend.Models;
using PrzychodniaFrontend.Services;

namespace PrzychodniaFrontend.Controllers
{
    public class DoctorController : Controller
    {
        private CrudService _crudService;
        private readonly string _url = "https://localhost:44391/doctors";
        public DoctorController()
        {
            _crudService = new();
        }

        public async Task<IActionResult> Index()
        {
            var result = await _crudService.GetAllNoToken<DoctorModel>(_url);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            var result = await _crudService.GetById<DoctorModel>(_url, id,token);
            ViewData["Message"] = "";
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModel(int id, DoctorModel model)
        {
            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(nameof(NoAuthorize));
            }

            var response = await _crudService.Edit(_url, id, token, model);

            return  RedirectToAction(nameof(Index));
        }

        public IActionResult NoAuthorize()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorModel model)
        {
            model.Surname = Request.Form["Surname"];
            var token = HttpContext.Request.Cookies["jwt"];

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
    }
}
