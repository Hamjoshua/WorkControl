using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NLog;

namespace WorkControl
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        public WorkControlEntities db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            db = new WorkControlEntities();
        }
        

        private void Application_DispatcherUnhandledExceptions(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Error(e.Exception, e.Exception.Data.ToString());
        }
    }
}
