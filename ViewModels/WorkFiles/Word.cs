using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Linq;
using MainTable;
using Sasha_Project.ViewModels.DopModels;
using System.Data.SQLite;
using DocumentFormat.OpenXml.Bibliography;

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

        private void AddToRow(ref TableRow[] rows, TableCell cell, int i)
        {
            TableRow row = new TableRow();
            row.Append(cell);
            rows[i] = row;
        }

        private void CreateTable(List<string> mas, Body body, Dictionary<string, Dictionary<string, string>> dict)
        {
            Table table = new Table();

            TableCell mainCell = CreateCell("");
            TableRow main = new TableRow();
            main.Append(mainCell);

            TableRow[] rows = new TableRow[14];

            int strNumer = 1;
            int numer = 1;
            for (int i = 0; i < rows.Length; i += 2, strNumer += 2, numer++)
            {
                TableCell a = CreateCell(numer.ToString());
                TableCell b = CreateCell("");
                MergeHorizont(ref a, ref b);
            
                AddToRow(ref rows, a, i);
                AddToRow(ref rows, b, strNumer);
            }

            foreach (string item in mas)
            {
                main.Append(CreateCell(item));

                int nums = 1;
                int ints = 1;
                for (int i = 0; i < rows.Length; i += 2, ints += 2)
                {
                    string strNums = nums.ToString();
                    string value = dict[strNums][item];

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

                    rows[i].Append(a);
                    rows[ints].Append(b);
                    nums++;
                }
            }

            table.Append(main);
            foreach (TableRow i in rows)
            {
                table.Append(i);
            }
            body.Append(table);
            Paragraph para = new Paragraph();
            body.AppendChild(para);
        }

        public void CreateWord(string week, string day)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Create("Комнаты.docx", WordprocessingDocumentType.Document))
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
                foreach (CreateRoom item in mas)
                {
                    sortMas.Add(item.Value);
                    if (nums == 22)
                    {
                        CreateTable(sortMas, body, dict);
                        
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

        Dictionary<string, List<Group>> mas;

        Dictionary<string, Dictionary<int, Tables[]>> groups;

        Dictionary<string, List<string>> departMas;

        public WorkWord() { }
        private Paragraph CreateParagraph(string text, bool textBold, string textFont)
        {
            Run run = new Run(new Text(text));
            //run.RunProperties = new RunProperties(new RunFonts() { Ascii = "Times New Roman" });
            run.RunProperties = new RunProperties();
            if (textBold)
            {
                run.RunProperties.AppendChild(new Bold());
            }
            run.RunProperties.AppendChild(new FontSize() { Val = textFont });

            Paragraph paragraph = new Paragraph(run);

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            paragraphProperties.SpacingBetweenLines = new SpacingBetweenLines() { Before = "0", After = "0", Line = "200" };
            paragraph.ParagraphProperties = paragraphProperties;

            return paragraph;
        }

        private TableCell CreateCell(int num, string masString, string textFont, bool textBold, string width = "10000")
        {
            TableCell cell = new TableCell();
            if (masString.Contains("|"))
            {
                foreach (string i in masString.Split("|"))
                {
                    Paragraph paragraph = CreateParagraph(i, textBold, textFont);
                    cell.Append(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
                    cell.Append(paragraph);
                }
            }
            else
            {
                Paragraph paragraph = CreateParagraph(masString, textBold, textFont);
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

        void CreateHeader(ref Table table)
        {
            TableCell[] masRow1 = new TableCell[8];
            TableCell[] masRow2 = new TableCell[8];

            string[] masString1 = new string[] { "№ группы", "1 пара", "2 пара", "3 пара", "4 пара", "5 пара", "6 пара", "7 пара" };

            string[] masString2 = new string[] { "", "8:30-10.05", "10:15-11.50", "12.10-13.45", "13:55-15.30", "15:50-17.25", "17:35-19.10", "19:20-20.55" };

            FromMas(table, ref masRow1, masString1, "28", true);
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

        private string CheckShortTitle(string title)
        {
            if (title.Contains("("))
            {
                return title;
            }
            char[] spliters = new char[] { '-', ' ' };
            string[] mas = title.Split(spliters);
            string new_title;
            if (mas.Length > 2)
            {
                new_title = "";
                foreach (string i in mas)
                {
                    new_title += i.ToUpper().First();
                }
            } else
            {
                new_title = title;
            }

            return new_title;
        }

        private string MakeText(Tables oneTab, int i = 0)
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
                    readyText = $"{CheckShortTitle(oneTab.Lesson)}|{oneTab.Prepods}|{oneTab.Rooms}";
                } else if (i == 1)
                {
                    readyText = $"{CheckShortTitle(oneTab.Lesson)}|{oneTab.Changes}|{oneTab.Rooms}";
                } else
                {
                    readyText = $"{CheckShortTitle(oneTab.OfficeLesson)}|{oneTab.Office}|{oneTab.Rooms}";
                }
            }
            return readyText;
        }

        private void CreateHalfChanged(Tables oneTab, int index, int i, ref TableRow groupFreeRow, ref TableRow groupRow)
        {
            string readyText = MakeText(oneTab, index);

            TableCell freeCell = CreateCell(1, "замена", "16", true);
            TableCell paraCell = CreateCell(i, readyText, "16", false);

            groupFreeRow.Append(freeCell);
            groupRow.Append(paraCell);
        }

        private void CreateHalf(Tables oneTab, int i, ref TableRow groupFreeRow, ref TableRow groupRow)
        {

            string readyText;
            if (oneTab.Changes.Length > 1)
            {
                CreateHalfChanged(oneTab, 1, 1, ref groupFreeRow, ref groupRow);
            }
            else if (oneTab.OfficeLesson.Length > 2)
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

        private void createChanges(Tables oneTab, int index, ref TableRow groupFreeRow, ref TableRow groupRow)
        {
            string readyText = MakeText(oneTab, index);
            TableCell freeCell = CreateCell(1, "замена", "16", true);
            TableCell paraCell = CreateCell(1, readyText, "16", false);

            Merging(ref groupFreeRow, ref groupRow, freeCell, paraCell, false);
        }

        private void CreateGroupLine(ref Table table, string titleGroup, Dictionary<int, Tables[]> valueGroup)
        {
            TableRow groupRow = new TableRow();
            TableRow groupFreeRow = new TableRow();

            TableCell cell = CreateCell(0, titleGroup, "32", true);
            TableCell free = CreateCell(1, "", "14", false);

            Merging(ref groupFreeRow, ref groupRow, cell, free);

            for (int i = 1; i < 8; i++)
            {
                Tables[] tab = valueGroup[i];
                if (tab.Length == 1)
                {
                    Tables oneTab = tab[0];

                    if (oneTab.Changes.Length > 2)
                    {
                        createChanges(oneTab, 1, ref groupFreeRow, ref groupRow);
                    }
                    else if (oneTab.OfficeLesson.Length > 2)
                    {
                        createChanges(oneTab, 2, ref groupFreeRow, ref groupRow);
                    }
                    else
                    {
                        string readyText = MakeText(oneTab);
                        TableCell paraCell = CreateCell(1, "", "14", false);
                        TableCell freeCell = CreateCell(i, readyText, "16", false);

                        Merging(ref groupFreeRow, ref groupRow, freeCell, paraCell);
                    }
                }
                else
                {
                    CreateHalf(tab[0], i, ref groupFreeRow, ref groupRow);

                    CreateHalf(tab[1], i, ref groupFreeRow, ref groupRow);
                }
            }
            table.Append(groupFreeRow);
            table.Append(groupRow);
        }

        private void CreateText(ref Body body, string text, bool value = false, bool bold = false)
        {
            Run run = new Run(new Text(text));
            //run.RunProperties.AppendChild();
            run.RunProperties = new RunProperties();
            //run.RunProperties.AppendChild(new RunFonts() { Ascii = "Times New Roman Italic" });
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
            CreateText(ref body, "_______________________Ф.С.Шумчик ", true);

            CreateText(ref body, "ИЗМЕНЕНИЯ");
            CreateText(ref body, $"в расписании учебных занятий групп {text} формы получения образования на");
            CreateText(ref body, $"{date.GetNextMonday(day).ToShortDateString()}", false, false);
            //CreateText(ref body, "");
        }

        private void SelecterGroup(SQLiteDataReader reader)
        {
            string group = reader.GetString(1);
            string office = reader.GetString(2);
            int dist = reader.GetInt32(3);
            bool distant = false;
            if (dist == 1) distant = true;

            if (mas.ContainsKey(office))
                mas[office].Add(new Group() { Gr = group, Distant = distant });
            else
                mas.Add(office, new List<Group>() { new Group() { Gr = group, Distant = distant } });
        }

        private void SelectGroups()
        {
            mas = new Dictionary<string, List<Group>>();
            string request = "SELECT * FROM Groups ORDER BY offices ASC";
            WorkBase.SelectValues(request, SelecterGroup);
        }

        private void SelecterWord(SQLiteDataReader reader)
        {
            string titleGroup = reader.GetString(1);
            string para = reader.GetString(7);

            Tables[] tables;

            bool RazdelPara = reader.GetBoolean(8);

            Tables table = new Tables()
            {
                Groups = titleGroup,
                Rooms = reader.GetString(2),
                Prepods = reader.GetString(3),
                Changes = reader.GetString(4),
                Office = reader.GetString(5),
                Lesson = reader.GetString(14),
                OfficeLesson = reader.GetString(16),
            };

            if (RazdelPara)
            {
                tables = new Tables[2];

                Tables tableTwo = new Tables()
                {
                    Groups = "Вторая половина",
                    Rooms = reader.GetString(9),
                    Prepods = reader.GetString(10),
                    Changes = reader.GetString(11),
                    Office = reader.GetString(12),
                    Lesson = reader.GetString(15),
                    OfficeLesson = reader.GetString(17),
                };

                tables[0] = table;
                tables[1] = tableTwo;

            }
            else
            {
                tables = new Tables[1];
                tables[0] = table;
            }

            int paraInt = int.Parse(para);
            if (groups.ContainsKey(titleGroup))
            {
                groups[titleGroup].Add(paraInt, tables);
            }
            else
            {
                groups.Add(titleGroup, new Dictionary<int, Tables[]> { { paraInt, tables } });
            }
        }

        private void SelectForWord(string week, int day)
        {
            groups = new Dictionary<string, Dictionary<int, Tables[]>>();
            mas = new Dictionary<string, List<Group>>();
            string request = $"SELECT * FROM Tables WHERE week = '{week}' AND days = {day} ORDER BY groups ASC";
            WorkBase.SelectValues(request, SelecterWord);
        }

        Dictionary<string, List<string>> groupsDict = new Dictionary<string, List<string>>() {
            { "1", new List<string>()},
            { "2", new List<string>()},
            { "3", new List<string>()},
            { "4", new List<string>()},
            { "5", new List<string>()},
            { "6", new List<string>()},
        };

        private void SelecterSpec(SQLiteDataReader reader)
        {
            string spec = reader.GetString(0);
            string page = reader.GetString(1);

            if (!groupsDict.TryAdd(page, new List<string>() { spec }))
            {
                groupsDict[page].Add(spec);
            }
        }

        private void SelectSpec()
        {
            departMas = new Dictionary<string, List<string>>();
            string request = "SELECT Spec, Pages FROM Scep ORDER BY Departments ASC";
            WorkBase.SelectValues(request, SelecterSpec);
        }

        public void MainDocument(int day, string week)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Create("Учащиеся.docx", WordprocessingDocumentType.Document))
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
                //CreateText(ref body, "Made by Yura", true);

                Table table = new Table();

                SelectForWord(week, day);

                SelectGroups();
                SelectSpec();

                int sum = 0;

                List<string> distantGroups = new List<string>();

                foreach (var depart in departMas)
                {

                    if (depart.Value.Count() > 0)
                    {
                        CreateHeader(ref table);

                        Dictionary<string, List<Group>> dictDays = date.GetDate();

                        foreach (string i in depart.Value)
                        {
                            List<Group> list = mas[i];
                            foreach (Group l in list)
                            {
                                dictDays[l.Gr.Substring(0, 1)].Add(l);
                            }
                        }

                        foreach (var sortedGroups in dictDays)
                        {
                            if (sortedGroups.Value.Count > 5)
                            {
                                sum++;
                            }

                            if (sum == 3)
                            {
                                table = NewRazdel(body, table);
                                CreateHeader(ref table);
                                sum = 0;
                            }

                            foreach (var group in sortedGroups.Value)
                            {
                                if (group.Distant == false)
                                {
                                    CreateGroupLine(ref table, group.Gr, groups[group.Gr]);
                                }
                                else
                                {
                                    distantGroups.Add(group.Gr);
                                }
                            }
                        }

                        table = NewRazdel(body, table);
                    }

                }

                CreateHeaderText(ref body, date, "заочной", day);
                CreateText(ref body, "", true);

                CreateHeader(ref table);
                foreach (var group in distantGroups)
                {
                    CreateGroupLine(ref table, group, groups[group]);
                }
                body.Append(table);
                // Сохранение документа
                mainPart.Document.Save();
            }
        }
    }

}
