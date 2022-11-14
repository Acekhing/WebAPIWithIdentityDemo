using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPIWithIdentityDemo.Migrations
{
    /// <inheritdoc />
    public partial class CreatingIdentityScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41215637-92c0-4e03-b3a0-8eae91b84059");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8dd219a-8be9-4202-a6a3-0b248515e171");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2628219-f5d9-4509-b8ab-74d66ac9bb44");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15227601-bc9f-4048-97c0-21f391236981", "6c3cfc87-7625-4da1-9e8a-3a12620d4d77", "User", "USER" },
                    { "6e0b3db1-a23a-4670-9417-ff948363a727", "43f78219-c826-4e91-a5c3-f3822daa5b0c", "Admin", "ADMIN" },
                    { "887cd3b4-4842-4aed-ad19-0cbc11de1605", "0deb959e-3569-4563-ba87-633e7effd540", "Staff", "STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15227601-bc9f-4048-97c0-21f391236981");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e0b3db1-a23a-4670-9417-ff948363a727");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "887cd3b4-4842-4aed-ad19-0cbc11de1605");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41215637-92c0-4e03-b3a0-8eae91b84059", "e33900e5-f45d-4183-b8e0-66e095fc9dc7", "Staff", "STAFF" },
                    { "c8dd219a-8be9-4202-a6a3-0b248515e171", "cfb029ca-42ff-4153-b181-3d20ec36e4b1", "Admin", "ADMIN" },
                    { "d2628219-f5d9-4509-b8ab-74d66ac9bb44", "0d2329b4-74a3-454d-ab5e-c22d16e321d7", "User", "USER" }
                });
        }
    }
}
