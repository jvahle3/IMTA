using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMTA.Cmds
{
    class AttackButtonClicked : CommandBase
    {
        private static bool IsAttackOptions = false;
        public override bool CanExecute(object parameter)
        {
            return !IsAttackOptions; //If the attack selection panel is open, disable the attack button
        }

        public override void Execute(object parameter)
        {
            MainWindow window = (MainWindow)parameter;
            window.NameHPTextBox.Visibility = System.Windows.Visibility.Hidden;
            foreach(UserObject us in UserObjectContainer.UOBJ)
            {
                string s = "\n" +
                    us.ObjectName + " \t" + "HP:" + us.UserHp +  //Should look somthing like this: name              HP;
                    "\n";
                window.NameHPTextBox.Text += s;
            }
        }
    }
}
