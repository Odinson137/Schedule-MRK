using Sasha_Project.Commands;
using System.Windows;

namespace Sasha_Project.ViewModels.SettingsPages
{
    public class SettingsViewModel : SettingBaseViewModel
    {
        public string Index { get; set; } = "1";
        private SettingBaseViewModel _SelectedSettingsBaseViewModel;
        public SettingBaseViewModel SelectedSettingsBaseViewModel
        {
            get
            {
                return _SelectedSettingsBaseViewModel;
            }
            set
            {
                _SelectedSettingsBaseViewModel = value;
            }
        }

        public SettingsViewModel()
        {
            //SelectedSettingsBaseViewModel = new SettingsRoomsViewModel();
            SelectedSettingsBaseViewModel = new SettingSpecialtyViewModel();
        }

        RelayCommand? newSettingsPage;
        public RelayCommand NewSettingsPage => newSettingsPage ??
            (newSettingsPage = new RelayCommand(obj =>
            {
                Index = obj as string ?? "1";
                if (Index == "1")
                    SelectedSettingsBaseViewModel = new SettingsRoomsViewModel();
                else if (Index == "2")
                    SelectedSettingsBaseViewModel = new SettingPrepodsViewModel();
                else if (Index == "3")
                    SelectedSettingsBaseViewModel = new SettingLessonsViewModel();
                else if (Index == "4")
                    SelectedSettingsBaseViewModel = new SettingSpecialtyViewModel();
                else if (Index == "5")
                    SelectedSettingsBaseViewModel = new SettingsGroupsViewModel();
                else if (Index == "6")
                    SelectedSettingsBaseViewModel = new SettingsScheduleViewModel();
                else
                    SelectedSettingsBaseViewModel = new SettingsScheduleViewModel();

                OnPropertyChanged("Index");
                OnPropertyChanged("SelectedSettingsBaseViewModel");
            }));
    }
}
