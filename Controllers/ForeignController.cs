using Dapper;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ReadEx.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;

namespace ReadEx.Controllers
{
    public class ForeignController : Controller
    {
        [HttpGet]
        public IActionResult Index(List<Models.Foreign> items = null)
        {
            items = items == null ? new List<Models.Foreign>() : items;
            ViewData["result"] = "nil";
            return View();
        }


        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            System.Diagnostics.Debug.WriteLine("Hello");

            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            ViewData["result"] = this.GetList(file.FileName);
            return View();
        }



        //Function To store values in Database
        private string GetList(string fileName)
        {
            //SQL connection
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection("Server = DESKTOP-KJF3LV4\\SQLEXPRESS;Database = foreign;Integrated Security = true;");
            int i, count = 0;
            

            List<Models.Foreign> items = new List<Models.Foreign>();

            var fname = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fname, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    while (reader.Read())
                    {

                        i = 0;
                        count = 0;
                        while (i < 29)
                        {
                            if (reader.GetValue(i) == null) { count += 1; }

                            i += 1;
                        }
                        if (count == 0)
                        {
                            count = 0;
                        }
                        else if (count < 29)
                        {
                            reader.Close();
                            System.IO.File.Delete(fname);
                            return "false";
                        }
                        else if (count == 29)
                        {
                            foreach (Foreign item in items)
                            {
                                System.Diagnostics.Debug.WriteLine(item.Organisation_Address);
                                try
                                {
                                    var result = sqlConnection.Execute("insert into details values(@ID, @Name, @DOB, @Father_name, @POB, @Nationality, @Occupation, @Designation, @Organisation_Name, @Organisation_Address, @O_Telephone, @O_Fax, @O_Mobile, @Permanent_address, @Present_address, @P_Telephone, @P_Mobile, @P_Fax, @Place_of_stay, @Passport_No, @Place_of_issue, @Passport_Validity, @Visa_No, @Visa_Place_of_issue, @Visa_Validity, @Visa_type, @Other_country, @From_Date, @To_Date);", item);
                                }
                                catch (Exception ex) {
                                    
                                }
                            }
                            reader.Close();
                            System.IO.File.Delete(fname);
                            return "true";
                        }


                        

                        System.Diagnostics.Debug.WriteLine(reader.GetValue(0).ToString());
                        items.Add(new Models.Foreign()
                        {


                            ID = int.Parse(reader.GetValue(0).ToString()),
                            Name = reader.GetValue(1).ToString(),
                            DOB = DateTime.Parse(reader.GetValue(2).ToString()).ToString("yyyy/MM/dd"),
                            Father_name = reader.GetValue(3).ToString(),
                            POB = reader.GetValue(4).ToString(),
                            Nationality = reader.GetValue(5).ToString(),
                            Occupation = reader.GetValue(6).ToString(),
                            Designation = reader.GetValue(7).ToString(),
                            Organisation_Name = reader.GetValue(8).ToString(),
                            Organisation_Address = reader.GetValue(9).ToString(),
                            O_Telephone = reader.GetValue(10).ToString(),
                            O_Fax = reader.GetValue(11).ToString(),
                            O_Mobile = reader.GetValue(12).ToString(),
                            Permanent_address = reader.GetValue(13).ToString(),
                            Present_address = reader.GetValue(14).ToString(),
                            P_Telephone = reader.GetValue(15).ToString(),
                            P_Mobile = reader.GetValue(16).ToString(),
                            P_Fax = reader.GetValue(17).ToString(),
                            Place_of_stay = reader.GetValue(18).ToString(),
                            Passport_No = reader.GetValue(19).ToString(),
                            Place_of_issue = reader.GetValue(20).ToString(),
                            Passport_Validity = reader.GetValue(21).ToString(),
                            Visa_No = reader.GetValue(22).ToString(),
                            Visa_Place_of_issue = reader.GetValue(23).ToString(),
                            Visa_Validity = reader.GetValue(24).ToString(),
                            Visa_type = reader.GetValue(25).ToString(),
                            Other_country = reader.GetValue(26).ToString(),

                            From_Date = DateTime.Parse(reader.GetValue(27).ToString()).ToString("yyyy/MM/dd"),
                            To_Date = DateTime.Parse(reader.GetValue(28).ToString()).ToString("yyyy/MM/dd"),

                        });
                        
                    }
                }
            }
            foreach (Foreign item in items)
            {
                //System.Diagnostics.Debug.WriteLine(item.Organisation_Address);
                var result = sqlConnection.Execute("insert into details values(@ID, @Name, @DOB, @Father_name, @POB, @Nationality, @Occupation, @Designation, @Organisation_Name, @Organisation_Address, @O_Telephone, @O_Fax, @O_Mobile, @Permanent_address, @Present_address, @P_Telephone, @P_Mobile, @P_Fax, @Place_of_stay, @Passport_No, @Place_of_issue, @Passport_Validity, @Visa_No, @Visa_Place_of_issue, @Visa_Validity, @Visa_type, @Other_country, @From_Date, @To_Date);",item );
            }
            return "true";
        }
    }
}
