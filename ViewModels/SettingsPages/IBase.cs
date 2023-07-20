using Sasha_Project.Models.SettingsModels;
using Sasha_Project.ViewModels.DopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sasha_Project.ViewModels.SettingsPages
{
    interface IBase<T>
    {
        public bool SelectValues();
        public bool DeleteValue();
        public bool PutValue();
        public bool InsertValue(T newValue);
    }
}
