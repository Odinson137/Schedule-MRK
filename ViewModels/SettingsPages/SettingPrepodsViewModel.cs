﻿using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Sasha_Project.Commands;
using Sasha_Project.Models.SettingsModels;
using Sasha_Project.ViewModels.DopModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows;

namespace Sasha_Project.ViewModels.SettingsPages
{
    public class SettingPrepodsViewModel : SettingBaseViewModel, IBase<PrepodModel>
    {
        public List<PrepodModel> BigList { get; set; }
        public ObservableCollection<PrepodModel> List { get; set; }
        public ObservableCollection<string> TeacherLessons { get; set; }
        public List<LessonModel> Lessons { get; set; }

        private PrepodModel selectedItem;
        public PrepodModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;

                var b = Lessons.Where(x => x.ID == 0).ToList();

                var a = BigList.Where(x => x.Name == value.Name).Select(x => Lessons.Where(y => y.ID == x.LessonId && !string.IsNullOrEmpty(y.Lesson)).First()).Select(x => x.Lesson).Order().Distinct();
                TeacherLessons = new ObservableCollection<string>(a);

                OnPropertyChanged("TeacherLessons");
                OnPropertyChanged("SelectedItem");
            }
        }

        public SettingPrepodsViewModel()
        {
            BigList = new List<PrepodModel>(410);
            SelectValues();
            List = new ObservableCollection<PrepodModel>(BigList.DistinctBy(p => p.Name).ToList());
            Lessons = new List<LessonModel>() { new LessonModel() };
            SelectLessons();
        }

        public bool InsertValue(PrepodModel model)
        {
            string request = $"INSERT INTO Prepods (ID, LessonId, Prepods, DopNamePrepods) VALUES (@id, @value1, @value2, @value3)";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", model.ID },
                { "value1", model.LessonId },
                { "value2", model.Name },
                { "value3", model.LastName }
            });
        }
        public bool PutValue()
        {
            string request = $"UPDATE Prepods SET (LessonId, Prepods, DopNamePrepods) = (@value1, @value2, @value3) WHERE ID = @id";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "id", SelectedItem.ID },
                { "value1", SelectedItem.LessonId },
                { "value2", SelectedItem.Name },
                { "value3", SelectedItem.LastName }
            });
        }

        private static string Nulling(SQLiteDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
            {
                return reader.GetString(index);
            }
            else return null;
        }

        private void AddToList(SQLiteDataReader reader)
        {
            BigList.Add(new PrepodModel()
            {
                ID = reader.GetInt32(0),
                Lesson = Nulling(reader, 1),
                LessonId = reader.GetInt32(4),
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
            string request = $"DELETE FROM Prepods WHERE Prepods = @prepod";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "prepod", SelectedItem.Name}
            });
        }

        public static bool DeletePrepodsLesson(int id)
        {
            string request = $"DELETE FROM Prepods WHERE ID = @ID";
            return WorkBase.RequestValue(request, new Dictionary<string, object>()
            {
                { "ID", id}
            });
        }


        RelayCommand? putPrepod;
        public RelayCommand PutLesson => putPrepod ??
            (putPrepod = new RelayCommand(obj =>
            {
                if (SelectedItem.Name != null && SelectedItem.LastName != null)
                {
                    if (SelectedItem.ID == -1)
                    {
                        SelectedItem.ID = BigList.Max(x => x.ID) + 1;
                        if (InsertValue(SelectedItem))
                        {
                            BigList.Add(SelectedItem);
                            MessageBox.Show("Преподаватель добавлен");
                        }
                    }
                    else
                        if (PutValue())
                            MessageBox.Show("Данные успешно изменены");
                } else
                    MessageBox.Show("Поля пустые!");

            }));

        RelayCommand? deletePrepod;
        public RelayCommand DeletePrepod => deletePrepod ??
            (deletePrepod = new RelayCommand(obj =>
            {
                if (DeleteValue())
                {
                    List.Remove(SelectedItem);
                    SelectedItem = List[0];
                    MessageBox.Show("Удалено");
                }
            }));

        RelayCommand? deleteLesson;
        public RelayCommand DeleteLesson => deleteLesson ??
            (deleteLesson = new RelayCommand(obj =>
            {
                if (obj != null)
                {
                    string selectedLesson = obj as string;
                    int id = Lessons.Where(x => x.Lesson == selectedLesson).Select(x => x.ID).First();
                    int needId = BigList.Where(x => x.LessonId == id).Select(x => x.ID).First();
                    if (DeletePrepodsLesson(needId))
                    {
                        TeacherLessons.Remove(selectedLesson);
                        MessageBox.Show("Предмет удалён");
                    }

                } else
                    MessageBox.Show("Выберите предмет!");
            }));

        RelayCommand? addPrepod;
        public RelayCommand AddPrepod => addPrepod ??
            (addPrepod = new RelayCommand(obj =>
            {
                if (List[0].ID == -1)
                {
                    MessageBox.Show("Сохраните предыдущий результат!");
                    return;
                } 
                List.Insert(0, new PrepodModel()
                {
                    ID = -1
                });

                SelectedItem = List[0];
            }));

        RelayCommand? addLesson;
        public RelayCommand AddLesson => addLesson ??
            (addLesson = new RelayCommand(obj =>
            {
                if (obj != null)
                {
                    LessonModel lesson = (LessonModel)obj;

                    PrepodModel newLesson = new PrepodModel()
                    {
                        ID = BigList.Max(x => x.ID) + 1,
                        LessonId = Lessons.Where(x => x.Lesson == lesson.Lesson).Select(x => x.ID).First(),
                        Name = SelectedItem.Name,
                        LastName = SelectedItem.LastName
                    };

                    if (TeacherLessons.Contains(lesson.Lesson))
                    {
                        MessageBox.Show("Такой предмет уже есть");
                        return;
                    }

                    if (InsertValue(newLesson))
                    {
                        TeacherLessons.Insert(0, lesson.Lesson); // asdffffffffffffffffff

                        BigList.Add(newLesson);
                        MessageBox.Show("Предмет добавлен");
                    } 
                }
                else
                    MessageBox.Show("Выберите предмет!");
            }));
    }
}
