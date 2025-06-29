using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCine.Migrations
{
    /// <inheritdoc />
    public partial class ControlUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "GenerosXPeliculas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_modificacion",
                table: "GenerosXPeliculas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "swt",
                table: "GenerosXPeliculas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "usuario_creacion",
                table: "GenerosXPeliculas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_modificacion",
                table: "GenerosXPeliculas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "Clientes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_modificacion",
                table: "Clientes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "swt",
                table: "Clientes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "usuario_creacion",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuario_modificacion",
                table: "Clientes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "GenerosXPeliculas");

            migrationBuilder.DropColumn(
                name: "fecha_modificacion",
                table: "GenerosXPeliculas");

            migrationBuilder.DropColumn(
                name: "swt",
                table: "GenerosXPeliculas");

            migrationBuilder.DropColumn(
                name: "usuario_creacion",
                table: "GenerosXPeliculas");

            migrationBuilder.DropColumn(
                name: "usuario_modificacion",
                table: "GenerosXPeliculas");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "fecha_modificacion",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "swt",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "usuario_creacion",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "usuario_modificacion",
                table: "Clientes");
        }
    }
}
