using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace IMTA.Models
{
    public class DataRecorder
    {
        public enum RecordType
        {
            TookDamageFromEntity,
            TookDamageFromBox,
            AdvancedGameState,
            KilledEntity,
            SparedEntity,
            ClickedAttack,
            ClickedTalk,
            ClickedSpare,
            GameWon,
            GameLost,
            DamagedEntity,
            TriedToSpare,
            TalkedTo,
            SelectedEntity

        }
        private readonly string _WereToWrite = (string)MainWindowModelView.AppReader.GetValue("DataWrite", typeof(string)) + @"\" + DateTime.Now.ToString().Replace(" ", "").Replace("/", "-").Replace(":","-") + "-IMTA-Data";
        public DataRecorder()
        {
            using (StreamWriter fs = File.CreateText(_WereToWrite))
            {
                fs.WriteLineAsync("Recording Started at:" + DateTime.Now.ToString());
            }
        }

        public void Write(UserObject US, RecordType RT, string OptionalInfo)
        {
            FileInfo fileInfo = new FileInfo(_WereToWrite);
            string StringToWrite = "At " + DateTime.Now.ToString();
            switch (RT)
            {
                case RecordType.TookDamageFromEntity:
                    StringToWrite += ": user took " + US.UserAttack + " damage from Entity: " + US.ObjectName;
                    break;
                case RecordType.TookDamageFromBox:
                    StringToWrite += ": user took " + OptionalInfo + " damage from the box";
                    break;
                case RecordType.AdvancedGameState:
                    StringToWrite += ": user Advanced the game state";
                    break;
                case RecordType.KilledEntity:
                    StringToWrite += ": user killed: " + US.ObjectName;
                    break;
                case RecordType.SparedEntity:
                    StringToWrite += ": user spared: " + US.ObjectName;
                    break;
                case RecordType.ClickedAttack:
                    StringToWrite += ": user clicked attack button";
                    break;
                case RecordType.ClickedTalk:
                    StringToWrite += ": user clicked talk button";
                    break;
                case RecordType.ClickedSpare:
                    StringToWrite += ": user clicked spare button";
                    break;
                case RecordType.GameWon:
                    StringToWrite += ": user won the game";
                    break;
                case RecordType.GameLost:
                    StringToWrite += ": user lost the game";
                    break;
                case RecordType.DamagedEntity:
                    StringToWrite += ": user dealt " + MainWindowModelView.UserAttackPower + " to: " + US.ObjectName;
                    break;
                case RecordType.TriedToSpare:
                    StringToWrite += ": user tried to spare: " + US.ObjectName;
                    break;
                case RecordType.TalkedTo:
                    StringToWrite += ": user talked to: " + US.ObjectName;
                    break;
                case RecordType.SelectedEntity:
                    StringToWrite += ": user selected entity: " + US.ObjectName;
                    break;
                default:
                    break;
            }
            using(StreamWriter fs = fileInfo.AppendText())
            {
                fs.WriteLineAsync(StringToWrite);
            }
        }
    }
}
