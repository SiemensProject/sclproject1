using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
   public class SCL_FileExtractor
    {
        public Dictionary<string, List<string>> CreateInputVarOutputVarFiles(string filePath)
        {
           
            List<string> InputContents = new List<string>();
            List<string> OutputContents = new List<string>();
            Dictionary<string, List<string>> IODictionary = new Dictionary<string, List<string>>();
            if (!File.Exists(filePath))
                throw new Exception("File not found....");
            StreamReader reader = new StreamReader(filePath);
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
                    
                    InputContents.Add(str);
                }
                else if(i==2)
                {
                   
                    OutputContents.Add(str);
                }
            }
            IODictionary.Add("INPUT", InputContents);
            IODictionary.Add("OUTPUT", OutputContents);
            reader.Close();
            return IODictionary;
        }
    }
}
