using Sasha_Project.Commands;
using Sasha_Project.Models.SettingsModels;
using Sasha_Project.ViewModels.DopModels;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Windows;

namespace Sasha_Project.ViewModels.SettingsPages
{
    class SettingsGroupsViewModel : SettingBaseViewModel, IBase<GroupModel>
    {
        public List<GroupModel> Groups { get; set; }
        //public ObservableCollection<PrepodModel> List { get; set; }
        //public ObservableCollection<PrepodModel> TeacherLessons { get; set; }
        //public List<LessonModel> Lessons { get; set; }

        public ICollection<string> Offices { get; set; }

        private GroupModel selectedItem;
        public GroupModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;

                OnPropertyChanged("SelectedItem");
            }
        }

        public SettingsGroupsViewModel()
        {
            Groups = new List<GroupModel>();
            SelectValues();
            //List = new ObservableCollection<GroupModel>(Groups.DistinctBy(p => p.Title).ToList());
            //SelectOffices();
        }

        public bool InsertValue(GroupModel model)
        {
            string request = $"INSERT INTO Groups (ID, groups, offices, Distants) VALUES (@id, @value1, @value2, @value3)";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", model.ID },
                { "value1", model.Title },
                { "value2", model.Office },
                { "value3", model.Dist }
            });
        }
        public bool PutValue()
        {
            string request = $"UPDATE Prepods SET (groups, offices, Distants) = (@value1, @value2, @value3) WHERE ID = @id";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", SelectedItem.ID },
                { "value1", SelectedItem.Title },
                { "value2", SelectedItem.Office },
                { "value3", SelectedItem.Dist }
            });
        }

        private void AddToList(SQLiteDataReader reader)
        {
            Groups.Add(new GroupModel()
            {
                ID = reader.GetInt32(0),
                Title = reader.GetString(1),
                Office = reader.GetString(2),
                Dist = reader.GetString(3)
            });
        }

        public bool SelectValues()
        {
            string request = $"SELECT * FROM Groups ORDER BY groups ASC";
            return WorkBase.SelectValues(request, AddToList);
        }
        private void AddToLessons(SQLiteDataReader reader)
        {
            Offices.Add(reader.GetString(0));
        }

        public bool SelectOffices()
        {
            string request = $"SELECT Spec FROM Scep";
            return WorkBase.SelectValues(request, AddToLessons);
        }

        public bool DeleteValue()
        {
            string request = $"DELETE FROM Groups WHERE groups = @prepod";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "prepod", SelectedItem.Title}
            });
        }

        //public static bool DeletePrepodsLesson(int id)
        //{
        //    string request = $"DELETE FROM Grou WHERE ID = @ID";
        //    return WorkBase.RequestValue(request, new Dictionary<string, object>()
        //    {
        //        { "ID", id}
        //    });
        //}


        RelayCommand? putGroup;
        public RelayCommand PutGroup=> putGroup ??
            (putGroup = new RelayCommand(obj =>
            {
                if (SelectedItem.Title != null && SelectedItem.Office != null && SelectedItem.Dist != null)
                {
                    if (SelectedItem.ID == -1)
                    {
                        SelectedItem.ID = Groups.Max(x => x.ID) + 1;
                        if (InsertValue(SelectedItem))
                        {
                            Groups.Add(SelectedItem);
                            MessageBox.Show("Группа добавлена");
                        }
                    }
                    else
                        if (PutValue())
                        MessageBox.Show("Данные успешно изменены");
                }
                else
                    MessageBox.Show("Поля пустые!");

            }));

        RelayCommand? deleteGroup;
        public RelayCommand DeleteGroup => deleteGroup ??
            (deleteGroup = new RelayCommand(obj =>
            {
                if (DeleteValue())
                {
                    Groups.Remove(SelectedItem);
                    SelectedItem = Groups[0];
                    MessageBox.Show("Удалено");
                }
            }));

        //RelayCommand? deleteLesson;
        //public RelayCommand DeleteLesson => deleteLesson ??
        //    (deleteLesson = new RelayCommand(obj =>
        //    {
        //        if (obj != null)
        //        {
        //            PrepodModel selectedLesson = obj as PrepodModel;
        //            if (DeletePrepodsLesson(selectedLesson.ID))
        //            {
        //                TeacherLessons.Remove(selectedLesson);
        //                MessageBox.Show("Предмет удалён");
        //            }

        //        }
        //        else
        //            MessageBox.Show("Выберите предмет!");
        //    }));

        RelayCommand? addGroup;
        public RelayCommand AddGroup => addGroup ??
            (addGroup = new RelayCommand(obj =>
            {
                if (Groups[0].ID == -1)
                {
                    MessageBox.Show("Сохраните предыдущий результат!");
                    return;
                }
                Groups.Insert(0, new GroupModel()
                {
                    ID = -1
                });

                SelectedItem = Groups[0];
            }));

        //RelayCommand? addLesson;
        //public RelayCommand AddLesson => addLesson ??
        //    (addLesson = new RelayCommand(obj =>
        //    {
        //        if (obj != null)
        //        {
        //            LessonModel lesson = (LessonModel)obj;

        //            PrepodModel newLesson = new PrepodModel()
        //            {
        //                ID = BigList.Max(x => x.ID) + 1,
        //                Lesson = lesson.Lesson,
        //                Name = SelectedItem.Name,
        //                LastName = SelectedItem.LastName
        //            };

        //            if (TeacherLessons.Select(x => x.Lesson).Contains(lesson.Lesson))
        //            {
        //                MessageBox.Show("Такой предмет уже есть");
        //                return;
        //            }

        //            if (InsertValue(newLesson))
        //            {
        //                TeacherLessons.Insert(0, newLesson);

        //                BigList.Add(newLesson);
        //                MessageBox.Show("Предмет добавлен");
        //            }
        //        }
        //        else
        //            MessageBox.Show("Выберите предмет!");
        //    }));
    }
}
