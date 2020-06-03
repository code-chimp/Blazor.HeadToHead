using Microsoft.EntityFrameworkCore.Migrations;

namespace H2H.DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName", "Location" },
                values: new object[,]
                {
                    { 1, "J.R.R.", "Tolkien", "London, GB" },
                    { 2, "George R.R.", "Martin", "Not Writing" },
                    { 3, "Stephen", "King", "Derry, ME" }
                });

            migrationBuilder.InsertData(
                table: "BookDetails",
                columns: new[] { "Id", "Description", "NumberOfChapters", "NumberOfPages", "Weight" },
                values: new object[] { 1, "Lovely tome about the thing you wanted to read", 12, 218, 0.73000001907348633 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Science Fiction" },
                    { 2, "Fantasy" },
                    { 3, "Young Adult (YA)" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wackt" },
                    { 2, "Cyman and Feister" },
                    { 3, "O'Really" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "BookDetailId", "ISBN", "Price", "PublisherId", "Title" },
                values: new object[] { 1, 1, "999-88-3456-000", 22.149999618530273, 1, "Book #1" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "BookDetailId", "ISBN", "Price", "PublisherId", "Title" },
                values: new object[] { 2, null, "999-88-3456-018", 18.329999923706055, 2, "Book #2" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "BookDetailId", "ISBN", "Price", "PublisherId", "Title" },
                values: new object[] { 3, null, "999-88-3456-120", 14.119999885559082, 3, "Book #3" });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" },
                values: new object[] { 3, 1 });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" },
                values: new object[] { 3, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
