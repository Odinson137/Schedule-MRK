using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Linq;
using Sasha_Project.ViewModels.DopModels;
using System.Data.SQLite;
using Sasha_Project.Excel;

using System;
using System.Windows;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Sasha_Project.Word
{
    class CreateRoomWord
    {
        public CreateRoomWord() { }

        private TableCell CreateCell(string masString)
        {
            TableCell cell = new TableCell();
            
            Paragraph paragraph = new Paragraph(new Run(new Text(masString)));
            
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines() { Before = "0", After = "0", Line = "200" };
            paragraph.ParagraphProperties = paragraphProperties;

            cell.Append(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            cell.Append(paragraph);

            TableCellProperties tableCellProperties = new TableCellProperties();
            TableCellBorders tableCellBorders = new TableCellBorders();
            tableCellBorders.TopBorder = new TopBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellBorders.BottomBorder = new BottomBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellBorders.LeftBorder = new LeftBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellBorders.RightBorder = new RightBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellProperties.TableCellBorders = tableCellBorders;
            tableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            cell.Append(tableCellProperties);

            TableCellWidth tableCellWidth1;
            tableCellWidth1 = new TableCellWidth() { Width = "4000", Type = TableWidthUnitValues.Dxa };
            
            cell.Append(tableCellWidth1);
            return cell;
        }

        private void MergeHorizont(ref TableCell cell1, ref TableCell cell2)
        {
            TableCellProperties cellVerticalProperties = new TableCellProperties();
            cellVerticalProperties.Append(new VerticalMerge()
            {
                Val = MergedCellValues.Restart
            });

            TableCellProperties cellVerticalProperties2 = new TableCellProperties();
            cellVerticalProperties2.Append(new VerticalMerge()
            {
                Val = MergedCellValues.Continue
            });

            cell1.Append(cellVerticalProperties);
            cell2.Append(cellVerticalProperties2);
        }

        private void CreateTable(List<string> mas, Body body, Dictionary<string, Dictionary<string, string>> dict, int numer)
        {
            Table table = new Table();

            TableCell mainCell = CreateCell("");
            TableRow main = new TableRow();
            main.Append(mainCell);

            foreach (string item in mas)
            {
                TableCell cell = CreateCell(item);
                main.Append(cell);
            }
            
            table.Append(main);

            for (; numer <= 7; numer++)
            {
                TableCell cell1 = CreateCell(numer.ToString());
                TableCell cell2 = CreateCell("");
                MergeHorizont(ref cell1, ref cell2);

                TableRow tableRow1 = new TableRow();
                tableRow1.Append(cell1);
                TableRow tableRow2 = new TableRow();
                tableRow2.Append(cell2);

                for (int i = 0, ints = 0; i < mas.Count; i++, ints += 2)
                {
                    string value = dict[numer.ToString()][mas[i]];

                    TableCell a;
                    TableCell b;

                    if (value == "0")
                    {
                        a = CreateCell("");
                        b = CreateCell("");

                        MergeHorizont(ref a, ref b);
                    }
                    else if (value == "1")
                    {
                        a = CreateCell("X");
                        b = CreateCell("");
                    }
                    else if (value == "2")
                    {
                        a = CreateCell("");
                        b = CreateCell("X");
                    }
                    else
                    {
                        a = CreateCell("X");
                        b = CreateCell("X");

                        MergeHorizont(ref a, ref b);
                    }
                    tableRow1.Append(a);
                    tableRow2.Append(b);
                }

                table.Append(tableRow1);
                table.Append(tableRow2);
            }

            body.Append(table);
            Paragraph para = new Paragraph();
            body.AppendChild(para);
        }

        public void CreateWord(string week, string day)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Create($"Комнаты_{week}_{day}.docx", WordprocessingDocumentType.Document))
            {
                // Добавление основной части документа
                MainDocumentPart mainPart = document.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                int sheetWidth = 16839;

                // Создание раздела документа
                SectionProperties sectionProps = new SectionProperties();
                PageSize pageSize = new PageSize() { Width = 16839U, Height = 11907U, Orient = PageOrientationValues.Landscape };
                sectionProps.Append(pageSize);

                body.Append(sectionProps);

                WorkDate date = new WorkDate();

                string text = $"{date.GetDaier(int.Parse(day))[1]} {date.GetNextMonday(int.Parse(day)).ToShortDateString()}";

                Run run = new Run(new Text(text));
                run.RunProperties = new RunProperties();

                run.RunProperties.AppendChild(new Bold());
                run.RunProperties.AppendChild(new FontSize() { Val = "40" });

                Paragraph paragraph = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
                paragraph.Append(run);

                body.AppendChild(paragraph);

                WorkRoom a = new WorkRoom("FreeRooms", "rooms");
                List<CreateRoom> mas = a.SelectValues();
                Dictionary<string, Dictionary<string, string>> dict = a.SelectAllRooms(week, day, mas);

                List<string> sortMas = new List<string>();
                int nums = 1;

                int numer = day == "2" ? 0 : 1;

                foreach (CreateRoom item in mas)
                {
                    sortMas.Add(item.Value);
                    if (nums == 22)
                    {
                        CreateTable(sortMas, body, dict, numer);
                        
                        nums = 0;
                        sortMas.Clear();
                    }
                    nums++;
                }

                // Сохранение документа
                mainPart.Document.Save();
            }
        }


    }
    class WorkWord
    {

        List<ScheduleModel> groups = new List<ScheduleModel>();
        public WorkWord() { }

        private Paragraph CreateParagraph(string text, bool textBold, string textFont, bool change = false)
        {
            Run run = new Run(new Text(text));
            run.RunProperties = new RunProperties();

            //RunFonts runFonts = new RunFonts()
            //{
            //    Ascii = "Times New Roman",
            //    HighAnsi = "Times New Roman",
            //    ComplexScript = "Times New Roman"
            //};

            //run.RunProperties.AppendChild(runFonts);

            if (textBold)
            {
                run.RunProperties.AppendChild(new Bold());
            }

            run.RunProperties.AppendChild(new FontSize() { Val = textFont });

            Paragraph paragraph = new Paragraph(run);

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            if (change)
            {
                paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines() { Before = "0", After = "0", Line = "150" };
            } else
            {
                paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines() { Before = "10", After = "10", Line = "200" };

            }
            paragraph.ParagraphProperties = paragraphProperties;

            return paragraph;
        }

        private TableCell CreateCell(int num, string masString, string textFont, bool textBold, string width = "10000", bool change = false)
        {
            TableCell cell = new TableCell();
            int n = 0;
            if (masString.Contains("|"))
            {
                foreach (string i in masString.Split("|"))
                {
                    if (!string.IsNullOrEmpty(i))
                    {
                        Paragraph paragraph;
                        if (i.ToLower().Contains("практика") || i.ToLower().Contains("обучение"))
                        {
                            paragraph = CreateParagraph(i, true, "20");
                        }
                        else
                        {
                            paragraph = CreateParagraph(i, textBold, textFont);
                        }
                        cell.Append(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
                        cell.Append(paragraph);
                    } else
                    {
                        n++;
                    }
                }
                if (n == 3)
                {
                    Paragraph paragraph = CreateParagraph(" ", false, "16");
                    cell.Append(paragraph);
                }
            }
            else
            {
                Paragraph paragraph = CreateParagraph(masString, textBold, textFont, change);

                cell.Append(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
                cell.Append(paragraph);
            }

            TableCellProperties tableCellProperties = new TableCellProperties();
            TableCellBorders tableCellBorders = new TableCellBorders();
            tableCellBorders.TopBorder = new TopBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellBorders.BottomBorder = new BottomBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellBorders.LeftBorder = new LeftBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellBorders.RightBorder = new RightBorder() { Val = BorderValues.Single, Size = 4 };
            tableCellProperties.TableCellBorders = tableCellBorders;
            tableCellProperties.TableCellVerticalAlignment = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

            cell.Append(tableCellProperties);

            TableCellWidth tableCellWidth1;
            if (num == 0)
            {
                tableCellWidth1 = new TableCellWidth() { Width = "4000", Type = TableWidthUnitValues.Dxa };
            }
            else
            {
                tableCellWidth1 = new TableCellWidth() { Width = width, Type = TableWidthUnitValues.Dxa };
            }
            cell.Append(tableCellWidth1);

            return cell;
        }

        private void MergeHorizont(TableCell cell1, TableCell cell2)
        {
            TableCellProperties cellOneProperties = new TableCellProperties();
            cellOneProperties.Append(new HorizontalMerge()
            {
                Val = MergedCellValues.Restart
            });

            TableCellProperties cellTwoProperties = new TableCellProperties();
            cellTwoProperties.Append(new HorizontalMerge()
            {
                Val = MergedCellValues.Continue
            });

            cell1.Append(cellOneProperties);
            cell2.Append(cellTwoProperties);
        }

        private void FromMas(Table table, ref TableCell[] masRow1, string[] mas, string textFont, bool textBold)
        {
            int num = 0;

            TableRow headerRow = new TableRow();

            foreach (string masString in mas)
            {
                TableCell cell = CreateCell(num, masString, textFont, textBold);

                TableCell cellDop = CreateCell(num, "", textFont, textBold);

                MergeHorizont(cell, cellDop);

                masRow1[num] = cell;
                num++;
                headerRow.Append(cell, cellDop);
            }
            table.Append(headerRow);
        }

        void CreateHeader(ref Table table, bool infoHour)
        {
            TableCell[] masRow1;
            TableCell[] masRow2;
            string[] masString1;
            string[] masString2;
            if (infoHour)
            {
                masRow1 = new TableCell[9];
                masRow2 = new TableCell[9];

                masString1 = new string[] { "№ группы", "", "1 пара", "2 пара", "3 пара", "4 пара", "5 пара", "6 пара", "7 пара" };
                masString2 = new string[] { "", "8:00-8:20", "8:30-10.05", "10:15-11.50", "12.10-13.45", "13:55-15.30", "15:50-17.25", "17:35-19.10", "19:20-20.55" };
            }
            else
            {
                masRow1 = new TableCell[8];
                masRow2 = new TableCell[8];

                masString1 = new string[] { "№ группы", "1 пара", "2 пара", "3 пара", "4 пара", "5 пара", "6 пара", "7 пара" };
                masString2 = new string[] { "", "8:30-10.05", "10:15-11.50", "12.10-13.45", "13:55-15.30", "15:50-17.25", "17:35-19.10", "19:20-20.55" };
            }

            FromMas(table, ref masRow1, masString1, "26", true);
            FromMas(table, ref masRow2, masString2, "18", false);

            ToOneCell(ref masRow1[0], ref masRow2[0]);
        }

        private void ToOneCell(ref TableCell free, ref TableCell cell)
        {
            TableCellProperties cellVerticalProperties = new TableCellProperties();
            cellVerticalProperties.Append(new VerticalMerge()
            {
                Val = MergedCellValues.Restart
            });

            TableCellProperties cellVerticalProperties2 = new TableCellProperties();
            cellVerticalProperties2.Append(new VerticalMerge()
            {
                Val = MergedCellValues.Continue
            });

            free.Append(cellVerticalProperties);
            cell.Append(cellVerticalProperties2);
        }

        private void Merging(ref TableRow groupFreeRow, ref TableRow groupRow, TableCell cell, TableCell free, bool value = true)
        {
            TableCell cellDop = CreateCell(1, "", "20", false);
            TableCell cellDop2 = CreateCell(1, "", "20", false);

            MergeHorizont(cell, cellDop);
            MergeHorizont(free, cellDop2);

            if (value)
            {
                ToOneCell(ref cell, ref free);
            }

            groupFreeRow.Append(cell, cellDop);
            groupRow.Append(free, cellDop2);
        }
        private string MakeText(ScheduleModel oneTab, int i = 0)
        {
            string readyText;
            if (oneTab.Lesson == " ")
            {
                readyText = "";
            }
            else
            {
                if (i == 0)
                {
                    readyText = $"{oneTab.Lesson}{(!string.IsNullOrEmpty(oneTab.DopText) && !string.IsNullOrWhiteSpace(oneTab.DopText) ? $" {oneTab.DopText}"  : string.Empty)}|{oneTab.Prepod1}|{oneTab.Room1}";
                } else if (i == 1)
                {
                    readyText = $"{oneTab.Lesson}{(!string.IsNullOrEmpty(oneTab.DopText) && !string.IsNullOrWhiteSpace(oneTab.DopText) ? $" {oneTab.DopText}"  : string.Empty)}|{oneTab.Changes1}|{oneTab.Room1}";
                    //readyText = $"{oneTab.Lesson}|{oneTab.Changes1}|{oneTab.Room1}";
                } else
                {
                    readyText = $"{oneTab.LessonZamena}{(!string.IsNullOrEmpty(oneTab.DopTextOffice1) && !string.IsNullOrWhiteSpace(oneTab.DopTextOffice1) ? $" {oneTab.DopTextOffice1}"  : string.Empty)}|{oneTab.Office1}|{oneTab.Room1}";
                    //readyText = $"{oneTab.Office1}|{oneTab.Office1}|{oneTab.Room1}";
                }
            }
            return readyText;
        }

        private void CreateHalfChanged(ScheduleModel oneTab, int index, int i, ref TableRow groupFreeRow, ref TableRow groupRow)
        {
            string readyText = MakeText(oneTab, index);

            TableCell freeCell = CreateCell(1, "Замена", "16", true, change: true);

            TableCell paraCell = CreateCell(i, readyText, "16", false);

            groupFreeRow.Append(freeCell);
            groupRow.Append(paraCell);
        }

        private void CreateHalf(ScheduleModel oneTab, int i, ref TableRow groupFreeRow, ref TableRow groupRow)
        {

            string readyText;
            if (oneTab.Changes1.Length > 1)
            {
                CreateHalfChanged(oneTab, 1, 1, ref groupFreeRow, ref groupRow);
            }
            else if (oneTab.LessonZamena.Length > 2)
            {
                CreateHalfChanged(oneTab, 1, 2, ref groupFreeRow, ref groupRow);
            }

            else
            {
                readyText = MakeText(oneTab);
                TableCell paraCell = CreateCell(i, readyText, "16", false);
                TableCell freeCell = CreateCell(1, "", "13", false);
                ToOneCell(ref paraCell, ref freeCell);
                groupFreeRow.Append(paraCell);
                groupRow.Append(freeCell);
            }
        }

        private void createChanges(ScheduleModel oneTab, int index, ref TableRow groupFreeRow, ref TableRow groupRow)
        {
            string readyText = MakeText(oneTab, index);
            TableCell freeCell = CreateCell(1, "Замена", "16", true, change: true);
            TableCell paraCell = CreateCell(1, readyText, "16", false);

            Merging(ref groupFreeRow, ref groupRow, freeCell, paraCell, false);
        }

        private void CreateGroupLine(ref Table table, List<ScheduleModel> group, bool infoHour)
        {
            TableRowProperties rowProperties = new TableRowProperties(new TableRowHeight { Val = 400U, HeightType = HeightRuleValues.AtLeast });
            TableRowProperties rowProperties1 = new TableRowProperties(new TableRowHeight { Val = 400U, HeightType = HeightRuleValues.AtLeast });

            TableRow groupRow = new TableRow(rowProperties);
            TableRow groupFreeRow = new TableRow(rowProperties1);


            TableCell cell = CreateCell(0, group[0].Title, "32", true);
            TableCell free = CreateCell(1, "", "14", false);

            string? last = "";

            Merging(ref groupFreeRow, ref groupRow, cell, free);

            for (int i = 0; i < (infoHour ? 8 : 7); i++)
            {
                ScheduleModel tab = group[i];
                if (!tab.RazdelPara)
                {
                    if (tab.Changes1.Length > 2)
                    {
                        createChanges(tab, 1, ref groupFreeRow, ref groupRow);
                    }
                    else if (tab.Office1.Length > 2)
                    {
                        createChanges(tab, 2, ref groupFreeRow, ref groupRow);
                    }
                    else
                    {
                        string readyText = MakeText(tab);
                        TableCell paraCell = CreateCell(1, "", "14", false);
                        TableCell freeCell = CreateCell(i, readyText, "16", false);

                        Merging(ref groupFreeRow, ref groupRow, freeCell, paraCell);

                        if ((last.Contains("обучение") || last.Contains("практика")) && readyText.Length == 2 && (!readyText.ToLower().Contains("практика") || !readyText.ToLower().Contains("обучение")))
                        {
                            TableCellProperties cellOneProperties = new TableCellProperties();
                            cellOneProperties.Append(new HorizontalMerge()
                            {
                                Val = MergedCellValues.Continue
                            });

                            TableCellProperties cellTwoProperties = new TableCellProperties();
                            cellTwoProperties.Append(new HorizontalMerge()
                            {
                                Val = MergedCellValues.Continue
                            });

                            paraCell.Append(cellOneProperties);
                            freeCell.Append(cellTwoProperties);
                        }
                        else
                        {
                            TableCellProperties cellOneProperties = new TableCellProperties();
                            cellOneProperties.Append(new HorizontalMerge()
                            {
                                Val = MergedCellValues.Restart
                            });

                            paraCell.Append(cellOneProperties);

                            last = readyText.ToLower();
                        }


                    }
                }
                else
                {
                    CreateHalf(tab, i, ref groupFreeRow, ref groupRow);

                    ScheduleModel oneTab = tab.Copy();

                    CreateHalf(oneTab, i, ref groupFreeRow, ref groupRow);
                }
            }

            table.Append(groupFreeRow);
            table.Append(groupRow);
        }

        private void CreateText(ref Body body, string text, bool value = false, bool bold = false)
        {
            Run run = new Run(new Text(text));
            run.RunProperties = new RunProperties();

            //RunFonts runFonts = new RunFonts()
            //{
            //    Ascii = "Times New Roman",
            //    HighAnsi = "Times New Roman",
            //    ComplexScript = "Times New Roman"
            //};
            //run.RunProperties.AppendChild(runFonts);

            run.RunProperties.AppendChild(new FontSize() { Val = "20" });
            if (bold)
            {
                run.RunProperties.AppendChild(new Bold());
            }

            Paragraph paragraph;
            if (value)
            {
                paragraph = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Right }));
            } else
            {
                paragraph = new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            }

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines() { Before = "0", After = "0", Line = "200" };
            paragraph.Append(paragraphProperties);

            paragraph.Append(run);

            body.AppendChild(paragraph);
        }

        Table NewRazdel(Body body, Table table)
        {
            body.Append(table);
            table = new Table();
            Paragraph paragraph = new Paragraph(new Break() { Type = BreakValues.Page });
            body.AppendChild(paragraph);
            return table;
        }

        void CreateHeaderText(ref Body body, WorkDate date, string text, int day)
        {
            CreateText(ref body, "УТВЕРЖДЕНО                                                .", true);
            CreateText(ref body, "Заместитель директора по УР                  .", true);
            CreateText(ref body, "_______________________ Ф.С.Шумчик ", true);

            CreateText(ref body, "ИЗМЕНЕНИЯ");
            CreateText(ref body, $"в расписании учебных занятий групп {text} формы получения образования на");
            CreateText(ref body, $"{date.GetNextMonday(day).ToShortDateString()}", false, false);
            //CreateText(ref body, "");
        }

        private static string Nulling(SQLiteDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
            {
                return reader.GetString(index);
            }
            else return "";
        }

        private void SelecterWord(SQLiteDataReader reader)
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

                Para = reader.GetString(8),

                Lesson2 = reader.GetString(14),
                LessonZamena = reader.GetString(15),
                LessonZamena2 = reader.GetString(16),

                DopText = Nulling(reader, 17),
                DopText2 = Nulling(reader, 18),
                DopTextOffice1 = Nulling(reader, 19),
                DopTextOffice2 = Nulling(reader, 20),

                Page = reader.GetInt32(21),
                Numeric = reader.GetInt32(22)
            };

            groups.Add(scheduleModel);

        }

        private void SelectForWord(string week, int day)
        {
            //groups = new Dictionary<string, Dictionary<int, Tables[]>>();

            string request = $"SELECT t.ID, t.groups, t.rooms, t.prepods, t.lessons, t.changes, t.offices, t.week, t.para, " +
                $"t.razdelsPara, t.rooms_2, t.prepods_2, t.changes_2, t.offices_2, t.lessons_2, t.lessons_zamena, t.lessons_zamena_2, " +
                $"t.dopTextLesson, t.dopTextLesson_2, t.dopTextLessonZamena, t.dopTextLessonZamena_2, g.Page, g.Numeric " +
                $"FROM Tables t " +
                $"INNER JOIN Groups g ON t.groups = g.groups " +
                $"WHERE t.week = '{week}' AND t.days = {day} " +
                $"ORDER BY t.groups ASC";

            WorkBase.SelectValues(request, SelecterWord);
        }

        public void MainDocument(int day, string week)
        {
            try
            {
                using (WordprocessingDocument document = WordprocessingDocument.Create($"Учащиеся_{week}_{day}.docx", WordprocessingDocumentType.Document))
                {
                    // Добавление основной части документа
                    MainDocumentPart mainPart = document.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = mainPart.Document.AppendChild(new Body());

                    int sheetWidth = 16839;

                    // Создание раздела документа
                    SectionProperties sectionProps = new SectionProperties();
                    PageSize pageSize = new PageSize() { Width = 16839U, Height = 11907U, Orient = PageOrientationValues.Landscape };
                    PageMargin pageMargin1 = new PageMargin() { Left = 720U, Right = 720U, Top = 720, Bottom = 720 };

                    sectionProps.Append(pageMargin1);
                    sectionProps.Append(pageSize);

                    WorkDate date = new WorkDate();

                    body.Append(sectionProps);

                    CreateHeaderText(ref body, date, "дневной", day);
                    CreateText(ref body, "Made by Yuri", true);

                    Table table = new Table();

                    SelectForWord(week, day);

                    int maxPage = groups.Max(x => x.Page);
                    int maxMumeric;
                    for (int i = 1; i < maxPage; i++)
                    {
                        if (i != 1)
                        {
                            table = NewRazdel(body, table);
                        }

                        maxMumeric = groups.Where(x => x.Page == i).Max(x => x.Numeric);

                        bool infoHour = groups.Where(x => x.Page == i && x.Para == "0").Any(x => !string.IsNullOrEmpty(x.Lesson));

                        CreateHeader(ref table, infoHour);

                        for (int a = 1; a <= maxMumeric; a++)
                        {
                            IOrderedEnumerable<ScheduleModel> request;
                            if (infoHour)
                                request = groups.Where(x => x.Page == i && x.Numeric == a).OrderBy(x => x.Title).OrderBy(x => x.Para);
                            else
                                request = groups.Where(x => x.Page == i && x.Numeric == a && x.Para != "0").OrderBy(x => x.Title).OrderBy(x => x.Para);

                            List<ScheduleModel> list = request.ToList();
                            CreateGroupLine(ref table, list, infoHour);
                        }
                    }

                    table = NewRazdel(body, table);
                    CreateHeaderText(ref body, date, "заочной/дистанционной", day);
                    CreateText(ref body, "Made by Yuri", true);

                    CreateHeader(ref table, false);
                    maxMumeric = groups.Where(x => x.Page == maxPage).Max(x => x.Numeric);

                    for (int a = 1; a <= maxMumeric; a++)
                    {
                        List<ScheduleModel> list = groups.Where(x => x.Page == maxPage && x.Numeric == a && x.Para != "0").OrderBy(x => x.Title).OrderBy(x => x.Para).ToList();
                        CreateGroupLine(ref table, list, false);
                    }
                    // Устанавливаем свойство, чтобы растянуть таблицу на всю ширину листа
                    TableProperties tableProperties = new TableProperties(new TableWidth { Width = "100%", Type = TableWidthUnitValues.Pct });
                    table.AppendChild(tableProperties);
                    body.Append(table);

                    // Сохранение документа
                    mainPart.Document.Save();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Закройте предыдущий ворд файл, " + ex.Message.ToString());
            }


        }
    }

}
