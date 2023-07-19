using Sasha_Project.Models.SettingsModels;
using Sasha_Project.ViewModels.DopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sasha_Project.ViewModels.SettingsPages
{
    interface IBase
    {
        public void SelectValues();
        public void DeleteValue(int id);
        public void PutValue(int id);
        public void InsertValue(string request);
    }
}
