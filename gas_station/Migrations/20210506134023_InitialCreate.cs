using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace gas_station.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Men",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: true),
                    ManId = table.Column<int>(type: "integer", nullable: true),
                    PositionId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Men", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Men_Men_ManId",
                        column: x => x.ManId,
                        principalTable: "Men",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Men_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Summ = table.Column<int>(type: "integer", nullable: false),
                    EndBy = table.Column<int>(type: "integer", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Men_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Men",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeModelStationModel",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "integer", nullable: false),
                    StationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeModelStationModel", x => new { x.EmployeesId, x.StationsId });
                    table.ForeignKey(
                        name: "FK_EmployeeModelStationModel_Men_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Men",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeModelStationModel_Stations_StationsId",
                        column: x => x.StationsId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    ManId = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Men_ManId",
                        column: x => x.ManId,
                        principalTable: "Men",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DeliverId = table.Column<int>(type: "integer", nullable: false),
                    DeliveryId = table.Column<int>(type: "integer", nullable: true),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Summ = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FuelTypeId = table.Column<int>(type: "integer", nullable: false),
                    DeliveryItemId = table.Column<int>(type: "integer", nullable: false),
                    StorageModelId = table.Column<int>(type: "integer", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fuels_DeliveryItems_DeliveryItemId",
                        column: x => x.DeliveryItemId,
                        principalTable: "DeliveryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fuels_FuelTypes_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fuels_Storage_StorageModelId",
                        column: x => x.StorageModelId,
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    ManId = table.Column<int>(type: "integer", nullable: false),
                    FuelId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Summ = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Men_ManId",
                        column: x => x.ManId,
                        principalTable: "Men",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BeginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FuelId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceLists_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_EmployeeId",
                table: "Deliveries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryId",
                table: "DeliveryItems",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeModelStationModel_StationsId",
                table: "EmployeeModelStationModel",
                column: "StationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_DeliveryItemId",
                table: "Fuels",
                column: "DeliveryItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_FuelTypeId",
                table: "Fuels",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_StorageModelId",
                table: "Fuels",
                column: "StorageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Men_ManId",
                table: "Men",
                column: "ManId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Men_PositionId",
                table: "Men",
                column: "PositionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FuelId",
                table: "Orders",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ManId",
                table: "Orders",
                column: "ManId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_FuelId",
                table: "PriceLists",
                column: "FuelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ManId",
                table: "Users",
                column: "ManId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeModelStationModel");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropTable(
                name: "DeliveryItems");

            migrationBuilder.DropTable(
                name: "FuelTypes");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Men");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
