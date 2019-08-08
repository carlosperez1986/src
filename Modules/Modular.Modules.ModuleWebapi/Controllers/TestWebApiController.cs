using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleWebapi.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class TestWebApiController : ControllerBase
    { 
        [Route("test")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
