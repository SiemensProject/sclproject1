using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp2
{
    public class VariableName
    {
        public void GetVariables(string filename, string key, string value,string file)
        {
            string str;
            StreamWriter wr = new StreamWriter(file,true);
            StreamReader ireader = new StreamReader(filename);
            string[] words;
            while (!ireader.EndOfStream)
            {
                str = ireader.ReadLine();
                if (str.Contains(key+":="+"'"+value+"'")|| str.Contains(key + " :=" + "'" + value + "'") ||str.Contains(key + ":= " + "'" + value + "'") ||str.Contains(key + " := " + "'" + value + "'"))
                {
                    words = str.Split('{');
                    // Console.WriteLine(words[0]);
                    wr.WriteLine(words[0]);
                }
            }
            wr.Close();
        }
    }
}