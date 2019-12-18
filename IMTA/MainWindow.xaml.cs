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
using System.Windows.Markup;
using System.IO;
using System.Xml;
namespace IMTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                InitalizePanels();
            } catch (Exception e)
            {
                string error = e.Message;
                string exception = "Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage image = MessageBoxImage.Error;
                MessageBox.Show(error, exception, button, image);
            }
        }
        
        private void InitalizePanels()
        {
            mainPanel.Height = this.Height;
            mainPanel.Width = this.Width;

            EnemyBox.Height = this.Height / 3;
            EnemyBox.Width = this.Width;

            InfoBox.Height = this.Height / 3;
            InfoBox.Width = this.Width;

            ButtonBox.Height = this.Height / 3;
            ButtonBox.Width = this.Width;

            UserPanel.Height = this.Height / 3;
            UserPanel.Width = this.Width;

            InfoPanel.Height = this.Height / 3;
            InfoPanel.Width = this.Width;
        }

    }
}
