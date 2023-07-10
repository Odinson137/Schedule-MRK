using Sasha_Project.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Sasha_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed )
            {
                DragMove();
            }
        }

        private void MiniMized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Closed_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FullWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void ReplaceMenu(object sender, RoutedEventArgs e)
        //{
        //    Replacer.SelectedIndex = 1;
        //    WorkBase a = new WorkBase("FreeRooms", "rooms");
        //    RoomsList.ItemsSource = a.SelectValues();
        //}

        //private void ReplaceTab1(object sender, RoutedEventArgs e)
        //{
        //    Replacer.SelectedIndex = 0;
        //}

        //private void DeleteRoom(object sender, MouseButtonEventArgs e)
        //{
        //    int titleRoom = int.Parse(((TextBlock)sender).ToolTip.ToString());
        //    WorkBase a = new WorkBase("FreeRooms", "rooms");
        //    a.DeleteValue(titleRoom);
        //    RoomsList.ItemsSource = a.SelectValues();
        //}
        //private void AddRoom(object sender, MouseButtonEventArgs e)
        //{
        //    string roomBox = titleToomBox.Text;
        //    if (roomBox.Contains("Введите")) return;
        //    WorkBase a = new WorkBase("FreeRooms", "rooms");
        //    a.InsertValue(roomBox);
        //    RoomsList.ItemsSource = a.SelectValues();
        //}
        //private void SelectedChangeRoom(object sender, MouseButtonEventArgs e)
        //{
        //    if (titleToomBox.Text.Contains("Введите"))
        //    {
        //        titleToomBox.Text = "";
        //    }
        //}


        //private void AddSpec(object sender, MouseButtonEventArgs e)
        //{
        //    string specBox = titleSpecBox.Text;
        //    string sequenceBox = SequenceSpecBox.Text;

        //    if (specBox.Contains("Введите")) return;
        //    WorkBase a = new WorkBase("Scep", "Spec");
        //    a.InsertValue(sequenceBox, specBox, "Spec", "Spec");
        //    //SpecList.ItemsSource = a.SelecJustValues();
        //    SpecList.ItemsSource = a.SelectValues();
        //}

        //private void DeleteSpec(object sender, MouseButtonEventArgs e)
        //{
        //    int titleSpec = int.Parse(((TextBlock)sender).ToolTip.ToString());
        //    WorkBase a = new WorkBase("Scep", "Spec");
        //    a.DeleteValue(titleSpec);
        //    //SpecList.ItemsSource = a.SelecJustValues();
        //    SpecList.ItemsSource = a.SelectValues();
        //}


        //private void ReplaceRoomTab0(object sender, RoutedEventArgs e)
        //{
        //    ReplaceRoom.SelectedIndex = 0;
        //}

        //private void ReplaceRoomTab1(object sender, RoutedEventArgs e)
        //{
        //    GroupTab a = new GroupTab("Scep", "Spec");
        //    GroupList.ItemsSource = a.SelectValues();

        //    ReplaceRoom.SelectedIndex = 1;

        //    GroupsComboBox.ItemsSource = ((WorkBase)a).SelectValues();
        //}

        //private void ReplaceRoomTab2(object sender, RoutedEventArgs e)
        //{
        //    LessonsTab a = new LessonsTab("Prepods", "Prepods");
        //    PrepodsList.ItemsSource = a.SelectValues();

        //    ReplaceRoom.SelectedIndex = 2;

        //    LessonsComboBox.ItemsSource = ((WorkBase)a).SelectValues();
        //}

        //private void ReplaceRoomTab3(object sender, RoutedEventArgs e)
        //{
        //    LessonsTab a = new LessonsTab("Scep", "Spec");
        //    SpecList.ItemsSource = a.SelectValues();
        //    ReplaceRoom.SelectedIndex = 3;
        //}

        //private void ReplaceRoomTab4(object sender, RoutedEventArgs e)
        //{
        //    LessonsTab a = new LessonsTab("Lessons", "Lessons");
        //    PredmetList.ItemsSource = a.SelectValues();
        //    ReplaceRoom.SelectedIndex = 4;
        //}

        //private void DeletePredmet(object sender, MouseButtonEventArgs e)
        //{
        //    int titleSpec = int.Parse(((TextBlock)sender).ToolTip.ToString());
        //    LessonsTab a = new LessonsTab("Lessons", "Lessons");
        //    a.DeleteValue(titleSpec);
        //    PredmetList.ItemsSource = a.SelectValues();
        //}

        //private void AddPredmet(object sender, MouseButtonEventArgs e)
        //{
        //    string specBox = titlePredmetBox.Text;
        //    if (specBox.Contains("Введите")) return;
        //    LessonsTab a = new LessonsTab("Lessons", "Lessons");
        //    a.InsertValue(specBox);
        //    PredmetList.ItemsSource = a.SelectValues();
        //}

        //private void DeletePrepod(object sender, MouseButtonEventArgs e)
        //{
        //    int titleSpec = int.Parse(((TextBlock)sender).ToolTip.ToString());
        //    LessonsTab a = new LessonsTab("Prepods", "Prepods");
        //    a.DeleteValue(titleSpec);
        //    PrepodsList.ItemsSource = a.SelectValues();
        //}

        //private void AddPrepod(object sender, MouseButtonEventArgs e)
        //{
        //    string specBox = titlePrepodBox.Text;

        //    if (specBox.Contains("Введите")) return;
        //    var selectedValue = LessonsComboBox.SelectedItem;
        //    if (selectedValue == null)
        //    {
        //        MessageBox.Show("Выберите предмет!");
        //        return;
        //    }
        //    string lesson = selectedValue?.ToString() ?? "";
        //    LessonsTab a = new LessonsTab("Prepods", "prepods");
        //    a.InsertValue(specBox, lesson, "lessons", "prepods");
        //    PrepodsList.ItemsSource = a.SelectValues();
        //}

        //private void DeleteGroup(object sender, MouseButtonEventArgs e)
        //{
        //    string titleSpec = ((TextBlock)sender).ToolTip.ToString();
        //    GroupTab a = new GroupTab("Groups", "groups");
        //    a.DeleteAllValue(titleSpec);
        //    GroupList.ItemsSource = a.SelectValues();
        //}

        //private void AddGroup(object sender, MouseButtonEventArgs e)
        //{
        //    string specBox = titleGroupBox.Text;

        //    if (specBox.Contains("Введите")) return;
        //    var selectedValue = GroupsComboBox.SelectedItem;
        //    if (selectedValue == null)
        //    {
        //        MessageBox.Show("Выберите специальность!");
        //        return;
        //    }
        //    string lesson = selectedValue?.ToString() ?? "";
        //    int distant = DistantComboBox.SelectedIndex;
        //    GroupTab a = new GroupTab("Groups", "groups");
        //    a.InsertValue(specBox, lesson, distant);
        //    GroupList.ItemsSource = a.SelectValues();
        //}


    }
}
