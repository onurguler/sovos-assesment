using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sovos.Invoicing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorInvoicePk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceId",
                table: "Invoices",
                column: "InvoiceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceId",
                table: "Invoices");
        }
    }
}
