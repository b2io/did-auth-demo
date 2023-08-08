using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DidAuthDemo.IssuerApi.Migrations
{
    /// <inheritdoc />
    public partial class TypeOnCredential : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "credential_schemas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "credential_schemas");
        }
    }
}
