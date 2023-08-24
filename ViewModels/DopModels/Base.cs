using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using MainTable;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace Sasha_Project.ViewModels.DopModels;

interface IBase
{
    public List<CreateRoom> SelectValues();
    public void DeleteValue(int id);
    public void PutValue(int id);
    public void InsertValue(string v0);
}

class WorkBase
{
    public delegate void Select(SQLiteDataReader reader);
    public static bool SelectValues(string request, Select select)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(request, connection))
                {
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                        select(reader);
                }
                connection.Close();
            }
        } catch (Exception e)
        {
            MessageBox.Show("Ошибка при чтении таблицы: " + request + " Тип ошибки: " + e.Message);
            return false;
        }
        return true;

    }

    public static bool RequestValue(string request, Dictionary<string, object> mas)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(request, connection))
                {
                    foreach (var s in mas)
                        command.Parameters.AddWithValue(s.Key, s.Value);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Ошибка при работе с базой данных: {request}. Тип ошибки: {e.Message}");
            return false;
        }
        return true;
    }

    //public static bool InsertValue(string request, Dictionary<string, object> mas)
    //{
    //    try
    //    {
    //        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //        {
    //            connection.Open();

    //            using (SQLiteCommand command = new SQLiteCommand(request, connection))
    //            {
    //                foreach (var s in mas)
    //                {
    //                    command.Parameters.AddWithValue(s.Key, s.Value);
    //                }
    //                command.ExecuteNonQuery();
    //            }

    //            connection.Close();
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        MessageBox.Show("Ошибка при добавлении записи в базу: " + request + " Тип ошибки: " + e.Message);
    //        return false;
    //    }
    //    return true;
    //}

    //public static bool PutValue(string request, Dictionary<string, object> mas)
    //{
    //    try
    //    {
    //        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //        {
    //            connection.Open();

    //            using (SQLiteCommand command = new SQLiteCommand(request, connection))
    //            {
    //                foreach (var s in mas)
    //                {
    //                    command.Parameters.AddWithValue(s.Key, s.Value);
    //                }
    //                command.ExecuteNonQuery();
    //            }
    //        }
    //    } 
    //    catch (Exception e)
    //    {
    //        MessageBox.Show("Ошибка при обновлении записи в базе: " + request + " Тип ошибки: " + e.Message);
    //        return false;
    //    }
    //    return true;
    //}

    //public void DeleteValue(string request, Dictionary<string, object> mas)
    //{
    //    try
    //    {
    //        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //        {
    //            connection.Open();

    //            using (SQLiteCommand command = new SQLiteCommand(request, connection))
    //            {
    //                foreach (var s in mas)
    //                {
    //                    command.Parameters.AddWithValue(s.Key, s.Value);
    //                }
    //                command.ExecuteNonQuery();
    //            }

    //            connection.Close();
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        MessageBox.Show("Ошибка при удалении записи из базы: " + request + " Тип ошибки: " + e.Message);
    //        return false;
    //    }
    //}
}



//class WorkBase : IBase
//{
//    private protected string titleTable;
//    private protected string column;
//    public WorkBase(string titleTable, string column)
//    {
//        this.titleTable = titleTable;
//        this.column = column;
//    }


//    public List<CreateRoom> SelectValues()
//    {
//        List<CreateRoom> mas = new List<CreateRoom>();

//        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        {
//            connection.Open();

//            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {titleTable} ORDER BY {column} ASC", connection))
//            {
//                SQLiteDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    int id = reader.GetInt32(0);
//                    string value = reader.GetString(1);
//                    mas.Add(new CreateRoom() { Id = id, Value = value });
//                }
//            }
//            connection.Close();
//        }
//        return mas;
//    }

//    public void DeleteValue(int id)
//    {

//        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        {
//            connection.Open();

//            using (SQLiteCommand command = new SQLiteCommand($"DELETE FROM {titleTable} WHERE ID = @Id", connection))
//            {
//                command.Parameters.AddWithValue("Id", id);
//                command.ExecuteNonQuery();
//            }

//            connection.Close();
//        }
//    }

//    public void InsertValue(string value)
//    {
//        try
//        {
//            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//            {
//                connection.Open();

//                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO {titleTable} ({column}) VALUES (@room)", connection))
//                {
//                    command.Parameters.AddWithValue("room", value);
//                    command.ExecuteNonQuery();
//                }

//                connection.Close();
//            }
//        }
//        catch
//        {
//            MessageBox.Show("Error!");
//        }
//    }

//    public void InsertValue(string prepod, string lesson, string firstColumn, string secondColumn) { }

//}

class WorkRoom
{
    private string val1;
    private string val2;
    public WorkRoom(string value1, string value2)
    {
        val1 = value1;
        val2 = value2;
    }

    public List<CreateRoom> SelectValues()
    {
        List<CreateRoom> mas = new List<CreateRoom>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {val1} ORDER BY {val2} ASC", connection))
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

//class LessonsTab : WorkBase
//{
//    public LessonsTab(string value1, string value2) : base(value1, value2) { }
//    public new List<CreateRoom> SelectValues()
//    {
//        List<CreateRoom> mas = new List<CreateRoom>();

//        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        {
//            connection.Open();

//            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {titleTable} ORDER BY {column} ASC", connection))
//            {
//                SQLiteDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    int id = reader.GetInt32(0);
//                    string lesson = reader.GetString(1);
//                    string prepod = reader.GetString(2);
//                    mas.Add(new CreatePrepods() { Id = id, Value = prepod, ValueTwo = lesson });
//                }
//            }
//            connection.Close();
//        }
//        return mas;
//    }

//    public new void InsertValue(string prepod, string lesson, string firstColumn, string secondColumn)
//    {
//        try
//        {
//            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//            {
//                connection.Open();

//                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO {titleTable} ({firstColumn}, {secondColumn}) VALUES (@les, @pred)", connection))
//                {
//                    command.Parameters.AddWithValue("les", lesson);
//                    command.Parameters.AddWithValue("pred", prepod);
//                    command.ExecuteNonQuery();
//                }

//                connection.Close();
//            }
//        }
//        catch
//        {
//            MessageBox.Show("Error!");
//        }
//    }
//}

//// потом наследовать этот класс от прошлого и просто передать значения
//class GroupTab : WorkBase
//{
//    public GroupTab(string a, string b) : base(a, b) { }

//    private static string GetTypeLearning(string distant)
//    {
//        string valueThree;
//        if (distant == "0") valueThree = "дневная";
//        else if (distant == "1") valueThree = "заочная";
//        else valueThree = "дистанционная";
//        return valueThree;
//    }

//    public new List<CreateRoom> SelectValues()
//    {
//        List<CreateRoom> mas = new List<CreateRoom>();

//        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        {
//            connection.Open();

//            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM Groups ORDER BY groups ASC", connection))
//            {
//                SQLiteDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    int id = reader.GetInt32(0);
//                    string group = reader.GetString(1);
//                    string office = reader.GetString(2);
//                    string distant = reader.GetString(3);

//                    mas.Add(new CreateNewPrepods() { Id = id, Value = group, ValueTwo = office, ValueThree = GetTypeLearning(distant) });
//                }
//            }
//            connection.Close();
//        }
//        return mas;
//    }

//    public static void InsertValue(string groups, string office, int dist)
//    {
//        try
//        {
//            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//            {
//                connection.Open();

//                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Groups (groups, offices, Distants) VALUES (@les, @pred, @dist)", connection))
//                {
//                    command.Parameters.AddWithValue("les", groups);
//                    command.Parameters.AddWithValue("pred", office);
//                    command.Parameters.AddWithValue("dist", dist);
//                    command.ExecuteNonQuery();
//                }

//                connection.Close();
//            }
//            WordGroup a = new WordGroup(groups);
//            a.AddInfoGroup();
//        }
//        catch
//        {
//            MessageBox.Show("Error!");
//        }
//    }
//    public static void DeleteAllValue(string group)
//    {

//        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        {
//            connection.Open();

//            using (SQLiteCommand command = new SQLiteCommand($"DELETE FROM Groups WHERE groups = @group", connection))
//            {
//                command.Parameters.AddWithValue("group", group);
//                command.ExecuteNonQuery();
//            }

//            connection.Close();
//        }
//        WordGroup a = new WordGroup(group);
//        a.DeleteInfoGroup();
//    }
//}

//class WordGroup
//{
//    public string group { get; set; }

//    public WordGroup(string group_)
//    {
//        group = group_;
//    }

//    public void DeleteInfoGroup()
//    {
//        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        {
//            connection.Open();

//            using (SQLiteCommand command = new SQLiteCommand($"DELETE FROM Tables WHERE groups = @group", connection))
//            {
//                command.Parameters.AddWithValue("group", group);
//                command.ExecuteNonQuery();
//            }

//            connection.Close();
//        }
//    }

//    public void AddInfoGroup()
//    {
//        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        {
//            connection.Open();

//            List<string> weak = new List<string> { "ч", "з" };

//            List<int> para = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

//            List<int> days = new List<int> { 1, 2, 3, 4, 5, 6 };

//            foreach (string w in weak)
//            {
//                foreach (int d in days)
//                {
//                    foreach (int p in para)
//                    {
//                        using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Tables (groups, week, para, days) VALUES (@group, @week, @para, @day)", connection))
//                        {
//                            command.Parameters.AddWithValue("@group", group);
//                            command.Parameters.AddWithValue("@week", w);
//                            command.Parameters.AddWithValue("@para", p);
//                            command.Parameters.AddWithValue("@day", d);
//                            command.ExecuteNonQuery();
//                        }
//                    }

//                }
//            }
//            connection.Close();
//        }
//    }

//}

//class TableBase
//{
//    private static void SelectTable(SQLiteDataReader reader)
//    {
//        prepods.InsertNewStruct(new Lessons()
//        {
//            Lesson = reader.GetString(0),
//            Prepod = reader.GetString(1),
//            Kurs = reader.GetInt32(2)
//        });
//        prepods.InsertNewValue(prepod);
//    }

//    public static void SelectLessons(UpdateValuesPrepods prepods)
//    {
//        prepods.DeleteValues();

//        string request = "SELECT Lessons.Lessons, Prepods.Prepods, Lessons.Kurs FROM Lessons INNER JOIN Prepods ON Lessons.Lessons = Prepods.Lessons ORDER BY Lessons.Lessons;";
//        WorkBase.SelectValues(request, SelectTable);

//        //try
//        //{
//        //    using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
//        //    {
//        //        connection.Open();
//        //        using (SQLiteCommand command = new SQLiteCommand("SELECT Lessons.Lessons, Prepods.Prepods, Lessons.Kurs FROM Lessons INNER JOIN Prepods ON Lessons.Lessons = Prepods.Lessons ORDER BY Lessons.Lessons;", connection))
//        //        {
//        //            SQLiteDataReader reader = command.ExecuteReader();

//        //            while (reader.Read())
//        //            {
//        //                string lesson = reader.GetString(0);
//        //                string prepod = reader.GetString(1);
//        //                int kurs = reader.GetInt32(2);

//        //                prepods.InsertNewStruct(new Lessons()
//        //                {
//        //                    Lesson = lesson,
//        //                    Prepod = prepod,
//        //                    Kurs = kurs
//        //                });

//        //                prepods.InsertNewValue(prepod);
//        //            }
//        //        }
//        //        connection.Close();
//        //    }
//        //}
//        //catch
//        //{
//        //    MessageBox.Show("Базы данных нет 1");
//        //}
//    }
//}

class DataBase
{
    public static void UpdateValueInTableFull(int id, Tables list, bool value, Tables list2)
    {
        Tables listCapl;
        if (!value)
        {
            list2 = new Tables
            {
                Rooms = "0",
                Prepods = "0",
                Office = "0",
                Changes = "0"
            };
        }
        else
        {
            if (list.Groups.Contains("2 час"))
            {
                listCapl = list;
                list = list2;
                list2 = listCapl;
            }
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

    public static void InsertGroups(string group, string office, int id)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Groups (ID, groups, offices) VALUES (@id, @group, @office)", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@group", group);
                    command.Parameters.AddWithValue("@office", office);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        catch
        {
            MessageBox.Show("Error InsertGroups");
        }

    }

    public static Dictionary<string, string> SelectSpecCode()
    {
        Dictionary<string, string> groups = new Dictionary<string, string>();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT Spec, Code FROM Scep ORDER BY Departments ASC", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string spec = reader.GetString(0);
                    string code = reader.GetString(1);

                    groups.Add(code, spec);
                }
            }
            connection.Close();
        }
        return groups;
    }

    public static void InsertGeneraltBase(List<string> groups)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();
                List<string> weak = new List<string> { "ч", "з" };
                List<int> para = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
                List<int> days = new List<int> { 1, 2, 3, 4, 5, 6 };

                int id = 0;
                foreach (string m in groups)
                {
                    foreach (string w in weak)
                    {
                        foreach (int d in days)
                        {
                            foreach (int p in para)
                            {
                                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Tables (ID, groups, week, para, days) VALUES (@ID, @group, @week, @para, @day)", connection))
                                {
                                    command.Parameters.AddWithValue("@ID", id++);
                                    command.Parameters.AddWithValue("@group", m);
                                    command.Parameters.AddWithValue("@week", w);
                                    command.Parameters.AddWithValue("@para", p);
                                    command.Parameters.AddWithValue("@day", d);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                connection.Close();
            }
        }
        catch
        {
            MessageBox.Show("Error InsertGeneraltBase");
        }
    }



    //public static void SelectRooms(UpdateValuesRooms rooms)
    //{
    //    rooms.DeleteValues();
    //    try
    //    {
    //        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //        {
    //            connection.Open();

    //            using (SQLiteCommand command = new SQLiteCommand("SELECT rooms FROM FreeRooms ORDER BY rooms ASC", connection))
    //            {
    //                SQLiteDataReader reader = command.ExecuteReader();

    //                while (reader.Read())
    //                {
    //                    string freeRoom = reader.GetString(0);
    //                    rooms.InsertNewValue(freeRoom);
    //                }
    //            }
    //            connection.Close();
    //        }
    //    }
    //    catch
    //    {
    //        MessageBox.Show("Базы данных нет 2");
    //    }

    //}
    //private static string Nulling(string value)
    //{
    //    if (value == "0") value = "";
    //    return value;
    //}

    //public static List<string[]> SelectBaseSelecter(string week, int days)
    //{
    //    List<string[]> strings = new List<string[]>();

    //    using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //    {
    //        connection.Open();
    //        using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Tables WHERE week = @week AND days = @days ORDER BY para ASC", connection))
    //        {
    //            command.Parameters.AddWithValue("@week", week);
    //            command.Parameters.AddWithValue("@days", days);
    //            SQLiteDataReader reader = command.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                string[] mas = new string[11];

    //                mas[0] = Nulling(reader.GetString(1));
    //                mas[1] = Nulling(reader.GetString(2));
    //                mas[2] = Nulling(reader.GetString(3));
    //                mas[3] = reader.GetBoolean(8).ToString();
    //                mas[4] = Nulling(reader.GetString(4));
    //                mas[5] = Nulling(reader.GetString(5));

    //                mas[6] = Nulling(reader.GetString(9));
    //                mas[7] = Nulling(reader.GetString(10));
    //                mas[8] = Nulling(reader.GetString(11));
    //                mas[9] = Nulling(reader.GetString(12));

    //                mas[10] = Nulling(reader.GetString(7));

    //                strings.Add(mas);
    //            }
    //        }
    //        connection.Close();
    //    }
    //    return strings;
    //}

    //public static Dictionary<string, List<string>> SelectSpec()
    //{
    //    Dictionary<string, List<string>> groupsDict = new Dictionary<string, List<string>>() {
    //        { "1", new List<string>()},
    //        { "2", new List<string>()},
    //        { "3", new List<string>()},
    //        { "4", new List<string>()},
    //        { "5", new List<string>()},
    //        { "6", new List<string>()},
    //    };

    //    using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //    {
    //        connection.Open();
    //        using (SQLiteCommand command = new SQLiteCommand("SELECT Spec, Pages FROM Scep ORDER BY Departments ASC", connection))
    //        {
    //            SQLiteDataReader reader = command.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                string spec = reader.GetString(0);
    //                string page = reader.GetString(1);

    //                groups[page].Add(spec);

    //            }
    //        }
    //        connection.Close();
    //    }
    //    return groups;
    //}

    public static Dictionary<string, List<Group>> SelectGroups()
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
                        groups[office].Add(new Group() { Gr = group, Distant = distant });
                    else
                        groups.Add(office, new List<Group>() { new Group() { Gr = group, Distant = distant } });
                }
            }
            connection.Close();
        }
        return groups;
    }

    public static ObservableCollection<Tables> SelectBigBase(int indexLastPara, string week, int indexDay, UpdateValues rooms, UpdateValuesPrepods prepods)
    {
        ObservableCollection<Tables> phonesList = new ObservableCollection<Tables> { };
        try
        {
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
                        string room = reader.GetString(2) ?? "";
                        string room2 = reader.GetString(9) ?? "";
                        string prepod = reader.GetString(3) ?? "";
                        string prepod2 = reader.GetString(10) ?? "";
                        bool razdelPara = reader.GetBoolean(8);
                        string titleGroup = reader.GetString(1) ?? "";

                        if (razdelPara) titleGroup += " (1 час)";

                        Tables firstTable = new Tables
                        {
                            Id = id,
                            Groups = titleGroup,
                            Rooms = room,
                            Prepods = prepod,
                            Changes = reader.GetString(4) ?? "",
                            Office = reader.GetString(5) ?? "",
                            OfficeLesson = reader.GetString(16) ?? "",
                            RazdelPara = razdelPara,
                            Lesson = reader.GetString(14) ?? "",
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
                                Changes = reader.GetString(11) ?? "",
                                OfficeLesson = reader.GetString(17) ?? "",
                                Office = reader.GetString(12) ?? "",
                                RazdelPara = true,
                                Lesson = reader.GetString(15) ?? "",
                                Link = firstTable
                            };
                            phonesList.Add(secondTable);
                            firstTable.Link = secondTable;
                        }
                    }
                }
                connection.Close();
            }
        }
        catch
        {
            MessageBox.Show("Базы данных нет");
        }

        rooms.ZapolDict(phonesList);
        prepods.ZapolDict(phonesList);
        return phonesList;
    }

    //public Dictionary<string, Dictionary<int, Tables[]>> SelectForWord(string week, int days)
    //{
    //    Dictionary<string, Dictionary<int, Tables[]>> groups = new Dictionary<string, Dictionary<int, Tables[]>>();

    //    using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
    //    {
    //        connection.Open();
    //        using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Tables WHERE week = @week AND days = @days ORDER BY groups ASC", connection))
    //        {
    //            command.Parameters.AddWithValue("@week", week);
    //            command.Parameters.AddWithValue("@days", days);

    //            SQLiteDataReader reader = command.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                string titleGroup = reader.GetString(1);
    //                string para = reader.GetString(7);

    //                Tables[] tables;

    //                bool RazdelPara = reader.GetBoolean(8);

    //                Tables table = new Tables()
    //                {
    //                    Groups = titleGroup,
    //                    Rooms = reader.GetString(2),
    //                    Prepods = reader.GetString(3),
    //                    Changes = reader.GetString(4),
    //                    Office = reader.GetString(5),
    //                    Lesson = reader.GetString(14),
    //                    OfficeLesson = reader.GetString(16),
    //                };

    //                if (RazdelPara)
    //                {
    //                    tables = new Tables[2];

    //                    Tables tableTwo = new Tables()
    //                    {
    //                        Groups = "Вторая половина",
    //                        Rooms = reader.GetString(9),
    //                        Prepods = reader.GetString(10),
    //                        Changes = reader.GetString(11),
    //                        Office = reader.GetString(12),
    //                        Lesson = reader.GetString(15),
    //                        OfficeLesson = reader.GetString(17),
    //                    };

    //                    tables[0] = table;
    //                    tables[1] = tableTwo;

    //                }
    //                else
    //                {
    //                    tables = new Tables[1];
    //                    tables[0] = table;
    //                }

    //                int paraInt = int.Parse(para);
    //                if (groups.ContainsKey(titleGroup))
    //                {
    //                    groups[titleGroup].Add(paraInt, tables);
    //                }
    //                else
    //                {
    //                    groups.Add(titleGroup, new Dictionary<int, Tables[]> { { paraInt, tables } });
    //                }
    //            }
    //        }
    //        connection.Close();
    //    }
    //    return groups;
    //}

    public static void InserPrepods(int id, string lesson, string prepod, string dopName)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Prepods (ID, lessons, prepods, DopNamePrepods) VALUES (@id, @lesson, @prepod, @dopName)", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@lesson", lesson);
                    command.Parameters.AddWithValue("@prepod", prepod);
                    command.Parameters.AddWithValue("@dopName", dopName);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        catch
        {
            MessageBox.Show("Error InserPrepods");
        }


    }

    public static void InsertLessons(int id, string lesson, int kurs)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO Lessons (ID, Lessons, Kurs) VALUES (@id, @lesson, @kurs)", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@lesson", lesson);
                    command.Parameters.AddWithValue("@kurs", kurs);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        catch
        {
            MessageBox.Show("Error InsertLessons");
        }

    }

    public static void CLeanTable(string table)
    {
        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand($"DELETE FROM {table}", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private static List<string> GetDate()
    {
        List<string> mas = new List<string>();

        for (int i = 0; i < 5; i++)
        {
            DateTime years = DateTime.Now.AddYears(-i);
            mas.Add(years.Year.ToString().Substring(3));
        }
        return mas;
    }

    public static IEnumerable<string> SelectFirstLetterGroup()
    {
        List<string> firstCharacters = new List<string>();

        List<string> sortedMas = GetDate();

        using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.sqlite"))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT DISTINCT SUBSTR(groups, 1, 1) AS firstCharacter FROM Groups", connection))
            {
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string firstCharacter = reader.GetString(0);
                    firstCharacters.Add(firstCharacter);
                }
            }
            connection.Close();
        }

        IEnumerable<string> mas = sortedMas.Intersect(firstCharacters);

        return mas;
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