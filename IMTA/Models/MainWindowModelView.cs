using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
namespace IMTA.Models
{



    public class MainWindowModelView
    {
           public class Pack
        {
            public string name;
            public Button b;
            public MainWindow m;
        }
        private MainWindow mainW;
        public enum EndState { PlayerWin, PlayerLose, NothingYet };
        public static EndState _gameOverEndState;
        public static EndState GameOverEndState { get
            {
                return _gameOverEndState;
            } set {
                _gameOverEndState = value;
                GameOver();
            } }

        private static void GameOver()
        {
            if(GameOverEndState == EndState.PlayerWin)
            {
                MessageBoxImage mi = MessageBoxImage.Information;
                string title = "YOU WIN!";
                string msg = "Congradulations, you won!";
                MessageBox.Show(title, msg, MessageBoxButton.OK, mi);
                Environment.Exit(0);
            } else if (GameOverEndState == EndState.PlayerLose)
            {
                MessageBoxImage mi = MessageBoxImage.Information;
                string title = "You Lose!";
                string msg = ":(";
                MessageBox.Show(title, msg, MessageBoxButton.OK, mi);
                Environment.Exit(0);
            }
        }
        private static bool _IsUsersTurn = true;
        public static AppSettingsReader AppReader = new AppSettingsReader();
        public static DataRecorder DataRecorderObject { get; } = new DataRecorder();
        public static int UserAttackPower { get; } = (int)AppReader.GetValue("UserAttackPower", typeof(int));
        public static int UserHealth { get; set; } = (int)AppReader.GetValue("UserHealth", typeof(int));
        public static bool IsDeathText { get; set; }
        public static bool IsTalkText { get; set; }
        public static bool IsSpareText { get; set; }
        public static bool IsUsersTurn { get
            {
                return _IsUsersTurn;
            } set {
                _IsUsersTurn = value;
                foreach(UserObject us in UserObjectContainer.UOBJ)
                {
                    if (us.IsAlive) return;
                    GameOverEndState = EndState.PlayerWin;
                }
            } }
        public static bool IsAttackInfo { get; set; }
        public static UIElementCollection AttackPaths { get; set; }
        public MainWindowModelView(MainWindow mainWindow)
        {
            mainW = mainWindow;
            SetUpEntityButtons();
        }
        private void SetUpEntityButtons()
        {
            
            foreach (UserObject us in UserObjectContainer.UOBJ)
            {
                Button button = new Button
                {
                    Background = Brushes.Black,
                    Foreground = Brushes.White,
                    Name = us.ObjectName
                };
                string s = "\n" +
                    us.ObjectName + " \t\t\t\t" + "HP:" + us.UserHp +  //Should look somthing like this: name              HP;
                    "\n";
                button.Content = s;
                button.Command = mainW.EntitySelected;
                button.CommandParameter = new Pack { name = us.ObjectName, b = button, m = mainW };
                mainW.EntitySelectionMenu.Children.Add(button);
            }

        }
        public static UserObject FineObjectByName(string Name)
        {
            foreach(UserObject us in UserObjectContainer.UOBJ)
            {
                if (us.ObjectName.Equals(Name)) return us;
            }
            throw new KeyNotFoundException("No Object By This Name Found");
        }
    }
}
