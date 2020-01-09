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
using IMTA.Cmds;
namespace IMTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICommand _AttackButtonClicked = null;
        public ICommand AttackButtonClicked => _AttackButtonClicked ?? (_AttackButtonClicked = new AttackButtonClicked());
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                InitalizePanels();
                LoadImages();
            } catch (Exception e)
            {
                string error = e.Message + '\n' + e.StackTrace + '\n' + e.InnerException ;
                string exception = "Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage image = MessageBoxImage.Error;
                MessageBox.Show(error, exception, button, image);
                Environment.Exit(1);
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
        }

        private void LoadImages()
        {

            int _numberOfObjects = UserObjectContainer.UOBJ.Count;
            foreach (UserObject us in UserObjectContainer.UOBJ)
            {
                if (us.ImageFileName.Substring(us.ImageFileName.LastIndexOf(@"\")).Contains(".gif")) return;
                Image image = new Image();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(us.ImageFileName);
                image.Source = bitmapImage;
                bitmapImage.EndInit();
                EnemyBox.Children.Add(image);
            }
        }
    }
}
