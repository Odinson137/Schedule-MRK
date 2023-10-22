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
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Sasha_Project.Excel;

internal class WorkExcel
{
    //static List<string[]> masMain = new List<string[]>();
    List<ScheduleModel> scheduleModels = new List<ScheduleModel>();

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

    private void Zapol(ref ExcelWorksheet sheet, ref int num, ScheduleModel s)
    {
        sheet.Cells[$"A{num}"].Value = s.Title;
        sheet.Cells[$"B{num}"].Value = s.Room1.Replace("\n", " ");

        sheet.Cells[$"C{num}"].Value = s.Prepod1.Replace("\n", " ");
        sheet.Cells[$"D{num}"].Value = s.Changes1.Replace("\n", " ");
        sheet.Cells[$"E{num}"].Value = s.Office1.Replace("\n", " ");

        num++;
        if (s.RazdelPara)
        {
            sheet.Cells[$"A{num - 1}:A{num}"].Merge = true;
            sheet.Cells[$"B{num}"].Value = s.Room2.Replace("\n", " ");
            sheet.Cells[$"C{num}"].Value = s.Prepod2.Replace("\n", " ");
            sheet.Cells[$"D{num}"].Value = s.Changes2.Replace("\n", " ");
            sheet.Cells[$"E{num}"].Value = s.Office2.Replace("\n", " ");
            num++;
        }

    }

    private void CreateHeader(ref ExcelWorksheet sheet, ref int num, int days, char week, int para)
    {
        WorkDate date = new WorkDate();
        sheet.Cells[$"A{num}:E{num}"].Merge = true;
        sheet.Cells[$"A{num}"].Value = $"{date.GetDaier(days)[1]} {date.GetWeeker(week)[1]} {para} пара {date.GetPara(para)}";
        sheet.Cells[$"F{num}"].Value = date.GetNextMonday(days).ToShortDateString();
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



    private void Selecter(SQLiteDataReader reader)
    {
        ScheduleModel scheduleModel = new ScheduleModel()
        {
            Title = reader.GetString(1),
            Room1 = reader.GetString(2),
            Prepod1 = reader.GetString(3),
            Lesson = reader.GetString(4),
            Changes1 = reader.GetString(5),
            Office1 = reader.GetString(6),
            RazdelPara = reader.GetBoolean(9),
            Room2 = reader.GetString(10),
            Prepod2 = reader.GetString(11),
            Changes2 = reader.GetString(12),
            Office2 = reader.GetString(13),

            //Day = reader.GetString(14),
            Para = reader.GetString(8),

            Lesson2 = reader.GetString(14),
            LessonZamena = reader.GetString(15),
            LessonZamena2 = reader.GetString(16),

            //DopText = Nulling(reader, 18),
            //DopText2 = Nulling(reader, 19),
            //DopTextOffice1 = Nulling(reader, 20),
            //DopTextOffice2 = Nulling(reader, 21)
        };

        scheduleModels.Add(scheduleModel);
    }

    private void SelectBaseSelecter(string week, int days)
    {
        string request = $"SELECT ID, " +
                    $"groups, rooms, prepods, lessons, changes, offices, week, para, razdelsPara, rooms_2, prepods_2, changes_2, offices_2,  " +
                    $"lessons_2, lessons_zamena, lessons_zamena_2 " +
                    $"FROM Tables WHERE week = '{week}' and days = {days} ORDER BY para ASC, groups ASC";
        //string request = $"SELECT * FROM Tables WHERE week = '{week}' AND days = {days} ORDER BY para ASC";
        WorkBase.SelectValues(request, Selecter);
    }

    public void CreateExcel(string week, int days)
    {
        int para = 0;

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

            int i = 0;
            
            if (days != 2)
            {
                para = 1;
                while (scheduleModels[i].Para != "1")
                    i++;
            }

            CreateHeader(ref sheet, ref num, days, week[0], para);
            num++;
            var v = para.ToString();

            for (; i < scheduleModels.Count; i++)
            {
                ScheduleModel s = scheduleModels[i];

                if (s.Prepod1.Length > 1 || s.Prepod2.Length > 1)
                {
                    if (v != s.Para)
                    {
                        num = num + 1;
                        CreateHeader(ref sheet, ref num, days, week[0], ++para);
                        num = num + 1;

                        v = s.Para;
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

class ScheduleModel
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Lesson { get; set; }
    public string DopText { get; set; }
    public string Lesson2 { get; set; }
    public string DopText2 { get; set; }
    public string Room1 { get; set; }
    public string Room2 { get; set; }
    public string Prepod1 { get; set; }
    public string Prepod2 { get; set; }
    public bool RazdelPara { get; set; }
    public string Changes1 { get; set; }
    public string Changes2 { get; set; }
    public string LessonZamena { get; set; }
    public string Office1 { get; set; }
    public string DopTextOffice1 { get; set; }
    public string LessonZamena2 { get; set; }
    public string Office2 { get; set; }
    public string DopTextOffice2 { get; set; }
    public string Day { get; set; }
    public string Para { get; set; }
    public int Page { get; set; }
    public int Numeric { get; set; }

    public ScheduleModel Copy()
    {
        return new ScheduleModel()
        {
            Title = Title,
            Lesson = Lesson2,
            LessonZamena = LessonZamena2,
            Room1 = Room2,
            Prepod1 = Prepod2,
            Changes1 = Changes2,
            Office1 = Office2,
            DopText = DopText2,
            DopTextOffice1 = DopTextOffice2,
        };
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
                        group = group.Replace("+", "");

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
        //List<string> la = 
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

        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            List<Lessons> les = CreateMas(worksheet);
            List<Lessons> list = removeDuplicates(les);
            //var iList = list.OrderBy(x => x.Lesson);

            //CreateBaseLessons(list);
            //CreateBasePrepods(list);
            //CreateBaseGroups(list);

            //MessageBox.Show("good");
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


