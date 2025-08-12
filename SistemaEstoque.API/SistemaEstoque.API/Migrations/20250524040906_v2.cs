using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sellius.API.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CidadeId",
                table: "Fornecedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_CidadeId",
                table: "Fornecedores",
                column: "CidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Cidades_CidadeId",
                table: "Fornecedores",
                column: "CidadeId",
                principalTable: "Cidades",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Cidades_CidadeId",
                table: "Fornecedores");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_CidadeId",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "CidadeId",
                table: "Fornecedores");
        }
    }
}
