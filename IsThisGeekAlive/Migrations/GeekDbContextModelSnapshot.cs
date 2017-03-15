using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IsThisGeekAlive.Data;

namespace IsThisGeekAlive.Migrations
{
    [DbContext(typeof(GeekDbContext))]
    partial class GeekDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("IsThisGeekAlive.Models.Geek", b =>
                {
                    b.Property<int>("GeekId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastActivityLocalTime")
                        .HasColumnType("TIMESTAMP");

                    b.Property<short>("LastActivityLocalTimeUtcOffset");

                    b.Property<DateTime>("LastActivityServerTime")
                        .HasColumnType("TIMESTAMP");

                    b.Property<short>("LastActivityServerTimeUtcOffset");

                    b.Property<string>("LoginCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
