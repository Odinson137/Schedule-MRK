using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DocumentFormat.OpenXml.Presentation;
using MainTable;

namespace Sasha_Project.ViewModels.DopModels
{
    public abstract class UpdateValues
    {
        public Dictionary<string, bool[]> dict = new Dictionary<string, bool[]>();

        public abstract void ZapolDict(ObservableCollection<Tables> tables);

        private protected void KnowingValue(List<string> values, string rooms)
        {
            string[] roomMas = rooms.Split("\n");
            if (roomMas.Length > 0)
            {
                foreach (string s in rooms.Split("\n"))
                {
                    values.Add(s);
                }
            }
            else
            {
                values.Add(rooms);
            }
        }

        public void DopFunc(string value, int i)
        {
            string[] mas = value.Split("\n");

            if (value.Length > 0)
            {
                foreach (string m in mas)
                {
                    if (dict.ContainsKey(m)) dict[m][i] = false;
                }
            }
            else
            {
                if (dict.ContainsKey(value)) dict[value][i] = false;
            }
        }

        public void FuncWithout(string key)
        {
            if (dict.ContainsKey(key))
            {
                dict[key][0] = false;
                dict[key][1] = false;
            }
        }


        private protected void GetValues(string group, out bool razdel, out bool val)
        {
            if (group.Contains("1 час"))
            {
                razdel = true;
                val = true;
            }
            else if (group.Contains("2 час"))
            {
                razdel = true;
                val = false;
            }
            else
            {
                razdel = false;
                val = false;
            }
        }

        public abstract List<string> GetMas(string group, string rooms);
        //public abstract List<string> GetMas(string group, string rooms, string lesson, int kurs);
        public abstract void DeleteValues();

        public abstract void InsertNewValue(string value);
    }

    public class UpdateValuesRooms : UpdateValues
    {

        public UpdateValuesRooms() { }

        public override void InsertNewValue(string value)
        {
            dict.Add(value, new bool[2] { true, true });
        }

        public override void DeleteValues()
        {
            dict.Clear();
        }

        public override void ZapolDict(ObservableCollection<Tables> tables)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                Tables table = tables[i];

                string room = table.Rooms;

                bool razdel = table.RazdelPara;
                if (razdel)
                {
                    Tables tableNext = tables[++i];
                    string roomNext = tableNext.Rooms;

                    DopFunc(room, 0);
                    DopFunc(roomNext, 1);
                }
                else
                {
                    string[] mas = room.Split("\n");

                    if (mas.Length > 0)
                    {
                        foreach (string m in mas)
                        {
                            FuncWithout(m);
                        }
                    }
                    else
                    {
                        FuncWithout(room);
                    }
                }
            }
        }

        public override List<string> GetMas(string group, string rooms)
        {
            GetValues(group, out bool razdel, out bool value);

            IEnumerable<string> mas;
            if (razdel == false)
            {
                mas = from i in dict
                      where i.Value[0] == true && i.Value[1] == true
                      select i.Key;

            }
            else
            {
                int num = 1;
                if (value == true) num = 0;
                mas = from i in dict
                      where i.Value[num] == true
                      select i.Key;
            }

            List<string> values = new List<string>(mas) { "" };

            KnowingValue(values, rooms);

            return values;
        }
    }

    public struct Lessons
    {
        public string Lesson { get; set; }
        public string Prepod { get; set; }
        public int Kurs { get; set; }
    }

    public class UpdateValuesPrepods : UpdateValues
    {
        public string Lesson { get; set; }
        public int Kurs { get; set; }

        public UpdateValuesPrepods() { }


        private List<Lessons> lessons = new List<Lessons>();
        public void NewMas(List<Lessons> mas)
        {
            lessons = mas;
        }
        public override void DeleteValues()
        {
            lessons.Clear();
        }

        public override void InsertNewValue(string value)
        {
            dict.TryAdd(value, new bool[2] { true, true });
        }

        public void InsertNewStruct(Lessons value)
        {
            lessons.Add(value);
        }

        public IEnumerable<string> GetLessons()
        {
            IEnumerable<string> uniqueList = lessons.Where(x => x.Kurs == Kurs).Select(x => x.Lesson).Distinct();
            return uniqueList;
        }

        public override void ZapolDict(ObservableCollection<Tables> tables)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                Tables table = tables[i];

                string prepod = table.Prepods;
                string prepodZamena = table.Changes;
                string prepodCadefra = table.Office;

                bool razdel = table.RazdelPara;
                if (razdel)
                {
                    Tables tableNext = tables[++i];
                    string prepodNext = tableNext.Prepods;
                    string prepodNextZamena = tableNext.Changes;
                    string prepodNextCafedra = tableNext.Office;

                    DopFunc(prepod, 0);
                    DopFunc(prepodNextZamena, 0);
                    DopFunc(prepodCadefra, 0);
                    DopFunc(prepodNext, 1);
                    DopFunc(prepodNextZamena, 1);
                    DopFunc(prepodNextCafedra, 1);
                }
                else
                {
                    string[] mas = prepod.Split("\n");

                    if (mas.Length > 1)
                    {
                        foreach (string m in mas)
                        {
                            FuncWithout(m);
                        }
                    }
                    else
                    {
                        FuncWithout(prepod);
                    }

                    string[] mas1 = prepodZamena.Split("\n");

                    if (mas1.Length > 1)
                    {
                        foreach (string m in mas1)
                        {
                            FuncWithout(m);
                        }
                    }
                    else
                    {
                        FuncWithout(prepodZamena);
                    }

                    string[] mas2 = prepodCadefra.Split("\n");

                    if (mas1.Length > 1)
                    {
                        foreach (string m in mas2)
                        {
                            FuncWithout(m);
                        }
                    }
                    else
                    {
                        FuncWithout(prepodCadefra);
                    }
                }
            }
        }

        public List<string> GetAllPrepods(string group, string strPrepods)
        {
            GetValues(group, out bool razdel, out bool value);
            IEnumerable<string> mas;
            if (razdel == false)
            {
                mas = from i in lessons
                      where dict[i.Prepod][0] == true && dict[i.Prepod][1] == true
                      select i.Prepod;
            }
            else
            {
                int num = 1;
                if (value) num = 0;
                mas = from i in lessons
                      where dict[i.Prepod][num] == true
                      select i.Prepod;
            }
            Console.WriteLine(mas.Count());
            List<string> values = new List<string>(mas) { "asf" };

            KnowingValue(values, strPrepods);

            return values;
        }

        public override List<string> GetMas(string group, string strPrepods)
        {
            GetValues(group, out bool razdel, out bool value);

            if (Lesson.Length > 1)
            {
                IEnumerable<string> mas;
                if (razdel == false)
                {
                    mas = from i in lessons
                          where i.Lesson == Lesson
                          where dict[i.Prepod][0] == true && dict[i.Prepod][1] == true
                          select i.Prepod;
                }
                else
                {
                    int num = 1;
                    if (value) num = 0;
                    mas = from i in lessons
                          where i.Lesson == Lesson
                          where dict[i.Prepod][num] == true
                          select i.Prepod;
                }
                List<string> values = new List<string>(mas);

                KnowingValue(values, strPrepods);

                return values;
            }
            return new List<string>();
        }
    }
}
