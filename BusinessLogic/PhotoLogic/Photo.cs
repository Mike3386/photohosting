using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BusinessLogic.PhotoLogic
{
    public class PhotoLogic
    {
        private IHostingEnvironment _appEnvironment;
        private ImageContext _context;
        private IFormFile _file { get; set; }

        public PhotoLogic(IHostingEnvironment env, ImageContext context)
        {
            _appEnvironment = env;
            _context = context;
        }
        
        public async Task<ActionResult> SaveImageAsync(IFormFile image)
        {
            _file = image;
            if (_file != null)
            {
                var name = _file.FileName.Substring(0, _file.FileName.LastIndexOf('.'));
                var extension = _file.FileName.Substring(_file.FileName.LastIndexOf('.'), _file.FileName.Length - _file.FileName.LastIndexOf('.'));

                var im = await _context.ImageFile.FirstOrDefaultAsync(p => p.Name.Equals(name));
                if (im == null)
                {
                    string path = _appEnvironment.ContentRootPath + "/Files/" + _file.FileName;

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await _file.CopyToAsync(fileStream);
                    }

                    _context.ImageFile.Add(new ImageFile() { Name = name, Extension = extension, ContentType = _file.ContentType });
                    _context.SaveChanges();
                    return new OkObjectResult("created");
                }
                else return new BadRequestObjectResult("Already exists");
            }
            else return new BadRequestObjectResult("File param not exists");
        }

        public async Task<ActionResult> ReadImage(string name)
        {
            var imageFile = await _context.ImageFile.FirstOrDefaultAsync(p => p.Name.Equals(name));
            if (imageFile != null)
            {
                var dir = _appEnvironment.ContentRootPath + "/Files";
                var path = Path.Combine(dir, imageFile.Name + imageFile.Extension);
                FileStream stream = new FileStream(path, FileMode.Open);

                return new FileStreamResult(stream, imageFile.ContentType)
                {
                    FileDownloadName = imageFile.Name
                };
            }
            else return new NotFoundResult();

        }
    }
}
