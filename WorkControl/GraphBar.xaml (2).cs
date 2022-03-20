using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System.Data.Entity.Core.Objects;
using WorkControl;
using System.Linq;

namespace Wpf.CartesianChart.Basic_Bars
{
    public partial class GraphBar : UserControl
    {
        private WorkControlEntities db = new WorkControlEntities();

        public const int DayMode = 1110;
        public const int MonthMode = 1111;
        public const int YearMode = 1112;

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<int, TimeSpan> Formatter { get; set; }
        public GraphBar()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            DataContext = this;
        }

        public void Update(int mode, Users user)
        {
            List<ISeriesView> rows = new List<ISeriesView>();
            foreach (var row in SeriesCollection)
            {
                rows.Add(row);
            }

            Dictionary<string, int> progDict;

            if (mode == DayMode)
            {
                progDict = GetProgDictFromDaysMode(user);
            }
            else if (mode == MonthMode)
            {
                progDict = GetProgDictFromMonthMode(user);
            }
            else
            {
                progDict = GetProgDictFromYearMode(user);
            }

            foreach (KeyValuePair<string, int> keyVal in progDict)
            {
                string name = keyVal.Key;
                int value = keyVal.Value;

                ISeriesView title = rows.Find(t => t.Title.Contains(name));

                if (title != null)
                {
                    int index = rows.IndexOf(title);
                    SeriesCollection[index].Values = new ChartValues<int> { value };
                }
                else
                {
                    SeriesCollection.Add(new RowSeries
                    {
                        Title = name,
                        Values = new ChartValues<int> { value }
                    });
                }
            }

            //db.SaveChanges();
        }

        private Dictionary<string, int> GetProgDictFromDaysMode(Users user)
        {
            DateTime nowDate = DateTime.Now.Date;
            var progTimes = db.ProgramTime.Where(p => p.Stats.UserId == user.UserId
            && p.Stats.StartDate.Value.Month == nowDate.Month
            && p.Stats.StartDate.Value.Year == nowDate.Year
            && p.Stats.StartDate.Value.Day == nowDate.Day)
            .Select(p => new
            {
                Program = p.Program,
                Time = p.Time
            })
            .ToList();            

            return progTimes.ToDictionary(p => p.Program.Name, g => g.Time.Value);
        }

        private Dictionary<string, int> GetProgDictFromMonthMode(Users user)
        {
            int nowMonth = DateTime.Now.Month;
            int nowYear = DateTime.Now.Year;
            var progTimes = db.ProgramTime.Where(p => p.Stats.UserId == user.UserId
            && p.Stats.StartDate.Value.Month == nowMonth &&
            p.Stats.StartDate.Value.Year == nowYear)
            .GroupBy(p => p.Program)
            .Select(p => new
            {
                Program = p.Key,
                Time = p.Average(g => g.Time)
            }).ToList();
            return progTimes.ToDictionary(p => p.Program.Name, g => Convert.ToInt32(g.Time.Value));
        }

        private Dictionary<string, int> GetProgDictFromYearMode(Users user)
        {
            int nowYear = DateTime.Now.Year;
            var progTimes = db.ProgramTime.Where(p => p.Stats.UserId == user.UserId
            && p.Stats.StartDate.Value.Year == nowYear)
            .GroupBy(p => p.Program)
            .Select(p => new
            {
                Program = p.Key,
                Time = p.Average(g => g.Time)
            }).ToList();
            return progTimes.ToDictionary(p => p.Program.Name, g => Convert.ToInt32(g.Time.Value));
        }

    }
}