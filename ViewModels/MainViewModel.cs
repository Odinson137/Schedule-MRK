using Sasha_Project.Commands;

namespace Sasha_Project.ViewModels
{
    public class ViewModel : BaseViewModel
    {
        private BaseViewModel _SelectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get
            {
                return _SelectedViewModel;
            }
            set
            {
                _SelectedViewModel = value;
            }
        }

        public ViewModel()
        {
            SelectedViewModel = new TableViewModel();
        }

        RelayCommand? newPage;
        public RelayCommand NewPage => newPage ??
            (newPage = new RelayCommand(obj =>
            {
                if (SelectedViewModel is TableViewModel)
                {
                    SelectedViewModel = new SettingsViewModel();
                } else
                {
                    SelectedViewModel = new TableViewModel();
                }
                OnPropertyChanged("SelectedViewModel");
            }));
    }
}