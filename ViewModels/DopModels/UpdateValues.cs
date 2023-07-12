using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DocumentFormat.OpenXml.Wordprocessing;
using MainTable;

namespace Sasha_Project
{
    public class UpdateValues
    {
        private protected Dictionary<string, bool[]> dict = new Dictionary<string, bool[]> ();

        public UpdateValues() { }

        public void InsertNewValue(string room)
        {
            dict.Add(room, new bool[2] { true, true });
        }

        public void DeleteValues()
        {
            dict.Clear();
        }

        public void DopFunc(string value, int i)
        {
            string[] mas = value.Split("\n");

            if (value.Length > 0)
            {
                foreach (string m in mas)
                {
                    if (dict.ContainsKey(m))
                    {
                        dict[m][i] = false;
                    }
                }
            }
            else
            {
                if (dict.ContainsKey(value))
                {
                    dict[value][i] = false;
                }
            }
        }

        private protected void FuncWithout(string m)
        {
            if (dict.ContainsKey(m))
            {
                dict[m][0] = false;
                dict[m][1] = false;
            }
        }

        public virtual void ZapolDict(ObservableCollection<Tables> tables)
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

        public void GetValues(Tables table, out bool razdel, out bool val)
        {
            razdel = table.RazdelPara;
            if (table.Link != null)
            {
                val = false;
            }
            else val = true;

        }

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

        public virtual List<string> GetMas(bool razdel, bool val, string rooms, string lesson = "")
        {
            IEnumerable<string> mas;
            if (razdel == false)
            {
                mas = from i in dict
                                where i.Value[0] == true && i.Value[1] == true
                                select i.Key;

            } else if (val == true)
            {
                mas = from i in dict
                                where i.Value[0] == true
                                select i.Key;
            } else
            {
                mas = from i in dict
                                where i.Value[1] == true
                                select i.Key;
            }

            List<string> values = new List<string>(mas) { "" };

            KnowingValue(values, rooms);

            return values;
        }

        public void DeleteValue(string deletePerson, bool razdel, bool value)
        {
            if (razdel)
            {
                if (value)
                {
                    dict[deletePerson][0] = false;
                } else
                {
                    dict[deletePerson][1] = false;
                }
            } else
            {
                dict[deletePerson][0] = false;
                dict[deletePerson][1] = false;
            }
        }
    }

    
    public class UpdateValuesPrepods : UpdateValues
    {
        public UpdateValuesPrepods() { }

        Dictionary<string, List<string>> prepods = new Dictionary<string, List<string>>();

        public void NewDict(Dictionary<string, List<string>> preps)
        {
            prepods = preps;
        }

        public List<string> GetLessons()
        {
            List<string> values = new List<string>(prepods.Keys) { " ", "" };
            return values;
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
                    string prepodNextZamena = tableNext.Office;
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

        public List<string> GetAllPrepods(bool razdel, string strPrepods, bool val)
        {
            IEnumerable<string> mas;
            if (razdel == false)
            {
                mas = from i in dict
                      where i.Value[0] == true && i.Value[1] == true
                      orderby i.Key
                      select i.Key;
            }
            else if (val == true)
            {
                mas = from i in dict
                      where i.Value[0] == true
                      orderby i.Key
                      select i.Key;
            }
            else
            {
                mas = from i in dict
                      where i.Value[1] == true
                      orderby i.Key
                      select i.Key;
            }

            List<string> values = new List<string>(mas) { "" };

            KnowingValue(values, strPrepods);

            return values;
        }

        public override List<string> GetMas(bool razdel, bool val, string strPrepods, string lesson)
        {
            if (lesson.Length > 1)
            {
                IEnumerable<string> mas;
                if (razdel == false)
                {
                    mas = from i in prepods[lesson]
                          where dict[i][0] == true && dict[i][1] == true
                          select i;
                }
                else if (val == true)
                {
                    mas = from i in prepods[lesson]
                          where dict[i][0] == true
                          select i;
                }
                else
                {
                    mas = from i in prepods[lesson]
                          where dict[i][1] == true
                          select i;
                }
                List<string> values = new List<string>(mas);

                KnowingValue(values, strPrepods);

                return values;
            }
            return new List<string>();
        }
    }
}
