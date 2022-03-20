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
using WorkControl.AutoUpdate;
using System.Net.Mail;
using System.Net;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {        
        public Login()
        {
            Updater.GitHubRepo = "/Hamjoshua/WorkControl";
            string[] args = new string[] { };
            if(Properties.Settings.Default.AutoRestore)
                Restore();
            if(Properties.Settings.Default.NotifyAboutUpdate)
                Update();
            InitializeComponent();         
        }       

        private void testMail()
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("biryukov-10@list.ru", "Mark");
            // кому отправляем
            MailAddress to = new MailAddress("biryukov-10@list.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("biryukov-10@list.ru", "vHEEUj9aLs6LRmQez1sX");
            smtp.EnableSsl = true;
            smtp.Send(m);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string loginText = login.Text;
            string passwordText = password.Password;
            throw new Exception();

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
        
        private void Update()
        {
            if (Updater.HasUpdate)
            {
                string currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                if (MessageBox.Show($"Доступно обновление.\nНовая версия:{Updater.LatestVersion}" +
                    $"\nТекущая версия: {currentVersion}\nХотите установить?", "Внимание",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Updater.Update();
                    Application.Current.Shutdown();
                }
            }
            Updater.cleanUpdateFiles();
        }

        private void Restore()
        {
            if (Updater.CheckForRestore())
            {
                if (MessageBox.Show("Отсутствуют некоторые файлы, это может повлять на работу программы." +
                    "Сейчас будет запущено восстановление", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Updater.Restore();
                    Application.Current.Shutdown();
                }
            }            
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}
