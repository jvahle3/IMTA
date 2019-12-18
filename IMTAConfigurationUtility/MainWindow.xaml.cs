using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IMTA;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
namespace IMTAConfigurationUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BinaryFormatter bin = new BinaryFormatter();
        private IList<UserObject> _userObjects = new ObservableCollection<UserObject>();
        public MainWindow()
        {
            InitializeComponent();
            _userObjects.Add(new UserObject { ObjectName = "newObject" });
            Selector.ItemsSource = _userObjects;
        }

        private IList<UserObject> ToSerialArray(IList<UserObject> list)
        {
            IList<UserObject> userObjects = new List<UserObject>();
            foreach (UserObject userObject in list)
            {
                userObjects.Add(new UserObject
                {
                    ObjectName = userObject.ObjectName,
                    UserAttack = userObject.UserAttack,
                    UserDef = userObject.UserDef,
                    UserHp = userObject.UserHp,
                    ImageFileName = userObject.ImageFileName,
                    Ints = userObject.Ints,
                    TextList = userObject.TextList,
                    XAML = userObject.XAML,
                    XAMLName = userObject.XAMLName,
                    DeathText = userObject.DeathText
                });
            }
            return userObjects;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _userObjects?.Add(new UserObject { ObjectName = "newObject" });
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Select data file",
                InitialDirectory = Environment.CurrentDirectory
            };
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == false) return;
            using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bin.Serialize(fs, ToSerialArray(_userObjects));
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select data file",
                InitialDirectory = Environment.CurrentDirectory
            };
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == false) return;
            using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open)) 
            {
                List<UserObject> obj = (List<UserObject>)bin.Deserialize(fs);
                _userObjects = new ObservableCollection<UserObject>(obj);
                Selector.ItemsSource = _userObjects;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem == null)
            {
                return;
            }
            else
            {
                _userObjects.RemoveAt(Selector.SelectedIndex);
            }

        }

        private void FileButtonImage_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem == null) return;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Image file",
                InitialDirectory = Environment.CurrentDirectory
            };
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == false) return;
            UserObject tmpuser = (UserObject)Selector.SelectedItem;
            if (tmpuser == null) return;
            tmpuser.ImageFileName = openFileDialog.FileName;

        }

        private void FileButtonAnimation_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem == null) return;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select XAML file",
                InitialDirectory = Environment.CurrentDirectory
            };
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == false) return;
            using (FileStream fileStream = File.OpenRead(openFileDialog.FileName))
            {
                StreamReader sr = new StreamReader(fileStream);
                string s = sr.ReadToEnd();
                UserObject us = (UserObject)Selector.SelectedItem;
                us.XAML = s;
                AnimationFileName.Text = openFileDialog.FileName;
                us.XAMLName = openFileDialog.FileName;
                sr.Close();
            }
        }
        private void AddText_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem == null) return;
            UserObject us = (UserObject)Selector.SelectedItem;
            us.Ints.Add(us.Ints.Count + 1);
            us.TextList.Add("");
        }

        private void SaveText_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem == null) return;
            if (textComboBox.SelectedItem == null) return;
            int i = textComboBox.SelectedIndex;
            UserObject us = (UserObject)Selector.SelectedItem;
            us.TextList.Insert(i, TextB.Text);
        }
        private void TextComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Selector.SelectedItem == null) return;
            if (textComboBox.SelectedItem == null) return;
            int i = textComboBox.SelectedIndex;
            UserObject us = (UserObject)Selector.SelectedItem;
            TextB.Text = us.TextList.ElementAt(i);
        }

        private void RemoveText_Click(object sender, RoutedEventArgs e)
        {
            if (textComboBox.SelectedItem == null || Selector.SelectedItem == null) return;
            UserObject us = (UserObject)Selector.SelectedItem;
            int i = textComboBox.SelectedIndex;
            us.TextList.RemoveAt(i);
            var a = us.Ints.Last();
            us.Ints.Remove(a);
            textComboBox.SelectedItem = null;
        }

        private void DeathTextSave_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.SelectedItem == null) return;
            UserObject us = (UserObject)Selector.SelectedItem;
            us.DeathText = DeathTextBox.Text;
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Selector.SelectedItem == null) return;
            UserObject us = (UserObject) Selector.SelectedItem;
            DeathTextBox.Text = us.DeathText;
        }
    }
}