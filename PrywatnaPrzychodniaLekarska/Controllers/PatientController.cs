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

    public class PatientController : ControllerBase
    {
        private ICrudScheme<PatientService> _service;

        public PatientController(ICrudScheme<PatientService> service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Create([FromBody] PatientModel model)
        {
            var result = _service.Create(model);

            return Ok(result);
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = "admin, user")]
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

       [HttpPut("{id}")]
     //  [Authorize(Roles = "admin")]
       //[Authorize]
       public ActionResult Update([FromBody] PatientModel dto, [FromRoute] int id)
       {
           var result = _service.Update(id, dto);

           return Ok(result);
       }
    }
}
