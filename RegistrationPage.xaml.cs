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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkControl.ModelMethods;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public WorkControlEntities db = new WorkControlEntities();
        public RegistrationPage()
        {
            InitializeComponent();
            updateRoles();
            updateWorkWeekComboBox();
        }

        public void updateRoles()
        {
            rolesComboBox.ItemsSource = db.Roles.ToList();
            rolesComboBox.DisplayMemberPath = "Name";
            rolesComboBox.SelectedValuePath = "RoleId";
        }

        public void updateWorkWeekComboBox()
        {
            var weeks = db.WorkWeeks.ToList();
            workWeekComboBox.ItemsSource = weeks;

            workWeekComboBox.DisplayMemberPath = "NameOfWeek";
            workWeekComboBox.SelectedValuePath = "WorkWeekId";
        }

        private void generatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 8; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            string newPassword = builder.ToString();
            passwordNameTextBox.Text = newPassword;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string secondName = secondNameTextBox.Text;
            string name = nameTextBox.Text;
            string lastName = lastNameTextBox.Text;

            string workPost = workPostTextBox.Text;

            string login = loginNameTextBox.Text;
            string password = passwordNameTextBox.Text;

            bool allFieldsAreFilled = new List<string>() { secondName, name, workWeekComboBox.Text, rolesComboBox.Text, lastName,
                workPost, login, password }.All(x => !string.IsNullOrEmpty(x));


            if (allFieldsAreFilled)
            {
                int roleId = (int)rolesComboBox.SelectedValue;
                int workWeekId = (int)workWeekComboBox.SelectedValue;

                bool result = UserMethods.registerUser(secondName, name, lastName, workPost, login, password, roleId, workWeekId, new WorkControlEntities());
                if (result)
                {
                    MessageBox.Show("Данные успешно добавлены в бд", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка на стороне сервера", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
