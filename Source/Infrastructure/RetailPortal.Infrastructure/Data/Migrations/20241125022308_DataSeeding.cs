using Microsoft.EntityFrameworkCore.Migrations;
using RetailPortal.Infrastructure.Db.Sql;

#nullable disable

namespace RetailPortal.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlHelper.GetSqlFromFile("Tables", "Role", "v0"));
            migrationBuilder.Sql(SqlHelper.GetSqlFromFile("Tables", "Category", "v0"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Do nothing since this is an initial data seeding
        }
    }
}
