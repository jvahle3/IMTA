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
namespace IMTA.Models
{



    public class MainWindowModelView
    {
        
        private MainWindow mainW;

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

                mainW.EntitySelectionMenu.Children.Add(button);
            }

        }
    }
}
