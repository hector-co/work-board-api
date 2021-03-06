﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkBoard.DataAccess.Ef.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Board",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Veryfied = table.Column<bool>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardColumn",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardColumn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardColumn_Board_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "dbo",
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoardUser",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardUser", x => new { x.UserId, x.BoardId });
                    table.ForeignKey(
                        name: "FK_BoardUser_Board_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "dbo",
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardUser_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    EstimatedPoints = table.Column<float>(nullable: false),
                    ConsumedPoints = table.Column<float>(nullable: false),
                    Done = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    BoardId = table.Column<int>(nullable: false),
                    ColumnId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Board_BoardId",
                        column: x => x.BoardId,
                        principalSchema: "dbo",
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Card_BoardColumn_ColumnId",
                        column: x => x.ColumnId,
                        principalSchema: "dbo",
                        principalTable: "BoardColumn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardUser",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardUser", x => new { x.UserId, x.CardId });
                    table.ForeignKey(
                        name: "FK_CardUser_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "dbo",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardUser_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardColumn_BoardId",
                schema: "dbo",
                table: "BoardColumn",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_BoardId",
                schema: "dbo",
                table: "BoardUser",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_BoardId",
                schema: "dbo",
                table: "Card",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_ColumnId",
                schema: "dbo",
                table: "Card",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_CardUser_CardId",
                schema: "dbo",
                table: "CardUser",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CardUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Card",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BoardColumn",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Board",
                schema: "dbo");
        }
    }
}
