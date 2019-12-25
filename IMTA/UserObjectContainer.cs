using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace IMTA
{
    /// <summary>
    /// This class will hold a property of the user object used to define the behavior of the runtime.
    /// It does nothing but load the property on start up.
    /// If it can't find the property to load it will instead institate a new UserObject with default values
    /// </summary>
    public static class UserObjectContainer
    {
        public static readonly IList<UserObject> UOBJ;
        static UserObjectContainer()
        {
            try
            {
                using (Stream stream = File.OpenRead(@"C:\Users\jvahle3\source\repos\IMTA\IMTA\bin\Debug\UserObject.dat"))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    UOBJ = (List<UserObject>) binaryFormatter.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
            {
                UOBJ = new List<UserObject> { new UserObject { UserHp = 100, UserAttack = 10, UserDef = 10 } };
            }
        }
    }
}
