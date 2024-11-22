using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ms_storage_location",
                columns: table => new
                {
                    location_id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    location_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ms_stora__771831EA8B61A42D", x => x.location_id);
                });

            migrationBuilder.CreateTable(
                name: "ms_user",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    user_name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ms_user__B9BE370F9187C927", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tr_bpkb",
                columns: table => new
                {
                    agreement_number = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    bpkb_no = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    branch_id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    bpkb_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    faktur_no = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    faktur_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    location_id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    police_no = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    bpkb_date_in = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_updated_by = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    last_updated_on = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tr_bpkb__21912C808EF894A5", x => x.agreement_number);
                    table.ForeignKey(
                        name: "FK__tr_bpkb__locatio__286302EC",
                        column: x => x.location_id,
                        principalTable: "ms_storage_location",
                        principalColumn: "location_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tr_bpkb_location_id",
                table: "tr_bpkb",
                column: "location_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ms_user");

            migrationBuilder.DropTable(
                name: "tr_bpkb");

            migrationBuilder.DropTable(
                name: "ms_storage_location");
        }
    }
}
