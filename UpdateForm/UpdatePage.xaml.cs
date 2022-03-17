using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WorkControl.UpdateForm
{
    /// <summary>
    /// Логика взаимодействия для UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {
        public string Status
        {
            get { return statusBlock.Text; }
            set { statusBlock.Text = value; }
        }
        string exeName;
        string url;
        public UpdatePage(string _exeName, string _url)
        {
            exeName = _exeName;
            url = string.Concat("https://github.com", _url, "/releases/latest");
            InitializeComponent();
            UpdateApp();
        }

        private void UpdateApp()
        {
            Status = "Скачивание...";
            WebRequest wrq = WebRequest.Create(url);
            WebResponse wrs = wrq.GetResponse();
            using (var stm = wrs.GetResponseStream())
            {
                var zip = new ZipArchive(stm);
                foreach (var entry in zip.Entries)
                {
                    Status = string.Concat("Разархивация ", entry.Name, "...");
                    try
                    {
                        File.Delete(entry.FullName);
                    }
                    catch { }
                    var d = System.IO.Path.GetDirectoryName(entry.FullName);
                    if (!string.IsNullOrEmpty(d))
                    {
                        try
                        {
                            Directory.CreateDirectory(d);
                        }
                        catch { }
                    }
                    using (var stm1 = entry.Open())
                    using (var stm2 = File.OpenWrite(entry.FullName))
                        stm1.CopyTo(stm2);


                }
                //zip.ExtractToDirectory(Environment.CurrentDirectory, true);
            }
            Status = "Приложение обновлено. Закройте для завершения обновления.";
            
        }

    }
}
