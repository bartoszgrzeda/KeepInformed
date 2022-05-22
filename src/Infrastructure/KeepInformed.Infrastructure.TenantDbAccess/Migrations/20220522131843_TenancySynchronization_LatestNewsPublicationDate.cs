using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeepInformed.Infrastructure.TenantDbAccess.Migrations
{
    public partial class TenancySynchronization_LatestNewsPublicationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Synchronizations",
                newName: "LatestNewsPublicationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LatestNewsPublicationDate",
                table: "Synchronizations",
                newName: "Date");
        }
    }
}
