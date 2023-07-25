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

        private PrepodModel selectedItem;
        public PrepodModel SelectedItem
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

        public bool SelectValues()
        {
            throw new NotImplementedException();
        }

        RelayCommand? putLesson;
        public RelayCommand PutLesson => putLesson ??
            (putLesson = new RelayCommand(obj =>
            {

            }));

        RelayCommand? deleteLesson;
        public RelayCommand DeleteLesson => deleteLesson ??
            (deleteLesson = new RelayCommand(obj =>
            {
                
            }));


        RelayCommand? addLesson;
        public RelayCommand AddLesson => addLesson ??
            (addLesson = new RelayCommand(obj =>
            {
                List.Add(new LessonModel() { ID = -1 });
            }));

        RelayCommand? saveLesson;
        public RelayCommand SaveLesson => saveLesson ??
            (saveLesson = new RelayCommand(obj =>
            {

            }));
    }
}
