using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library
{
    public class FileWriter
    {
        public void WriteCSV(string OutputFileLocation,string Filename,List<EDC>Keys)
        {
            StreamWriter writer = new StreamWriter(OutputFileLocation + "\\" + Filename);
            writer.WriteLine("Id,EDCId,Name,dotNetDataType,hmiVisible,SignalStatus,Address,DefaultValue,LoggingEnabled");
            foreach (var item in Keys)
            {
                writer.WriteLine(item.Id + "," + item.EDCId + "," + item.Name + "," + item.dotNetDataType + "," + item.hmiVisible + "," + item.signalStatus + "," + item.Props.Address + "," + item.Props.DefaultValue + "," + item.Props.LoggingEnabled);
            }
            writer.Close();
        }
        public void WriteJson(string OutputFileLocation, string Filename, List<EDC> Keys)
        {
            var json = JsonConvert.SerializeObject(Keys);
            StreamWriter writer2 = new StreamWriter(OutputFileLocation + "\\" + Filename);
            var obj = JsonConvert.DeserializeObject(json);
            var f = JsonConvert.SerializeObject(obj, Formatting.Indented);
            writer2.WriteLine(f);
            writer2.Close();

        }
    }
}
