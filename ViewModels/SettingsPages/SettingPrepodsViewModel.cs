using Sasha_Project.Commands;
using Sasha_Project.Models.SettingsModels;
using Sasha_Project.ViewModels.DopModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Windows;

namespace Sasha_Project.ViewModels.SettingsPages
{
    public class SettingPrepodsViewModel : SettingBaseViewModel, IBase<PrepodModel>
    {
        public List<PrepodModel> BigList { get; set; }
        public List<PrepodModel> List { get; set; }
        public List<string> TeacherLessons { get; set; }
        public List<LessonModel> Lessons { get; set; }

        private PrepodModel selectedItem;
        public PrepodModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;

                TeacherLessons = BigList.Where(x => x.Name == value.Name).Select(x => x.Lesson).Order().Distinct().ToList();

                OnPropertyChanged("TeacherLessons");
                OnPropertyChanged("SelectedItem");
            }
        }

        public SettingPrepodsViewModel()
        {
            BigList = new List<PrepodModel>(410);
            SelectValues();
            List = BigList.DistinctBy(p => p.Name).ToList();
            Lessons = new List<LessonModel>() { new LessonModel() };
            SelectLessons();
        }

        public bool InsertValue(PrepodModel model)
        {
            //string request = $"INSERT INTO FreeRooms (ID, rooms) VALUES (@id, @value1)";
            //return WorkBase.RequestValue(request, new Dictionary<string, object>()
            //{
            //    { "id", model.ID },
            //    { "value1", model.Value }
            //});
            return false;
        }

        public bool PutValue()
        {
            //string request = $"UPDATE FreeRooms SET (rooms) = (@value1) WHERE ID = @ID";
            //return WorkBase.RequestValue(request, new Dictionary<string, object>()
            //{
            //    { "value1", selectedItem.Value },
            //    { "ID", selectedItem.ID }
            //});
            return false;
        }

        private void AddToList(SQLiteDataReader reader)
        {
            BigList.Add(new PrepodModel()
            {
                ID = reader.GetInt32(0),
                Lesson = reader.GetString(1),
                Name = reader.GetString(2),
                LastName = reader.GetString(3)
            });
        }

        public bool SelectValues()
        {
            string request = $"SELECT * FROM Prepods ORDER BY Prepods ASC";
            return WorkBase.SelectValues(request, AddToList);
        }


        private void AddToLessons(SQLiteDataReader reader)
        {
            Lessons.Add(new LessonModel()
            {
                ID = reader.GetInt32(0),
                Lesson = reader.GetString(1)
            });
        }
        public bool SelectLessons()
        {
            string request = $"SELECT ID, Lessons FROM Lessons ORDER BY Lessons ASC";
            return WorkBase.SelectValues(request, AddToLessons);
        }

        public bool DeleteValue()
        {
            //string request = $"DELETE FROM FreeRooms WHERE ID = @Id";
            //return WorkBase.RequestValue(request, new Dictionary<string, object>()
            //{
            //    { "Id", selectedItem.ID }
            //});
            return false;
        }

        RelayCommand? deletePrepod;
        public RelayCommand DeletePrepod => deletePrepod ??
            (deletePrepod = new RelayCommand(obj =>
            {
                List.Remove(SelectedItem);
                OnPropertyChanged("List");
                SelectedItem = List[0];
                //if (DeleteValue())
                //{
                //    List.Remove(SelectedItem);
                //    OnPropertyChanged("List");
                //    OnPropertyChanged("SelectedItem");

                //    SelectedItem = List[0];
                //    MessageBox.Show("Удалено");
                //}
            }));

        RelayCommand? addPrepod;
        public RelayCommand AddPrepod => addPrepod ??
            (addPrepod = new RelayCommand(obj =>
            {
                
                //BigList.Insert(0, new PrepodModel()
                //{
                //    ID = -1
                //});

                List.Insert(0, new PrepodModel()
                {
                    ID = -1
                });

                OnPropertyChanged("List");
                //OnPropertyChanged("BigList");

                SelectedItem = List[0];

                //string text = obj as string ?? "";
                //if (List.Contains(new RoomModel() { Value = text }))
                //    MessageBox.Show("Уже есть");
                //else
                //{
                //    int maxId = List.Max(x => x.ID);
                //    RoomModel newRoom = new RoomModel()
                //    {
                //        ID = ++maxId,
                //        Value = text
                //    };

                //    if (InsertValue(newRoom))
                //    {
                //        List.Insert(0, newRoom);
                //        SelectedItem = List[0];

                //        OnPropertyChanged("List");
                //        MessageBox.Show("Добавлено");
                //    }
                //}
            }));

        //RelayCommand? saveRoom;
        //public RelayCommand SaveRoom => saveRoom ??
        //    (saveRoom = new RelayCommand(obj =>
        //    {
        //        //selectedItem.Value = obj as string ?? "Пустое значение";
        //        //if (PutValue())
        //        //{
        //        //    OnPropertyChanged("List");
        //        //    MessageBox.Show("Сохранено");
        //        //}
        //    }));
    }
}
