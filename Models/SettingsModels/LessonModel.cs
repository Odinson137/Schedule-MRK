using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sasha_Project.Models.SettingsModels
{
    public class LessonModel : IModel, INotifyPropertyChanged
    {
        public int ID { get; set; }

        private string lesson;
        public string Lesson
        {
            get { return lesson; }
            set 
            { 
                lesson = value;
                OnPropertyChanged("Lesson");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
