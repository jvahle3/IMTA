using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
namespace IMTA.Cmds
{
    class TalkButtonClicked : CommandBase
    {
        public static bool IsTalkMenu = false;
        public override bool CanExecute(object parameter)
        {
            return !IsTalkMenu && !Models.MainWindowModelView.IsDeathText && !Models.MainWindowModelView.IsTalkText && !Models.MainWindowModelView.IsSpareText; //only work if button has not been pressed already
        }

        public override void Execute(object parameter)
        {
            IsTalkMenu = true;
            MainWindow window = (MainWindow)parameter;
            window.EntitySelectionMenu.Visibility = System.Windows.Visibility.Visible;
            AttackButtonClicked.IsAttackOptions = false;
            SpareButtonClicked.IsSpareMenu = false;
        }
    }
}
