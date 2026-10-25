using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neamtu_Marian_lab2.Migrations
{
    public partial class AddAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorsID",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    ID = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    FirstName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    LastName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.ID);
                });

           
            migrationBuilder.Sql(@"
                INSERT INTO Authors (FirstName, LastName)
                SELECT
                    LEFT(AuthorName, CHARINDEX(' ', AuthorName + ' ') - 1),
                    LTRIM(SUBSTRING(
                        AuthorName,
                        CHARINDEX(' ', AuthorName + ' ') + 1,
                        LEN(AuthorName)
                    ))
                FROM (
                    SELECT DISTINCT LTRIM(RTRIM(Author)) AS AuthorName
                    FROM Book
                    WHERE Author IS NOT NULL
                      AND LTRIM(RTRIM(Author)) <> ''
                ) AS ExistingAuthors;
            ");

            migrationBuilder.Sql(@"
                UPDATE b
                SET b.AuthorsID = a.ID
                FROM Book AS b
                INNER JOIN Authors AS a
                    ON LTRIM(RTRIM(b.Author)) =
                       a.FirstName + ' ' + a.LastName;
            ");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorsID",
                table: "Book",
                column: "AuthorsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_AuthorsID",
                table: "Book",
                column: "AuthorsID",
                principalTable: "Authors",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_AuthorsID",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                UPDATE b
                SET b.Author = a.FirstName + ' ' + a.LastName
                FROM Book AS b
                INNER JOIN Authors AS a
                    ON b.AuthorsID = a.ID;
            ");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorsID",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorsID",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}