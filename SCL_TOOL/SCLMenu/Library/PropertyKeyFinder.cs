using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp2
{
    public class PropertyKeyFinder
    {
        public List<string> FindPropertyKey( List<string> Contents, string key, string value)
        {
            string[] propertyKey;
            List<string> LstPropKey=new List<string>();
            foreach (string currentStr in  Contents)
            {
                
                if (currentStr.Contains(key+":="+"'"+value+"'")|| currentStr.Contains(key + " :=" + "'" + value + "'") ||currentStr.Contains(key + ":= " + "'" + value + "'") ||currentStr.Contains(key + " := " + "'" + value + "'"))
                {
                    propertyKey = currentStr.Split('{');
                    LstPropKey.Add(propertyKey[0]);
                }
            }
            return LstPropKey;
        }
    }
}