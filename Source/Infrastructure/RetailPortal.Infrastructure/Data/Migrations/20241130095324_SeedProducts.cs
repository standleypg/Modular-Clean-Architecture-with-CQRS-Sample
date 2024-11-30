using Microsoft.EntityFrameworkCore.Migrations;
using RetailPortal.Infrastructure.Db.Sql;

#nullable disable

namespace RetailPortal.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlHelper.GetSqlFromFile("Common", "Seed", "v1"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Do nothing, this is just a seed migration
        }
    }
}
