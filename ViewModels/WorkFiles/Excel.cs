﻿using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Base;
using System.Collections.Generic;
using System;
using DopFiles;
using System.Linq;

namespace Sasha_Project.Excel;

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
}

class ReadBook
{
    public ReadBook() { }
    string UpperFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        char[] array = str.ToCharArray();
        array[0] = char.ToUpper(array[0]);

        return new string(array);
    }

    List<Lessons> CreateMas(ExcelWorksheet worksheet)
    {
        int num = 11;

        List<Lessons> les = new List<Lessons>();
        while (num < 2000)
        {
            num++;
            object v = worksheet.Cells[$"AQ{num}"].Value;
            if (v != null)
            {
                string l = (string)worksheet.Cells[$"E{num}"].Value;
                string prepod = (string)v;

                if (!l.Contains("ДП") &&
                    !l.Contains("ГКК") &&
                    !l.Contains("Консультации") &&
                    !l.Contains("Ответственный") &&
                    !l.Contains("Председатель") &&
                    !l.Contains("Факультативные") &&
                    !prepod.Contains("Вакансия") &&
                    !prepod.Contains("Сторонние"))
                {

                    if (l.Contains(","))
                    {
                        string[] newMas = l.Split(",");
                        l = newMas[0];
                    }

                    if (l.Contains("Физическая культура")) l = "Физическая культура и здоровье";

                    if (prepod.Contains("/")) continue;

                    string[] prepodMas = prepod.Split(' ');

                    if (prepodMas.Length > 1)
                    {

                        string firstName = prepodMas[0];
                        string lastName = prepodMas[1];

                        firstName = firstName.ToLower();
                        firstName = UpperFirstLetter(firstName);

                        object k = worksheet.Cells[$"AS{num}"].Value;

                        Double kurs;
                        if (k != null)
                        {
                            kurs = (Double)k;

                            les.Add(new Lessons(l, firstName, lastName, kurs));
                        }

                    }
                }
            }
        }
        return les;
    }

    void SelectOpenendFile()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        FileInfo fileInfo = new FileInfo(@"test.xlsx");
        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            List<Lessons> les = CreateMas(worksheet);

            List<T> removeDuplicates<T>(List<T> list)
            {
                return new HashSet<T>(list).ToList();
            }

            List<Lessons> list = removeDuplicates<Lessons>(les);
            var iList = list.OrderBy(x => x.Lesson);

            DataBase a = new DataBase();

            List<string> mas = new List<string>();
            foreach (Lessons l in list)
            {
                if (mas.Contains(l.Lesson))
                {
                    a.InsertLessons(l.Lesson, Convert.ToInt32(l.Kurs));
                }
            }

            foreach (Lessons l in list)
            {
                a.InserPrepods(l.Lesson, l.FirstName, l.LastName);
            }

            //foreach (Lessons l in iList)
            //{
            //    Console.WriteLine($"{l.Lesson}  - {l.Kurs}");
            //}
        }
    }

    struct Lessons
    {
        public string Lesson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Kurs { get; set; }

        public Lessons(string lesson, string firstName, string lastName, double kurs)
        {
            Lesson = lesson;
            FirstName = firstName;
            LastName = lastName;
            Kurs = kurs;
        }
    }
}


