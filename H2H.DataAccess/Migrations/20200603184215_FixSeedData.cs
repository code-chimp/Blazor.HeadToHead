using Microsoft.EntityFrameworkCore.Migrations;

namespace H2H.DataAccess.Migrations
{
    public partial class FixSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 0.72999999999999998);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 22.149999999999999);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 18.329999999999998);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 14.119999999999999);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 0.73000001907348633);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 22.149999618530273);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 18.329999923706055);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 14.119999885559082);
        }
    }
}
