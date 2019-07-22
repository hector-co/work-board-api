﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkBoard.DataAccess.Ef;

namespace WorkBoard.DataAccess.Ef.Migrations
{
    [DbContext(typeof(WorkBoardContext))]
    [Migration("20190722024817_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorkBoard.Application.Dtos.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<Guid>("Guid");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.Property<int>("Version");

                    b.Property<bool>("Veryfied");

                    b.HasKey("Id");

                    b.ToTable("User","dbo");
                });

            modelBuilder.Entity("WorkBoard.DataAccess.Ef.BoardColumnDataAccess.BoardColumnDtoDataAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("BoardId");

                    b.Property<string>("Description");

                    b.Property<Guid>("Guid");

                    b.Property<int>("Order");

                    b.Property<string>("Title");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("BoardColumn","dbo");
                });

            modelBuilder.Entity("WorkBoard.DataAccess.Ef.BoardDataAccess.BoardDtoDataAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<Guid>("Guid");

                    b.Property<int>("State");

                    b.Property<string>("Title");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Board","dbo");

                    b.HasDiscriminator().HasValue("BoardDtoDataAccess");
                });

            modelBuilder.Entity("WorkBoard.DataAccess.Ef.BoardDataAccess.BoardDtoDataAccessUserDto", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("BoardId");

                    b.HasKey("UserId", "BoardId");

                    b.HasIndex("BoardId");

                    b.ToTable("BoardUser","dbo");
                });

            modelBuilder.Entity("WorkBoard.DataAccess.Ef.BoardColumnDataAccess.BoardColumnDtoDataAccess", b =>
                {
                    b.HasOne("WorkBoard.DataAccess.Ef.BoardDataAccess.BoardDtoDataAccess", "BoardDataAccess")
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkBoard.DataAccess.Ef.BoardDataAccess.BoardDtoDataAccessUserDto", b =>
                {
                    b.HasOne("WorkBoard.DataAccess.Ef.BoardDataAccess.BoardDtoDataAccess", "Board")
                        .WithMany("UsersDataAccess")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkBoard.Application.Dtos.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
