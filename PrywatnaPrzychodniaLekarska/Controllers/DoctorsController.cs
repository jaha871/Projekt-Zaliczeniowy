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
    [Route("doctors")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private ICrudScheme<DoctorsServices> _service;

        public DoctorsController(ICrudScheme<DoctorsServices> service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult Create([FromBody] DoctorModel model)
        {
            var result = _service.Create(model);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<DoctorModel>> Get()
        {
            var result = _service.Get<DoctorModel>();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
 
        public ActionResult Update([FromBody] DoctorModel dto, [FromRoute] int id)
        {
            var result = _service.Update(id, dto);

            return Ok(result);
        }
    }
}
