using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SISRESERVAS.Migrations
{
    /// <inheritdoc />
    public partial class migra1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chofer",
                columns: table => new
                {
                    Idchofer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombrechofer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chofer", x => x.Idchofer);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    IdDep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.IdDep);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Viajes",
                columns: table => new
                {
                    idbus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombrebus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viajes", x => x.idbus);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    IdRes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    choferIdchofer = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    departamentoIdDep = table.Column<int>(type: "int", nullable: false),
                    busidbus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.IdRes);
                    table.ForeignKey(
                        name: "FK_Reservas_Departamentos_departamentoIdDep",
                        column: x => x.departamentoIdDep,
                        principalTable: "Departamentos",
                        principalColumn: "IdDep",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Viajes_busidbus",
                        column: x => x.busidbus,
                        principalTable: "Viajes",
                        principalColumn: "idbus",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_chofer_choferIdchofer",
                        column: x => x.choferIdchofer,
                        principalTable: "chofer",
                        principalColumn: "Idchofer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_busidbus",
                table: "Reservas",
                column: "busidbus");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_choferIdchofer",
                table: "Reservas",
                column: "choferIdchofer");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_departamentoIdDep",
                table: "Reservas",
                column: "departamentoIdDep");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_usuarioId",
                table: "Reservas",
                column: "usuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Viajes");

            migrationBuilder.DropTable(
                name: "chofer");
        }
    }
}
