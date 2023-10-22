using DocumentFormat.OpenXml.EMMA;
using Sasha_Project.Commands;
using Sasha_Project.Models.SettingsModels;
using Sasha_Project.ViewModels.DopModels;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows;

namespace Sasha_Project.ViewModels.SettingsPages
{
    class SettingsGroupsViewModel : SettingBaseViewModel, IBase<GroupModel>
    {
        public List<GroupModel> Groups { get; set; }

        //public ICollection<string> Offices { get; set; }

        public ICollection<int> Days { get; set; } = new List<int>()
        {
            1, 2, 3, 4, 5, 6, 7
        };

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
            //Offices = new ObservableCollection<string>();
            //SelectOffices();
        }

        public bool InsertValue(GroupModel model)
        {
            string request = $"INSERT INTO Groups (ID, groups, Page, Numeric) VALUES (@id, @value1, @value2, @value3)";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", model.ID },
                { "value1", model.Title },
                { "value2", model.Page },
                { "value3" , model.Numeric },
            });
        }

        public bool InsertValueToTable(string group, string week, int i, int a)
        {
            string request = $"INSERT INTO Tables (groups, week, para, days) VALUES (@value1, @value2, @value3, @value4)";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "value1", group },
                { "value2", week },
                { "value3" , a },
                { "value4" , i },
            });
        }

        public bool PutValue()
        {
            string request = $"UPDATE Groups SET (groups, Page, Numeric) = (@value1, @value2, @value3) WHERE ID = @id";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", SelectedItem.ID },
                { "value1", SelectedItem.Title },
                { "value2", SelectedItem.Page },
                { "value3", SelectedItem.Numeric },
            });
        }

        private void AddToList(SQLiteDataReader reader)
        {
            Groups.Add(new GroupModel()
            {
                ID = reader.GetInt32(0),
                Title = reader.GetString(1),
                Page = reader.GetInt32(4),
                Numeric = reader.GetInt32(5)
            });
        }

        public bool SelectValues()
        {
            string request = $"SELECT * FROM Groups ORDER BY Page ASC";
            return WorkBase.SelectValues(request, AddToList);
        }
        //private void AddToLessons(SQLiteDataReader reader)
        //{
        //    Offices.Add(reader.GetString(0));
        //}

        //public bool SelectOffices()
        //{
        //    string request = $"SELECT Spec FROM Scep";
        //    return WorkBase.SelectValues(request, AddToLessons);
        //}

        public bool DeleteValue()
        {
            string request = $"DELETE FROM Groups WHERE groups = @group";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "group", SelectedItem.Title}
            });
        }

        public bool DeleteValueFromTable()
        {
            string request = $"DELETE FROM Tables WHERE groups = @group";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "group", SelectedItem.Title}
            });
        }

        RelayCommand? putGroup;
        public RelayCommand PutGroup=> putGroup ??
            (putGroup = new RelayCommand(obj =>
            {
                if (SelectedItem.Title != null && SelectedItem.Page != null && SelectedItem.Numeric != null)
                {
                    if (SelectedItem.ID == -1)
                    {
                        SelectedItem.ID = Groups.Max(x => x.ID) + 1;
                        if (InsertValue(SelectedItem))
                        {
                            Groups.Add(SelectedItem);

                            string[] mas = new string[] { "ч", "з" };
                            foreach (string week in mas)
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    for (int a = 0; a < 7; i++)
                                    {
                                        if (!InsertValueToTable(SelectedItem.Title, week, i, a))
                                        {
                                            MessageBox.Show("Произошла ошибка при добавление данных в основую группу");
                                        }
                                    }
                                }

                            }

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

                    if (DeleteValueFromTable())
                    {
                        MessageBox.Show("Произошла ошибка при удалении группы из основной таблицы");
                    }
                    MessageBox.Show("Удалено");
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при удалении группы из данной таблицы");
                }
            }));

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

    }
}
