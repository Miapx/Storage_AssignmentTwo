using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Customers_CustomerEntityId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerEntityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerEntityId",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerEntityId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerEntityId",
                table: "Customers",
                column: "CustomerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Customers_CustomerEntityId",
                table: "Customers",
                column: "CustomerEntityId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
