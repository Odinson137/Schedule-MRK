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
    public class SettingsRoomsViewModel : SettingBaseViewModel, IBase
    {
        public ObservableCollection<RoomModel> List { get; set; }

        private string selectedModel;
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
                selectedModel = value.Value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public SettingsRoomsViewModel()
        {
            List = new ObservableCollection<RoomModel>();
            SelectValues();
        }

        public void DeleteValue(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertValue()
        {
            string request = $"INSERT INTO FreeRooms (rooms) VALUES (@value1)";

            WorkBase.InsertValue(request);
        }

        public void PutValue(int id, string value)
        {
            WorkBase.PutValue(request);
        }

        private void AddToList(SQLiteDataReader reader)
        {
            List.Add(new RoomModel()
            {
                ID = reader.GetInt32(0),
                Value = reader.GetString(1)
            });
        }

        public void SelectValues()
        {
            string request = $"SELECT * FROM FreeRooms ORDER BY rooms ASC";
            WorkBase.SelectValues(request, AddToList);
        }

        RelayCommand? deleteRoom;
        public RelayCommand DeleteRoom => deleteRoom ??
            (deleteRoom = new RelayCommand(obj =>
            {
                List.Remove(SelectedItem);
                OnPropertyChanged("List");
                OnPropertyChanged("SelectedItem");
            }));

        RelayCommand? addRoom;
        public RelayCommand AddRoom => addRoom ??
            (addRoom = new RelayCommand(obj =>
            {
                if (List.Contains(new RoomModel() { Value = selectedModel }))
                    MessageBox.Show("Уже есть");
                else
                {
                    List.Insert(0, new RoomModel()
                    {
                        ID = -1,
                        Value = SelectedItem.Value
                    });
                    SelectedItem.Value = selectedModel;
                    SelectedItem = List[0];

                    PutValue(SelectedItem.Value);

                    OnPropertyChanged("List");

                    MessageBox.Show("Add");
                }
            }));

        RelayCommand? saveRoom;
        public RelayCommand SaveRoom => saveRoom ??
            (saveRoom = new RelayCommand(obj =>
            {


                OnPropertyChanged("List");
                MessageBox.Show("Save");
            }));
    }
}
