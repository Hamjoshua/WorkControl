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
using WorkControl.AutoUpdate;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private bool notifyBool
        {
            get { return Properties.Settings.Default.NotifyAboutUpdate; }
            set
            {
                Properties.Settings.Default.NotifyAboutUpdate = value;
                Properties.Settings.Default.Save();
            }
        }

        private bool autoRestore
        {
            get { return Properties.Settings.Default.AutoRestore; }
            set
            {
                Properties.Settings.Default.AutoRestore = value;
                Properties.Settings.Default.Save();
            }
        }

        public SettingsWindow()
        {
            InitializeComponent();
            notifyAboutUpdateChb.IsChecked = notifyBool;
            autoRestoreChb.IsChecked = autoRestore;
        }

        private void backward_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void notifyAboutUpdateChb_Checked(object sender, RoutedEventArgs e)
        {
            notifyBool = notifyAboutUpdateChb.IsChecked.Value;
        }

        private void autoRestoreChb_Checked(object sender, RoutedEventArgs e)
        {
            autoRestore = autoRestoreChb.IsChecked.Value;
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
            else
            {
                MessageBox.Show("Вы пользуетесь последней версией данной программы.", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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
            else
            {
                MessageBox.Show("Проверка прошла успешно. Все в порядке.", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void restoreBtn_Click(object sender, RoutedEventArgs e)
        {
            Restore();
        }

        private void checkForUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
