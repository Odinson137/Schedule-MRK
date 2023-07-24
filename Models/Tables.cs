using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MainTable
{
    public class Tables : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string group;
        public string Groups
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
                OnPropertyChanged("Groups");
            }
        }
        private string[] rooms = new string[3] { "", "", "" };
        public string Rooms
        {
            set
            {
                string[] mas = value.Split('\n');
                if (mas.Length <= 1)
                    rooms[0] = value;
                else
                    for (int i = 0; i < mas.Length; i++)
                        rooms[i] = mas[i];
                OnPropertyChanged("Rooms");
            }
            get
            {
                string str;
                if (rooms[2] != "") str = rooms[0] + "\n" + rooms[1] + "\n" + rooms[2];
                else if (rooms[1] != "") str = rooms[0] + "\n" + rooms[1];
                else str = rooms[0];
                return str;
            }
        }

        public string Room1
        {
            set
            {
                rooms[0] = value;
                OnPropertyChanged("Rooms");
            }
            get
            {
                return rooms[0];
            }
        }

        public string Room2
        {
            set
            {
                rooms[1] = value;
                OnPropertyChanged("Rooms");
            }
            get
            {
                return rooms[1];
            }
        }
        public string Room3
        {
            set
            {
                rooms[2] = value;
                OnPropertyChanged("Rooms");
            }
            get
            {
                return rooms[2];
            }
        }

        private string[] prepods = new string[3] { "", "", "" };
        public string Prepods
        {
            set
            {
                string[] mas = value.Split('\n');
                if (mas.Length <= 1)
                    prepods[0] = value;
                else
                    for (int i = 0; i < mas.Length; i++)
                        prepods[i] = mas[i];
                OnPropertyChanged("Prepods");
            }
            get
            {
                string str;
                if (prepods[2] != "") str = Prepod1 + "\n" + Prepod2 + "\n" + Prepod3;
                else if (prepods[1] != "") str = Prepod1 + "\n" + Prepod2;
                else str = Prepod1;
                return str;
            }
        }

        public string Prepod1
        {
            set
            {
                prepods[0] = value;
                OnPropertyChanged("Prepods");

            }
            get
            {
                return prepods[0];
            }
        }

        public string Prepod2
        {
            set
            {
                prepods[1] = value;
                OnPropertyChanged("Prepods");
            }
            get
            {
                return prepods[1];
            }
        }

        public string Prepod3
        {
            set
            {
                prepods[2] = value;
                OnPropertyChanged("Prepods");
            }
            get
            {
                return prepods[2];
            }
        }

        private string[] changes = new string[3] { "", "", "" };

        public string Changes
        {
            set
            {
                string[] mas = value.Split('\n');
                if (mas.Length <= 1)
                    changes[0] = value;
                else
                    for (int i = 0; i < mas.Length; i++)
                        changes[i] = mas[i];
                OnPropertyChanged("Changes");
            }
            get
            {
                string str;
                if (changes[2] != "") str = Changes1 + "\n" + Changes2 + "\n" + Changes3;
                else if (changes[1] != "") str = Changes1 + "\n" + Changes2;
                else str = Changes1;
                return str;
            }
        }

        public string Changes1
        {
            set
            {
                changes[0] = value;
                OnPropertyChanged("Changes");
            }
            get
            {
                return changes[0];
            }
        }

        public string Changes2
        {
            set
            {
                changes[1] = value;
                OnPropertyChanged("Changes");
            }
            get
            {
                return changes[1];
            }
        }

        public string Changes3
        {
            set
            {
                changes[2] = value;
                OnPropertyChanged("Changes");
            }
            get
            {
                return changes[2];
            }
        }

        private string[] office = new string[3] { "", "", "" };

        public string Office
        {
            set
            {
                string[] mas = value.Split('\n');
                if (mas.Length <= 1)
                    office[0] = value;
                else
                    for (int i = 0; i < mas.Length; i++)
                        office[i] = mas[i];
                OnPropertyChanged("Office");
            }
            get
            {
                string str;
                if (office[2] != "") str = Office1 + "\n" + Office2 + "\n" + Office3;
                else if (office[1] != "") str = Office1 + "\n" + Office2;
                else str = Office1;
                return str;
            }
        }

        public string Office1
        {
            set
            {
                office[0] = value;
                OnPropertyChanged("Office");
            }
            get
            {
                return office[0];
            }
        }

        public string Office2
        {
            set
            {
                office[1] = value;
                OnPropertyChanged("Office");
            }
            get
            {
                return office[1];
            }
        }

        public string Office3
        {
            set
            {
                office[2] = value;
                OnPropertyChanged("Office");
            }
            get
            {
                return office[2];
            }
        }

        public string MainLesson
        {
            get
            {
                if (officeValue)
                {
                    return officeLesson;
                } else
                {
                    return lesson;
                }
            }
            set {}
        }

        private string officeLesson = "";
        public string OfficeLesson
        {
            set
            {
                if (value == "")
                {
                    officeLesson = "";
                }
                else
                {
                    if (officeLesson != "") Office = " ";
                    officeLesson = value;
                }
                OnPropertyChanged("OfficeLesson");
                OnPropertyChanged("MainLesson");
            }
            get
            {
                return officeLesson;
            }
        }

        private string lesson = "";
        public string Lesson {
            set
            {
                if (lesson != "")
                {
                    Prepods = " ";
                    Changes = "";
                }
                lesson = value;
                //}
                OnPropertyChanged("Lesson");
                OnPropertyChanged("Changes");
                OnPropertyChanged("MainLesson");
            }
            get
            {
                return lesson;
            }
        }

        public bool RazdelPara { get; set; }

        private bool officeValue = false;

        public bool OfficeValue
        {
            get
            {
                return officeValue;
            }
            set
            {
                officeValue = value;

                OnPropertyChanged("OfficeValue");
                OnPropertyChanged("MainLesson");
            }
        }

        private bool second = false;

        public bool Second
        {
            get
            {
                if (Room2.Length > 1 || Prepod2.Length > 1)
                {
                    second = true;
                }
                return second;
            }
            set
            {
                Room2 = "";
                Prepod2 = "";
                second = value;

                OnPropertyChanged("Second");
            }
        }

        private bool third = false;

        public bool Third
        {
            get
            {
                if (Room3.Length > 1 || Prepod3.Length > 1)
                {
                    third = true;
                }
                return third;
            }
            set
            {
                Room3 = "";
                Prepod3 = "";
                third = value;

                OnPropertyChanged("Third");
            }
        }

        public Tables? Link { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
