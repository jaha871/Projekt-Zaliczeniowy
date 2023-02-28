using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrzychodniaFrontend.Models;
using PrzychodniaFrontend.Services;

namespace PrzychodniaFrontend.Controllers
{
    public class VisitController : Controller
    {
        private CrudService _crudService;
        private readonly string _url = "https://localhost:44391/visits";
        private readonly string _urlPatients = "https://localhost:44391/patients";
        private readonly string _urlDoctors = "https://localhost:44391/doctors";

        public VisitController()
        {
            _crudService = new();
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Request.Cookies["jwt"];
            var result = await _crudService.GetAllWithToken<GetVisitModel>(_url, token);
            return View(result);
        }

        public IActionResult NoAuthorize()
        {
            return View();
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
            var doctors = await _crudService.GetAllNoToken<DoctorModel>(_urlDoctors);
            var patients = await _crudService.GetAllNoToken<PatientModel>(_urlPatients);
            var response = new VisitCreateModel
            {
                DoctorModel = doctors,
                PatientModel = patients
            };

            ViewData["Message"] = "";
            ViewData["Id"] = id;
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var doctors = await _crudService.GetAllNoToken<DoctorModel>(_urlDoctors);
            var patients = await _crudService.GetAllNoToken<PatientModel>(_urlPatients);
            var response = new VisitCreateModel
            {
                DoctorModel = doctors,
                PatientModel = patients
            };

            ViewData["Message"] = "";
           return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisitCreateModel model)
        {
            string date = Request.Form["DateOfWizit"];

            var patientId = Request.Form["patients"];
            var doctorId = Request.Form["doctors"];
            var dateOfVisit = DateTime.Today;
            if (!string.IsNullOrEmpty(date))
            {
                dateOfVisit = DateTime.Parse(date);
            }
            var description = Request.Form["Description"]; 
            var discount = Request.Form["Discount"]; 
            var visitPrice = Request.Form["VisitPrice"]; 

            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(nameof(NoAuthorize));
            }

            var visitModel = new VisitModel
            {
                DateOfVisit = dateOfVisit,
                Description = description,
                Discount = int.Parse(discount),
                DoctorId = int.Parse(doctorId),
                PatientId = int.Parse(patientId),
                VisitPrice = double.Parse(visitPrice),
            };

            var response = await _crudService.Create(token, _url, visitModel);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditVisit(int id)
        {
            string date = Request.Form["DateOfWizit"];

            var patientId = Request.Form["patients"];
            var doctorId = Request.Form["doctors"];
            var dateOfVisit = DateTime.Today;
            if (!string.IsNullOrEmpty(date))
            {
                dateOfVisit = DateTime.Parse(date);
            }
            var description = Request.Form["Description"];
            var discount = Request.Form["Discount"];
            var visitPrice = Request.Form["VisitPrice"];

            var token = HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction(nameof(NoAuthorize));
            }

            var visitModel = new VisitModel
            {
                DateOfVisit = dateOfVisit,
                Description = description,
                Discount = int.Parse(discount),
                DoctorId = int.Parse(doctorId),
                PatientId = int.Parse(patientId),
                VisitPrice = double.Parse(visitPrice),
            };

            var response = await _crudService.Edit<VisitModel>(_url, id, token, visitModel);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
