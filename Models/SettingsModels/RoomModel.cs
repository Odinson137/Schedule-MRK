using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sasha_Project.Models.SettingsModels
{
    public class RoomModel : INotifyPropertyChanged
    {
        public int ID { get; set; }

        private string value;
        public required string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            RoomModel other = (RoomModel)obj;
            return Value == other.Value;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
