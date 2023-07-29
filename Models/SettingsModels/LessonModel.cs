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

        private string shorts;
        public string Shorts
        {
            get { return shorts; }
            set
            {
                shorts = value;
                OnPropertyChanged("Shorts");
            }
        }

        private bool kurs1;
        public bool Kurs1
        {
            get { return kurs1; }
            set
            {
                kurs1 = value;
                OnPropertyChanged("Kurs1");
            }
        }

        private bool kurs2;
        public bool Kurs2
        {
            get { return kurs2; }
            set
            {
                kurs2 = value;
                OnPropertyChanged("Kurs2");
            }
        }

        private bool kurs3;
        public bool Kurs3
        {
            get { return kurs3; }
            set
            {
                kurs3 = value;
                OnPropertyChanged("Kurs3");
            }
        }

        private bool kurs4;
        public bool Kurs4
        {
            get { return kurs4; }
            set
            {
                kurs4 = value;
                OnPropertyChanged("Kurs4");
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
