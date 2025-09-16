using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registro.Migrations
{
    /// <inheritdoc />
    public partial class Jugador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Jugadores_TurnoJugadorId",
                table: "Partidas");

            migrationBuilder.RenameColumn(
                name: "Partidas",
                table: "Jugadores",
                newName: "Victorias");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Jugadores_TurnoJugadorId",
                table: "Partidas",
                column: "TurnoJugadorId",
                principalTable: "Jugadores",
                principalColumn: "JugadorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Jugadores_TurnoJugadorId",
                table: "Partidas");

            migrationBuilder.RenameColumn(
                name: "Victorias",
                table: "Jugadores",
                newName: "Partidas");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Jugadores_TurnoJugadorId",
                table: "Partidas",
                column: "TurnoJugadorId",
                principalTable: "Jugadores",
                principalColumn: "JugadorId");
        }
    }
}
