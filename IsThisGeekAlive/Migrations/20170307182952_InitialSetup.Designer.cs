using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IsThisGeekAlive.Data;

namespace IsThisGeekAlive.Migrations
{
    [DbContext(typeof(GeekDbContext))]
    [Migration("20170307182952_InitialSetup")]
    partial class InitialSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("IsThisGeekAlive.Models.Geek", b =>
                {
                    b.Property<int>("GeekId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("LastActivityLocalTime");

                    b.Property<DateTimeOffset>("LastActivityServerTime");

                    b.Property<int>("NotAliveDangerWindow");

                    b.Property<int>("NotAliveWarningWindow");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("UsernameLower")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.HasKey("GeekId");

                    b.HasIndex("UsernameLower")
                        .HasName("Geek_UsernameLower");

                    b.ToTable("Geeks");
                });
        }
    }
}
