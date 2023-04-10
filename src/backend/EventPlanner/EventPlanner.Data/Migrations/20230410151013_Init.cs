using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventPlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "content");

            migrationBuilder.CreateTable(
                name: "event_types",
                schema: "content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                schema: "content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    organizer_name = table.Column<string>(type: "text", nullable: false),
                    location_name = table.Column<string>(type: "text", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    begin_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    registration_end_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    slots = table.Column<int>(type: "integer", nullable: true),
                    extra_slots_per_user = table.Column<int>(type: "integer", nullable: false),
                    resources = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_events_event_types_type_id",
                        column: x => x.type_id,
                        principalSchema: "content",
                        principalTable: "event_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "admins",
                schema: "content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admins", x => x.id);
                    table.ForeignKey(
                        name: "fk_admins_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "content",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_users",
                schema: "content",
                columns: table => new
                {
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    is_participating = table.Column<bool>(type: "boolean", nullable: false),
                    taken_extra_users_count = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_users", x => new { x.event_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_event_users_events_event_id",
                        column: x => x.event_id,
                        principalSchema: "content",
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_users_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "content",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_admins_user_id",
                schema: "content",
                table: "admins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_users_user_id",
                schema: "content",
                table: "event_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_type_id",
                schema: "content",
                table: "events",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "content",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins",
                schema: "content");

            migrationBuilder.DropTable(
                name: "event_users",
                schema: "content");

            migrationBuilder.DropTable(
                name: "events",
                schema: "content");

            migrationBuilder.DropTable(
                name: "users",
                schema: "content");

            migrationBuilder.DropTable(
                name: "event_types",
                schema: "content");
        }
    }
}
