using Sasha_Project.Commands;
using System.Windows;

namespace Sasha_Project.ViewModels
{
    public class SettingsViewModel : SettingBaseViewModel
    {
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
            SelectedSettingsBaseViewModel = new SettingRoomsViewModel();
        }

        RelayCommand? newSettingsPage;
        public RelayCommand NewSettingsPage => newSettingsPage ??
            (newSettingsPage = new RelayCommand(obj =>
            {
                if (SelectedSettingsBaseViewModel is SettingRoomsViewModel)
                {
                    SelectedSettingsBaseViewModel = new SettingPrepodsViewModel();
                }
                else
                {
                    SelectedSettingsBaseViewModel = new SettingRoomsViewModel();
                }
                OnPropertyChanged("SelectedSettingsBaseViewModel");
            }));
    }
}
