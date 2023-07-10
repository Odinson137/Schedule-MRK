using System;
using System.Collections.Generic;

namespace DopFiles
{
    class CreateRoom
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    struct Group
    {
        public Group() {}
        public string Gr { get; set; } = "";
        public bool Distant { get; set; } = false;
    
        public override string ToString()
        {
            return Gr;
        }
    }

    class CreatePrepods : CreateRoom
    {
        public string ValueTwo { get; set; }
    }

    class CreateNewPrepods : CreatePrepods
    {
        public string ValueThree { get; set; }
    }

    struct WorkDate
    {
        public WorkDate() {}

        private Dictionary<int, string[]> daier = new Dictionary<int, string[]>()
        {
            [1] = new string[] { "пн", "Понедельник" },
            [2] = new string[] { "вт", "Вторник" },
            [3] = new string[] { "ср", "Среда" },
            [4] = new string[] { "чт", "Четверг" },
            [5] = new string[] { "пт", "Пятницу" },
            [6] = new string[] { "сб", "Суббота" }
        };

        public string[] GetDaier(int i)
        {
            return daier[i];
        }

        private readonly Dictionary<int, DayOfWeek> daierCount = new Dictionary<int, DayOfWeek>()
        {
            [1] = DayOfWeek.Monday,
            [2] = DayOfWeek.Thursday,
            [3] = DayOfWeek.Wednesday,
            [4] = DayOfWeek.Wednesday,
            [5] = DayOfWeek.Friday,
            [6] = DayOfWeek.Saturday
        };

        public DateTime GetNextMonday(int day)
        {
            DateTime todayDate = DateTime.Now;
            int daysUntilMonday = ((int)daierCount[day] - (int)todayDate.DayOfWeek + 7) % 7;
            return todayDate.AddDays(daysUntilMonday);
        }


        Dictionary<char, string[]> weeker = new Dictionary<char, string[]>()
        {
            ['ч'] = new string[] { "числ", "числитель" },
            ['з'] = new string[] { "знам", "знаменатель" },
        };

        public string[] GetWeeker(char i)
        {
            return weeker[i];
        }


        Dictionary<int, string> parier = new Dictionary<int, string>()
        {
            [1] = "(8:30-10:05)",
            [2] = "(10:15 - 11:50)",
            [3] = "(12:10 - 13:45)",
            [4] = "(13:55 - 15:30)",
            [5] = "(15:50 - 17:25)",
            [6] = "(17:35 - 19:10)",
            [7] = "(19:20 - 20:55)",
        };

        public string GetPara(int i)
        {
            return parier[i];
        }

        public Dictionary<string, List<Group>> GetDate()
        {
            Dictionary<string, List<Group>> mas = new Dictionary<string, List<Group>>();

            for (int i = 0; i < 5; i++)
            {
                DateTime years = DateTime.Now.AddYears(-i);
                mas.Add(years.Year.ToString().Substring(3), new List<Group>());
            }
            return mas;
        }
    }
}
