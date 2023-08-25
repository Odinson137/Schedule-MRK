using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sasha_Project.Models.SettingsModels
{
    public class SpecialityModel : INotifyPropertyChanged, IModel
    {
        public int ID { get; set; }

        private string specTitle;
        public string SpecTitle
        {
            get => specTitle;
            set
            {
                specTitle = value;
                OnPropertyChanged("SpecTitle");
            }
        }

        private string excelPage;
        public string ExcelPage
        {
            get => excelPage;
            set
            {
                excelPage = value;
                OnPropertyChanged("ExcelPage");
            }
        }

        private string departmentName;
        public string DepartmentName
        {
            get => departmentName;
            set
            {
                departmentName = value;
                OnPropertyChanged("DepartmentName");
            }
        }

        private string code;
        public string Code
        {
            get => code;
            set
            {
                code = value;
                OnPropertyChanged("Code");
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
