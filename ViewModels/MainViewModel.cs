using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Base;
using System.Windows.Input;
using MainTable;
using Sasha_Project.Excel;
using System.Diagnostics;
using System.IO;
using Sasha_Project.Word;
using System.Collections.Generic;
using Sasha_Project.Commands;
using System.Windows;

namespace Sasha_Project.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Tables selectedPhone;
        public List<string> Lessons { get; set; }
        public string Lesson { get; set; }
        private readonly UpdateValuesPrepods prepods = new UpdateValuesPrepods();
        private readonly UpdateValues rooms = new UpdateValues();

        private void UpdateValues()
        {
            DataBase a = new DataBase();
            a.SelectLessons(prepods);
            a.SelectRooms(rooms);

            Phones = a.SelectBigBase(Para + 1, weekStr, Day + 1, rooms, prepods);

            OnPropertyChanged("Phones");
        }

        public List<string> RoomsMas { get; set; }
        public List<string> TeacherMas { get; set; }
        public List<string> OfficeMas { get; set; }
        public List<string> ChangesMas { get; set; }

        int para = 0;
        public int Para
        {
            get
            {
                return para;
            }
            set
            {
                para = value;
                UpdateValues();
            }
        }

        private string weekStr = "ч";
        public string Week
        {
            get
            {
                if (weekStr == "ч")
                {
                    return "Числитель";
                }
                else
                {
                    return "Знаменатель";
                }
            }
        }
        int day = 0;
        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
                UpdateValues();
            }
        }

        public ObservableCollection<Tables> Phones { get; set; }
        public Tables SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                rooms.GetValues(value, out bool razdel, out bool vel);
                RoomsMas = rooms.GetMas(razdel, vel);
                RoomsMas.Add(" ");
  
                TeacherMas = prepods.GetMas(razdel, vel, value.Lesson);
                TeacherMas.Add(" ");

                OfficeMas = prepods.GetMas(razdel, vel, value.OfficeLesson);
                OfficeMas.Add(" ");

                ChangesMas = prepods.GetAllPrepods(razdel, vel);
                ChangesMas.Add(" ");

                Lesson = value.Lesson;

                selectedPhone = value;

                OnPropertyChanged("RoomsMas");
                OnPropertyChanged("TeacherMas");
                OnPropertyChanged("ChangesMas");
                OnPropertyChanged("OfficeMas");
                OnPropertyChanged("SelectedPhone");
            }
        }
     
        public ViewModel()
        {
            DataBase a = new DataBase();
            a.SelectLessons(prepods);
            a.SelectRooms(rooms);

            Lessons = prepods.GetLessons();

            Phones = a.SelectBigBase(Para+1, weekStr, Day+1, rooms, prepods);
        }

        RelayCommand? getWeek;
        public RelayCommand GetWeek => getWeek ??
            (getWeek = new RelayCommand(obj =>
            {
                if (weekStr == "ч")
                {
                    weekStr = "з";
                }
                else
                {
                    weekStr = "ч";
                }
                OnPropertyChanged("Week");

                UpdateValues();
            }));

        RelayCommand? swipeLessons;
        public RelayCommand SwipeLessons => swipeLessons ??
            (swipeLessons = new RelayCommand(obj =>
            {
                if (obj as string == "0")
                {
                    Lesson = selectedPhone.Lesson;
                    selectedPhone.OfficeValue = false;
                }
                else
                {
                    Lesson = selectedPhone.OfficeLesson;
                    selectedPhone.OfficeValue = true;
                }
            }));

        private RelayCommand? click;
        public ICommand Click => click ??= new RelayCommand(PerformClick);

        private void PerformClick(object commandParameter)
        {
            if (Lesson != selectedPhone.Lesson)
            {
                prepods.GetValues(selectedPhone, out bool razdel, out bool vel);
                MessageBox.Show("asd");
                if (commandParameter as string == "1")
                {
                    TeacherMas = prepods.GetMas(razdel, vel, selectedPhone.Lesson);
                    TeacherMas.Add(" ");

                    ChangesMas = prepods.GetAllPrepods(razdel, vel);
                    ChangesMas.Add(" ");

                    OnPropertyChanged("TeacherMas");
                    OnPropertyChanged("ChangesMas");
                }
                else
                {
                    OfficeMas = prepods.GetMas(razdel, vel, selectedPhone.OfficeLesson);
                    OfficeMas.Add(" ");

                    OnPropertyChanged("OfficeMas");
                }
            }
        }

        RelayCommand? clickSave;
        public RelayCommand ClickSave => clickSave ??
            (clickSave = new RelayCommand(obj =>
            {
                DataBase dataBase = new DataBase();
                dataBase.UpdateValueInTableFull(selectedPhone.Id, selectedPhone, selectedPhone.RazdelPara, selectedPhone.Link);
                MessageBox.Show("Good");
            }));

        private RelayCommand? selectFile;
        public RelayCommand SelectFile => selectFile ??
            (selectFile = new RelayCommand(obj =>
            {
                string val = obj.ToString();
                if (val == "0")
                {
                    WorkExcel a = new WorkExcel();
                    a.CreateExcel(weekStr, Para + 1, Day + 1);

                    Process.Start(CreatePathExcel(), "Преподаватели.xlsx");
                }
                else if (val == "1")
                {
                    CreateRoomWord a = new CreateRoomWord();
                    a.CreateWord(weekStr, (Para + 1).ToString());

                    Process.Start(CreatePathWord(), "Комнаты.docx");
                }
                else
                {
                    WorkWord a = new WorkWord();
                    a.MainDocument(Day + 1, weekStr);

                    Process.Start(CreatePathWord(), "Учащиеся.docx");
                }
            }));

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private string CreatePathExcel()
        {
            string path = @"Files\Excel.txt";
            string fileText = File.ReadAllText(path);
            return $@"{fileText}";
        }
        private string CreatePathWord()
        {
            string path = @"Files\Word.txt";
            string fileText = File.ReadAllText(path);
            return $@"{fileText}";
        }

        private RelayCommand? manageSecond;
        public ICommand ManageSecond => manageSecond ??= new RelayCommand(PerformManageSecond);

        private void PerformManageSecond(object commandParameter)
        {
            selectedPhone.Second = (string)commandParameter == "1";
        }

        private RelayCommand? manageThird;
        public ICommand ManageThird => manageThird ??= new RelayCommand(PerformManageThird);

        private void PerformManageThird(object commandParameter)
        {
            selectedPhone.Third = (string)commandParameter == "1";
        }

        private RelayCommand? selectRazdel;
        public ICommand SelectRazdel => selectRazdel ??= new RelayCommand(PerformRazdel);

        private void PerformRazdel(object commandParameter)
        {
            SelectedPhone.RazdelPara = true;

            int index = 0;
            foreach (Tables table in Phones)
            {
                index++;
                if (table.Groups == SelectedPhone.Groups)
                {
                    break;
                }
            }

            Tables newTable = new Tables()
            {
                Groups = SelectedPhone.Groups + " (2 час)",
                Id = SelectedPhone.Id,
                RazdelPara = true
            };

            SelectedPhone.Groups += " (1 час)";

            selectedPhone.Link = newTable;
            newTable.Link = selectedPhone;

            Phones.Insert(index, newTable);
            OnPropertyChanged("SelectedPhone");
            OnPropertyChanged("Phones");
        }

        private RelayCommand? selectPara;
        public ICommand SelectPara => selectPara ??= new RelayCommand(PerformlPara);

        private void PerformlPara(object commandParameter)
        {
            SelectedPhone.RazdelPara = false;

            string[] mas = SelectedPhone.Groups.Split(" ");
            SelectedPhone.Groups = mas[0];
            Phones.Remove(SelectedPhone.Link);

            OnPropertyChanged("SelectedPhone");
            OnPropertyChanged("Phones");
        }
    }
}