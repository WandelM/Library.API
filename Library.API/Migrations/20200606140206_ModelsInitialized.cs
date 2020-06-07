using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class ModelsInitialized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Surname = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfDeath = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicationHouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationHouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ISBN = table.Column<string>(fixedLength: true, maxLength: 13, nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    PublicationType = table.Column<int>(nullable: false),
                    PublicationHouseId = table.Column<Guid>(nullable: false),
                    PageCount = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_PublicationHouses_PublicationHouseId",
                        column: x => x.PublicationHouseId,
                        principalTable: "PublicationHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationAuthors",
                columns: table => new
                {
                    PublicationId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationAuthors", x => new { x.PublicationId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_PublicationAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationAuthors_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationCategories",
                columns: table => new
                {
                    PublicationId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationCategories", x => new { x.PublicationId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PublicationCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationCategories_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicationAuthors_AuthorId",
                table: "PublicationAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationCategories_CategoryId",
                table: "PublicationCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_PublicationHouseId",
                table: "Publications",
                column: "PublicationHouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationAuthors");

            migrationBuilder.DropTable(
                name: "PublicationCategories");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "PublicationHouses");
        }
    }
}
