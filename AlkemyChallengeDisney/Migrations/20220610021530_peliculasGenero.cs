using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlkemyChallengeDisney.Migrations
{
    public partial class peliculasGenero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Generos_GeneroId",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_GeneroId",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Peliculas");

            migrationBuilder.CreateTable(
                name: "GeneroPelicula",
                columns: table => new
                {
                    PeliculaListId = table.Column<int>(type: "int", nullable: false),
                    generosListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroPelicula", x => new { x.PeliculaListId, x.generosListId });
                    table.ForeignKey(
                        name: "FK_GeneroPelicula_Generos_generosListId",
                        column: x => x.generosListId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroPelicula_Peliculas_PeliculaListId",
                        column: x => x.PeliculaListId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneroPelicula_generosListId",
                table: "GeneroPelicula",
                column: "generosListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneroPelicula");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Peliculas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_GeneroId",
                table: "Peliculas",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Generos_GeneroId",
                table: "Peliculas",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id");
        }
    }
}
