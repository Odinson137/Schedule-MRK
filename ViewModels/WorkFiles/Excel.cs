using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System;
using System.Linq;
using Sasha_Project.ViewModels.DopModels;
using System.Windows;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Microsoft.VisualBasic;
using System.Data.SQLite;
using Sasha_Project.Models.SettingsModels;

namespace Sasha_Project.Excel;

internal class WorkExcel
{
    static List<string[]> masMain = new List<string[]>();

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

    private static void Selecter(SQLiteDataReader reader)
    {
        string[] mas = new string[11];

        mas[0] = reader.GetString(1);
        mas[1] = reader.GetString(2);
        mas[2] = reader.GetString(3);
        mas[3] = reader.GetBoolean(8).ToString();
        mas[4] = reader.GetString(4);
        mas[5] = reader.GetString(5);

        mas[6] = reader.GetString(9);
        mas[7] = reader.GetString(10);
        mas[8] = reader.GetString(11);
        mas[9] = reader.GetString(12);
        mas[10] = reader.GetString(7);

        masMain.Add(mas);
    }

    private static void SelectBaseSelecter(string week, int days)
    {
        string request = $"SELECT * FROM Tables WHERE week = '{week}' AND days = {days} ORDER BY para ASC";
        WorkBase.SelectValues(request, Selecter);
    }

    public void CreateExcel(string week, int para, int days)
    {
        WorkDate date = new WorkDate();
        SelectBaseSelecter(week, days);
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

                        l = l.Trim();

                        string group = (string)worksheet.Cells[$"A{num}"].Value;

                        Double kurs;
                        if (k != null)
                        {
                            kurs = (Double)k;

                            les.Add(new Lessons(l, firstName, lastName, group, kurs));
                        }

                    }
                }
            }
        }
        return les;
    }

    private List<T> removeDuplicates<T>(List<T> list)
    {
        return new HashSet<T>(list).ToList();
    }

    private List<IOnlyPrepods> removeDuplicatesTwo(List<IOnlyPrepods> list)
    {
        List<Tuple<string, string>> checkMas = new List<Tuple<string, string>>();
        List<IOnlyPrepods> mas = new List<IOnlyPrepods>();
        foreach (IOnlyPrepods l in list)
        {
            Tuple<string, string> tuple = Tuple.Create(l.Lesson, l.FirstName);
            if (!checkMas.Contains(tuple))
            {
                mas.Add(l);
                checkMas.Add(tuple);
            }
        }
        return mas;
    }

    private void CreateBasePrepods(List<Lessons> list)
    {
        DataBase.CLeanTable("Prepods");

        List<IOnlyPrepods> interfaceList = list.ConvertAll(s => (IOnlyPrepods)s);
        List<IOnlyPrepods> listTwo = removeDuplicatesTwo(interfaceList);

        int num = 0;
        foreach (IOnlyPrepods l in listTwo)
        {
            DataBase.InserPrepods(num++, l.Lesson, l.FirstName, l.LastName);
        }
    }

    private void CreateBaseLessons(List<Lessons> list)
    {
        DataBase.CLeanTable("Lessons");

        //List<LessonModel> mas = new List<LessonModel>();
        var lsits = list.DistinctBy(x => x.Lesson);
        int num = 0;
        foreach (Lessons l in lsits)
        {
            //int i = (int)l.Kurs;
            //mas.Add(new LessonModel()
            //{
            //    Lesson = l.Lesson,
            //});
            //if (!mas.Contains(l.Lesson))
            //{
            DataBase.InsertLessons(num++, l.Lesson);
                //mas.Add(l.Lesson);
            //}
        }
    }

    private void CreateBaseTables(List<string> listGroups)
    {
        DataBase.CLeanTable("Tables");

        DataBase.InsertGeneraltBase(listGroups);
    }

    private void CreateBaseGroups(List<Lessons> list)
    {
        DataBase.CLeanTable("Groups");

        Dictionary<string, string> spec = DataBase.SelectSpecCode();
        List<string> interfaceGroupd = list.ConvertAll(s => s.Group);
        List<string> listGroups = removeDuplicates(interfaceGroupd);

        int id = 1;
        foreach (string group in listGroups)
        {
            string code = group.Substring(2, 2);
            DataBase.InsertGroups(group, spec[code], id++);
        }

        CreateBaseTables(listGroups);
    }

    public void SelectOpenendFile(FileInfo fileInfo)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //FileInfo fileInfo = new FileInfo(@"test.xlsx");
        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            List<Lessons> les = CreateMas(worksheet);
            List<Lessons> list = removeDuplicates(les);
            //var iList = list.OrderBy(x => x.Lesson);

            CreateBaseLessons(list);
            CreateBasePrepods(list);
            CreateBaseGroups(list);

            MessageBox.Show("good");
        }
    }


    interface IOnlyPrepods
    {
        public string Lesson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    struct Lessons : IOnlyPrepods
    {
        public string Lesson { get; set; }
        public string Group { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Kurs { get; set; }

        public Lessons(string lesson, string firstName, string lastName, string group, double kurs)
        {
            Lesson = lesson;
            FirstName = firstName;
            LastName = lastName;
            Group = group;
            Kurs = kurs;
        }
    }
}


