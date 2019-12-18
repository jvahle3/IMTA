using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IMTA
{
    public class Flag : INotifyPropertyChanged
    {
        public string FlagName { get; set; }
        public bool Bool { get; set; }
        public string Text { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propteryName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propteryName));
        }
    }
}
