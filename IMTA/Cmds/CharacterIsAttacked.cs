using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMTA.Models;
using IMTA;
namespace IMTA.Cmds
{
    /// <summary>
    /// The Character attacked command should fire when the user attacks an onscreen character
    /// </summary>
    class CharacterIsAttacked : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            if(parameter as Creature !=  null)
            {
                return true; //check to make sure the incoming argment is of type creature
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            //this will remove the hp from the creture attacked
            Creature cr = (Creature)parameter;
        }
    }
}
