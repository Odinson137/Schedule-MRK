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
            throw new NotImplementedException();
        }

        public bool InsertValue(LessonModel newValue)
        {
            throw new NotImplementedException();
        }

        public bool PutValue()
        {
            throw new NotImplementedException();
        }

        private void AddToLessons(SQLiteDataReader reader)
        {
            List.Add(new LessonModel()
            {
                ID = reader.GetInt32(0),
                Lesson = reader.GetString(1),
                Shorts = reader.GetString(2),
                Kurs = reader.GetInt32(3)
            });
        }
        public bool SelectLessons()
        {
            string request = $"SELECT ID, Lessons, Shorts, Kurs FROM Lessons ORDER BY Lessons ASC";
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
                PutValue();
            }));

        RelayCommand? deleteLesson;
        public RelayCommand DeleteLesson => deleteLesson ??
            (deleteLesson = new RelayCommand(obj =>
            {
                DeleteValue();
            }));


        RelayCommand? addLesson;
        public RelayCommand AddLesson => addLesson ??
            (addLesson = new RelayCommand(obj =>
            {
                if (List[0].ID != -1)
                {
                    List.Insert(0, new LessonModel() { ID = -1 });
                    SelectedItem = List[0];
                } else
                    MessageBox.Show("Заполните предыдущее значение!");
            }));

        RelayCommand? addLessonToBase;
        public RelayCommand AddLessonToBase => addLessonToBase ??
            (addLessonToBase = new RelayCommand(obj =>
            {
                InsertValue(new LessonModel());
                //if (List[0].ID != -1)
                //{
                //    List.Insert(0, new LessonModel() { ID = -1 });
                //    SelectedItem = List[0];
                //}
                //else
                //    MessageBox.Show("Заполните предыдущее значение!");
            }));

        RelayCommand? saveLesson;
        public RelayCommand SaveLesson => saveLesson ??
            (saveLesson = new RelayCommand(obj =>
            {

            }));
    }
}
