using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaPNT.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "libros",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    autor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    editorial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    año = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    genero = table.Column<int>(type: "int", nullable: false),
                    prestado = table.Column<bool>(type: "bit", nullable: false),
                    prestatario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libros", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "libros");
        }
    }
}
