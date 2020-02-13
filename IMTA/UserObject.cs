using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
namespace IMTA
{
    /// <summary>
    /// This object will hold the user's prefrences on how they want the runtime to act.
    /// it will store info like player hp and attack
    /// but also data to tell the run time what buttons should do what
    /// and what text to play when an event happens
    /// </summary>
    [Serializable]
    public class UserObject : INotifyPropertyChanged
    {
        private string _objectName;
        private int _userAttack;
        private int _userHp;
        private int _userDef;
        private int _SpareHP;
        private string _deathText;
        private string _sparedText;
        private string _xaml = null;
        private string _xamlName;
        private string _imageFileName;
        private bool _hpSpare;
        private bool _textSpare;
        private bool _noSpare;
        private ObservableCollection<string> _textList = new ObservableCollection<string>();
        private ObservableCollection<int> _ints = new ObservableCollection<int>();
        [NonSerialized]
        public Canvas UserCanvas;
        public string ObjectName
        {
            set
            {
                _objectName = value;
                OnPropertyChanged();
            }
            get => _objectName;
        }
        public string DeathText
        {
            get { return _deathText; }
            set
            {
                _deathText = value;
                OnPropertyChanged();
            }
        }
        public string SparedText
        {
            get
            {
                return _sparedText;
            }
            set
            {
                _sparedText = value;
                OnPropertyChanged();
            }
        }
        public int SpareHP
        {
            get
            {
                return _SpareHP;
            }
            set
            {
                _SpareHP = value;
                OnPropertyChanged();
            }
        }
        public int UserAttack
        {
            set
            {
                _userAttack = value;
                OnPropertyChanged();
            }
            get => _userAttack;
        }
        public int UserHp
        {
            set
            {
                _userHp = value;
                OnPropertyChanged();
            }
            get => _userHp;
        }
        public int UserDef
        {
            set
            {
                _userDef = value;
                OnPropertyChanged();
            }
            get => _userDef;
        }
        public int RuntimeInt { get; set; } //For use by runtime not config tool
        public string XAML
        {
            set
            {
                _xaml = value;
                OnPropertyChanged();
            }
            get => _xaml;
        }
        public string XAMLName
        {
            get
            {
                return _xamlName;
            }
            set
            {
                _xamlName = value;
                OnPropertyChanged();
            }
        }
        public string ImageFileName
        {
            get
            {
                return _imageFileName;
            }
            set
            {
                _imageFileName = value;
                OnPropertyChanged();
            }
        }
        public bool HpSpare
        {
            get
            {
                return _hpSpare;
            }
            set
            {
                _hpSpare = value;
                OnPropertyChanged();
            }
        }
        public bool TextSpare
        {
            get
            {
                return _textSpare;
            }
            set
            {
                _textSpare = value;
                OnPropertyChanged();
            }
        }
        public bool NoSpare
        {
            get
            {
                return _noSpare;
            }
            set
            {
                _noSpare = value;
                OnPropertyChanged();
            }
        }
        public bool IsAlive { get; set; } = true;
        public ObservableCollection<string> TextList
        {
            get
            {
                return _textList;
            }
            set
            {
                _textList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<int> Ints
        {
            get
            {
                return _ints;
            }
            set
            {
                _ints = value;
                OnPropertyChanged();
            }
        }
        public List<string> RuntimeTextList { get; set; } = new List<string>(); //For use by runtime not config tool
        public bool TextEndReached { get; set; } //For use by runtime not config tool
        public event PropertyChangedEventHandler PropertyChanged;
        


        protected void OnPropertyChanged([CallerMemberName] string propteryName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propteryName));
        }
    }
    
}
