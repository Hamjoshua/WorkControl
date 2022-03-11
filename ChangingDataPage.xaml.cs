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
    /// Логика взаимодействия для ChangingDataPage.xaml
    /// </summary>
    public partial class ChangingDataPage : Page
    {
        WorkControlEntities db = new WorkControlEntities();
        Users user;
        public ChangingDataPage()
        {
            InitializeComponent();
            updateWorkWeekComboBox();
            updateRoles();
            RefreshUserToChangeComboBox();
            removeUserButton.Visibility = Visibility.Hidden;
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
            workWeekupdateComboBox.ItemsSource = weeks;
            workWeekupdateComboBox.DisplayMemberPath = "NameOfWeek";
            workWeekupdateComboBox.SelectedValuePath = "WorkWeekId";
        }

        public void RefreshUserToChangeComboBox()
        {            
            userToChangeComboBox.ItemsSource = db.Users.Select(u => new
            {
                UsersId = u.UserId,
                Name = "ФИО: " + u.SecondName + " " + u.Name + " " + u.LastName
            }).ToList();
            userToChangeComboBox.DisplayMemberPath = "Name";
            userToChangeComboBox.SelectedValuePath = "UsersId";

        }

        private void hangingdataButton_Click(object sender, RoutedEventArgs e)
        {
            string secondName = secondNameupdateTextBox.Text;
            string name = nameupdateTextBox.Text;
            string lastName = lastNameupdateTextBox.Text;

            string workPost = workPostupdateTextBox.Text;

            string login = loginNameupdateTextBox.Text;
            string password = passwordNameupdateTextBox.Text;

            bool allFieldsAreFilled = new List<string>() { secondName, name, userToChangeComboBox.Text, workWeekupdateComboBox.Text, 
                rolesComboBox.Text, lastName, workPost, login, password }.All(x => !string.IsNullOrEmpty(x));

            if (allFieldsAreFilled)
            {
                int userId = (int)userToChangeComboBox.SelectedValue;
                int roleId = (int)rolesComboBox.SelectedValue;
                int workWeekId = (int)workWeekupdateComboBox.SelectedValue;

                bool result = UserMethods.changeUser(userId, secondName, name, lastName, workPost, login,
                password, roleId, workWeekId, db);
                if (result)
                {
                    MessageBox.Show("Данные успешно обновлены в бд", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserToChangeComboBox();
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

        private void generatePasswordupdateButton_Click(object sender, RoutedEventArgs e)
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
            passwordNameupdateTextBox.Text = newPassword;
        }

        private void userToChangeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userToChangeComboBox.SelectedValue == null)
            {
                if(user == null)
                {
                    removeUserButton.Visibility = Visibility.Hidden;
                    secondNameupdateTextBox.Text = null;
                    nameupdateTextBox.Text = null;
                    lastNameupdateTextBox.Text = null;

                    workPostupdateTextBox.Text = null;

                    loginNameupdateTextBox.Text = null;
                    passwordNameupdateTextBox.Text = null;

                    rolesComboBox.SelectedValue = null;
                    workWeekupdateComboBox.SelectedValue = null;
                    return;
                }                
                userToChangeComboBox.SelectedValue = user.UserId;
            }
            else
            {
                int id = (int)userToChangeComboBox.SelectedValue;
                user = db.Users.Find(id);
            }


            secondNameupdateTextBox.Text = user.SecondName;
            nameupdateTextBox.Text = user.Name;
            lastNameupdateTextBox.Text = user.LastName;

            workPostupdateTextBox.Text = user.WorkPost;

            loginNameupdateTextBox.Text = user.Login;
            passwordNameupdateTextBox.Text = user.Password;

            rolesComboBox.SelectedValue = user.RoleId;
            workWeekupdateComboBox.SelectedValue = user.WorkWeekId;
            removeUserButton.Visibility = Visibility.Visible;
        }

        private void removeUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Внимание, удаление этого пользователя приведет к удалению его статистики." +
                " Вы хотите удалить этого пользователя?", "Предупреждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                bool result = UserMethods.removeUser(user.UserId, db);
                if(result)
                {
                    user = null;
                    RefreshUserToChangeComboBox();
                }
                else
                {
                    MessageBox.Show("Произошла ошибка на стороне сервера", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
    }
}
