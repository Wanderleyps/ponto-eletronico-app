using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoEletronico.Infra.Data.Migrations
{
    public partial class ReferenciaFuncionarioUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Funcionarios",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Funcionarios");
        }
    }
}
