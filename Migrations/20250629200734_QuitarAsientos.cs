using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCine.Migrations
{
    /// <inheritdoc />
    public partial class QuitarAsientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservasXAsientos_Asientos_IdAsiento",
                table: "ReservasXAsientos");

            migrationBuilder.DropTable(
                name: "Asientos");

            migrationBuilder.DropIndex(
                name: "IX_ReservasXAsientos_IdAsiento",
                table: "ReservasXAsientos");

            migrationBuilder.DropColumn(
                name: "IdAsiento",
                table: "ReservasXAsientos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAsiento",
                table: "ReservasXAsientos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Asientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdSala = table.Column<int>(type: "int", nullable: false),
                    Columna = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fila = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    swt = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    usuario_creacion = table.Column<int>(type: "int", nullable: false),
                    usuario_modificacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asientos_Salas_IdSala",
                        column: x => x.IdSala,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasXAsientos_IdAsiento",
                table: "ReservasXAsientos",
                column: "IdAsiento");

            migrationBuilder.CreateIndex(
                name: "IX_Asientos_IdSala",
                table: "Asientos",
                column: "IdSala");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservasXAsientos_Asientos_IdAsiento",
                table: "ReservasXAsientos",
                column: "IdAsiento",
                principalTable: "Asientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
