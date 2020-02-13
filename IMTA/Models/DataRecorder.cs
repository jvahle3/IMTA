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
            TriedToSpare

        }
        private readonly string _WereToWrite = (string)MainWindowModelView.AppReader.GetValue("DataWrite", typeof(string)) + @"\" + DateTime.Now.ToString().Replace(" ", "").Replace("/", "-").Replace(":","-") + "-IMTA-Data";
        public DataRecorder()
        {
            using (StreamWriter fs = File.CreateText(_WereToWrite))
            {
                fs.WriteLineAsync("Recording Started at:" + DateTime.Now.ToString());
            }
        }

        public void Write(UserObject US, RecordType RT)
        {
            string StringToWrite = DateTime.Now.ToString();
        }
    }
}
