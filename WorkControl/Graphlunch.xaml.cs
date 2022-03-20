using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для Graphlunch.xaml
    /// </summary>
    public partial class Graphlunch : UserControl, INotifyPropertyChanged
    {
        public SectionsCollection sections;
        public Graphlunch()
        {
            InitializeComponent();
            Value = 90;
            DataContext = this;
        }

        public void InitSections(int value)
        {
            Value = value;
            AnGu.ToValue = value;            

            int value50 = Convert.ToInt32(value / 2);
            int value25 = Convert.ToInt32(value50 / 2);

            var an1 = new AngularSection();
            an1.Fill = Brushes.Green;
            an1.ToValue = value;
            an1.FromValue = value50;

            var an2 = new AngularSection();
            an2.Fill = Brushes.Yellow;
            an2.ToValue = value50;
            an2.FromValue = value25;

            var an3 = new AngularSection();
            an3.Fill = Brushes.Red;
            an3.ToValue = value25;
            an3.FromValue = 0;

            AnGu.Sections = new List<AngularSection>() { an1, an2, an3};
            
        }

        private int _value;        

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");                
            }
        }                

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));            
        }
    }
}


