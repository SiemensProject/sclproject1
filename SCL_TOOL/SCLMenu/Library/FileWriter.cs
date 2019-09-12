using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class FileWriter
    {
        public void WriteCSV(string OutputFileLocation,string Filename,List<string>Keys)
        {
            StreamWriter writer = new StreamWriter(OutputFileLocation + "\\" + Filename);
            foreach (var item in Keys)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }
    }
}
