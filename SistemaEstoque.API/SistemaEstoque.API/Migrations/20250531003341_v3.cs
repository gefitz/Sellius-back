using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sellius.API.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cidades_Estados_id",
                table: "Cidades");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Logins");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Logins",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Cidades",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Cidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_ClienteId",
                table: "Logins",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_EstadoId",
                table: "Cidades",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cidades_Estados_EstadoId",
                table: "Cidades",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Clientes_ClienteId",
                table: "Logins",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cidades_Estados_EstadoId",
                table: "Cidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Clientes_ClienteId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_ClienteId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Cidades_EstadoId",
                table: "Cidades");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Cidades");

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Logins",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Cidades",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Cidades_Estados_id",
                table: "Cidades",
                column: "id",
                principalTable: "Estados",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
