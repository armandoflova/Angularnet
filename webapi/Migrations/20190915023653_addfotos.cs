using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class addfotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Usuarios_UsuarioID",
                table: "Foto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foto",
                table: "Foto");

            migrationBuilder.RenameTable(
                name: "Foto",
                newName: "Fotos");

            migrationBuilder.RenameIndex(
                name: "IX_Foto_UsuarioID",
                table: "Fotos",
                newName: "IX_Fotos_UsuarioID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fotos",
                table: "Fotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Usuarios_UsuarioID",
                table: "Fotos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Usuarios_UsuarioID",
                table: "Fotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fotos",
                table: "Fotos");

            migrationBuilder.RenameTable(
                name: "Fotos",
                newName: "Foto");

            migrationBuilder.RenameIndex(
                name: "IX_Fotos_UsuarioID",
                table: "Foto",
                newName: "IX_Foto_UsuarioID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foto",
                table: "Foto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Usuarios_UsuarioID",
                table: "Foto",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
