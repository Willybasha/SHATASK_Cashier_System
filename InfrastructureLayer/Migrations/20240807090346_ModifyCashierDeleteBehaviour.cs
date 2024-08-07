using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    public partial class ModifyCashierDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHeader_Cashier",
                table: "InvoiceHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHeader_Cashier",
                table: "InvoiceHeader",
                column: "CashierID",
                principalTable: "Cashier",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceHeader_Cashier",
                table: "InvoiceHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceHeader_Cashier",
                table: "InvoiceHeader",
                column: "CashierID",
                principalTable: "Cashier",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
