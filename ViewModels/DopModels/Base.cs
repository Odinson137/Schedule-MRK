using Sasha_Project;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using DopFiles;
using MainTable;
using System.Collections.ObjectModel;

namespace Base;

interface IBase
{
    public List<CreateRoom> SelectValues();
    public void InsertValue(string value);
    public void InsertValue(string value, string value2, string firstColumn, string secondColumn);
}

class WorkBase : IBase
{
    private protected string titleTable;
    private protected string column;
    public WorkBase(string titleTable, string column) {
        this.titleTable = titleTable;
        this.column = column;
    }


    public List<CreateRoom> SelectValues()
    {
        List<CreateRoom> mas = new List<CreateRoom>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {titleTable} ORDER BY {column} ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string value = reader.GetString(1);
                    mas.Add(new CreateRoom() { Id = id, Value = value });
                }
            }
            connection.Close();
        }
        return mas;
    }

    public void DeleteValue(int id)
    {

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"DELETE FROM {titleTable} WHERE ID = @Id", connection))
            {
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void InsertValue(string value)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO {titleTable} ({column}) VALUES (@room)", connection))
                {
                    command.Parameters.AddWithValue("room", value);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        } catch
        {
            MessageBox.Show("Error!");
        }
    }

    public void InsertValue(string prepod, string lesson, string firstColumn, string secondColumn) { }

}

class WorkRoom : WorkBase
{
    public WorkRoom(string value1, string value2) : base(value1, value2) { }
    public Dictionary<string, Dictionary<string, string>> SelectAllRooms(string week, string days, List<CreateRoom> mas)
    {
        Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>> {
            { "1", new Dictionary<string, string>()},
            { "2", new Dictionary<string, string>()},
            { "3", new Dictionary<string, string>()},
            { "4", new Dictionary<string, string>()},
            { "5", new Dictionary<string, string>()},
            { "6", new Dictionary<string, string>()},
            { "7", new Dictionary<string, string>()},
        };

        foreach (var i in dict)
        {
            foreach (var s in mas)
            {
                dict[i.Key].Add(s.Value, "0");
            }
        }

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT para, rooms, rooms_2, razdelsPara FROM Tables WHERE week = @week AND days = @days ORDER BY para ASC", connection))
            {
                command.Parameters.AddWithValue("@week", week);
                command.Parameters.AddWithValue("@days", days);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string para = reader.GetString(0);
                    string room1 = reader.GetString(1);
                    string room2 = reader.GetString(2);
                    bool value = reader.GetBoolean(3);

                    if (value)
                    {
                        if (room1.Length > 1 && room1.Contains(" "))
                        {
                            foreach (string s in room1.Split(" "))
                            {
                                dict[para][s] = "1";
                            }
                        }
                        else
                        {
                            dict[para][room1] = "1";
                        }
                        if (room2.Length > 1 && room2.Contains(" "))
                        {
                            foreach (string s in room2.Split(" "))
                            {
                                dict[para][s] = "2";
                            }
                        }
                        else
                        {
                            dict[para][room2] = "2";
                        }
                    }
                    else
                    {
                        if (room1.Contains(" "))
                        {
                            foreach (string s in room1.Split(" "))
                            {
                                dict[para][s] = "3";
                            }
                        }
                        else
                        {
                            dict[para][room1] = "3";
                        }
                    }
                }
            }
            connection.Close();
        }

        return dict;
    }
}

class LessonsTab : WorkBase
{
    public LessonsTab(string value1, string value2) : base(value1, value2) { }
    public new List<CreateRoom> SelectValues()
    {
        List<CreateRoom> mas = new List<CreateRoom>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {titleTable} ORDER BY {column} ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string lesson = reader.GetString(1);
                    string prepod = reader.GetString(2);
                    mas.Add(new CreatePrepods() { Id = id, Value = prepod, ValueTwo = lesson  });
                }
            }
            connection.Close();
        }
        return mas;
    }

    public new void InsertValue(string prepod, string lesson, string firstColumn, string secondColumn)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO {titleTable} ({firstColumn}, {secondColumn}) VALUES (@les, @pred)", connection))
                {
                    command.Parameters.AddWithValue("les", lesson);
                    command.Parameters.AddWithValue("pred", prepod);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        catch
        {
            MessageBox.Show("Error!");
        }
    }
}

// потом наследовать этот класс от прошлого и просто передать значения
class GroupTab : WorkBase
{
    public GroupTab(string a, string b) : base(a, b) { }
    public new List<CreateRoom> SelectValues()
    {
        List<CreateRoom> mas = new List<CreateRoom>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM Groups ORDER BY groups ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string group = reader.GetString(1);
                    string office = reader.GetString(2);
                    string distant = reader.GetString(3);

                    string valueThree;
                    if (distant == "0")
                    {
                        valueThree = "дневная";
                    }
                    else if (distant == "1")
                    {
                        valueThree = "заочная";
                    } else
                    {
                        valueThree = "дистанционная";
                    }
                    mas.Add(new CreateNewPrepods() { Id = id, Value = group, ValueTwo = office, ValueThree = valueThree });
                }
            }
            connection.Close();
        }
        return mas;
    }

    public void InsertValue(string groups, string office, int dist)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Groups (groups, offices, Distants) VALUES (@les, @pred, @dist)", connection))
                {
                    command.Parameters.AddWithValue("les", groups);
                    command.Parameters.AddWithValue("pred", office);
                    command.Parameters.AddWithValue("dist", dist);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            WordGroup a = new WordGroup(groups);
            a.AddInfoGroup();
        }
        catch
        {
            MessageBox.Show("Error!");
        } 
    }
    public void DeleteAllValue(string group)
    {

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"DELETE FROM Groups WHERE groups = @group", connection))
            {
                command.Parameters.AddWithValue("group", group);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        WordGroup a = new WordGroup(group);
        a.DeleteInfoGroup();
    }
}

class WordGroup
{
    public string group { get; set; }

    public WordGroup(string group_)
    {
        group = group_;
    }

    public void DeleteInfoGroup()
    {
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"DELETE FROM Tables WHERE groups = @group", connection))
            {
                command.Parameters.AddWithValue("group", group);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddInfoGroup()
    {
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            List<string> weak = new List<string> { "ч", "з" };

            List<int> para = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

            List<int> days = new List<int> { 1, 2, 3, 4, 5, 6 };

            foreach (string w in weak)
            {
                foreach (int d in days)
                {
                    foreach (int p in para)
                    {
                        using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Tables (groups, week, para, days) VALUES (@group, @week, @para, @day)", connection))
                        {
                            command.Parameters.AddWithValue("@group", group);
                            command.Parameters.AddWithValue("@week", w);
                            command.Parameters.AddWithValue("@para", p);
                            command.Parameters.AddWithValue("@day", d);
                            command.ExecuteNonQuery();
                        }
                    }

                }
            }


            connection.Close();
        }
    }

}

class DataBase
{
    public void UpdateValueInTableFull(int id, Tables list, bool value, Tables list2)
    {
        if (list2 == null)
        {
            list2 = new Tables
            {
                Rooms = "0",
                Prepods = "0",
                Office = "0",
                Changes = "0"
            };
        }
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            // Создаем команду для обновления значения в таблице
            string sqlQuery = $"UPDATE Tables SET (rooms, prepods, changes, offices, " +
                                $"razdelsPara, rooms_2, prepods_2, changes_2, offices_2, lessons, lessons_2, lessons_zamena, lessons_zamena_2) = " +
                                $"(@newRooms, @newPrepods, @newChanges, @newOffices, " +
                                $" @newRazdelPara, @newRooms2, @newPrepods2, @newChanges2, @newOffices2, @lesson, @lesson2, @zamenaLesson, @zamenaLesson2) " +
                                $"WHERE Id = @id";

            using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@newRooms", list.Rooms);
                command.Parameters.AddWithValue("@newPrepods", list.Prepods);
                command.Parameters.AddWithValue("@newChanges", list.Changes);
                command.Parameters.AddWithValue("@newOffices", list.Office);
                command.Parameters.AddWithValue("@lesson", list.Lesson);
                command.Parameters.AddWithValue("@zamenaLesson", list.OfficeLesson);

                command.Parameters.AddWithValue("@newRazdelPara", value);

                command.Parameters.AddWithValue("@newRooms2", list2.Rooms);
                command.Parameters.AddWithValue("@newPrepods2", list2.Prepods);
                command.Parameters.AddWithValue("@newChanges2", list2.Changes);
                command.Parameters.AddWithValue("@newOffices2", list2.Office);
                command.Parameters.AddWithValue("@lesson2", list2.Lesson);
                command.Parameters.AddWithValue("@zamenaLesson2", list2.OfficeLesson);

                command.Parameters.AddWithValue("@id", id);
                int rowsChanged = command.ExecuteNonQuery();
            }
        }
    }

    public DataBase() { }

    public void InsertBase(string text)
    {
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Groups (groups) VALUES ('" + text + "')", connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void InserGeneraltBase(string groups, char week, int para)
    {
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Tables (groups, week, para) VALUES ('{groups}', '{week}', '{para}')", connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }


    public void SelectLessons(UpdateValuesPrepods prepods)
    {
        prepods.DeleteValues();
        Dictionary<string, List<string>> mas = new Dictionary<string, List<string>>();
        List<string> sovpda = new List<string>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT lessons, prepods FROM Prepods ORDER BY lessons ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string lesson = reader.GetString(0);
                    string prepod = reader.GetString(1);

                    if (!sovpda.Contains(prepod))
                    {
                        prepods.InsertNewValue(prepod);
                        sovpda.Add(prepod);
                    }


                    if (mas.ContainsKey(lesson))
                    {
                        mas[lesson].Add(prepod);
                    }
                    else
                    {
                        mas.Add(lesson, new List<string>());
                        mas[lesson].Add(prepod);
                    }

                }
            }
            connection.Close();
        }

        prepods.NewDict(mas);
    }

    public void SelectRooms(UpdateValues rooms)
    {
        rooms.DeleteValues();
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand("SELECT rooms FROM FreeRooms ORDER BY rooms ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string freeRoom = reader.GetString(0);

                    rooms.InsertNewValue(freeRoom);
                }
            }
            connection.Close();
        }
    }
    private string Nulling(string value)
    {
        if (value == "0") value = "";
        return value;
        
    }

    public List<string[]> SelectBaseSelecter(string week, int days)
    {
        List<string[]> strings = new List<string[]>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Tables WHERE week = @week AND days = @days ORDER BY para ASC", connection))
            {
                command.Parameters.AddWithValue("@week", week);
                command.Parameters.AddWithValue("@days", days);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string[] mas = new string[11];

                    mas[0] = Nulling(reader.GetString(1));
                    mas[1] = Nulling(reader.GetString(2));
                    mas[2] = Nulling(reader.GetString(3));
                    mas[3] = reader.GetBoolean(8).ToString();
                    mas[4] = Nulling(reader.GetString(4));
                    mas[5] = Nulling(reader.GetString(5));

                    mas[6] = Nulling(reader.GetString(9));
                    mas[7] = Nulling(reader.GetString(10));
                    mas[8] = Nulling(reader.GetString(11));
                    mas[9] = Nulling(reader.GetString(12));
                    
                    mas[10] = Nulling(reader.GetString(7));

                    strings.Add(mas);
                }
            }
            connection.Close();
        }
        return strings;
    }

    public Dictionary<string, List<string>> SelectSpec()
    {
        Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>() { 
            { "1", new List<string>()},
            { "2", new List<string>()},
            { "3", new List<string>()},
            { "4", new List<string>()},
            { "5", new List<string>()},
            { "6", new List<string>()},
        };

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Scep ORDER BY Departments ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string spec = reader.GetString(1);
                    string page  = reader.GetString(2);

                    groups[page].Add(spec);
         
                }
            }
            connection.Close();
        }
        return groups;
    }

    public Dictionary<string, List<Group>> SelectGroups()
    {

        Dictionary<string, List<Group>> groups = new Dictionary<string, List<Group>>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Groups ORDER BY offices ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string group = reader.GetString(1);
                    string office = reader.GetString(2);
                    string dist = reader.GetString(3);
                    bool distant = false;
                    if (dist == "1") distant = true;

                    if (groups.ContainsKey(office))
                    {
                        groups[office].Add(new Group() { Gr = group, Distant = distant});
                    }
                    else
                    {
                        groups.Add(office, new List<Group>() { new Group() { Gr = group, Distant = distant } });
                    }
                }
            }
            connection.Close();
        }
        return groups;
    }

    public ObservableCollection<Tables> SelectBigBase(int indexLastPara, string week, int indexDay, UpdateValues rooms, UpdateValuesPrepods prepods)
    {
        ObservableCollection<Tables> phonesList = new ObservableCollection<Tables> { };
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM Tables WHERE para = @para and week = @week and days = @days ORDER BY groups ASC", connection))
            {
                command.Parameters.AddWithValue("@para", indexLastPara);
                command.Parameters.AddWithValue("@week", week);
                command.Parameters.AddWithValue("@days", indexDay);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string room = reader.GetString(2);
                    string room2 = reader.GetString(9);
                    string prepod = reader.GetString(3);
                    string prepod2 = reader.GetString(10);
                    bool razdelPara = reader.GetBoolean(8);
                    string titleGroup = reader.GetString(1);

                    if (razdelPara)
                    {
                        titleGroup = titleGroup + " (1 час)";
                    }

                    Tables firstTable = new Tables
                    {
                        Id = id,
                        Groups = titleGroup,
                        Rooms = room,
                        Prepods = prepod,
                        Changes = reader.GetString(4),
                        Office = reader.GetString(5),
                        OfficeLesson = reader.GetString(16),
                        RazdelPara = razdelPara,
                        Lesson = reader.GetString(14),
                    };

                    phonesList.Add(firstTable);

                    if (razdelPara)
                    {
                        Tables secondTable = new Tables
                        {
                            Id = id,
                            Groups = reader.GetString(1) + " (2 час)",
                            Rooms = room2,
                            Prepods = prepod2,
                            Changes = reader.GetString(11),
                            OfficeLesson = reader.GetString(17),
                            Office = reader.GetString(12),
                            RazdelPara = true,
                            Lesson = reader.GetString(15),
                            Link = firstTable
                        };
                        phonesList.Add(secondTable);
                        firstTable.Link = secondTable;
                    }
                }
            }
            connection.Close();
        }

        rooms.ZapolDict(phonesList);
        prepods.ZapolDict(phonesList);
        return phonesList;
    }

    public Dictionary<string, Dictionary<int, Tables[]>> SelectForWord(string week, int days)
    {
        Dictionary<string, Dictionary<int, Tables[]>> groups = new Dictionary<string, Dictionary<int, Tables[]>>();


        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Tables WHERE week = @week AND days = @days ORDER BY groups ASC", connection))
            {
                command.Parameters.AddWithValue("@week", week);
                command.Parameters.AddWithValue("@days", days);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
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

                    } else
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
            }
            connection.Close();
        }
        return groups;
    }

    public void InserPrepods(string lesson, string prepod, string dopName)
    {
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Prepods (lessons, prepods, DopNamePrepods) VALUES ('{lesson}', '{prepod}', '{dopName}')", connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    //public void GeneraltBase()
    //{
    //    using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //    {
    //        connection.Open();

    //        Dictionary<string, List<Group>> a = SelectGroups();

    //        List<string> mas = new List<string>();

    //        foreach (var i in a)
    //        {
    //            foreach (Group sr in i.Value)
    //            {
    //                mas.Add(sr.Gr);
    //            }
    //        }

    //        List<string> weak = new List<string> { "ч", "з" };

    //        List<int> para = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

    //        List<int> days = new List<int> { 1, 2, 3, 4, 5, 6 };

    //        int id = 0;
    //        foreach (string m in mas)
    //        {
    //            foreach (string w in weak)
    //            {
    //                foreach (int d in days)
    //                {
    //                    foreach (int p in para)
    //                    {
    //                        using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Tables (ID, groups, week, para, days) VALUES (@ID, @group, @week, @para, @day)", connection))
    //                        {
    //                            command.Parameters.AddWithValue("@ID", id++);
    //                            command.Parameters.AddWithValue("@group", m);
    //                            command.Parameters.AddWithValue("@week", w);
    //                            command.Parameters.AddWithValue("@para", p);
    //                            command.Parameters.AddWithValue("@day", d);
    //                            command.ExecuteNonQuery();
    //                        }
    //                    }

    //                }
    //            }
    //        }


    //        connection.Close();
    //    }
    //}


}