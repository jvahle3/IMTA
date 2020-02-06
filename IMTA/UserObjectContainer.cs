using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;

namespace IMTA
{
    /// <summary>
    /// This class will hold a property of the user object used to define the behavior of the runtime.
    /// It does nothing but load the property on start up.
    /// If it can't find the property to load it will instead institate a new UserObject with default values
    /// </summary>
    public static class UserObjectContainer
    {
        public static readonly List<UserObject> UOBJ;
        static UserObjectContainer()
        {
            AppSettingsReader ASR = new AppSettingsReader();
            string _location = (string)ASR.GetValue("UserObjectLocation", typeof(string));
            try
            {
                using (Stream stream = File.OpenRead(_location))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    UOBJ = (List<UserObject>)binaryFormatter.Deserialize(stream);
                    foreach(UserObject us in UOBJ)
                    {
                        foreach(string s in us.TextList )
                        {
                            if (s.Equals(string.Empty)) continue;
                            us.RuntimeTextList.Add(s);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                UOBJ = new List<UserObject> { new UserObject { UserHp = 100, UserAttack = 10, UserDef = 10 } };
            }
        }
    }
}
