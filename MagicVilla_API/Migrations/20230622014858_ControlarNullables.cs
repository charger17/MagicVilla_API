using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class ControlarNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Detalle",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Amenidad",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DetallesEspeciales",
                table: "NumeroVillas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5863), new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5851) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5865), new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5865) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Detalle",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amenidad",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DetallesEspeciales",
                table: "NumeroVillas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4835), new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4825) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4837), new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4837) });
        }
    }
}
