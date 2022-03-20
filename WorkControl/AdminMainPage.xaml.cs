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
    /// Логика взаимодействия для AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        bool newWorkWeekCreating;
        bool newTimeProfileCreating;

        private WorkControlEntities db = new WorkControlEntities();
        public AdminMainPage()
        {
            InitializeComponent();
            RefreshWorkWeeks();
            RefreshTimeProfilesComboBox();
            removeWorkWeekButton.Visibility = Visibility.Hidden;
            removeTimeProfileButton.Visibility = Visibility.Hidden;
        }

        public void RefreshWorkWeeks()
        {
            var workWeeks = db.WorkWeeks.ToList();

            workWeekComboBox.ItemsSource = workWeeks;
            workWeekComboBox.DisplayMemberPath = "NameOfWeek";
            workWeekComboBox.SelectedValuePath = "WorkWeekId";
        }

        public void RefreshMondayDays()
        {
            var timeProfiles = db.TimeProfiles.ToList();
            pn_workWeekComboBox.ItemsSource = timeProfiles;
            pn_workWeekComboBox.DisplayMemberPath = "NameOfProfile";
            pn_workWeekComboBox.SelectedValuePath = "TimeProfileId";

            vt_workWeekComboBox.ItemsSource = timeProfiles;
            vt_workWeekComboBox.DisplayMemberPath = "NameOfProfile";
            vt_workWeekComboBox.SelectedValuePath = "TimeProfileId";

            sr_workWeekComboBox.ItemsSource = timeProfiles;
            sr_workWeekComboBox.DisplayMemberPath = "NameOfProfile";
            sr_workWeekComboBox.SelectedValuePath = "TimeProfileId";

            cht_workWeekComboBox.ItemsSource = timeProfiles;
            cht_workWeekComboBox.DisplayMemberPath = "NameOfProfile";
            cht_workWeekComboBox.SelectedValuePath = "TimeProfileId";

            pt_workWeekComboBox.ItemsSource = timeProfiles;
            pt_workWeekComboBox.DisplayMemberPath = "NameOfProfile";
            pt_workWeekComboBox.SelectedValuePath = "TimeProfileId";

            sb_workWeekComboBox.ItemsSource = timeProfiles;
            sb_workWeekComboBox.DisplayMemberPath = "NameOfProfile";
            sb_workWeekComboBox.SelectedValuePath = "TimeProfileId";

            vs_workWeekComboBox.ItemsSource = timeProfiles;
            vs_workWeekComboBox.DisplayMemberPath = "NameOfProfile";
            vs_workWeekComboBox.SelectedValuePath = "TimeProfileId";
        }

        public void RefreshTimeProfilesComboBox()
        {
            var timeProfiles = db.TimeProfiles.ToList();
            timeProfilesComboBox.ItemsSource = timeProfiles;
            timeProfilesComboBox.DisplayMemberPath = "NameOfProfile";
            timeProfilesComboBox.SelectedValuePath = "TimeProfileId";
            RefreshMondayDays();
        }

        bool TimeProfileIdExisting(int tp_id)
        {
            return db.TimeProfiles.Select(t => t.TimeProfileId).ToList().Contains(tp_id);
        }

        bool WorkWeekIdExisting(int tp_id)
        {
            return db.WorkWeeks.Select(t => t.WorkWeekId).ToList().Contains(tp_id);
        }

        private void removeWorkWeekButton_Click(object sender, RoutedEventArgs e)
        {
            if(workWeekComboBox.SelectedValue != null)
            {
                int ww_id = (int)workWeekComboBox.SelectedValue;
                List<string> relations = WorkWeekMethods.getWorkWeekRelations(ww_id, db);
                if(relations.Count > 0)
                {
                    if (MessageBox.Show("Эта рабочая неделя связана с некоторыми пользователями:\n\n" +
                        string.Join("\n", relations) +
                        "\n\nВсе ссылки на эту рабочую неделю будут заменены нулевым значением." +
                        " Вы хотите удалить эту рабочую неделю?", "Предупреждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    {
                        return;
                    };
                }
                WorkWeekMethods.removeWorkWeek(ww_id, db);
                RefreshWorkWeeks();
            }
        }

        private void removeTimeProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (timeProfilesComboBox.SelectedValue != null)
            {
                int tp_id = (int)timeProfilesComboBox.SelectedValue;
                List<string> relations = TimeProfileMethods.getTimeProfileRelations(tp_id, db);
                if (relations.Count > 0)
                {
                    if (MessageBox.Show("Этот временной профиль связан с некоторыми рабочими неделями:\n\n" +
                        string.Join("\n", relations) +
                        "\n\nВсе ссылки на этот временной профиль будут заменены нулевым значением." +
                        " Вы хотите удалить этот временной профиль?", "Предупреждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    {
                        return;
                    };
                }                
                RefreshTimeProfilesComboBox();
            }
        }

        private void saveWorkWeekButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> mondayDays = new List<object>()
            {
                pn_workWeekComboBox.SelectedValue,
                vt_workWeekComboBox.SelectedValue,
                sr_workWeekComboBox.SelectedValue,
                cht_workWeekComboBox.SelectedValue,
                pt_workWeekComboBox.SelectedValue,
                sb_workWeekComboBox.SelectedValue,
                vs_workWeekComboBox.SelectedValue
            };
            bool allFieldsFilled = mondayDays.All(t => t != null) && workWeekComboBox.SelectedValue != null && !string.IsNullOrEmpty(nameOfWorkWeek.Text);
            if (allFieldsFilled)
            {
                string name = nameOfWorkWeek.Text;

                int workWeekId = (int)workWeekComboBox.SelectedValue;
                List<int> mondayDaysInt = mondayDays.Select(t => Convert.ToInt32(t)).ToList();
                bool result = WorkWeekMethods.saveAndCreateWorkWeek(workWeekId, mondayDaysInt, name, db);

                if (result)
                {
                    RefreshWorkWeeks();
                    workWeekComboBox.SelectedValue = workWeekId;
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
                MessageBox.Show("Не все поля заполнены, невозможно сохранить выбранную неделю", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void newWorkWeekButton_Click(object sender, RoutedEventArgs e)
        {
            if (!newWorkWeekCreating)
            {
                newWorkWeekCreating = true;
                newWorkWeekButton.Content = "Отменить создание";

                WorkWeeks ww = new WorkWeeks();
                ww.WorkWeekId = db.WorkWeeks.OrderByDescending(w => w.WorkWeekId).First().WorkWeekId + 1;
                ww.NameOfWeek = "Новая рабочая неделя";
                workWeekComboBox.ItemsSource = new List<WorkWeeks>() { ww };
                workWeekComboBox.SelectedIndex = 0;
            }
            else
            {
                newWorkWeekCreating = false;
                newWorkWeekButton.Content = "Новая неделя";

                RefreshWorkWeeks();
            }
        }

        private void generatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (timeProfilesComboBox.SelectedValue != null)
            {
                int tp_id = (int)timeProfilesComboBox.SelectedValue;

                bool result = TimeProfileMethods.createAndSaveTimeProfile(tp_id, nameOfProfileTextBox.Text,
                                                              lanchTimePicker.SelectedTime.Value.TimeOfDay, startWorkTimePicker.SelectedTime.Value.TimeOfDay,
                                                              endWorkTimePicker.SelectedTime.Value.TimeOfDay, db);
                if (result)
                {
                    RefreshTimeProfilesComboBox();
                    timeProfilesComboBox.SelectedValue = tp_id;
                    MessageBox.Show("Данные успешно добавлены в бд", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка на стороне сервера", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void newTimeProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!newTimeProfileCreating)
            {
                newTimeProfileCreating = true;
                newTimeProfileButton.Content = "Отменить создание";

                var tp = new TimeProfiles();
                tp.TimeProfileId = -1;
                tp.NameOfProfile = "Новый профиль";

                timeProfilesComboBox.ItemsSource = new List<TimeProfiles>() { tp };
                timeProfilesComboBox.DisplayMemberPath = "NameOfProfile";
                timeProfilesComboBox.SelectedValuePath = "TimeProfileId";
                timeProfilesComboBox.SelectedIndex = 0;
            }
            else
            {
                newTimeProfileCreating = false;
                newTimeProfileButton.Content = "Новый профиль";
                RefreshTimeProfilesComboBox();
            }
        }

        private void timeProfilesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (timeProfilesComboBox.SelectedValue != null)
            {
                int tp_id = (int)timeProfilesComboBox.SelectedValue;
                if (TimeProfileIdExisting(tp_id))
                {
                    TimeProfiles tp = db.TimeProfiles.Find(tp_id);

                    nameOfProfileTextBox.Text = tp.NameOfProfile;
                    endWorkTimePicker.SelectedTime = DateTime.Today + tp.EndWorkTime;
                    startWorkTimePicker.SelectedTime = DateTime.Today + tp.StartWorkTime;
                    lanchTimePicker.SelectedTime = DateTime.Today + tp.LanchTime;
                    removeTimeProfileButton.Visibility = Visibility.Visible;
                    return;
                }
            }
            nameOfProfileTextBox.Text = null;
            endWorkTimePicker.SelectedTime = null;
            startWorkTimePicker.SelectedTime = null;
            lanchTimePicker.SelectedTime = null;
            removeTimeProfileButton.Visibility = Visibility.Hidden;
        }

        private void workWeekComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (workWeekComboBox.SelectedValue != null)
            {
                int ww_id = (int)workWeekComboBox.SelectedValue;
                if (WorkWeekIdExisting(ww_id))
                {
                    WorkWeeks ww = db.WorkWeeks.Find(ww_id);

                    nameOfWorkWeek.Text = ww.NameOfWeek;

                    pn_workWeekComboBox.SelectedValue = ww.Monday;
                    vt_workWeekComboBox.SelectedValue = ww.Tuesday;
                    sr_workWeekComboBox.SelectedValue = ww.Wednesday;
                    cht_workWeekComboBox.SelectedValue = ww.Thursday;
                    pt_workWeekComboBox.SelectedValue = ww.Friday;
                    sb_workWeekComboBox.SelectedValue = ww.Saturday;
                    vs_workWeekComboBox.SelectedValue = ww.Sunday;
                    removeWorkWeekButton.Visibility = Visibility.Visible;
                    return;
                }                
            }
            nameOfWorkWeek.Text = null;

            pn_workWeekComboBox.SelectedValue = null;
            vt_workWeekComboBox.SelectedValue = null;
            sr_workWeekComboBox.SelectedValue = null;
            cht_workWeekComboBox.SelectedValue = null;
            pt_workWeekComboBox.SelectedValue = null;
            sb_workWeekComboBox.SelectedValue = null;
            vs_workWeekComboBox.SelectedValue = null;
            removeWorkWeekButton.Visibility = Visibility.Hidden;
        }


    }
}

