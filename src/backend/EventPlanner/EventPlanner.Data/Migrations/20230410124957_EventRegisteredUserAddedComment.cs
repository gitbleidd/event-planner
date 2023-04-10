using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventRegisteredUserAddedComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "extra_slots_per_user",
                schema: "content",
                table: "event_registered_user",
                newName: "taken_extra_users_count");

            migrationBuilder.AddColumn<string>(
                name: "comment",
                schema: "content",
                table: "event_registered_user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comment",
                schema: "content",
                table: "event_registered_user");

            migrationBuilder.RenameColumn(
                name: "taken_extra_users_count",
                schema: "content",
                table: "event_registered_user",
                newName: "extra_slots_per_user");
        }
    }
}
