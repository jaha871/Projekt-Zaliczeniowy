using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Contracts;
using PrywatnaPrzychodniaLekarska.Models;
using PrywatnaPrzychodniaLekarska.Services;

namespace PrywatnaPrzychodniaLekarska.Controllers
{
    [ApiController]
    [Route("visits")]
    public class VisitController: ControllerBase
    {
        private ICrudScheme<VisitServices> _service;

        public VisitController(ICrudScheme<VisitServices> service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Create([FromBody] VisitModel model)
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
        public ActionResult<List<GetVisitModel>> Get()
        {
            var result = _service.Get<GetVisitModel>();
            return Ok(result);
        }

        [HttpPut("{id}")]
        //  [Authorize(Roles = "admin")]
        //[Authorize]
        public ActionResult Update([FromBody] VisitModel dto, [FromRoute] int id)
        {
            var result = _service.Update(id, dto);

            return Ok(result);
        }
    }
}
