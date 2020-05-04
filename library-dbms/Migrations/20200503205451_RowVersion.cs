using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace library_dbms.Migrations
{
    public partial class RowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asset",
                columns: table => new
                {
                    asset_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category = table.Column<string>(maxLength: 50, nullable: true),
                    barcode_num = table.Column<string>(maxLength: 12, nullable: true),
                    manufacturer = table.Column<string>(maxLength: 50, nullable: true),
                    model_num = table.Column<string>(maxLength: 50, nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    notes = table.Column<string>(maxLength: 255, nullable: true),
                    asset_status = table.Column<string>(maxLength: 50, nullable: true),
                    serial_num = table.Column<string>(maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset", x => x.asset_id);
                });

            migrationBuilder.CreateTable(
                name: "asset_category",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("asset_category$PrimaryKey", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    department_num = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_name = table.Column<string>(maxLength: 50, nullable: false),
                    phone_num = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("department$Index_8E4A7CB3_8A33_47A1", x => x.department_num);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "supplied_by",
                columns: table => new
                {
                    asset_id = table.Column<int>(nullable: false),
                    vendor_id = table.Column<int>(nullable: false),
                    purchase_price = table.Column<decimal>(type: "money", nullable: true),
                    purchase_date = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("supplied_by$Index_7D9EE7AC_5C9F_44CB", x => x.asset_id);
                });

            migrationBuilder.CreateTable(
                name: "vendor",
                columns: table => new
                {
                    vendor_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(maxLength: 255, nullable: true),
                    company_name = table.Column<string>(maxLength: 255, nullable: false),
                    phone_num = table.Column<string>(maxLength: 12, nullable: true),
                    website = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendor", x => x.vendor_id);
                });

            migrationBuilder.CreateTable(
                name: "asset_location",
                columns: table => new
                {
                    asset_id = table.Column<int>(nullable: false),
                    building_num = table.Column<string>(maxLength: 4, nullable: true),
                    room_num = table.Column<string>(maxLength: 4, nullable: true),
                    notes = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("asset_location$Index_F196FE77_CD58_49B2", x => x.asset_id);
                    table.ForeignKey(
                        name: "asset_location$Rel_3DFF26E2_E4F6_4FD4",
                        column: x => x.asset_id,
                        principalTable: "asset",
                        principalColumn: "asset_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "maintenance_log",
                columns: table => new
                {
                    log_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    asset_id = table.Column<int>(nullable: false),
                    date_performed = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    hours_logged = table.Column<int>(nullable: true),
                    description = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("maintenance_log$Index_817AC782_CA89_4215", x => x.log_id);
                    table.ForeignKey(
                        name: "maintenance_log$Rel_B29742EB_3B1B_4EBC",
                        column: x => x.asset_id,
                        principalTable: "asset",
                        principalColumn: "asset_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "assigned_to_dep",
                columns: table => new
                {
                    asset_id = table.Column<int>(nullable: false),
                    department_num = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("assigned_to_dep$Index_E9BFA968_9E1B_44C8", x => x.asset_id);
                    table.ForeignKey(
                        name: "assigned_to_dep$Rel_64E24362_A635_469B",
                        column: x => x.asset_id,
                        principalTable: "asset",
                        principalColumn: "asset_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "assigned_to_dep$Rel_D5117F10_A9F9_4356",
                        column: x => x.department_num,
                        principalTable: "department",
                        principalColumn: "department_num",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "department_location",
                columns: table => new
                {
                    department_num = table.Column<int>(nullable: false),
                    building_num = table.Column<string>(maxLength: 4, nullable: false),
                    room_num = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("department_location$Index_32791611_745F_46B1", x => new { x.department_num, x.building_num, x.room_num });
                    table.ForeignKey(
                        name: "department_location$Rel_7E770B8A_9D8E_4B3C",
                        column: x => x.department_num,
                        principalTable: "department",
                        principalColumn: "department_num",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_num = table.Column<int>(nullable: true),
                    email = table.Column<string>(maxLength: 255, nullable: true),
                    first_name = table.Column<string>(maxLength: 255, nullable: false),
                    last_name = table.Column<string>(maxLength: 255, nullable: false),
                    phone_num = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_id);
                    table.ForeignKey(
                        name: "employee$Rel_11CE5EBA_BA9C_4D08",
                        column: x => x.department_num,
                        principalTable: "department",
                        principalColumn: "department_num",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sales_rep",
                columns: table => new
                {
                    rep_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vendor_id = table.Column<int>(nullable: false),
                    email = table.Column<string>(maxLength: 255, nullable: true),
                    first_name = table.Column<string>(maxLength: 255, nullable: false),
                    last_name = table.Column<string>(maxLength: 255, nullable: false),
                    phone_num = table.Column<string>(maxLength: 12, nullable: true),
                    ext = table.Column<string>(maxLength: 4, nullable: true),
                    title = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sales_rep$Index_F7ADC6BE_30BB_4E7A", x => x.rep_id);
                    table.ForeignKey(
                        name: "sales_rep$Rel_BD8B7D81_23F8_4C13",
                        column: x => x.vendor_id,
                        principalTable: "vendor",
                        principalColumn: "vendor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "performed_by",
                columns: table => new
                {
                    employee_id = table.Column<int>(nullable: false),
                    log_id = table.Column<int>(nullable: false),
                    total_cost = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("performed_by$Index_F5E8F2D0_A1AB_4CC2", x => new { x.employee_id, x.log_id });
                    table.ForeignKey(
                        name: "performed_by$Rel_2E859975_9CD4_4647",
                        column: x => x.log_id,
                        principalTable: "maintenance_log",
                        principalColumn: "log_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "assigned_to_emp",
                columns: table => new
                {
                    asset_id = table.Column<int>(nullable: false),
                    employee_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("assigned_to_emp$Index_4143E321_EFA4_4B1B", x => x.asset_id);
                    table.ForeignKey(
                        name: "assigned_to_emp$Rel_9293AE47_A97C_44FE",
                        column: x => x.asset_id,
                        principalTable: "asset",
                        principalColumn: "asset_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "assigned_to_emp$Rel_86004752_E8F8_4801",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employee_location",
                columns: table => new
                {
                    employee_id = table.Column<int>(nullable: false),
                    building_num = table.Column<string>(maxLength: 4, nullable: false),
                    room_num = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_location$Index_8BC48910_8257_4055", x => new { x.employee_id, x.building_num, x.room_num });
                    table.ForeignKey(
                        name: "employee_location$Rel_5E0778F4_6BF9_4E3C",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lts_staff",
                columns: table => new
                {
                    employee_id = table.Column<int>(nullable: false),
                    labor_cost = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("lts_staff$Index_EAEF5A50_4D82_4BDB", x => x.employee_id);
                    table.ForeignKey(
                        name: "lts_staff$Rel_C2845076_B61A_423F",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "asset_location$Rel_3DFF26E2_E4F6_4FD4",
                table: "asset_location",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "assigned_to_dep$Rel_64E24362_A635_469B",
                table: "assigned_to_dep",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "assigned_to_dep$Rel_D5117F10_A9F9_4356",
                table: "assigned_to_dep",
                column: "department_num");

            migrationBuilder.CreateIndex(
                name: "assigned_to_emp$Rel_9293AE47_A97C_44FE",
                table: "assigned_to_emp",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "assigned_to_emp$Rel_86004752_E8F8_4801",
                table: "assigned_to_emp",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "department_location$Rel_7E770B8A_9D8E_4B3C",
                table: "department_location",
                column: "department_num");

            migrationBuilder.CreateIndex(
                name: "employee$Rel_11CE5EBA_BA9C_4D08",
                table: "employee",
                column: "department_num");

            migrationBuilder.CreateIndex(
                name: "employee_location$Rel_5E0778F4_6BF9_4E3C",
                table: "employee_location",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "lts_staff$Rel_C2845076_B61A_423F",
                table: "lts_staff",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "maintenance_log$Rel_B29742EB_3B1B_4EBC",
                table: "maintenance_log",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "performed_by$Rel_2E859975_9CD4_4647",
                table: "performed_by",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "sales_rep$Rel_BD8B7D81_23F8_4C13",
                table: "sales_rep",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "supplied_by$vendor_id",
                table: "supplied_by",
                column: "vendor_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asset_category");

            migrationBuilder.DropTable(
                name: "asset_location");

            migrationBuilder.DropTable(
                name: "assigned_to_dep");

            migrationBuilder.DropTable(
                name: "assigned_to_emp");

            migrationBuilder.DropTable(
                name: "department_location");

            migrationBuilder.DropTable(
                name: "employee_location");

            migrationBuilder.DropTable(
                name: "lts_staff");

            migrationBuilder.DropTable(
                name: "performed_by");

            migrationBuilder.DropTable(
                name: "sales_rep");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "supplied_by");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "maintenance_log");

            migrationBuilder.DropTable(
                name: "vendor");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "asset");
        }
    }
}
