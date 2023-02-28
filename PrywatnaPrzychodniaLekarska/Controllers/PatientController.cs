using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrywatnaPrzychodniaLekarska.Contracts;
using PrywatnaPrzychodniaLekarska.Models;
using PrywatnaPrzychodniaLekarska.Services;

namespace PrywatnaPrzychodniaLekarska.Controllers
{
    [ApiController]
    [Route("patients")]
    [Authorize]

    public class PatientController : ControllerBase
    {
        private ICrudScheme<PatientService> _service;

        public PatientController(ICrudScheme<PatientService> service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult Create([FromBody] PatientModel model)
        {
            var result = _service.Create<PatientModel>(model);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);
            return Ok(result);
        }

       [HttpGet]
       [AllowAnonymous]
       public ActionResult<List<PatientModel>> Get()
       {
           var result = _service.Get<PatientModel>();
           return Ok(result);
       }

       [HttpGet("{id}")]
       [Authorize(Roles = "admin, user")]
       public ActionResult<PatientModel> GetById([FromRoute] int id)
       {
           var result = _service.GetById<PatientModel>(id);
           return Ok(result);
       }

        [HttpPut("{id}")]
       [Authorize(Roles = "admin")]
       public ActionResult Update([FromBody] PatientModel dto, [FromRoute] int id)
       {
           var result = _service.Update(id, dto);

           return Ok(result);
       }
    }
}
