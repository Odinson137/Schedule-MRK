using System.Windows;
using System.Windows.Controls;

namespace Sasha_Project.Views
{
    /// <summary>
    /// Логика взаимодействия для TableView.xaml
    /// </summary>
    public partial class TableView : UserControl
    {
        public TableView()
        {
            InitializeComponent();
        }

        private void LessonBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) { VisibilityPanel.Visibility = Visibility.Visible; }

        private void VisibilityPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) { VisibilityPanel.Visibility = Visibility.Collapsed; }
    }
}
