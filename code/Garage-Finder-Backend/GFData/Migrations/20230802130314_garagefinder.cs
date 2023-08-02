using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GFData.Migrations
{
    public partial class garagefinder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "RoleName",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleName", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Subscribes",
                columns: table => new
                {
                    SubscribeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribes", x => x.SubscribeID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    AddressDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_RoleName_RoleID",
                        column: x => x.RoleID,
                        principalTable: "RoleName",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    LicensePlates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeCar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.CarID);
                    table.ForeignKey(
                        name: "FK_Car_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Garage",
                columns: table => new
                {
                    GarageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceID = table.Column<int>(type: "int", nullable: false),
                    DistrictsID = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatAddress = table.Column<double>(type: "float", nullable: true),
                    LngAddress = table.Column<double>(type: "float", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garage", x => x.GarageID);
                    table.ForeignKey(
                        name: "FK_Garage_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageToUsers",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUserID = table.Column<int>(type: "int", nullable: false),
                    ReceiverUserID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageToUsers", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_MessageToUsers_User_ReceiverUserID",
                        column: x => x.ReceiverUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageToUsers_User_SenderUserID",
                        column: x => x.SenderUserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    TokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.TokenID);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryGarage",
                columns: table => new
                {
                    CategoryGarageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGarage", x => x.CategoryGarageID);
                    table.ForeignKey(
                        name: "FK_CategoryGarage_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGarage_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteList",
                columns: table => new
                {
                    FavoriteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteList", x => x.FavoriteID);
                    table.ForeignKey(
                        name: "FK_FavoriteList_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID");
                    table.ForeignKey(
                        name: "FK_FavoriteList_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarageBrand",
                columns: table => new
                {
                    BrID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarageBrand", x => x.BrID);
                    table.ForeignKey(
                        name: "FK_GarageBrand_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarageBrand_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuestOrder",
                columns: table => new
                {
                    GuestOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GFOrderID = table.Column<int>(type: "int", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false),
                    TimeCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAppointment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandCarID = table.Column<int>(type: "int", nullable: true),
                    TypeCar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicensePlates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestOrder", x => x.GuestOrderID);
                    table.ForeignKey(
                        name: "FK_GuestOrder_Brand_BrandCarID",
                        column: x => x.BrandCarID,
                        principalTable: "Brand",
                        principalColumn: "BrandID");
                    table.ForeignKey(
                        name: "FK_GuestOrder_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageGarage",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageID = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGarage", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_ImageGarage_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoicesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscribeID = table.Column<int>(type: "int", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoicesID);
                    table.ForeignKey(
                        name: "FK_Invoices_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Subscribes_SubscribeID",
                        column: x => x.SubscribeID,
                        principalTable: "Subscribes",
                        principalColumn: "SubscribeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GFOrderID = table.Column<int>(type: "int", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false),
                    TimeCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAppointment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Car_CarID",
                        column: x => x.CarID,
                        principalTable: "Car",
                        principalColumn: "CarID");
                    table.ForeignKey(
                        name: "FK_Orders_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Report_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID");
                    table.ForeignKey(
                        name: "FK_Report_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RoomChat",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomChat", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_RoomChat_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomChat_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LinkImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GarageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staff_Garage_GarageID",
                        column: x => x.GarageID,
                        principalTable: "Garage",
                        principalColumn: "GarageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryGarageID = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_Service_CategoryGarage_CategoryGarageID",
                        column: x => x.CategoryGarageID,
                        principalTable: "CategoryGarage",
                        principalColumn: "CategoryGarageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileGuestOrders",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestOrderID = table.Column<int>(type: "int", nullable: false),
                    FileLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileGuestOrders", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileGuestOrders_GuestOrder_GuestOrderID",
                        column: x => x.GuestOrderID,
                        principalTable: "GuestOrder",
                        principalColumn: "GuestOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuestOrderDetail",
                columns: table => new
                {
                    GuestOrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestOrderID = table.Column<int>(type: "int", nullable: false),
                    CategoryGarageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestOrderDetail", x => x.GuestOrderDetailId);
                    table.ForeignKey(
                        name: "FK_GuestOrderDetail_CategoryGarage_CategoryGarageID",
                        column: x => x.CategoryGarageID,
                        principalTable: "CategoryGarage",
                        principalColumn: "CategoryGarageID");
                    table.ForeignKey(
                        name: "FK_GuestOrderDetail_GuestOrder_GuestOrderID",
                        column: x => x.GuestOrderID,
                        principalTable: "GuestOrder",
                        principalColumn: "GuestOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageGuestOrders",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestOrderID = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGuestOrders", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_ImageGuestOrders_GuestOrder_GuestOrderID",
                        column: x => x.GuestOrderID,
                        principalTable: "GuestOrder",
                        principalColumn: "GuestOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    Star = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_Feedback_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileOrders",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    FileLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileOrders", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileOrders_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageOrders",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageOrders", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_ImageOrders_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CategoryGarageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_CategoryGarage_CategoryGarageID",
                        column: x => x.CategoryGarageID,
                        principalTable: "CategoryGarage",
                        principalColumn: "CategoryGarageID");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageReport",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageReport", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_ImageReport_Report_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Report",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_RoomChat_RoomID",
                        column: x => x.RoomID,
                        principalTable: "RoomChat",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "StaffMessages",
                columns: table => new
                {
                    StaffMessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMessages", x => x.StaffMessageID);
                    table.ForeignKey(
                        name: "FK_StaffMessages_RoomChat_RoomID",
                        column: x => x.RoomID,
                        principalTable: "RoomChat",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffMessages_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "StaffNotification",
                columns: table => new
                {
                    StaffNotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffNotification", x => x.StaffNotificationID);
                    table.ForeignKey(
                        name: "FK_StaffNotification_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffRefreshToken",
                columns: table => new
                {
                    TokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRefreshToken", x => x.TokenID);
                    table.ForeignKey(
                        name: "FK_StaffRefreshToken_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_BrandID",
                table: "Car",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_UserID",
                table: "Car",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGarage_CategoryID",
                table: "CategoryGarage",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGarage_GarageID",
                table: "CategoryGarage",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteList_GarageID",
                table: "FavoriteList",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteList_UserID",
                table: "FavoriteList",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_OrderID",
                table: "Feedback",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_FileGuestOrders_GuestOrderID",
                table: "FileGuestOrders",
                column: "GuestOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_FileOrders_OrderID",
                table: "FileOrders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Garage_UserID",
                table: "Garage",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GarageBrand_BrandID",
                table: "GarageBrand",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_GarageBrand_GarageID",
                table: "GarageBrand",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_GuestOrder_BrandCarID",
                table: "GuestOrder",
                column: "BrandCarID");

            migrationBuilder.CreateIndex(
                name: "IX_GuestOrder_GarageID",
                table: "GuestOrder",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_GuestOrder_GFOrderID",
                table: "GuestOrder",
                column: "GFOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuestOrderDetail_CategoryGarageID",
                table: "GuestOrderDetail",
                column: "CategoryGarageID");

            migrationBuilder.CreateIndex(
                name: "IX_GuestOrderDetail_GuestOrderID",
                table: "GuestOrderDetail",
                column: "GuestOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageGarage_GarageID",
                table: "ImageGarage",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageGuestOrders_GuestOrderID",
                table: "ImageGuestOrders",
                column: "GuestOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageOrders_OrderID",
                table: "ImageOrders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageReport_ReportID",
                table: "ImageReport",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_GarageID",
                table: "Invoices",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SubscribeID",
                table: "Invoices",
                column: "SubscribeID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_RoomID",
                table: "Message",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserID",
                table: "Message",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageToUsers_ReceiverUserID",
                table: "MessageToUsers",
                column: "ReceiverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageToUsers_SenderUserID",
                table: "MessageToUsers",
                column: "SenderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserID",
                table: "Notification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_CategoryGarageID",
                table: "OrderDetail",
                column: "CategoryGarageID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarID",
                table: "Orders",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GarageID",
                table: "Orders",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GFOrderID",
                table: "Orders",
                column: "GFOrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserID",
                table: "RefreshToken",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_GarageID",
                table: "Report",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserID",
                table: "Report",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomChat_GarageID",
                table: "RoomChat",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomChat_UserID",
                table: "RoomChat",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_CategoryGarageID",
                table: "Service",
                column: "CategoryGarageID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_GarageID",
                table: "Staff",
                column: "GarageID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PhoneNumber_EmailAddress",
                table: "Staff",
                columns: new[] { "PhoneNumber", "EmailAddress" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMessages_RoomID",
                table: "StaffMessages",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMessages_StaffId",
                table: "StaffMessages",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffNotification_StaffId",
                table: "StaffNotification",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffRefreshToken_StaffId",
                table: "StaffRefreshToken",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_Name",
                table: "Subscribes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber_EmailAddress",
                table: "User",
                columns: new[] { "PhoneNumber", "EmailAddress" },
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteList");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FileGuestOrders");

            migrationBuilder.DropTable(
                name: "FileOrders");

            migrationBuilder.DropTable(
                name: "GarageBrand");

            migrationBuilder.DropTable(
                name: "GuestOrderDetail");

            migrationBuilder.DropTable(
                name: "ImageGarage");

            migrationBuilder.DropTable(
                name: "ImageGuestOrders");

            migrationBuilder.DropTable(
                name: "ImageOrders");

            migrationBuilder.DropTable(
                name: "ImageReport");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MessageToUsers");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "StaffMessages");

            migrationBuilder.DropTable(
                name: "StaffNotification");

            migrationBuilder.DropTable(
                name: "StaffRefreshToken");

            migrationBuilder.DropTable(
                name: "GuestOrder");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Subscribes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CategoryGarage");

            migrationBuilder.DropTable(
                name: "RoomChat");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Garage");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RoleName");
        }
    }
}
