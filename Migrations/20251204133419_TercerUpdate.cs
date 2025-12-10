using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientesAPI.Migrations
{
    /// <inheritdoc />
    public partial class TercerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaProgramada",
                table: "Reservas",
                newName: "FechaSalida");

            migrationBuilder.AddColumn<int>(
                name: "CantidadPersonas",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrada",
                table: "Reservas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Habitacion",
                table: "Reservas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPersonas",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "FechaEntrada",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Habitacion",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "FechaSalida",
                table: "Reservas",
                newName: "FechaProgramada");
        }
    }
}
