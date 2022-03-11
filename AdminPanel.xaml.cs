using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {

        public AdminPanel(bool isWatcher = false)
        {
            InitializeComponent();
            if(isWatcher)
            {
                GridMenu.Visibility = Visibility.Hidden;
                MainFrame.Content = new Statistics();
            }
            else
            {
                MainFrame.Content = new AdminMainPage();
            }
            MainFrame.Navigating += MainFrame_OnNavigating;
            
        }



        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите закрыть приложение?", "Предупреждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти из аккаунта?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Login f1 = new Login();
                this.Close();
                f1.Show();
            }
        }

        private void backward_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AdminMainPage();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new RegistrationPage();
        }
        private void Сhangingdata_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChangingDataPage();
        }
        private void Statistion_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Statistics();            
        }

        private void MainFrame_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.3);
            ta.DecelerationRatio = 0.7;
            ta.To = new Thickness(0, 0, 0, 0);
            if (e.NavigationMode == NavigationMode.New)
            {
                ta.From = new Thickness(500, 0, 0, 0);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(0, 0, 500, 0);
            }
                 (e.Content as Page).BeginAnimation(MarginProperty, ta);
        }
    }
}

