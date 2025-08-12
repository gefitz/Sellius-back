using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sellius.API.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Logins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EmpresaId",
                table: "Logins",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Empresas_EmpresaId",
                table: "Logins",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Empresas_EmpresaId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_EmpresaId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Logins");
        }
    }
}
