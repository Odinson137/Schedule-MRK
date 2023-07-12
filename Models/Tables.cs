using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MainTable
{
    public class Tables : INotifyPropertyChanged
    {
        public int Id { get; set; }

        string group = "";
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
        private string rooms = "";
        public string Rooms
        {
            set
            {
                if (value == "0")
                {
                    rooms = "";
                }
                else
                {
                    rooms = value;
                }
                OnPropertyChanged("Rooms");
            }
            get
            {
                return rooms;
            }
        }

        public string Room1
        {
            set
            {
                string[] mas = rooms.Split("\n");
                if (mas.Length > 0)
                {
                    mas[0] = value;
                    rooms = String.Join('\n', mas);
                }
                else
                {
                    rooms = value;
                }
                OnPropertyChanged("Rooms");
            }
            get
            {
                string[] mas = rooms.Split("\n");
                if (mas.Length > 0)
                {
                    return mas[0];
                }
                else
                {
                    return rooms;
                }
            }
        }

        public string Room2
        {
            set
            {
                string[] mas = rooms.Split("\n");
                if (mas.Length > 1)
                {
                    mas[1] = value;
                    rooms = String.Join('\n', mas);
                }
                else
                {
                    rooms += "\n" + value;
                }
                OnPropertyChanged("Rooms");
            }
            get
            {
                string[] mas = rooms.Split("\n");
                if (mas.Length > 1)
                {
                    return mas[1];
                }
                else
                {
                    return "";
                }
            }
        }

        public string Room3
        {
            set
            {
                string[] mas = rooms.Split("\n");
                if (mas.Length > 2)
                {
                    mas[2] = value;
                    rooms = String.Join('\n', mas);
                }
                else
                {
                    rooms += "\n" + value;
                }
                OnPropertyChanged("Rooms");
            }
            get
            {
                string[] mas = rooms.Split("\n");
                if (mas.Length > 2)
                {
                    return mas[2];
                }
                else
                {
                    return "";
                }
            }
        }

        private string prepods = "";
        public string Prepods
        {
            set
            {
                if (value == "0")
                {
                    prepods = "";
                }
                else
                {
                    prepods = value;
                }
                OnPropertyChanged("Prepods");
            }
            get
            {
                return prepods;
            }
        }

        public string Prepod1
        {
            set
            {
                string[] mas = prepods.Split("\n");
                if (mas.Length > 0)
                {
                    mas[0] = value;
                    prepods = String.Join('\n', mas);
                }
                else
                {
                    prepods = value;
                }
                OnPropertyChanged("Prepods");

            }
            get
            {
                string[] mas = prepods.Split("\n");
                if (mas.Length > 0)
                {
                    return mas[0];
                }
                else
                {
                    return prepods;
                }
            }
        }

        public string Prepod2
        {
            set
            {
                string[] mas = prepods.Split("\n");
                if (mas.Length > 1)
                {
                    mas[1] = value;
                    prepods = String.Join('\n', mas);
                }
                else {
                    prepods += "\n" + value;
                }
                OnPropertyChanged("Prepods");
            }
            get
            {
                string[] mas = prepods.Split("\n");
                if (mas.Length > 1)
                {
                    return mas[1];
                }
                else
                {
                    return " ";
                }
            }
        }

        public string Prepod3
        {
            set
            {
                string[] mas = prepods.Split("\n");
                if (mas.Length > 2)
                {
                    mas[2] = value;
                    prepods = String.Join('\n', mas);
                }
                else {
                    prepods += "\n" + value;
                }
                OnPropertyChanged("Prepods");
            }
            get
            {
                string[] mas = prepods.Split("\n");
                if (mas.Length > 2)
                {
                    return mas[2];
                }
                else
                {
                    return " ";
                }
            }
        }

        private string changes = "";

        public string Changes
        {
            set
            {
                if (value == "0")
                {
                    changes = "";
                }
                else
                {
                    changes = value;
                }
                OnPropertyChanged("Changes");
            }
            get
            {
                return changes;
            }
        }

        public string Changes1
        {
            set
            {
                string[] mas = changes.Split("\n");
                if (mas.Length > 0)
                {
                    mas[0] = value;
                    changes = String.Join('\n', mas);
                }
                else
                {
                    changes = value;
                }
                OnPropertyChanged("Changes");
            }
            get
            {
                string[] mas = changes.Split("\n");
                if (mas.Length > 0)
                {
                    return mas[0];
                }
                else
                {
                    return changes;
                }
            }
        }

        public string Changes2
        {
            set
            {
                string[] mas = changes.Split("\n");
                if (mas.Length > 1)
                {
                    mas[1] = value;
                    changes = String.Join('\n', mas);
                }
                else
                {
                    changes += "\n" + value;
                }
                OnPropertyChanged("Changes");
            }
            get
            {
                string[] mas = changes.Split("\n");
                if (mas.Length > 1)
                {
                    return mas[1];
                }
                else
                {
                    return " ";
                }
            }
        }

        public string Changes3
        {
            set
            {
                string[] mas = changes.Split("\n");
                if (mas.Length > 2)
                {
                    mas[2] = value;
                    changes = String.Join('\n', mas);
                }
                else
                {
                    changes += "\n" + value;
                }
                OnPropertyChanged("Changes");
            }
            get
            {
                string[] mas = changes.Split("\n");
                if (mas.Length > 2)
                {
                    return mas[2];
                }
                else
                {
                    return " ";
                }
            }
        }

        private string office = "";

        public string Office
        {
            set
            {
                if (value == "0")
                {
                    office = "";
                }
                else
                {
                    office = value;
                }
                OnPropertyChanged("Office");
            }
            get
            {
                return office;
            }
        }

        public string Office1
        {
            set
            {
                string[] mas = office.Split("\n");
                if (mas.Length > 0)
                {
                    mas[0] = value;
                    office = String.Join('\n', mas);
                }
                else
                {
                    office = value;
                }
                OnPropertyChanged("Office");
            }
            get
            {
                string[] mas = office.Split("\n");
                if (mas.Length > 0)
                {
                    return mas[0];
                }
                else
                {
                    return office;
                }
            }
        }

        public string Office2
        {
            set
            {
                string[] mas = office.Split("\n");
                if (mas.Length > 1)
                {
                    mas[1] = value;
                    office = String.Join('\n', mas);
                }
                else
                {
                    office += "\n" + value;
                }
                OnPropertyChanged("Office");
            }
            get
            {
                string[] mas = office.Split("\n");
                if (mas.Length > 1)
                {
                    return mas[1];
                }
                else
                {
                    return " ";
                }
            }
        }

        public string Office3
        {
            set
            {
                string[] mas = office.Split("\n");
                if (mas.Length > 2)
                {
                    mas[2] = value;
                    office = String.Join('\n', mas);
                }
                else
                {
                    office += "\n" + value;
                }
                OnPropertyChanged("Office");
            }
            get
            {
                string[] mas = office.Split("\n");
                if (mas.Length > 2)
                {
                    return mas[2];
                }
                else
                {
                    return " ";
                }
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
                if (value == "0")
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
                if (value == "0")
                {
                    lesson = "";
                }
                else
                {
                    if (lesson != "")
                    {
                        Prepods = " ";
                        Changes = "";
                    }
                    lesson = value;
                }
                OnPropertyChanged("Lesson");
                OnPropertyChanged("Changes");
                OnPropertyChanged("MainLesson");
            }
            get
            {
                return lesson;
            }
        }

        public bool RazdelPara { get; set; } = false;

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
                Rooms = Room1;
                Prepods = Prepod1;
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
                Rooms = Room1 + "\n" + Room2;
                Prepods = Prepod1 + "\n" + Prepod2;
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
