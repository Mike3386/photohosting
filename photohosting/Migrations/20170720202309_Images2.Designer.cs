using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BusinessLogic;
using BusinessLogic.Models;

namespace Photohosting.Migrations
{
    [DbContext(typeof(ImageContext))]
    [Migration("20170720202309_Images2")]
    partial class Images2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusinessLogic.Models.ImageFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("Extension");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("ImageFile");
                });
        }
    }
}
