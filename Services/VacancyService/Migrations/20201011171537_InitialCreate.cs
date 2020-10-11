using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Studfolio.VacancyService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    SalaryFrom = table.Column<int>(nullable: false),
                    SalaryTo = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Remote = table.Column<bool>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    FullTime = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacancies");
        }
    }
}
