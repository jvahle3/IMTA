using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;

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
        private string _deathText;
        private string _xaml = null;
        private string _xamlName;
        private string _imageFileName;
        private ObservableCollection<string> _textList = new ObservableCollection<string>();
        private ObservableCollection<int> _ints = new ObservableCollection<int>();
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propteryName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propteryName));
        }
    }
    
}
