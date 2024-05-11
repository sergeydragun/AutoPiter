using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoPiter.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"INSERT INTO [dbo].[Branches]
								(
									[Id],
									[Location],
									[Name]
								)
								VALUES
								('4CB5D30B-1234-49D0-AAC9-88F66DE51CEC', N'Тридевятое царство', N'Тридевятое царство'),
								('A8627ACA-70C9-455E-A111-86AAA37E20BE', N'Дремучий Лес', N'Дремучий Лес'),
								('886C3F7A-5A38-4B46-A43A-641A4ED1D348', N'Луна', N'Луна');

								INSERT INTO [dbo].[Employees]
								(
									[Id],
									[Name],
									[BranchId]
								)
								VALUES
								('2F7940BB-A3C9-47C6-A693-336832068E79', N'Царь', '4CB5D30B-1234-49D0-AAC9-88F66DE51CEC'),
								('D1C8DC39-E2B9-4F19-8DBD-9365551EC5E1', N'Яга', 'A8627ACA-70C9-455E-A111-86AAA37E20BE'),
								('192BDB8D-DAC0-4F89-877D-BBA34AB17936', N'Копатыч', '886C3F7A-5A38-4B46-A43A-641A4ED1D348'),
								('82E85B9C-BA91-4A2C-8705-C43F9E7B2EB1', N'Добрыня', '4CB5D30B-1234-49D0-AAC9-88F66DE51CEC'),
								('B386D706-8EE9-4377-82C2-6050715E24A4', N'Кощей', '886C3F7A-5A38-4B46-A43A-641A4ED1D348'),
								('4662C615-C2C7-4AEC-95CF-25334188B73F', N'Лосяш', '886C3F7A-5A38-4B46-A43A-641A4ED1D348');

								INSERT INTO [dbo].[Devices]
								(
									[Id],
									[Name],
									[ConnectionType]
								)
								VALUES
								( 'C88F31CE-9201-4E9E-BC13-DC959714E252', N'Папирус', 0),
								( '78BC1D4A-CC07-48D4-BBFB-9F0340DCED77', N'Бумага', 0),
								( '1D07CA3D-C7F4-44E8-B594-85C368A626F8', N'Камень', 1);

								INSERT INTO [dbo].[BranchesAndDevices]
								(
									[Id],
									[BranchId],
									[DeviceId],
									[DeviceName],
									[SerialNumber],
									[IsDefault]	
								)
								VALUES
								('AA1460DE-320F-49C0-BB72-31B516596901', '4CB5D30B-1234-49D0-AAC9-88F66DE51CEC', 'C88F31CE-9201-4E9E-BC13-DC959714E252', N'Дворец', 1, 1),
								('A94EC76E-7C13-4F39-A8B5-A370A27B519C', '4CB5D30B-1234-49D0-AAC9-88F66DE51CEC', '78BC1D4A-CC07-48D4-BBFB-9F0340DCED77', N'Конюшни', 2, 0),
								('563E1178-2BF0-4ADB-83CB-070D0534C87A', '4CB5D30B-1234-49D0-AAC9-88F66DE51CEC', '78BC1D4A-CC07-48D4-BBFB-9F0340DCED77', N'Оружейная', 3, 0),
								('B709B159-A648-4392-9790-9085B078CEA6', '886C3F7A-5A38-4B46-A43A-641A4ED1D348', '1D07CA3D-C7F4-44E8-B594-85C368A626F8', N'Кратер', 1, 1),
								('72AF1863-A1C0-423A-80AE-AFBA21AB8D53', 'A8627ACA-70C9-455E-A111-86AAA37E20BE', '78BC1D4A-CC07-48D4-BBFB-9F0340DCED77', N'Избушка', 1, 0),
								('539A5880-8F98-4BE8-9847-323BA4906AEF', 'A8627ACA-70C9-455E-A111-86AAA37E20BE', 'C88F31CE-9201-4E9E-BC13-DC959714E252', N'Топи', 1, 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DELETE FROM [dbo].[BranchesAndDevices];
								   DELETE FROM [dbo].[Employees];
								   DELETE FROM [dbo].[Devices];
								   DELETE FROM [dbo].[Branches];");
        }
    }
}
