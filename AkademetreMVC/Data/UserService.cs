using System;
using AkademetreMVC.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;

namespace AkademetreMVC.Data
{
	public class UserService : IUserService
	{

        public User Create(User user)
        {

            using (var context = new ApplicationDbContext())
            {

                context.Users.Add(user);
                context.SaveChangesAsync();

                return user;
            }
        }


        public IList<User> GetUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = context.Users.ToList();

                return users;
            }
        }

        public byte[] GenerateExcel()
        {
            var stream = new MemoryStream();

            using (var spreadsheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "UserSheet" };
                sheets.Append(sheet);

                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                var headerRow = new Row();
                headerRow.Append(CreateTextCell("Name"));
                headerRow.Append(CreateTextCell("Surname"));
                headerRow.Append(CreateTextCell("Address"));
                headerRow.Append(CreateTextCell("Email"));
                sheetData.Append(headerRow);

                var userList = GetUsers();

                foreach (var user in userList)
                {
                    var row = new Row();
                    row.Append(CreateTextCell(user.Name));
                    row.Append(CreateTextCell(user.Surname));
                    row.Append(CreateTextCell(user.Address));
                    row.Append(CreateTextCell(user.Email));
                    sheetData.Append(row);
                }
            }

            stream.Position = 0;
            return stream.ToArray();


        }

        private Cell CreateTextCell(string text)
        {
            return new Cell() { DataType = CellValues.String, CellValue = new CellValue(text) };

        }


    }
}

