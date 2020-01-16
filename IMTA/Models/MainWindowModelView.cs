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
        public static AppSettingsReader AppReader = new AppSettingsReader();
        public static int UserAttackPower { get; } = (int)AppReader.GetValue("UserAttackPower", typeof(int));
        public static int UserHealth { get; set; } = (int)AppReader.GetValue("UserHealth", typeof(int));

        public MainWindowModelView(MainWindow mainWindow)
        {
             mainW = mainWindow;
            SetUpEntityButtons();
        }
        private void SetUpEntityButtons()
        {
            
            foreach (UserObject us in UserObjectContainer.UOBJ)
            {
                Button button = new Button();
                button.Background = Brushes.Black;
                button.Foreground = Brushes.White;
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
