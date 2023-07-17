using MainTable;
using Sasha_Project.Commands;
using Sasha_Project.Excel;
using Sasha_Project.ViewModels.DopModels;
using Sasha_Project.Word;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sasha_Project.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        private Tables selectedPhone;
        public ObservableCollection<Tables> Phones { get; set; }
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

        public IEnumerable<string> Lessons { get; set; }
        public string Lesson { get; set; }
        private readonly UpdateValuesPrepods prepods = new UpdateValuesPrepods();
        private readonly UpdateValuesRooms rooms = new UpdateValuesRooms();

        private IEnumerable<string> firstCharacters;
        public TableViewModel()
        {
            //ReadBook r = new ReadBook();
            //r.SelectOpenendFile();

            DataBase.SelectLessons(prepods);
            DataBase.SelectRooms(rooms);

            firstCharacters = DataBase.SelectFirstLetterGroup();
            //Lessons = prepods.GetLessons();

            Phones = DataBase.SelectBigBase(Para + 1, weekStr, Day + 1, rooms, prepods);

            selectedPhone = Phones[0];
        }

        private void UpdateValues()
        {
            DataBase.SelectLessons(prepods);
            DataBase.SelectRooms(rooms);

            Phones = DataBase.SelectBigBase(Para + 1, weekStr, Day + 1, rooms, prepods);

            OnPropertyChanged("Phones");
        }

        private int GetKurs(Tables value)
        {
            char letterGroup = value.Groups.First();

            int kurs = 1;
            foreach (string charachter in firstCharacters)
            {
                if (charachter.First() == letterGroup)
                {
                    break;
                }
                kurs++;
            }
            return kurs;
        }

        public Tables SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                //if (selectedPhone != null)
                //{
                //    rooms.ChangeValue(SelectedPhone.Groups, selectedPhone.Rooms);
                //    prepods.ChangeValue(SelectedPhone.Groups, selectedPhone.Prepods);
                //    prepods.ChangeValue(SelectedPhone.Groups, selectedPhone.Office);
                //    prepods.ChangeValue(SelectedPhone.Groups, selectedPhone.Changes);
                //}

                char letterGroup = value.Groups.First();

                int kurs = 1;
                foreach (string charachter in firstCharacters)
                {
                    if (charachter.First() == letterGroup)
                    {
                        break;
                    }
                    kurs++;
                }

                prepods.Kurs = kurs;
                Lessons = prepods.GetLessons();

                RoomsMas = rooms.GetMas(value.Groups, value.Rooms);

                prepods.Lesson = value.Lesson;
                TeacherMas = prepods.GetMas(value.Groups, value.Prepods);

                prepods.Lesson = value.OfficeLesson;
                OfficeMas = prepods.GetMas(value.Groups, value.Office);

                ChangesMas = prepods.GetAllPrepods(value.Groups, value.Changes);

                Lesson = value.Lesson;
                selectedPhone = value;

                OnPropertyChanged("Lessons");
                OnPropertyChanged("RoomsMas");
                OnPropertyChanged("TeacherMas");
                OnPropertyChanged("ChangesMas");
                OnPropertyChanged("OfficeMas");
                OnPropertyChanged("SelectedPhone");
            }
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
                if (commandParameter as string == "1")
                {
                    prepods.Lesson = SelectedPhone.Lesson;
                    TeacherMas = prepods.GetMas(SelectedPhone.Groups, selectedPhone.Prepods);

                    ChangesMas = prepods.GetAllPrepods(SelectedPhone.Groups, selectedPhone.Prepods);

                    OnPropertyChanged("TeacherMas");
                    OnPropertyChanged("ChangesMas");
                }
                else
                {
                    prepods.Lesson = SelectedPhone.OfficeLesson;
                    OfficeMas = prepods.GetMas(SelectedPhone.Groups, selectedPhone.Prepods);

                    OnPropertyChanged("OfficeMas");
                }
            }
        }

        RelayCommand? clickSave;
        public RelayCommand ClickSave => clickSave ??
            (clickSave = new RelayCommand(obj =>
            {
                DataBase.UpdateValueInTableFull(selectedPhone.Id, selectedPhone, selectedPhone.RazdelPara, selectedPhone.Link);
                MessageBox.Show("Good");
                UpdateValues();
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

                    try
                    {
                        Process.Start(CreatePathExcel(), "Преподаватели.xlsx");
                    } catch
                    {
                        MessageBox.Show("Файл запуска Excel не найден. Проверьте путь в файле!");
                    }
                }
                else if (val == "1")
                {
                    CreateRoomWord a = new CreateRoomWord();
                    a.CreateWord(weekStr, (Para + 1).ToString());

                    try
                    {
                        Process.Start(CreatePathWord(), "Комнаты.docx");
                    } catch
                    {
                        MessageBox.Show("Файл запуска Word не найден. Проверьте путь в файле!");
                    }
            }
                else
                {
                    WorkWord a = new WorkWord();
                    a.MainDocument(Day + 1, weekStr);
                    try
                    {
                        Process.Start(CreatePathWord(), "Учащиеся.docx");
                    }
                    catch
                    {
                        MessageBox.Show("Файл запуска Word не найден. Проверьте путь в файле!");
                    }
                }
            }));

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
