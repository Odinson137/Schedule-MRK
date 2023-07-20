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

namespace Sasha_Project.ViewModels.SettingsPages
{
    class SettingLessonsViewModel : SettingBaseViewModel, IBase<LessonModel>
    {
        public bool DeleteValue()
        {
            throw new NotImplementedException();
        }

        public bool InsertValue(LessonModel newValue)
        {
            throw new NotImplementedException();
        }

        public bool PutValue()
        {
            throw new NotImplementedException();
        }

        public bool SelectValues()
        {
            throw new NotImplementedException();
        }
    }
}
