using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    emp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    emp_gender = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    emp_personal_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emp_work_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emp_nik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    emp_POB = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emp_DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    emp_marital_status = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    emp_st_address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    emp_city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emp_province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    emp_postal_code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    emp_phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.emp_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    personal_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    user_role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContact",
                columns: table => new
                {
                    emergency_contact_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    emergency_contact_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emergency_contact_phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    emergency_contact_relationship = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContact", x => x.emergency_contact_id);
                    table.ForeignKey(
                        name: "FK_EmergencyContact_Employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "Employees",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeUpdateRequests",
                columns: table => new
                {
                    request_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_id = table.Column<int>(type: "int", nullable: false),
                    new_full_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    new_gender = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    NewPersonalEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewPlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewMaritalStatus = table.Column<int>(type: "int", nullable: true),
                    new_street_address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    new_city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    new_province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    new_postal_code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    new_phone_number = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    NewEmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewEmergencyContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewEmergencyContactRelationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    request_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hr_reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeUpdateRequests", x => x.request_id);
                    table.ForeignKey(
                        name: "FK_EmployeeUpdateRequests_Employees_emp_id",
                        column: x => x.emp_id,
                        principalTable: "Employees",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentInformation",
                columns: table => new
                {
                    employment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_id = table.Column<int>(type: "int", nullable: false),
                    employment_status = table.Column<int>(type: "int", maxLength: 25, nullable: false),
                    employment_start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    employment_type = table.Column<int>(type: "int", maxLength: 25, nullable: false),
                    employment_department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    employment_position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    employment_supervisor_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentInformation", x => x.employment_id);
                    table.ForeignKey(
                        name: "FK_EmploymentInformation_Employees_emp_id",
                        column: x => x.emp_id,
                        principalTable: "Employees",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContact_employee_id",
                table: "EmergencyContact",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUpdateRequests_emp_id",
                table: "EmployeeUpdateRequests",
                column: "emp_id");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentInformation_emp_id",
                table: "EmploymentInformation",
                column: "emp_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmergencyContact");

            migrationBuilder.DropTable(
                name: "EmployeeUpdateRequests");

            migrationBuilder.DropTable(
                name: "EmploymentInformation");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
