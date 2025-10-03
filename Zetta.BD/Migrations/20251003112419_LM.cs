using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zetta.BD.Migrations
{
    /// <inheritdoc />
    public partial class LM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresItemDetalles_ItemsPresupuesto_ItemPresupuestoId",
                table: "PresItemDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Presupuestos_Cliente_ClienteId",
                table: "Presupuestos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsPresupuesto",
                table: "ItemsPresupuesto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "ItemsPresupuesto",
                newName: "ItemPresupuestos");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPresupuestos",
                table: "ItemPresupuestos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Obras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoObra = table.Column<int>(type: "int", nullable: false),
                    PresupuestoId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obras_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obras_Presupuestos_PresupuestoId",
                        column: x => x.PresupuestoId,
                        principalTable: "Presupuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obras_ClienteId",
                table: "Obras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_PresupuestoId",
                table: "Obras",
                column: "PresupuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresItemDetalles_ItemPresupuestos_ItemPresupuestoId",
                table: "PresItemDetalles",
                column: "ItemPresupuestoId",
                principalTable: "ItemPresupuestos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuestos_Clientes_ClienteId",
                table: "Presupuestos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresItemDetalles_ItemPresupuestos_ItemPresupuestoId",
                table: "PresItemDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Presupuestos_Clientes_ClienteId",
                table: "Presupuestos");

            migrationBuilder.DropTable(
                name: "Obras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPresupuestos",
                table: "ItemPresupuestos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "ItemPresupuestos",
                newName: "ItemsPresupuesto");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsPresupuesto",
                table: "ItemsPresupuesto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PresItemDetalles_ItemsPresupuesto_ItemPresupuestoId",
                table: "PresItemDetalles",
                column: "ItemPresupuestoId",
                principalTable: "ItemsPresupuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuestos_Cliente_ClienteId",
                table: "Presupuestos",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
