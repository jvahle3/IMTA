using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
namespace IMTA.Cmds
{
    class AttackButtonClicked : CommandBase
    {
        public static bool IsAttackOptions = false;
        public override bool CanExecute(object parameter)
        {
            if (!Models.MainWindowModelView.IsUsersTurn) return false;
            return !IsAttackOptions && !Models.MainWindowModelView.IsDeathText && !Models.MainWindowModelView.IsTalkText && !Models.MainWindowModelView.IsSpareText; //If the attack selection panel is open, disable the attack button
        }

        public override void Execute(object parameter)
        {
            IsAttackOptions = true;
            MainWindow window = (MainWindow)parameter;
            window.EntitySelectionMenu.Visibility = System.Windows.Visibility.Visible;
            SpareButtonClicked.IsSpareMenu = false;
            TalkButtonClicked.IsTalkMenu = false;
        }
    }
}
