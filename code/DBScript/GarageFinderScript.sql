Create database  GarageFinder;

Create table Users(
	UserID int IDENTITY(1,1) primary key,
	Name nvarchar(50),
	Birthday nvarchar(10),
	PhoneNumber nvarchar(15),
	EmailAddress nvarchar(max),
	Password nvarchar(max)
)

Create table Car(
	CarID int IDENTITY(1,1) primary key,
	UserID int foreign key references Users(UserID),
	LicensePlates nvarchar(max),
	Brand nvarchar(max),
	Color nvarchar(max),
	Type nvarchar(max)
)

Create table UserRole(
	RoleID int,
	UserID int foreign key references Users(UserID),
	CONSTRAINT PK_UserRole PRIMARY KEY (RoleID,UserID)
)
Create table RoleName(
	RoleID int primary key,
	NameRole nvarchar(10)
)

Create table Garage(
	GarageID int IDENTITY(1,1) primary key,
	UserID int foreign key references Users(UserID),
	GarageName nvarchar(max),
	Address nvarchar(max),
	OwnerName nvarchar(max),
	EmailAddress nvarchar(max),
	PhoneNumber nvarchar(15)
)

Create table Orders(
	OrderID int IDENTITY(1,1) primary key,
	CarID int foreign key references Car(CarID),
	GarageID int foreign key references Garage(GarageID),
	TimeCreate DateTime,
	TimeUpdate DateTime,
	Status nvarchar(10) not null
)

Create table Service(
	ServiceID int IDENTITY(1,1) primary key,
	GarageID int foreign key references Garage(GarageID),
	NameService nvarchar(20) not null,
	Cost float not null,
	Note nvarchar(max)
)

Create table OrderDetail(
	OrderDetailID int IDENTITY(1,1) primary key,
	OrderID int foreign key references Orders(OrderID),
	ServiceID int foreign key references Service(ServiceID) null,
	NameService nvarchar(max),
	Cost float,
	Note nvarchar(max)
)

Create table Feedback(
	CommentID int IDENTITY(1,1) primary key,
	GarageID int foreign key references Garage(GarageID),
	UserID int foreign key references Users(UserID),
	Star int not null,
	Content nvarchar(max) not null
)