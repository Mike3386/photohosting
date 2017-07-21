using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using log4net.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Photohosting.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(UsersController));

        [HttpGet]
        public string Get()
        {
            logger.Debug("test debug");
            return "aa";
        }
        
        [HttpGet("{name}")]
        public string Get(string name)
        {
            return name;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
