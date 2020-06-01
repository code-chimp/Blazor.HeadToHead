using Microsoft.EntityFrameworkCore.Migrations;

namespace H2H.DataAccess.Migrations
{
    public partial class AddView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.GetOnlyBookDetails
                AS
                SELECT b.ISBN, b.Title, b.Price from dbo.Books b
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.GetOnlyBookDetails");
        }
    }
}
