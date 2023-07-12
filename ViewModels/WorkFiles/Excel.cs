using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Base;
using System.Collections.Generic;
using System;
using DopFiles;

namespace Sasha_Project.Excel
{
    internal class WorkExcel
    {

        private bool IsWorksheetExist(string sheetName, ExcelPackage package)
        {
            bool result = false;

            foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
            {
                if (worksheet.Name == sheetName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void Zapol(ref ExcelWorksheet sheet, ref int num, string[] s)
        {
            sheet.Cells[$"A{num}"].Value = s[0];
            sheet.Cells[$"B{num}"].Value = s[1].Replace("\n", " ");

            sheet.Cells[$"C{num}"].Value = s[2].Replace("\n", " ");
            sheet.Cells[$"D{num}"].Value = s[4].Replace("\n", " ");
            sheet.Cells[$"E{num}"].Value = s[5].Replace("\n", " ");

            num++;
            if (s[3] == "True")
            {
                sheet.Cells[$"A{num - 1}:A{num}"].Merge = true;
                sheet.Cells[$"B{num}"].Value = s[6].Replace("\n", " ");
                sheet.Cells[$"C{num}"].Value = s[7].Replace("\n", " ");
                sheet.Cells[$"D{num}"].Value = s[8].Replace("\n", " ");
                sheet.Cells[$"E{num}"].Value = s[9].Replace("\n", " ");
                num++;
            }

        }

        private void CreateHeader(ref ExcelWorksheet sheet, ref int num, int days, char week, int para)
        {
            WorkDate date = new WorkDate();
            sheet.Cells[$"A{num}:E{num}"].Merge = true;
            sheet.Cells[$"A{num}"].Value = $"{date.GetDaier(days)[1]} {date.GetWeeker(week)[1]} {para} пара {date.GetPara(para)}";
            sheet.Cells[$"F{num}"].Value = GetDate();
            sheet.Cells[$"A{++num}"].Value = "Группа";
            sheet.Cells[$"B{num}"].Value = "ауд.";
            sheet.Cells[$"C{num}"].Value = "Ф.И.О. преподавателя";
            sheet.Cells[$"D{num}"].Value = "замена";
            sheet.Cells[$"E{num}"].Value = "кафедрально";
            sheet.Cells[$"F{num}"].Value = "подпись";

            var celler = sheet.Cells[$"A{num - 1}:F{num}"];
            celler.Style.Font.Bold = true;
            celler.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void BeatufulTable(ref ExcelWorksheet sheet, int len)
        {
            var cellerValue = sheet.Cells[$"A1:F{len}"];
            cellerValue.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cellerValue.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            cellerValue.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cellerValue.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cellerValue.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cellerValue.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            sheet.Cells.AutoFitColumns();
        }

        private string GetDate()
        {
            DateTime today = DateTime.Today;
            string todayStr = today.ToString("dd.MM.yyyy");
            return todayStr;
        }

        public void CreateExcel(string week, int para, int days)
        {
            DataBase basa = new DataBase();
            WorkDate date = new WorkDate();
            List<string[]> masMain = basa.SelectBaseSelecter(week, days);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var excel = new ExcelPackage(new FileInfo(@"Преподаватели.xlsx")))
            {
                string titleSheet = $"{date.GetDaier(days)[0]}-{date.GetWeeker(week[0])[0]}";
                bool existValue = IsWorksheetExist(titleSheet, excel);

                ExcelWorksheet sheet;
                if (existValue)
                {
                    sheet = excel.Workbook.Worksheets[titleSheet];
                    sheet.Cells.Clear();
                } else
                {
                    sheet = excel.Workbook.Worksheets.Add(titleSheet);
                }

                int num = 1;
                CreateHeader(ref sheet, ref num, days, week[0], para);
                num++;
                string v = para.ToString();

                foreach (string[] s in masMain)
                {
                    if (s[1].Length > 1)
                    {
                        if (v != s[10])
                        {
                            num = num + 1;
                            CreateHeader(ref sheet, ref num, days, week[0], ++para);
                            num = num + 1;

                            v = s[10];
                        }
                     
                        Zapol(ref sheet, ref num, s);
                    }

                }

                BeatufulTable(ref sheet, num-1);

                FileInfo excelFile = new FileInfo(@"Преподаватели.xlsx");
                excel.SaveAs(excelFile);
            }
        }

        //private string UpperFirstLetter(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return str;
        //    }

        //    char[] array = str.ToCharArray();
        //    array[0] = char.ToUpper(array[0]);

        //    return new string(array);
        //}

        //public void SelectOpenendFile()
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    DataBase a = new DataBase();

        //    Dictionary<string, List<string>> mas = new Dictionary<string, List<string>>();

        //    // открытие файла
        //    FileInfo fileInfo = new FileInfo(@"C:/test/+ПЕДНАГРУЗКА 2022-2023 ГЛАВНАЯ_ШФ. (1).xlsx");
        //    int num = 12;
        //    using (ExcelPackage package = new ExcelPackage(fileInfo))
        //    {
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

        //        while (true)
        //        {
        //            object v = worksheet.Cells[$"AQ${num}"].Value;
        //            if (v == null)
        //            {
        //                break;
        //            }

        //            string prepod = v.ToString() ?? "";

        //            if (!prepod.Contains('/'))
        //            {
        //                string[] prepodMas = prepod.Split(' ');
        //                if (prepodMas.Length > 1)
        //                {
        //                    string firstName = prepodMas[0];
        //                    string lastName = prepodMas[1];

        //                    if (lastName != "")
        //                    {
        //                        // получение значения ячейки  
        //                        string value = worksheet.Cells[$"E{num}"].Value.ToString() ?? "";
        //                        value = value.Trim();

        //                        if (value.Contains("п.") || value.Contains("п .") || value.Contains("п2") || value.Contains("п.2"))
        //                        {
        //                            value = value.Split(',')[0];
        //                        };

        //                        if (value.Contains("Физическая культура и здоровье"))
        //                        {
        //                            value = "Физическая культура и здоровье";
        //                        }

        //                        if (!value.Contains("Ответственный") && !value.Contains("Факультативные занятия") && !value.Contains("Факультатив") && !value.Contains(",") && !value.Contains("консультации") && !value.Contains("Председатель"))
        //                        {
        //                            char b = firstName[0];
        //                            firstName = firstName.ToLower();
        //                            firstName = UpperFirstLetter(firstName);

        //                            if (mas.ContainsKey(value))
        //                            {
        //                                if (mas[value].Contains(firstName) == false)
        //                                {
        //                                    mas[value].Add(firstName);
        //                                    a.InserPrepods(value, firstName, lastName);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                mas.Add(value, new List<string>());
        //                                mas[value].Add(firstName);
        //                                a.InserPrepods(value, firstName, lastName);
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //            //break;
        //            num++;
        //        }

        //    }
        //}
    }
}
