using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using LiveCharts.Wpf;
using System.Linq;
using LiveCharts;
using LiveCharts.Definitions.Series;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Data.Entity.Core.Objects;
using Wpf.CartesianChart.Basic_Bars;
using WorkControl.ModelMethods;

namespace WorkControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Users thisUser;
        TimeProfiles timeProfile;
        Stats stats;
        bool onTheLanch;
        public bool isHoliday;
        private WorkControlEntities db = new WorkControlEntities();

        public MainWindow(Users user)
        {
            thisUser = user;

            InitializeComponent();

            currentStatsRaduoButton.IsChecked = true;

            timeProfile = TimeProfileMethods.getTimeProfileFromWorkWeek(thisUser.WorkWeeks);

            this.Closed += new EventHandler(OnClose);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();

            theoryStartWork.Content = timeProfile.StartWorkTime;
            theoryEndWork.Content = timeProfile.EndWorkTime;

            InitStats();

            factStartWork.Content = stats.StartDate.Value.ToString("HH:mm:ss");
            if (stats.EndDate != null)
            {
                TimeSpan currentTimeSpan = stats.EndDate.Value.TimeOfDay;
                factEndWork.Content = currentTimeSpan.ToString(@"hh\:mm\:ss");
                if (currentTimeSpan != timeProfile.EndWorkTime)
                {
                    factEndWork.Content += " (время последнего закрытия)";
                }
            }
            else
            {
                factEndWork.Content = "-";
            }


            if (stats.LunchTime < TimeSpan.Zero)
            {
                goToLanch.Content = "Время на обед исчерпано";
                goToLanch.Background = (Brush)this.FindResource("noLanch");
                onTheLanch = false;
                goToLanch.IsEnabled = false;
            }
            else
            {
                goToLanch.Content = "Уйти на обед";
                goToLanch.Background = (Brush)this.FindResource("goOnLanch");
                onTheLanch = false;
            }

            InitGraphBar();
            InitGraphLanch();

        }

        public void InitStats()
        {
            DateTime nowDate = DateTime.Now.Date;
            var existingStats = (from stats in db.Stats
                                 where stats.UserId == thisUser.UserId &&
                                 EntityFunctions.TruncateTime(stats.StartDate.Value) == nowDate
                                 select stats).FirstOrDefault();
            if (existingStats == null)
            {
                stats = new Stats();
                stats.UserId = thisUser.UserId;
                stats.LunchTime = timeProfile.LanchTime;
                stats.StartDate = DateTime.Now;
                db.Stats.Add(stats);
                db.SaveChanges();
            }
            else
            {
                stats = existingStats;
            }
        }

        public void InitGraphLanch()
        {
            int minutes = Convert.ToInt32(timeProfile.LanchTime.Value.TotalMinutes);
            graphLanch.InitSections(minutes);
            graphLanch.Value = Convert.ToInt32(stats.LunchTime.Value.TotalMinutes);
            graphLunchLabel.Content = stats.LunchTime.Value.ToString(@"hh\:mm\:ss");
        }

        public void InitGraphBar()
        {
            int counter = 0;
            var progTimeList = stats.ProgramTime;

            foreach (var progTime in progTimeList)
            {
                string progName = progTime.Program.Name;
                int seconds = progTime.Time.Value;
                progGraph.SeriesCollection.Add(new RowSeries
                {
                    Title = progName,
                    Values = new ChartValues<int> { seconds }
                });
                counter++;
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);

        private string GetNameOfProgramm(Process process)
        {
            try
            {
                string fd = process.MainModule.FileVersionInfo.FileDescription;
                return fd;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return process.ProcessName;
            }
        }

        public void TimerTick(object sender, EventArgs e)
        {
            if (onTheLanch)
            {
                stats.LunchTime -= TimeSpan.FromSeconds(1);
                graphLunchLabel.Content = stats.LunchTime.Value.ToString(@"hh\:mm\:ss");
                graphLanch.Value = Convert.ToInt32(stats.LunchTime.Value.TotalMinutes);
            }
            else
            {
                IntPtr h = GetForegroundWindow();
                int pid = 0;
                GetWindowThreadProcessId(h, ref pid);
                Process p = Process.GetProcessById(pid);

                string programName = GetNameOfProgramm(p);
                RefreshProgramsDb(programName);

                var progTime = stats.ProgramTime.Where(pr => pr.Program.Name == programName).FirstOrDefault();

                if (progTime == null)
                {
                    progTime = new ProgramTime();
                    progTime.Stats = stats;
                    progTime.Program = db.Program.Where(pr => pr.Name == programName).FirstOrDefault();
                    progTime.Time = 0;
                    db.ProgramTime.Add(progTime);
                    db.SaveChanges();
                }
                else
                {
                    progTime.Time += 1;
                    db.SaveChanges();
                }

                if (currentStatsRaduoButton.IsChecked.Value)
                {
                    progGraph.Update(GraphBar.DayMode, thisUser);
                }
                else if (monthStatsRaduoButton.IsChecked.Value)
                {
                    progGraph.Update(GraphBar.MonthMode, thisUser);
                }
                else
                {
                    progGraph.Update(GraphBar.YearMode, thisUser);
                }

            }
        }

        private void RefreshProgramsDb(string progName)
        {
            if (!db.Program.Select(p => p.Name).ToList().Contains(progName))
            {
                Program prog = new Program();
                prog.Name = progName;
                db.Program.Add(prog);
                db.SaveChanges();
            }
        }

        public void OnClose(object sender, EventArgs e)
        {
            stats.EndDate = DateTime.Now;
            timer.Stop();
            db.SaveChanges();
            this.Close();
        }

        private void goToLanch_Click(object sender, RoutedEventArgs e)
        {

            if (onTheLanch == false)
            {
                goToLanch.Content = "Продолжить работу";
                goToLanch.Background = (Brush)this.FindResource("backFromLanch");
                onTheLanch = true;
            }

            else
            {
                if (stats.LunchTime.Value < TimeSpan.Zero)
                {
                    goToLanch.Content = "Время на обед закончено";
                    goToLanch.Background = (Brush)this.FindResource("noLanch");
                    onTheLanch = false;
                    goToLanch.IsEnabled = false;
                }
                else
                {
                    goToLanch.Content = "Уйти на обед";
                    goToLanch.Background = (Brush)this.FindResource("goOnLanch");
                    onTheLanch = false;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти из аккаунта?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Login f1 = new Login();
                this.Close();
                timer.Stop();
                f1.Show();
            }
        }

        private void backward_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}

