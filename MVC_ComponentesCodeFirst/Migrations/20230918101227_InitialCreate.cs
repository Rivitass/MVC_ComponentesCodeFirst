using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC_ComponentesCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordenadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenadores_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Componentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDeSerie = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Calor = table.Column<int>(type: "int", nullable: false),
                    Megas = table.Column<long>(type: "bigint", nullable: false),
                    Cores = table.Column<int>(type: "int", nullable: false),
                    Coste = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    OrdenadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componentes_Ordenadores_OrdenadorId",
                        column: x => x.OrdenadorId,
                        principalTable: "Ordenadores",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Componentes",
                columns: new[] { "Id", "Calor", "Cores", "Coste", "Descripcion", "Megas", "NumeroDeSerie", "OrdenadorId", "Tipo" },
                values: new object[,]
                {
                    { 1, 10, 9, 134, "Procesador Intel i7", 0L, "789-XCS", null, 0 },
                    { 2, 12, 10, 138, "Procesador Intel i7", 0L, "789-XCD", null, 0 },
                    { 3, 22, 11, 138, "Procesador Intel i7", 0L, "789-XCT", null, 0 },
                    { 4, 10, 0, 100, "Banco de Memoria SDRAM", 512L, "879FH", null, 1 },
                    { 5, 15, 0, 125, "Banco de Memoria SDRAM", 1024L, "879FH-L", null, 1 },
                    { 6, 24, 0, 150, "Banco de Memoria SDRAM", 1024L, "879FH-T", null, 1 },
                    { 7, 10, 0, 50, "DiscoDuro SanDisk", 512000L, "789-XX", null, 2 },
                    { 8, 29, 0, 90, "DiscoDuro SanDisk", 1024000L, "789-XX-2", null, 2 },
                    { 9, 39, 0, 128, "DiscoDuro SanDisk", 2048000L, "789-XX-3", null, 2 },
                    { 10, 30, 10, 78, "Procesador Ryzen AMD", 0L, "797-X", null, 0 },
                    { 11, 30, 29, 178, "Procesador Ryzen AMD", 0L, "797-X2", null, 0 },
                    { 12, 60, 34, 278, "Procesador Ryzen AMD", 0L, "797-X3", null, 0 },
                    { 13, 35, 0, 37, "Disco Mecánico Patatin", 250L, "788-fg", null, 2 },
                    { 14, 35, 0, 67, "Disco Mecánico Patatin", 250L, "788-fg-2", null, 2 },
                    { 15, 35, 0, 97, "Disco Mecánico Patatin", 250L, "788-fg-3", null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Componentes_OrdenadorId",
                table: "Componentes",
                column: "OrdenadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenadores_PedidoId",
                table: "Ordenadores",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Componentes");

            migrationBuilder.DropTable(
                name: "Ordenadores");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
