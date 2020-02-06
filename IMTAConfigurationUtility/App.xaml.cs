using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IMTAConfigurationUtility
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, error) =>
            {
                Exception exception = (Exception)error.ExceptionObject;
                string msg = exception.Message;
                string title = "Somthing happened";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Error;
                MessageBox.Show(msg, title, messageBoxButton, messageBoxImage);
                Environment.Exit(1);
            };
        }
    }
}
