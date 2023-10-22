using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Sasha_Project.Commands;
using Sasha_Project.Models.SettingsModels;
using Sasha_Project.ViewModels.DopModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sasha_Project.ViewModels.SettingsPages
{
    class SettingLessonsViewModel : SettingBaseViewModel, IBase<LessonModel>
    {
        public ObservableCollection<LessonModel> List { get; set; }

        private LessonModel selectedItem;
        public LessonModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public SettingLessonsViewModel()
        {
            List = new ObservableCollection<LessonModel>();
            SelectLessons();
            SelectedItem = List[0];
        }

        public bool DeleteValue()
        {
            string request = $"DELETE FROM Lessons WHERE ID = @ID";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "ID", SelectedItem.ID}
            });
        }

        public bool InsertValue(LessonModel newValue)
        {
            string request = $"INSERT INTO Lessons (ID, Lessons, Shorts, Kurs_1, Kurs_2, Kurs_3, Kurs_4) VALUES (@id, @value1, @value2, @value3, @value4, @value5, @value6)";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", newValue.ID },
                { "value1", SelectedItem.Lesson },
                { "value2", SelectedItem.Shorts },
                { "value3", SelectedItem.Kurs1 },
                { "value4", SelectedItem.Kurs2 },
                { "value5", SelectedItem.Kurs3 },
                { "value6", SelectedItem.Kurs4 }
            });
        }

        public bool PutValue()
        {
            string request = $"UPDATE Lessons SET (Lessons, Shorts, Kurs_1, Kurs_2, Kurs_3, Kurs_4) = (@value1, @value2, @value3, @value4, @value5, @value6) WHERE ID = @id";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", SelectedItem.ID },
                { "value1", SelectedItem.Lesson },
                { "value2", SelectedItem.Shorts },
                { "value3", SelectedItem.Kurs1 },
                { "value4", SelectedItem.Kurs2 },
                { "value5", SelectedItem.Kurs3 },
                { "value6", SelectedItem.Kurs4 }
            });
        }

        private void AddToLessons(SQLiteDataReader reader)
        {
            List.Add(new LessonModel()
            {
                ID = reader.GetInt32(0),
                Lesson = reader.GetString(1),
                Shorts = reader.GetString(2),
            });
        }
        public bool SelectLessons()
        {
            string request = $"SELECT * FROM Lessons ORDER BY Lessons ASC";
            return WorkBase.SelectValues(request, AddToLessons);
        }

        public bool SelectValues()
        {
            throw new NotImplementedException();
        }

        RelayCommand? putLesson;
        public RelayCommand PutLesson => putLesson ??
            (putLesson = new RelayCommand(obj =>
            {
                if (SelectedItem.Lesson != null)
                {
                    if (SelectedItem.ID == -1)
                    {
                        SelectedItem.ID = List.Max(x => x.ID) + 1;
                        if (InsertValue(SelectedItem))
                            MessageBox.Show("Предмет добавлен");
                    }
                    else
                        if (PutValue())
                            MessageBox.Show("Данные успешно изменены");
                }
                else
                    MessageBox.Show("Поля пустые!");
            }));

        RelayCommand? deleteLesson;
        public RelayCommand DeleteLesson => deleteLesson ??
            (deleteLesson = new RelayCommand(obj =>
            {
                if (DeleteValue())
                {
                    MessageBox.Show("Удалено");
                    List.Remove(SelectedItem);
                    SelectedItem = List[0];
                }
            }));


        RelayCommand? addLesson;
        public RelayCommand AddLesson => addLesson ??
            (addLesson = new RelayCommand(obj =>
            {
                if (List[0].ID != -1)
                {
                    List.Insert(0, new LessonModel() { ID = -1, Shorts = "" });
                    SelectedItem = List[0];
                } else
                    MessageBox.Show("Заполните предыдущее значение!");
            }));

        RelayCommand? changeKurs;
        public RelayCommand ChangeKurs => changeKurs ??
            (changeKurs = new RelayCommand(obj =>
            {
                string a = obj.ToString();
                switch (a)
                {
                    case "1":
                        SelectedItem.Kurs1 = !SelectedItem.Kurs1;
                        break;
                    case "2":
                        SelectedItem.Kurs2 = !SelectedItem.Kurs2;
                        break;
                    case "3":
                        SelectedItem.Kurs3 = !SelectedItem.Kurs3;
                        break;
                    case "4":
                        SelectedItem.Kurs4 = !SelectedItem.Kurs4;
                        break;
                }
            }));
    }
}
