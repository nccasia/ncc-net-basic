using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LinQDemo.Data.Migrations
{
    public partial class Initial_10082019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    NumberOfCitizens = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.Id);
                    table.ForeignKey(
                        name: "FK_County_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountyId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_School_County_CountyId",
                        column: x => x.CountyId,
                        principalTable: "County",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Grade = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SchoolId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Class_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    SchoolId = table.Column<long>(nullable: true),
                    ClassId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Hà Nội" },
                    { 55, "Tân An" },
                    { 54, "Tam Kỳ" },
                    { 53, "Tam Điệp" },
                    { 52, "Sơn La" },
                    { 51, "Sông Công" },
                    { 50, "Sóc Trăng" },
                    { 49, "Rạch Giá" },
                    { 48, "Quy Nhơn" },
                    { 47, "Quảng Ngãi" },
                    { 46, "Pleiku" },
                    { 45, "Phủ Lý" },
                    { 44, "Phúc Yên" },
                    { 43, "Phan Thiết" },
                    { 42, "Phan Rang - Tháp Chàm" },
                    { 41, "Ninh Bình" },
                    { 56, "Tây Ninh" },
                    { 57, "Thanh Hóa" },
                    { 58, "Thái Bình" },
                    { 59, "Thái Nguyên" },
                    { 75, "Hải Phòng" },
                    { 74, "Sầm Sơn" },
                    { 73, "Sa Đéc" },
                    { 72, "Bảo Lộc" },
                    { 71, "Yên Bái" },
                    { 70, "Vũng Tàu" },
                    { 69, "Vĩnh Yên" },
                    { 40, "Nha Trang" },
                    { 68, "Vĩnh Long" },
                    { 66, "Vinh" },
                    { 65, "Việt Trì" },
                    { 64, "Uông Bí" },
                    { 63, "Tuy Hòa" },
                    { 62, "Tuyên Quang" },
                    { 61, "Trà Vinh" },
                    { 60, "Thủ Dầu Một" },
                    { 67, "Vị Thanh" },
                    { 76, "Đà Nẵng" },
                    { 39, "Nam Định" },
                    { 37, "Móng Cái" },
                    { 16, "Châu Đốc" },
                    { 15, "Cẩm Phả" },
                    { 14, "Cà Mau" },
                    { 13, "Cao Lãnh" },
                    { 12, "Cao Bằng" },
                    { 11, "Cam Ranh" },
                    { 10, "Buôn Ma Thuột" },
                    { 9, "Biên Hòa" },
                    { 8, "Bến Tre" },
                    { 7, "Bắc Ninh" },
                    { 6, "Bắc Kạn" },
                    { 5, "Bắc Giang" },
                    { 4, "Bạc Liêu" },
                    { 3, "Bà Rịa" },
                    { 2, "Thành phố Hồ Chí Minh" },
                    { 17, "Chí Linh" },
                    { 18, "Đà Lạt" },
                    { 19, "Điện Biên Phủ" },
                    { 20, "Đông Hà" },
                    { 36, "Long Xuyên" },
                    { 35, "Long Khánh" },
                    { 34, "Lạng Sơn" },
                    { 33, "Lào Cai" },
                    { 32, "Lai Châu" },
                    { 31, "Kon Tum" },
                    { 30, "Hưng Yên" },
                    { 38, "Mỹ Tho" },
                    { 29, "Huế" },
                    { 27, "Hòa Bình" },
                    { 26, "Hà Tĩnh" },
                    { 25, "Hà Tiên" },
                    { 24, "Hà Giang" },
                    { 23, "Hải Dương" },
                    { 22, "Đồng Xoài" },
                    { 21, "Đồng Hới" },
                    { 28, "Hội An" },
                    { 77, "Cần Thơ" }
                });

            migrationBuilder.InsertData(
                table: "County",
                columns: new[] { "Id", "CityId", "Name", "NumberOfCitizens" },
                values: new object[,]
                {
                    { 1, 1, "Ba Đình", 247100L },
                    { 47, 2, "Tân Bình", 470350L },
                    { 48, 2, "Tân Phú", 464493L },
                    { 49, 2, "Thủ Đức", 550820L },
                    { 13, 75, "Đồ Sơn", 102234L },
                    { 14, 75, "Dương Kinh", 127360L },
                    { 15, 75, "Hải An", 150600L },
                    { 16, 75, "Hồng Bàng", 172310L },
                    { 17, 75, "Kiến An", 120780L },
                    { 18, 75, "Lê Chân", 240150L },
                    { 19, 75, "Ngô Quyền", 208650L },
                    { 20, 76, "Cẩm Lệ", 143632L },
                    { 21, 76, "Hải Châu", 221324L },
                    { 22, 76, "Liên Chiểu", 170153L },
                    { 23, 76, "Ngũ Hành Sơn", 115872L },
                    { 24, 76, "Sơn Trà", 173455L },
                    { 25, 76, "Thanh Khê", 205341L },
                    { 26, 77, "Bình Thủy", 172317L },
                    { 27, 77, "Cái Răng", 165057L },
                    { 28, 77, "Ninh Kiều", 388600L },
                    { 29, 77, "Ô Môn", 160350L },
                    { 30, 77, "Thốt Nốt", 202600L },
                    { 50, 77, "Cờ Đỏ", 135709L },
                    { 51, 77, "Phong Điền", 123136L },
                    { 46, 2, "Phú Nhuận", 181210L },
                    { 52, 77, "Thới Lai", 148000L },
                    { 45, 2, "Gò Vấp", 663313L },
                    { 43, 2, "Bình Tân", 702650L },
                    { 2, 1, "Bắc Từ Liêm", 333300L },
                    { 3, 1, "Cầu Giấy", 266800L },
                    { 4, 1, "Đống Đa", 420900L },
                    { 5, 1, "Hà Đông", 352000L },
                    { 6, 1, "Hai Bà Trưng", 318000L },
                    { 7, 1, "Hoàn Kiếm", 160600L },
                    { 8, 1, "Hoàng Mai", 411500L },
                    { 9, 1, "Long Biên", 291900L },
                    { 10, 1, "Nam Từ Liêm", 236700L },
                    { 11, 1, "Tây Hồ", 168300L },
                    { 12, 1, "Thanh Xuân", 285400L },
                    { 31, 2, "Quận 1", 205200L },
                    { 32, 2, "Quận 2", 170080L },
                    { 33, 2, "Quận 3", 197600L },
                    { 34, 2, "Quận 4", 203060L },
                    { 35, 2, "Quận 5", 187640L },
                    { 36, 2, "Quận 6", 271050L },
                    { 37, 2, "Quận 7", 324780L },
                    { 38, 2, "Quận 8", 451300L },
                    { 39, 2, "Quận 9", 316450L },
                    { 40, 2, "Quận 10", 372450L },
                    { 41, 2, "Quận 11", 332536L },
                    { 42, 2, "Quận 12", 528170L },
                    { 44, 2, "Bình Thạnh", 490380L },
                    { 53, 77, "Vĩnh Thạnh", 152200L }
                });

            migrationBuilder.InsertData(
                table: "School",
                columns: new[] { "Id", "CityId", "CountyId", "Name" },
                values: new object[,]
                {
                    { 5L, 1, 1, "Trường Trung học phổ thông Phan Đình Phùng" },
                    { 2L, 1, 3, "Trường Trung học phổ thông Chuyên Hà Nội - Amsterdam" },
                    { 3L, 1, 3, "Trường Trung học phổ thông Lương Thế Vinh" },
                    { 4L, 1, 3, "Trường Trung học phổ thông Nguyễn Tất Thành" },
                    { 9L, 1, 3, "Trường Trung học phổ thông Chuyên Ngoại ngữ, Đại học Quốc gia Hà Nội" },
                    { 10L, 1, 3, "Trường Trung học phổ thông chuyên Đại học Sư phạm Hà Nội" },
                    { 6L, 1, 4, "Trường Trung học phổ thông Quang Trung" },
                    { 7L, 1, 7, "Trường Trung học phổ thông Trần Phú" },
                    { 8L, 1, 7, "Trường Trung học phổ thông Việt Đức" },
                    { 1L, 1, 11, "Trường Trung học phổ thông Chu Văn An" }
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "Id", "Grade", "Name", "SchoolId" },
                values: new object[,]
                {
                    { 1L, 10, "10 A1", 1L },
                    { 2L, 10, "10 A2", 1L },
                    { 3L, 10, "10 D1", 1L },
                    { 4L, 10, "10 D2", 1L },
                    { 5L, 10, "10 D3", 1L },
                    { 6L, 11, "11 A1", 1L },
                    { 7L, 11, "11 A2", 1L },
                    { 8L, 11, "11 D1", 1L },
                    { 9L, 11, "11 D2", 1L },
                    { 10L, 11, "11 D3", 1L },
                    { 11L, 12, "12 A1", 1L },
                    { 12L, 12, "12 A2", 1L },
                    { 13L, 12, "12 D1", 1L },
                    { 14L, 12, "12 D2", 1L },
                    { 15L, 12, "12 D3", 1L }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "ClassId", "DateOfBirth", "FirstName", "LastName", "SchoolId" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2014, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 1", "Student", 1L },
                    { 2L, 1L, new DateTime(2014, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 2", "Student", 1L },
                    { 3L, 2L, new DateTime(2014, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 3", "Student", 1L },
                    { 4L, 3L, new DateTime(2014, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 4", "Student", 1L },
                    { 5L, 4L, new DateTime(2014, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 5", "Student", 1L },
                    { 9L, 6L, new DateTime(2013, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 9", "Student", 1L },
                    { 10L, 6L, new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 10", "Student", 1L },
                    { 6L, 9L, new DateTime(2013, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 6", "Student", 1L },
                    { 8L, 9L, new DateTime(2013, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 8", "Student", 1L },
                    { 7L, 10L, new DateTime(2013, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 7", "Student", 1L },
                    { 11L, 12L, new DateTime(2012, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 11", "Student", 1L },
                    { 12L, 13L, new DateTime(2012, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 12", "Student", 1L },
                    { 13L, 14L, new DateTime(2012, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 13", "Student", 1L },
                    { 14L, 14L, new DateTime(2012, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 14", "Student", 1L },
                    { 15L, 14L, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "No 15", "Student", 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_SchoolId",
                table: "Class",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_County_CityId",
                table: "County",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_School_CityId",
                table: "School",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_School_CountyId",
                table: "School",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                table: "Student",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SchoolId",
                table: "Student",
                column: "SchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "County");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
