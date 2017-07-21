using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BusinessLogic.PhotoLogic;
using BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Photohosting.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller ///Odata controller, model mapper
    {
        private ImageContext _context;
        
        IHostingEnvironment _appEnvironment;
        PhotoLogic logic;

        public ImagesController(IHostingEnvironment appEnvironment, ImageContext context)
        {
            _context = context;
            _appEnvironment = appEnvironment;

            logic = new PhotoLogic(appEnvironment, _context);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
            return await logic.ReadImage(name);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]IFormFile image)
        {
           return await logic.SaveImageAsync(image);
        }
    }
}
