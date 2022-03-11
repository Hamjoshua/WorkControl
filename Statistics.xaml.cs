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
using Wpf.CartesianChart.Basic_Bars;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        WorkControlEntities db = new WorkControlEntities();
        public Statistics(bool isWatcher = false)
        {
            InitializeComponent();            
              
            currentStatsRaduoButton.Checked += UpdateGraphBars;
            monthStatsRaduoButton.Checked += UpdateGraphBars;
            yearStatsRaduoButton.Checked += UpdateGraphBars;
            yearStatsRaduoButton.IsChecked = true;
        }

        public void UpdateGraphBars(object s, EventArgs e)
        {
            ListOfGraphBars.Items.Clear();
            int mode;
            if(currentStatsRaduoButton.IsChecked.Value)
            {
                mode = GraphBar.DayMode;
            }
            else if(monthStatsRaduoButton.IsChecked.Value)
            {
                mode = GraphBar.MonthMode;
            }
            else
            {
                mode = GraphBar.YearMode;
            }

            List<Users> users = db.Users.Where(u => u.Roles.Name == "Сотрудник").ToList();

            foreach(Users user in users)
            {
                StackPanel sp = new StackPanel();
                TextBlock tb = new TextBlock();
                tb.Text = $"ФИО: {user.SecondName} {user.Name} {user.LastName}\nДолжность: {user.WorkPost}";
                sp.Children.Add(tb);
                GraphBar gb = new GraphBar();
                sp.Children.Add(gb);                
                gb.Update(mode, user);
                gb.Width = ListOfGraphBars.Width;
                gb.Height = 200;
                ListOfGraphBars.Items.Add(sp);
            }
        }      
    }
}
