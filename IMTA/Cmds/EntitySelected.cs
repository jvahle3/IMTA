﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using IMTA.Models;
namespace IMTA.Cmds
{
    class EntitySelected : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        private void ResetMenus(MainWindow main)
        {
            AttackButtonClicked.IsAttackOptions = false;
            TalkButtonClicked.IsTalkMenu = false;
            SpareButtonClicked.IsSpareMenu = false;
            main.EntitySelectionMenu.Visibility = Visibility.Hidden;
        }
        public override void Execute(object parameter)
        {
            MainWindowModelView.Pack pack = (MainWindowModelView.Pack)parameter;

            string ObjectName = pack.name;
            MainWindow mainW = pack.m;
            Button button = pack.b;

            if(AttackButtonClicked.IsAttackOptions)
            {
                UserObject us = Models.MainWindowModelView.FineObjectByName(ObjectName);
                MainWindowModelView.DataRecorderObject.Write(us,DataRecorder.RecordType.ClickedAttack,null);
                us.UserHp -= (MainWindowModelView.UserAttackPower - us.UserDef);
                MainWindowModelView.DataRecorderObject.Write(us, DataRecorder.RecordType.DamagedEntity, null);
                button.Content = "\n" + us.ObjectName + " \t\t\t\t" + "HP:" + us.UserHp + "\n";
                ResetMenus(mainW);
                mainW.SoundPlayer.Source = new Uri((string)MainWindowModelView.AppReader.GetValue("HurtSound", typeof(string)));
                mainW.SoundPlayer.Play();
                mainW.HurtAnimation(us.ObjectName);
                if (us.UserHp <= 0)
                {
                    MainWindowModelView.DataRecorderObject.Write(us, DataRecorder.RecordType.KilledEntity, null);
                    mainW.OnEntityDeath(ObjectName);
                    return;
                }
            } else if (TalkButtonClicked.IsTalkMenu)
            {
                UserObject us = Models.MainWindowModelView.FineObjectByName(ObjectName);
                MainWindowModelView.DataRecorderObject.Write(us, DataRecorder.RecordType.TalkedTo, null);
                ResetMenus(mainW);
                mainW.InfoText.Visibility = Visibility.Visible;
                if (us.RuntimeInt > us.RuntimeTextList.Count -1)
                {
                    us.RuntimeInt = 0;
                    us.TextEndReached = true;
                }
                MainWindowModelView.IsTalkText = true;
                mainW.InfoText.Text = us.ObjectName + " says: " + us.RuntimeTextList.ElementAt(us.RuntimeInt);
                us.RuntimeInt++;
            } else if (SpareButtonClicked.IsSpareMenu)
            {
                UserObject us = Models.MainWindowModelView.FineObjectByName(ObjectName);
                ResetMenus(mainW);
                MainWindowModelView.DataRecorderObject.Write(us, DataRecorder.RecordType.TriedToSpare, null);
                MainWindowModelView.IsSpareText = true;
                if (us.HpSpare)
                {
                    if (us.UserHp <= us.SpareHP)
                    {
                        mainW.OnEntitySpare(us.ObjectName);
                        return;
                    } else
                    {
                        mainW.InfoText.Visibility = Visibility.Visible;
                        mainW.InfoText.Text = "This entity can not be spared yet";
                    }
                }
                if(us.TextSpare)
                {
                    if(us.TextEndReached)
                    {
                        mainW.OnEntitySpare(us.ObjectName);
                        return;
                    } else
                    {
                        mainW.InfoText.Visibility = Visibility.Visible;
                        mainW.InfoText.Text = "This entity can not be spared yet";
                    }
                }
                if (us.NoSpare)
                {
                    mainW.InfoText.Visibility = Visibility.Visible;
                    mainW.InfoText.Text = "This entity can not be spared!";
                }
            } else
            {
                throw new ArgumentException("No Valid Argument set to true, this statement should not have fired");
            }

        }
    }
}
