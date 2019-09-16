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
    {/// <summary>
    /// "WriteCSV" is a method to write list of EDC objects into CSV file.
    /// "WriteJson" is a method to serialize the list of objects and write into an indented json file.
    /// 
    /// </summary>
    /// <param name="OutputFileLocation">OutputFileLocation is a user selected location to store created output files. </param>
    /// <param name="Filename">Filename is the name given for the result file by the user.</param>
    /// <param name="Keys">Keys is the list of objects of type EDC</param>
        public void WriteCSV(string OutputFileLocation,string Filename,List<EDC>Keys)
        {
            StreamWriter writer = new StreamWriter(OutputFileLocation + "\\" + Filename);
            writer.WriteLine("Id,EDCId,Name,dotNetDataType,hmiVisible,SignalStatus,Address,DefaultValue,LoggingEnabled");
            foreach (var item in Keys)
            {
                writer.WriteLine(item.id + "," + item.edcId + "," + item.name + "," + item.dotNetDataType + "," + item.hmiVisible + "," + item.signalStatus + "," + item.props.address + "," + item.props.defaultValue + "," + item.props.loggingEnabled);
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
