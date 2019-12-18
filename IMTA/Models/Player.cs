using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
namespace IMTA.Models
{
    public class Player
    {
        #region vars
        private int _Health;
        #endregion
        #region Props
        public int Health
        {
            get
            {
                return _Health;
            }
            set
            {
                if (_Health + value <= 0) YouDied(); //Tell the game you died
                _Health += value;
            }
        }
        public int Power { get; set; }
        #endregion

        private void YouDied() //Should do somthing to end the game and tell you your dead...
        {

        }
    }
}
