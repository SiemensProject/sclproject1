using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    class SCLSplit
    {
        public void SplitFile(string file)
        {
            StreamWriter writer1 = new StreamWriter("H:\\OUTPUT"+file+".txt");
            StreamWriter writer = new StreamWriter("H:\\INPUT" + file + ".txt");
            if (!File.Exists(file))
                throw new Exception("File not found....");
            StreamReader reader = new StreamReader(file);
            string str;
            int i = 0;
            while (!reader.EndOfStream)
            {
                str = reader.ReadLine();
                if(str.Contains("VAR_INPUT"))
                {
                    i = 1;
                }
                if (str.Contains("VAR_OUTPUT"))
                {
                    i = 2;
                }
                if (str.Contains("END_VAR"))
                {
                    i = 0;
                }
                if (i == 0)
                    continue;
                else if(i==1)
                {
                    //Console.WriteLine(  str);
                    writer.WriteLine(str);
                }
                else if(i==2)
                {
                    writer1.WriteLine(str);
                }
            }
            writer.Close();
            writer1.Close();
            
            reader.Close();
        }
    }
}
