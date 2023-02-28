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
    [Route("users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private ICrudScheme<UserService> _service;

        public UserController(ICrudScheme<UserService> service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create([FromBody] UserModel model)
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
        public ActionResult<List<GetVisitModel>> Get()
        {
            var result = _service.Get<UserModel>();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<UserModel> GetById([FromRoute] int id)
        {
            var result = _service.GetById<UserModel>(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Update([FromBody] UserModel dto, [FromRoute] int id)
        {
            var result = _service.Update(id, dto);

            return Ok(result);
        }
    }
}
