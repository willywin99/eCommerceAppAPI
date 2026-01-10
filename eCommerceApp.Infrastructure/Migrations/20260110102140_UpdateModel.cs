using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f3c0ef4-5711-4cd8-a23e-53195710daff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7e0a6fa-2fa4-47c9-821d-f7bd03dadf5a");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("118ef4e5-6cf1-4ac3-8f30-1782e416a7f1"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ChecoutAchieves",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32a6dc6f-a59c-4147-b69c-15f5b8e38094", null, "Admin", "ADMIN" },
                    { "445b9b99-6b28-41da-9f90-d29a4388dfe2", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("2086d6a4-7162-483b-8d0b-14efd461027c"), "Credit Card" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32a6dc6f-a59c-4147-b69c-15f5b8e38094");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "445b9b99-6b28-41da-9f90-d29a4388dfe2");

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: new Guid("2086d6a4-7162-483b-8d0b-14efd461027c"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ChecoutAchieves",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f3c0ef4-5711-4cd8-a23e-53195710daff", null, "Admin", "ADMIN" },
                    { "b7e0a6fa-2fa4-47c9-821d-f7bd03dadf5a", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("118ef4e5-6cf1-4ac3-8f30-1782e416a7f1"), "Credit Card" });
        }
    }
}
