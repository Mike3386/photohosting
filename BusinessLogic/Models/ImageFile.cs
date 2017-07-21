using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public enum ImageStatus { PUBLIC = 1, PRIVATE = 2}

    public class ImageFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public ImageStatus Status { get; set; } 

        public ImageFile()
        {
            Status = ImageStatus.PUBLIC;
        }
    }
}
