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
    public class SettingSpecialtyViewModel : SettingBaseViewModel, IBase<SpecialityModel>
    {
        public ObservableCollection<SpecialityModel> List { get; set; }

        private SpecialityModel selectedItem;
        public SpecialityModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public SettingSpecialtyViewModel()
        {
            List = new ObservableCollection<SpecialityModel>();
            SelectValues();
            SelectedItem = List[0];
        }

        private void AddToList(SQLiteDataReader reader)
        {
            List.Add(new SpecialityModel()
            {
                ID = reader.GetInt32(0),
                SpecTitle = reader.GetString(1),
                //ExcelPage = reader.GetInt32(2),
                //Code = reader.GetInt32(3)
            });
        }

        public bool SelectValues()
        {
            string request = "SELECT * FROM Scep";
            return WorkBase.SelectValues(request, AddToList);
        }

        public bool DeleteValue()
        {
            throw new NotImplementedException();
        }

        public bool PutValue()
        {
            throw new NotImplementedException();
        }

        public bool InsertValue(SpecialityModel newValue)
        {
            throw new NotImplementedException();
        }

        RelayCommand? putSpec;
        public RelayCommand PutSpec => putSpec ??
            (putSpec = new RelayCommand(obj =>
            {
                if (SelectedItem.SpecTitle != null)
                {
                    if (SelectedItem.ID == -1)
                    {
                        SelectedItem.ID = List.Max(x => x.ID) + 1;
                        if (InsertValue(SelectedItem))
                            MessageBox.Show("Специальность добавлена добавлен");
                    }
                    else
                        if (PutValue())
                            MessageBox.Show("Данные успешно изменены");
                }
                else
                    MessageBox.Show("Поля пустые!");
            }));

        RelayCommand? deleteSpec;
        public RelayCommand DeleteSpec => deleteSpec ??
            (deleteSpec = new RelayCommand(obj =>
            {
                if (DeleteValue())
                {
                    MessageBox.Show("Удалено");
                    List.Remove(SelectedItem);
                    SelectedItem = List[0];
                }
            }));


        RelayCommand? addSpec;
        public RelayCommand AddSpec => addSpec ??
            (addSpec = new RelayCommand(obj =>
            {
                if (List[0].ID != -1)
                {
                    List.Insert(0, new SpecialityModel() { ID = -1, SpecTitle = "" });
                    SelectedItem = List[0];
                }
                else
                    MessageBox.Show("Заполните предыдущее значение!");
            }));
    }
}
