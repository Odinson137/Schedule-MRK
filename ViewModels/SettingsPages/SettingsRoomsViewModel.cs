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
using System.Windows.Controls;

namespace Sasha_Project.ViewModels.SettingsPages
{
    public class SettingsRoomsViewModel : SettingBaseViewModel, IBase<RoomModel>
    {
        public ObservableCollection<RoomModel> List { get; set; }

        private RoomModel selectedItem;
        public RoomModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public SettingsRoomsViewModel()
        {
            List = new ObservableCollection<RoomModel>();
            SelectValues();
        }

        public bool InsertValue(RoomModel model)
        {
            string request = $"INSERT INTO FreeRooms (ID, rooms) VALUES (@id, @value1)";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", model.ID },
                { "value1", model.Value }
            });
        }

        public bool PutValue()
        {
            string request = $"UPDATE FreeRooms SET (rooms) = (@value1) WHERE ID = @ID";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "value1", selectedItem.Value }, 
                { "ID", selectedItem.ID }
            });
        }

        private void AddToList(SQLiteDataReader reader)
        {
            List.Add(new RoomModel()
            {
                ID = reader.GetInt32(0),
                Value = reader.GetString(1)
            });
        }

        public bool SelectValues()
        {
            string request = $"SELECT * FROM FreeRooms ORDER BY rooms ASC";
            return WorkBase.SelectValues(request, AddToList);
        }

        public bool DeleteValue()
        {
            string request = $"DELETE FROM FreeRooms WHERE ID = @Id";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "Id", selectedItem.ID }
            });
        }

        RelayCommand? deleteRoom;
        public RelayCommand DeleteRoom => deleteRoom ??
            (deleteRoom = new RelayCommand(obj =>
            {
                if (DeleteValue())
                {
                    List.Remove(SelectedItem);
                    OnPropertyChanged("List");
                    OnPropertyChanged("SelectedItem");

                    SelectedItem = List[0];
                    MessageBox.Show("Удалено");
                }
            }));

        RelayCommand? addRoom;
        public RelayCommand AddRoom => addRoom ??
            (addRoom = new RelayCommand(obj =>
            {
                string text = obj as string ?? "";
                if (List.Contains(new RoomModel() { Value = text }))
                    MessageBox.Show("Уже есть");
                else
                {
                    int maxId = List.Max(x => x.ID);
                    RoomModel newRoom = new RoomModel()
                    {
                        ID = ++maxId,
                        Value = text
                    };

                    if (InsertValue(newRoom))
                    {
                        List.Insert(0, newRoom);
                        SelectedItem = List[0];

                        OnPropertyChanged("List");
                        MessageBox.Show("Добавлено");
                    }
                }
            }));

        RelayCommand? saveRoom;
        public RelayCommand SaveRoom => saveRoom ??
            (saveRoom = new RelayCommand(obj =>
            {
                selectedItem.Value = obj as string ?? "Пустое значение";
                if (PutValue())
                {
                    OnPropertyChanged("List");
                    MessageBox.Show("Сохранено");
                }
            }));
    }
}
