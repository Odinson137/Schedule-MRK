using Microsoft.Win32;
using Sasha_Project.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sasha_Project.Views.SettingsViews
{
    /// <summary>
    /// Логика взаимодействия для SettingsScheduleView.xaml
    /// </summary>
    public partial class SettingsScheduleView : UserControl
    {
        public SettingsScheduleView()
        {
            InitializeComponent();
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"; 
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    ReadBook a = new ReadBook();
                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    a.SelectOpenendFile(fileInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
