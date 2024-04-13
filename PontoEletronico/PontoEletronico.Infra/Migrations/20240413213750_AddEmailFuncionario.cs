using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoEletronico.Infra.Data.Migrations
{
    public partial class AddEmailFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Funcionarios",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Funcionarios");
        }
    }
}
