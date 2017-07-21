using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace BusinessLogic
{
    public class UserContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
    }

    public class ImageContext : DbContext
    {
        public DbSet<ImageFile> ImageFile { get; set; }
        public ImageContext(DbContextOptions<ImageContext> options)
            : base(options)
        {
        }
    }
}
