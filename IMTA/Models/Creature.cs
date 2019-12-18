using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMTA.Models
{
    public class Creature
    {
        #region vars
        private int _HP;
        #endregion
        #region Property
        public int HP { get => _HP; set {
                if (_HP + value <= 0) CreatureDies();
                _HP += value;
            } }
        public int Def { get; set; }
        public int Atk { get; set; }
        #endregion



        private void CreatureDies() //should do somthing when a creature dies.
        {
            throw new NotImplementedException();
        }

    }
}
