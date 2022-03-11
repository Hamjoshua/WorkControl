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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkControl.ModelMethods;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string loginText = login.Text;
            string passwordText = password.Password;

            if (string.IsNullOrEmpty(loginText) || string.IsNullOrEmpty(passwordText))
            {
                MessageBox.Show("Вы ввели не все данные, заполните все поля", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Users user = UserMethods.getUserFromLoginData(loginText, passwordText, new WorkControlEntities());
                if (user != null)
                {                    
                    if (user.Roles.Name == "Сотрудник")
                    {
                        if (TimeProfileMethods.isHoliday(TimeProfileMethods.getTimeProfileFromWorkWeek(user.WorkWeeks)))
                        {
                            MessageBox.Show("У вас сегодня выходной, работы не запланировао", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();

                    }
                    else if (user.Roles.Name == "Наблюдатель")
                    {
                        AdminPanel adminPanel = new AdminPanel(isWatcher: true);
                        adminPanel.Show();
                    }
                    else
                    {
                        AdminPanel adminPanel = new AdminPanel();
                        adminPanel.Show();
                    }

                    MessageBox.Show("Успешная авторизация!", "Поздравляем!",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    this.Close();

                }
                else
                {
                    MessageBox.Show("Вы ввели неверные данные, проверьте правильность ввода данных и повторите попытку ", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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



        private void backward_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
