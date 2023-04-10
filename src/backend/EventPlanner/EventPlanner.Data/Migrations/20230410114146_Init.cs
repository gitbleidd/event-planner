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
                name: "event_type",
                schema: "content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
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
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event",
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
                    table.PrimaryKey("pk_event", x => x.id);
                    table.ForeignKey(
                        name: "fk_event_event_types_type_id",
                        column: x => x.type_id,
                        principalSchema: "content",
                        principalTable: "event_type",
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
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_participant",
                schema: "content",
                columns: table => new
                {
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_participant", x => new { x.event_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_event_participant_event_event_id",
                        column: x => x.event_id,
                        principalSchema: "content",
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_participant_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "content",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_registered_user",
                schema: "content",
                columns: table => new
                {
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    extra_slots_per_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_registered_user", x => new { x.event_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_event_registered_user_event_event_id",
                        column: x => x.event_id,
                        principalSchema: "content",
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_registered_user_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "content",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_admins_user_id",
                schema: "content",
                table: "admins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_type_id",
                schema: "content",
                table: "event",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_participant_user_id",
                schema: "content",
                table: "event_participant",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_registered_user_user_id",
                schema: "content",
                table: "event_registered_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                schema: "content",
                table: "user",
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
                name: "event_participant",
                schema: "content");

            migrationBuilder.DropTable(
                name: "event_registered_user",
                schema: "content");

            migrationBuilder.DropTable(
                name: "event",
                schema: "content");

            migrationBuilder.DropTable(
                name: "user",
                schema: "content");

            migrationBuilder.DropTable(
                name: "event_type",
                schema: "content");
        }
    }
}
