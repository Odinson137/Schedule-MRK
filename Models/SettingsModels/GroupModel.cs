using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sasha_Project.Models.SettingsModels
{
    public class GroupModel : INotifyPropertyChanged, IModel
    {
        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }

        //private string office;
        //public string Office
        //{
        //    get
        //    {
        //        return office;
        //    }
        //    set
        //    {
        //        office = value;
        //        OnPropertyChanged("Office");
        //    }
        //}

        private int page;
        public int Page
        {
            get
            {
                return page;
            }
            set
            {
                page = value;
                OnPropertyChanged("Page");
            }
        }

        private int numeric;
        public int Numeric
        {
            get
            {
                return numeric;
            }
            set
            {
                numeric = value;
                OnPropertyChanged("Numeric");
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