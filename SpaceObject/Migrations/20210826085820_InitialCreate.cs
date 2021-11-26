using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceObjectApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "space_objects",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_space_objects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asteroids",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    weight = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asteroids", x => x.id);
                    table.ForeignKey(
                        name: "FK_asteroids_space_objects_id",
                        column: x => x.id,
                        principalTable: "space_objects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "black_holes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    diameter = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_black_holes", x => x.id);
                    table.ForeignKey(
                        name: "FK_black_holes_space_objects_id",
                        column: x => x.id,
                        principalTable: "space_objects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planets",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    distance_from_earth = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planets", x => x.id);
                    table.ForeignKey(
                        name: "FK_planets_space_objects_id",
                        column: x => x.id,
                        principalTable: "space_objects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stars",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    number_of_years = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stars", x => x.id);
                    table.ForeignKey(
                        name: "FK_stars_space_objects_id",
                        column: x => x.id,
                        principalTable: "space_objects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "space_objects",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 1, "Небольшой околоземный астероид из группы аполлонов, который характеризуется крайне вытянутой орбитой. Он был открытнемецким астрономом Вальтером Бааде в Паломарской обсерватории США и назван в честь Икара,персонажа древнегреческой мифологии, известного своей необычной смертью", "Икар" });

            migrationBuilder.InsertData(
                table: "space_objects",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 2, "Сверхгигантская эллиптическая галактика типа cD в скоплении галактик Abell 85 в созвездии Кита на расстоянии около 700 млн световых лет от Солнца", "Holmberg 15A" });

            migrationBuilder.InsertData(
                table: "space_objects",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 3, "Четвёртая по удалённости от Солнца и седьмая по размеру планетаСолнечной системы; масса планеты составляет 10,7 % массы Земли.Названа в честь Марса — древнеримского бога войны, соответствующего древнегреческому Аресу.", "Марс" });

            migrationBuilder.InsertData(
                table: "space_objects",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 4, "Звезда созвездия Большого Пса. Звезда главной последовательностиспектрального класса A1. Ярчайшая звезда ночного неба,её светимость в 25 раз превышает светимость Солнца, при этом не является рекордной в мире звёзд.", "Сириус" });

            migrationBuilder.InsertData(
                table: "asteroids",
                columns: new[] { "id", "weight" },
                values: new object[] { 1, "990000000" });

            migrationBuilder.InsertData(
                table: "black_holes",
                columns: new[] { "id", "diameter" },
                values: new object[] { 2, "1500000" });

            migrationBuilder.InsertData(
                table: "planets",
                columns: new[] { "id", "distance_from_earth" },
                values: new object[] { 3, "55760000" });

            migrationBuilder.InsertData(
                table: "stars",
                columns: new[] { "id", "number_of_years" },
                values: new object[] { 4, "230000000" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asteroids");

            migrationBuilder.DropTable(
                name: "black_holes");

            migrationBuilder.DropTable(
                name: "planets");

            migrationBuilder.DropTable(
                name: "stars");

            migrationBuilder.DropTable(
                name: "space_objects");
        }
    }
}
