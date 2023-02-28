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
    public class PatientController : Controller
    {
        private CrudService _crudService;
        private readonly string _url = "https://localhost:44391/patients";

        public PatientController()
        {
            _crudService = new();
        }

        public async Task<IActionResult> Index()
        {
            var result = await _crudService.GetAllNoToken<PatientModel>(_url);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult NoAuthorize()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Request.Cookies["jwt"];
            var result = await _crudService.GetById<PatientModel>(_url, id, token);
            ViewData["Message"] = "";
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientModel model)
        {
            //model.Surname = Request.Form["Surname"];
            var dateOfBirth = DateTime.Parse(Request.Form["DateOfBirth"]);
            var pesel = int.Parse(Request.Form["Pesel"]);

            model.Pesel = pesel;
            model.DateOfBirth = dateOfBirth;

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

        public async Task<IActionResult> EditModel(int id, PatientModel model)
        {
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
