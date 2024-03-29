USE [master]
GO
/****** Object:  Database [VirtualLibrary]    Script Date: 01/05/2023 02:39:52 ******/
CREATE DATABASE [VirtualLibrary2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VirtualLibrary', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\VirtualLibrary.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VirtualLibrary_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\VirtualLibrary_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VirtualLibrary] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VirtualLibrary].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VirtualLibrary] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VirtualLibrary] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VirtualLibrary] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VirtualLibrary] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VirtualLibrary] SET ARITHABORT OFF 
GO
ALTER DATABASE [VirtualLibrary] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VirtualLibrary] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VirtualLibrary] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VirtualLibrary] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VirtualLibrary] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VirtualLibrary] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VirtualLibrary] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VirtualLibrary] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VirtualLibrary] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VirtualLibrary] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VirtualLibrary] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VirtualLibrary] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VirtualLibrary] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VirtualLibrary] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VirtualLibrary] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VirtualLibrary] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VirtualLibrary] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VirtualLibrary] SET RECOVERY FULL 
GO
ALTER DATABASE [VirtualLibrary] SET  MULTI_USER 
GO
ALTER DATABASE [VirtualLibrary] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VirtualLibrary] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VirtualLibrary] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VirtualLibrary] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VirtualLibrary] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VirtualLibrary] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'VirtualLibrary', N'ON'
GO
ALTER DATABASE [VirtualLibrary] SET QUERY_STORE = OFF
GO
USE [VirtualLibrary]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 02/05/2023 02:39:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorFirstName] [nvarchar](max) NOT NULL,
	[AuthorLastName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookLists]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookLists](
	[BookListId] [int] IDENTITY(1,1) NOT NULL,
	[BookListName] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_BookLists] PRIMARY KEY CLUSTERED 
(
	[BookListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[BookTitle] [nvarchar](max) NOT NULL,
	[PublicationYear] [date] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Summary] [text] NOT NULL,
	[BookCover] [nvarchar](max) NOT NULL,
	[BookPdfUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DigitalLicence]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DigitalLicence](
	[LicenceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LicenceKey] [nvarchar](max) NOT NULL,
	[LicenceXml] [nvarchar](max) NOT NULL,
	[PublicKey] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DigitalLicence] PRIMARY KEY CLUSTERED 
(
	[LicenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[GenreId] [int] IDENTITY(1,1) NOT NULL,
	[GenreName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenresInBook]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenresInBook](
	[GenresInBookId] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [PK_GenresInBook] PRIMARY KEY CLUSTERED 
(
	[GenresInBookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[MembershipId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NULL,
	[TotalBooks] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Customer] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[MembershipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[MembershipId] [int] NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBookLists]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBookLists](
	[UserBookListId] [int] IDENTITY(1,1) NOT NULL,
	[BookListId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_UserBookLists] PRIMARY KEY CLUSTERED 
(
	[UserBookListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02/05/2023 02:39:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](128) NOT NULL,
	[LastName] [nvarchar](128) NOT NULL,
	[Username] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[LoginUserId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230201024152_InitialMigration', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230201025100_AddUsersTable', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230216160724_AddTablesGenresAuthorsBooksGenresInBooks', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230218115904_UpdateBooks', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230218142132_BookListsAndUserBookLists', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230218200303_UpdateBookListsAndUserBookLists', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230222224953_UpdateBooksAddPdfColumn', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230223144652_AddRoles', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230223183837_AddPaymentAndMembership', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230315193505_AddLicenceTable', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230315194229_ChangeLicenceToDigitalLicence', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230317151952_AddCustomerToMembership', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230317185527_RemoveStripeTokenColumnFromPayment', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230417093346_AddMembershipIsActive', N'7.0.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230420014238_removeHasLoggedInBeforeFromUsers', N'7.0.2')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Admin', N'Admin', N'ADMIN', N'1')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'PaidMember', N'PaidMember', N'PAIDMEMBER', N'3')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'RegisteredUser', N'RegisteredUser', N'REGISTEREDUSER', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3b140bde-c82c-4ac6-b545-af3005bd8f57', N'Admin')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'33a63313-0e72-487f-97b7-ba43cab1dc06', N'test333', N'TEST333', N'test3@test.ie', N'TEST3@TEST.IE', 1, N'AQAAAAEAACcQAAAAECm/XHkKHXoJoVuA45vg6MMEa0/8019qqTdIj0V766RqAa2OD4o+hqtCAbAAtCHiJA==', N'2NVQP6D4ABLP53TCZIGEXG4VBCA4YP67', N'971ed087-0bfd-4a49-8d2d-226ba7361a74', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3b140bde-c82c-4ac6-b545-af3005bd8f57', N'admin123', N'ADMIN123', N'admin@admin.ie', N'ADMIN@ADMIN.IE', 1, N'AQAAAAEAACcQAAAAEBU7VhukwIIS62gxNYVNBE4/qdgV0H6vn2Dusre7HEnlFckO0A6TN1ZTwnkq0w7dAg==', N'G3XIQLAHOR5NTGJSB5AINCO37JNKW4EL', N'af5283f9-d635-4c44-a66b-e915d1ee4278', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7007754b-313e-466f-af89-53d51be16730', N'test222', N'TEST222', N'test2@test.ie', N'TEST2@TEST.IE', 1, N'AQAAAAEAACcQAAAAELrab9pDFMuDYS1bUfL1gUQ59kNaLmJR/aXhviZmXYDi80nPloMtbz/uDYEhkZ4W2g==', N'C4XUGI63JTURNH3IINYDUQHNSZ34DBYU', N'ac6d6bab-b999-4819-b00b-185271fc642c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e2098e2f-c3c3-4f14-9f48-1deaaa908c71', N'test111', N'TEST111', N'test1@test.ie', N'TEST1@TEST.IE', 1, N'AQAAAAEAACcQAAAAEM417TYqtZVfX89W722Qkz6sK9hnwy6PNBZeK6kNELQCN6lkRSAV623PaSotHUpl3w==', N'JUKPIJJ7CCMYFZV6P6R667OE4LVD5BAC', N'd9a2bce6-cb21-478a-b69f-776cf5536d46', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (1, N'M.R.', N'James')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (2, N'Bob', N'Brown')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (3, N'Wilkie', N'Collins')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (4, N'Joseph', N'Conrad')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (5, N'P. G.', N'Wodehouse')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (6, N'Charles', N'Garvice')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (7, N'Alexandre', N'Dumas')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (8, N'Victor', N'Hugo')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (9, N'Jack', N'London')
INSERT [dbo].[Authors] ([AuthorId], [AuthorFirstName], [AuthorLastName]) VALUES (21, N'George', N'Eliot')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[BookLists] ON 

INSERT [dbo].[BookLists] ([BookListId], [BookListName], [UserId]) VALUES (20, N'Want to read', 17)
INSERT [dbo].[BookLists] ([BookListId], [BookListName], [UserId]) VALUES (21, N'Currently reading', 17)
INSERT [dbo].[BookLists] ([BookListId], [BookListName], [UserId]) VALUES (22, N'Already read', 17)
SET IDENTITY_INSERT [dbo].[BookLists] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (1, N'Ghost Stories of an Antiquary', CAST(N'1904-01-01' AS Date), 1, N'Eight classics by a great Edwardian scholar and storyteller. "Number Thirteen," "The Mezzotint," "Canon Alberic''s Scrapbook," and more. Renowned for their wit, erudition and suspense, these stories are each masterfully constructed and represent a high achievement in the ghost genre. We are delighted to publish this classic book as part of our extensive Classic Library collection. Many of the books in our collection have been out of print for decades, and therefore have not been accessible to the general public. The aim of our publishing program is to facilitate rapid access to this vast reservoir of literature, and our view is that this is a significant literary work, which deserves to be brought back into print after many decades. The contents of the vast majority of titles in the Classic Library have been scanned from the original works. To ensure a high quality product, each title has been meticulously hand curated by our staff. Our philosophy has been guided by a desire to provide the reader with a book that is as close as possible to ownership of the original work. We hope that you will enjoy this wonderful classic work, and that for you it becomes an enriching experience.', N'GhostStoriesOfAnAntiquary.jpg', N'Protected_R2hvc3RTdG9yaWVzT2ZBbkFudGlxdWFyeQ==.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (2, N'The Complete Book of Cheese', CAST(N'1955-01-01' AS Date), 2, N'1. I Remember Cheese 2. The Big Cheese 3. Foreign Greats 4. Native Americans 5. Sixty-five Sizzling Rabbits 6. The Fondue 7. Soufflés, Puffs and Ramekins 8. Pizzas, Blintzes, Pastes and Cheese Cake 9. Au Gratin, Soups, Salads and Sauces 10. Appetizers, Crackers, Sandwiches, Savories, Snacks, Spreads and Toasts 11. "Fit for Drink" 12. Lazy Lou APPENDIX—The A-B-Z of Cheese', N'TheCompleteBookOfCheese.jpg', N'Protected_VGhlQ29tcGxldGVCb29rT2ZDaGVlc2U=.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (3, N'The Woman in White', CAST(N'1859-01-01' AS Date), 3, N'''In one moment, every drop of blood in my body was brought to a stop... There, as if it had that moment sprung out of the earth, stood the figure of a solitary Woman, dressed from head to foot in white'' The Woman in White famously opens with Walter Hartright''s eerie encounter on a moonlit London road. Engaged as a drawing master to the beautiful Laura Fairlie, Walter becomes embroiled in the sinister intrigues of Sir Percival Glyde and his ''charming'' friend Count Fosco, who has a taste for white mice, vanilla bonbons, and poison. Pursuing questions of identity and insanity along the paths and corridors of English country houses and the madhouse, The Woman in White is the first and most influential of the Victorian genre that combined Gothic horror with psychological realism. Matthew Sweet''s introduction explores the phenomenon of Victorian ''sensation'' fiction, and discusses Wilkie Collins''s biographical and societal influences. Included in this edition are appendices on theatrical adaptations of the novel and its serialisation history.', N'TheWomanInWhite.jpg', N'Protected_VGhlV29tYW5JbldoaXRl.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (4, N'The Secret Agent: A Simple Tale', CAST(N'1907-01-01' AS Date), 4, N'In a rough-and-ready shop in Soho in Victorian London, Mr. Verloc lives a humble existence sustaining himself, his wife, her infirm mother, and her disabled brother, Stevie. But all is not as it seems with Mr. Verloc, a secret agent in the employ of a foreign government. When a plot to bomb Greenwich Observatory falls apart, Verloc’s identity is exposed, and those closest to him must bear the burden of his failures. Harkening back to the writings of Charles Dickens, Joseph Conrad’s early masterpiece effectively explores themes of subversion, politics, and crime.)', N'TheSimpleAgentASimpleTale.jpg', N'Protected_VGhlU2ltcGxlQWdlbnRBU2ltcGxlVGFsZQ==.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (5, N'Right Ho, Jeeves', CAST(N'1934-01-01' AS Date), 5, N'Gussie Fink-Nottle has locked himself away in the country studying newts ever since he came into his estate. So it is a surprise when Bertie hears that Gussie is not only in London, but he is there to woo Madeline Bassett! At odds with Jeeves over the decorum of a white jacket, Bertie decides to take on Gussie''s problem himself. Off to Brinkley Court, Bertie must deal with the prize-giving at Market Snodsbury Grammar School, the broken engagement of his cousin Angela, and the resignation of Anatole, his aunt''s genius chef. Will Jeeves be able to sort out the mess?', N'RightHoJeeves.jpg', N'Protected_UmlnaHRIb0plZXZlcw==.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (6, N'Only a Girl''s Love', CAST(N'1979-01-01' AS Date), 6, N'It is a warm evening in early Summer; the sun is setting behind a long range of fir and yew-clad hills, at the feet of which twists in and out, as it follows their curves, a placid, peaceful river. Opposite these hills, and running beside the river, are long-stretching meadows, brilliantly green with fresh-springing grass, and gorgeously yellow with newly-opened buttercups. Above, the sunset sky gleams and glows with fiery red and rich deep chromes. And London is almost within sight. It is a beautiful scene, such as one sees only in this England of ours--a scene that defies poet and painter. At this very moment it is defying one of the latter genus; for in a room of a low-browed, thatched-roofed cottage which stood on the margin of the meadow, James Etheridge sat beside his easel, his eyes fixed on the picture framed in the open window, his brush and mahl-stick drooping in his idle hand', N'OnlyAGirlsLove.jpg', N'Protected_T25seUFHaXJsc0xvdmU=.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (7, N'Twenty Years After', CAST(N'1845-01-01' AS Date), 7, N'Twenty Years After (1845), the sequel to The Three Musketeers, is a supreme creation of suspense and heroic adventure. Two decades have passed since the musketeers triumphed over Cardinal Richelieu and Milady. Time has weakened their resolve, and dispersed their loyalties. But treasons and stratagems still cry out for justice: civil war endangers the throne of France, while in England Cromwell threatens to send Charles I to the scaffold. Dumas brings his immortal quartet out of retirement to cross swords with time, the malevolence of men, and the forces of history. But their greatest test is a titanic struggle with the son of Milady, who wears the face of Evil.', N'TwentyYearsAfter.jpg', N'Protected_VHdlbnR5WWVhcnNBZnRlcg==.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (8, N'Les Misérables', CAST(N'1862-01-01' AS Date), 8, N'Victor Hugo''s tale of injustice, heroism and love follows the fortunes of Jean Valjean, an escaped convict determined to put his criminal past behind him. But his attempts to become a respected member of the community are constantly put under threat: by his own conscience, when, owing to a case of mistaken identity, another man is arrested in his place; and by the relentless investigations of the dogged Inspector Javert. It is not simply for himself that Valjean must stay free, however, for he has sworn to protect the baby daughter of Fantine, driven to prostitution by poverty.', N'LesMiserables.jpg', N'Protected_TGVzTWlzZXJhYmxlcw==.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (9, N'The Call of the Wild', CAST(N'1903-01-01' AS Date), 9, N'First published in 1903, The Call of the Wild is regarded as Jack London''s masterpiece. Based on London''s experiences as a gold prospector in the Canadian wilderness and his ideas about nature and the struggle for existence, The Call of the Wild is a tale about unbreakable spirit and the fight for survival in the frozen Alaskan Klondike.', N'TheCallOfTheWild.jpg', N'Protected_VGhlQ2FsbE9mVGhlV2lsZA==.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (10, N'The Three Musketeers', CAST(N'1844-01-01' AS Date), 7, N'Alexandre Dumas’s most famous tale— and possibly the most famous historical novel of all time— in a handsome hardcover volume. This swashbuckling epic of chivalry, honor, and derring-do, set in France during the 1620s, is richly populated with romantic heroes, unattainable heroines, kings, queens, cavaliers, and criminals in a whirl of adventure, espionage, conspiracy, murder, vengeance, love, scandal, and suspense. Dumas transforms minor historical figures into larger- than-life characters: the Comte d’Artagnan, an impetuous young man in pursuit of glory; the beguilingly evil seductress “Milady”; the powerful and devious Cardinal Richelieu; the weak King Louis XIII and his unhappy queen—and, of course, the three musketeers themselves, Athos, Porthos, and Aramis, whose motto “all for one, one for all” has come to epitomize devoted friendship. With a plot that delivers stolen diamonds, masked balls, purloined letters, and, of course, great bouts of swordplay, The Three Musketeers is eternally entertaining.', N'TheThreeMusketeers.jpg', N'Protected_VGhlVGhyZWVNdXNrZXRlZXJz.pdf')
INSERT [dbo].[Books] ([BookId], [BookTitle], [PublicationYear], [AuthorId], [Summary], [BookCover], [BookPdfUrl]) VALUES (43, N'Middlemarch', CAST(N'1872-01-01' AS Date), 21, N'Taking place in the years leading up to the First Reform Bill of 1832, Middlemarch explores nearly every subject of concern to modern life: art, religion, science, politics, self, society, human relationships. Among her characters are some of the most remarkable portraits in English literature: Dorothea Brooke, the heroine, idealistic but naive; Rosamond Vincy, beautiful and egoistic: Edward Casaubon, the dry-as-dust scholar: Tertius Lydgate, the brilliant but morally-flawed physician: the passionate artist Will Ladislaw: and Fred Vincey and Mary Garth, childhood sweethearts whose charming courtship is one of the many humorous elements in the novel''s rich comic vein.', N'Middlemarch.jpg', N'Protected_TWlkZGxlbWFyY2g=.pdf')
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (1, N'Fantasy')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (2, N'Adventure')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (3, N'Romance')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (4, N'Mystery')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (5, N'Horror')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (6, N'Historical Fiction')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (7, N'Cooking')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (8, N'Classics')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (9, N'Health')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (10, N'Fiction')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (11, N'Literature')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (12, N'Crime')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (13, N'Thriller')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (14, N'Non-Fiction')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (15, N'Humour')
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[GenresInBook] ON 

INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (4, 2, 14)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (5, 2, 7)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (6, 3, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (7, 3, 6)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (8, 3, 4)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (9, 3, 13)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (10, 3, 11)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (11, 3, 8)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (12, 4, 8)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (13, 4, 11)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (14, 4, 4)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (15, 4, 12)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (16, 4, 13)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (17, 5, 15)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (18, 5, 11)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (19, 5, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (20, 5, 6)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (21, 6, 3)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (22, 6, 11)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (23, 6, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (24, 7, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (25, 7, 8)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (26, 7, 6)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (27, 7, 2)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (28, 7, 11)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (29, 8, 11)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (30, 8, 8)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (31, 8, 6)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (32, 8, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (33, 8, 3)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (34, 9, 8)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (35, 9, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (36, 9, 2)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (37, 9, 6)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (38, 9, 11)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (39, 10, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (40, 10, 8)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (150, 1, 1)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (151, 1, 5)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (152, 1, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (181, 43, 6)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (182, 43, 8)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (183, 43, 10)
INSERT [dbo].[GenresInBook] ([GenresInBookId], [BookId], [GenreId]) VALUES (184, 43, 11)
SET IDENTITY_INSERT [dbo].[GenresInBook] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [Username], [Email], [LoginUserId]) VALUES (17, N'Admin', N'Admin', N'admin123', N'admin@admin.ie', N'3b140bde-c82c-4ac6-b545-af3005bd8f57')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 02/05/2023 02:39:53 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 02/05/2023 02:39:53 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookLists_UserId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_BookLists_UserId] ON [dbo].[BookLists]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Books_AuthorId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_Books_AuthorId] ON [dbo].[Books]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DigitalLicence_UserId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_DigitalLicence_UserId] ON [dbo].[DigitalLicence]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GenresInBook_BookId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_GenresInBook_BookId] ON [dbo].[GenresInBook]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GenresInBook_GenreId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_GenresInBook_GenreId] ON [dbo].[GenresInBook]
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Membership_UserId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_Membership_UserId] ON [dbo].[Membership]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Payment_MembershipId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_Payment_MembershipId] ON [dbo].[Payment]
(
	[MembershipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserBookLists_BookId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_UserBookLists_BookId] ON [dbo].[UserBookLists]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserBookLists_BookListId]    Script Date: 02/05/2023 02:39:53 ******/
CREATE NONCLUSTERED INDEX [IX_UserBookLists_BookListId] ON [dbo].[UserBookLists]
(
	[BookListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookLists] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT (N'') FOR [BookCover]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT (N'') FOR [BookPdfUrl]
GO
ALTER TABLE [dbo].[Membership] ADD  DEFAULT (N'') FOR [Customer]
GO
ALTER TABLE [dbo].[Membership] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BookLists]  WITH CHECK ADD  CONSTRAINT [FK_BookLists_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookLists] CHECK CONSTRAINT [FK_BookLists_Users_UserId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([AuthorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Authors_AuthorId]
GO
ALTER TABLE [dbo].[DigitalLicence]  WITH CHECK ADD  CONSTRAINT [FK_DigitalLicence_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DigitalLicence] CHECK CONSTRAINT [FK_DigitalLicence_Users_UserId]
GO
ALTER TABLE [dbo].[GenresInBook]  WITH CHECK ADD  CONSTRAINT [FK_GenresInBook_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GenresInBook] CHECK CONSTRAINT [FK_GenresInBook_Books_BookId]
GO
ALTER TABLE [dbo].[GenresInBook]  WITH CHECK ADD  CONSTRAINT [FK_GenresInBook_Genres_GenreId] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([GenreId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GenresInBook] CHECK CONSTRAINT [FK_GenresInBook_Genres_GenreId]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [FK_Membership_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [FK_Membership_Users_UserId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Membership_MembershipId] FOREIGN KEY([MembershipId])
REFERENCES [dbo].[Membership] ([MembershipId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Membership_MembershipId]
GO
ALTER TABLE [dbo].[UserBookLists]  WITH CHECK ADD  CONSTRAINT [FK_UserBookLists_BookLists_BookListId] FOREIGN KEY([BookListId])
REFERENCES [dbo].[BookLists] ([BookListId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserBookLists] CHECK CONSTRAINT [FK_UserBookLists_BookLists_BookListId]
GO
ALTER TABLE [dbo].[UserBookLists]  WITH CHECK ADD  CONSTRAINT [FK_UserBookLists_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserBookLists] CHECK CONSTRAINT [FK_UserBookLists_Books_BookId]
GO
USE [master]
GO
ALTER DATABASE [VirtualLibrary] SET  READ_WRITE 
GO
