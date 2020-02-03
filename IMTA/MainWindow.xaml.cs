using IMTA.Cmds;
using IMTA.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
namespace IMTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICommand _AttackButtonClicked = null;
        public ICommand AttackButtonClicked => _AttackButtonClicked ?? (_AttackButtonClicked = new AttackButtonClicked());
        private ICommand _SpareButtonClicked = null;
        public ICommand SpareButtonClicked => _SpareButtonClicked ?? (_SpareButtonClicked = new SpareButtonClicked());
        private ICommand _TalkButtonClicked = null;
        public ICommand TalkButtonClicked => _TalkButtonClicked ?? (_TalkButtonClicked = new TalkButtonClicked());
        private ICommand _EntitySelected = null;
        public ICommand EntitySelected => _EntitySelected ?? (_EntitySelected = new EntitySelected());
        public MediaElement SoundPlayer = new MediaElement { Visibility = Visibility.Hidden, LoadedBehavior = MediaState.Manual };
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                InitalizePanels();
                MainWindowModelView mainWindowModelView = new MainWindowModelView(this);
                LoadImages();
                mainPanel.Children.Add(SoundPlayer);

            }
            catch (Exception e)
            {
                string error = e.Message + '\n' + e.StackTrace + '\n' + e.InnerException;
                string exception = "Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage image = MessageBoxImage.Error;
                MessageBox.Show(error, exception, button, image);
                Environment.Exit(1);
            }
        }

        internal void OnEntityDeath(string EntityName)
        {
            UserObject us = MainWindowModelView.FineObjectByName(EntityName);
            InfoText.Text = EntityName + " Says: " + us.DeathText;
            InfoText.Visibility = Visibility.Visible;
            MainWindowModelView.IsDeathText = true;
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Completed += (o, s) => { };
            doubleAnimation.From = 1;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 2));
            foreach (Image i in EnemyBox.Children)
            {
                if (i.Name.Equals(EntityName))
                {
                    i.BeginAnimation(OpacityProperty, doubleAnimation);
                    break;
                }
                else continue;
            }
            foreach (Button button in EntitySelectionMenu.Children)
            {
                if (button.Name.Equals(us.ObjectName))
                {
                    EntitySelectionMenu.Children.Remove(button);
                    break;
                }
            }
        }
        internal void OnEntitySpare(string EntityName)
        {
            UserObject us = MainWindowModelView.FineObjectByName(EntityName);
            InfoText.Text = EntityName + " Says: " + us.SparedText;
            InfoText.Visibility = Visibility.Visible;
            MainWindowModelView.IsDeathText = true;
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Completed += (o, s) => { };
            doubleAnimation.From = 1;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 2));
            foreach (Image i in EnemyBox.Children)
            {
                if (i.Name.Equals(EntityName))
                {
                    i.BeginAnimation(OpacityProperty, doubleAnimation);
                    break;
                }
                else continue;
            }
            foreach (Button button in EntitySelectionMenu.Children)
            {
                if (button.Name.Equals(us.ObjectName))
                {
                    EntitySelectionMenu.Children.Remove(button);
                    break;
                }
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
                image.Name = us.ObjectName;
                bitmapImage.EndInit();
                EnemyBox.Children.Add(image);
            }
        }
        public void HurtAnimation(string ObjectName)
        {
            TranslateTransform translateTransform = new TranslateTransform();
            DoubleAnimation translateAnimation = new DoubleAnimation();
            translateAnimation.Completed += (o, s) => { };
            translateAnimation.From = 0;
            translateAnimation.To = 50;
            translateAnimation.Duration = new Duration(new TimeSpan(999999));
            translateAnimation.AutoReverse = true;
            foreach (Image image in EnemyBox.Children)
            {
                if (image.Name.Equals(ObjectName))
                {
                    image.RenderTransform = translateTransform;
                    translateTransform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
                }
            }
        }
        private void InfoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainWindowModelView.IsDeathText == true || MainWindowModelView.IsTalkText == true || MainWindowModelView.IsSpareText == true)
            {
                MainWindowModelView.IsDeathText = false;
                MainWindowModelView.IsTalkText = false;
                MainWindowModelView.IsSpareText = false;
                InfoText.Text = string.Empty;
                InfoText.Visibility = Visibility.Hidden;
                EntityTurn();
            }
            else return;
        }
        private void EntityTurn()
        {
            
        }
    }
}
