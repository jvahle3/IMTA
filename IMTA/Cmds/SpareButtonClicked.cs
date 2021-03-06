﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMTA.Cmds
{
    class SpareButtonClicked : CommandBase
    {
        public static bool IsSpareMenu = false;
        public override bool CanExecute(object parameter)
        {
            if (!Models.MainWindowModelView.IsUsersTurn) return false;
            return !IsSpareMenu && !Models.MainWindowModelView.IsDeathText && !Models.MainWindowModelView.IsTalkText && !Models.MainWindowModelView.IsSpareText;
        }

        public override void Execute(object parameter)
        {
            IsSpareMenu = true;
            MainWindow window = (MainWindow)parameter;
            window.EntitySelectionMenu.Visibility = System.Windows.Visibility.Visible;
            AttackButtonClicked.IsAttackOptions = false;
            TalkButtonClicked.IsTalkMenu = false;
        }
    }
}
