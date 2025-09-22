using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbOperationWithCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Language",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Language",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CurrenciesId",
                table: "BookPrice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "United States Dollar", "USD" },
                    { 2, "Euro", "EUR" },
                    { 3, "British Pound", "GBP" },
                    { 4, "Indian INR", "INR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPrice_BookId",
                table: "BookPrice",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPrice_CurrenciesId",
                table: "BookPrice",
                column: "CurrenciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPrice_Books_BookId",
                table: "BookPrice",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPrice_Currencies_CurrenciesId",
                table: "BookPrice",
                column: "CurrenciesId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPrice_Books_BookId",
                table: "BookPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPrice_Currencies_CurrenciesId",
                table: "BookPrice");

            migrationBuilder.DropIndex(
                name: "IX_BookPrice_BookId",
                table: "BookPrice");

            migrationBuilder.DropIndex(
                name: "IX_BookPrice_CurrenciesId",
                table: "BookPrice");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "CurrenciesId",
                table: "BookPrice");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Language",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Description",
                table: "Language",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
