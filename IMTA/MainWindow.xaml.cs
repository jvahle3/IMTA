using IMTA.Cmds;
using IMTA.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Threading;

namespace IMTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);
        private ICommand _AttackButtonClicked = null;
        public ICommand AttackButtonClicked => _AttackButtonClicked ?? (_AttackButtonClicked = new AttackButtonClicked());
        private ICommand _SpareButtonClicked = null;
        public ICommand SpareButtonClicked => _SpareButtonClicked ?? (_SpareButtonClicked = new SpareButtonClicked());
        private ICommand _TalkButtonClicked = null;
        public ICommand TalkButtonClicked => _TalkButtonClicked ?? (_TalkButtonClicked = new TalkButtonClicked());
        private ICommand _EntitySelected = null;
        public ICommand EntitySelected => _EntitySelected ?? (_EntitySelected = new EntitySelected());
        public MediaElement SoundPlayer = new MediaElement { Visibility = Visibility.Hidden, LoadedBehavior = MediaState.Manual };
        private int _toAttack;
        public MainWindow()
        {
            InitializeComponent();
            InitalizePanels();
            MainWindowModelView mainWindowModelView = new MainWindowModelView(this);
            SetUpHealth();
            LoadImages();
            mainPanel.Children.Add(SoundPlayer);
        }

        private void SetUpHealth()
        {
            string s = "/" + MainWindowModelView.UserHealth;
            MaxHp.Content = s;
            CurrentHP.Content = MainWindowModelView.UserHealth;
        }

        internal void OnEntityDeath(string EntityName)
        {
            UserObject us = MainWindowModelView.FineObjectByName(EntityName);
            us.IsAlive = false;
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
            us.IsAlive = false;
            MainWindowModelView.DataRecorderObject.Write(us, DataRecorder.RecordType.SparedEntity, null);
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
            InfoText.Visibility = Visibility.Visible;
            InfoText.Text = ObjectName + ": took " + MainWindowModelView.UserAttackPower + " damage";
            MainWindowModelView.IsAttackInfo = true;
        }
        private void InfoBox_KeyDown(object sender, KeyEventArgs e)
        {
             if (MainWindowModelView.IsDeathText == true || MainWindowModelView.IsTalkText == true || MainWindowModelView.IsSpareText == true || MainWindowModelView.IsAttackInfo == true)
            {
                MainWindowModelView.IsDeathText = false;
                MainWindowModelView.IsTalkText = false;
                MainWindowModelView.IsSpareText = false;
                InfoText.Text = string.Empty;
                InfoText.Visibility = Visibility.Hidden;
                MainWindowModelView.DataRecorderObject.Write(null, DataRecorder.RecordType.AdvancedGameState, null);
                EntityTurn();
                MainWindowModelView.IsAttackInfo = false;
            }
            else return;
        }
        private void EntityTurn()
        {
            MainWindowModelView.IsUsersTurn = false;
            foreach (UserObject us in UserObjectContainer.UOBJ)
            {

                if (us.IsAlive)
                {
                    _toAttack++;
                    AttackWindow.Visibility = Visibility.Visible;
                    AttackBox.Visibility = Visibility.Visible;
                    Arrow1.Visibility = Visibility.Visible;
                    Arrow2.Visibility = Visibility.Visible;
                    Warning1.Visibility = Visibility.Visible;
                    Warning2.Visibility = Visibility.Visible;
                    XmlReader xmlr = XmlReader.Create(new StringReader(us.XAML));
                    Shape userE = (Shape)XamlReader.Load(xmlr);
                    userE.Name = us.ObjectName;
                    userE.MouseEnter += new MouseEventHandler(AttackHitPlayer);
                    Storyboard storyboard = userE.FindName("Animation") as Storyboard;
                    storyboard.Completed += new EventHandler(AttackCompleted);
                    AttackWindow.Children.Add(userE);
                }
             
            }
        }
        public void AttackHitPlayer(object sender, MouseEventArgs e)
        {
            Shape shape = (Shape)sender;
            shape.Visibility = Visibility.Hidden;
            UserObject us = MainWindowModelView.FineObjectByName(shape.Name);
            MainWindowModelView.UserHealth -= us.UserAttack;
            MainWindowModelView.DataRecorderObject.Write(us, DataRecorder.RecordType.TookDamageFromEntity, null);
            CurrentHP.Content = MainWindowModelView.UserHealth;
            if(MainWindowModelView.UserHealth <= 0)
            {
                MainWindowModelView.GameOverEndState = MainWindowModelView.EndState.PlayerLose;
            }
            shape.MouseEnter -= AttackHitPlayer;
        }
        private int _attacksCompleted;
        public void AttackCompleted(object sender, EventArgs e)
        {

            _attacksCompleted++;
            if (_toAttack == _attacksCompleted)
            {   
                _attacksCompleted = 0;
                _toAttack = 0;
                AttackBox.Visibility = Visibility.Hidden;
                AttackWindow.Visibility = Visibility.Hidden;
                Arrow1.Visibility = Visibility.Hidden;
                Arrow2.Visibility = Visibility.Hidden;
                Warning1.Visibility = Visibility.Hidden;
                Warning2.Visibility = Visibility.Hidden;
                AttackWindow.Children.Clear();
                MainWindowModelView.IsUsersTurn = true;
                if (MouseOutTime != 0)
                {
                    long tempInt = DateTime.Now.Ticks - MouseOutTime;
                    TimeSpan timeSpan = TimeSpan.FromTicks(tempInt);
                    MainWindowModelView.UserHealth -= (int)timeSpan.TotalSeconds;
                    CurrentHP.Content = MainWindowModelView.UserHealth;
                    int I = (int)timeSpan.TotalSeconds;
                    if(I != 0) MainWindowModelView.DataRecorderObject.Write(null, DataRecorder.RecordType.TookDamageFromBox, I.ToString());
                    if (MainWindowModelView.UserHealth <= 0)
                    {
                        MainWindowModelView.GameOverEndState = MainWindowModelView.EndState.PlayerLose;
                    }
                }
                MouseOutTime = 0;
            }
        }
        private long MouseOutTime;
        private void AttackBox_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOutTime = DateTime.Now.Ticks;
        }

        private void AttackBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (MainWindowModelView.IsUsersTurn) return;
            if(MouseOutTime !=0)
            { 
                long tempInt = DateTime.Now.Ticks - MouseOutTime;
                TimeSpan timeSpan = TimeSpan.FromTicks(tempInt);
                MainWindowModelView.UserHealth -= (int)timeSpan.TotalSeconds;
                CurrentHP.Content = MainWindowModelView.UserHealth;
                int I = (int)timeSpan.TotalSeconds;
                if(I !=0) MainWindowModelView.DataRecorderObject.Write(null, DataRecorder.RecordType.TookDamageFromBox, I.ToString());

                if (MainWindowModelView.UserHealth <= 0)
                {
                    MainWindowModelView.GameOverEndState = MainWindowModelView.EndState.PlayerLose;
                }
            }
            MouseOutTime = 0;
            
        }
    }
}
