using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using ConsoleApp2;

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
        public void WriteJson(string OutputFileLocation, string Filename, List<EDC> Keys,string name)
        {           
            StreamWriter writer2 = new StreamWriter(OutputFileLocation + "\\" + Filename);
            writer2.WriteLine("{\r\n" +
  "\"id\":" + "\""+ name +"_FB\",\r\n" +
  "\"type\":" + "\"OsInterface_POT\",\r\n" +
  "\"props\": {\r\n" +
                "\"typeId\": \"\"\r\n" +
 " },\r\n" +
  "\"members\":[\r\n{" +
      "\r\n\"id\": 2,\r\n" +
      "\"edcId\": \"\",\r\n" +
      "\"name\": \"AlarmState\",\r\n" +
      "\"dotNetDataType\": \"System.String[]\",\r\n" +
      "\"hmiVisible\":\" false\",\r\n" +

      "\"props\": {\r\n " +
                    "\"address\":\"\",\r\n" +
        "\"signalName\":\"\",\r\n" +
        "\"defaultValue\": [\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"\",\r\n" +
          "\"\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
         "\"0\",\r\n" +
          "\"\",\r\n" +
           "\"0\",\r\n" +
          "\"0\",\r\n" +
          "\"0\",\r\n" +
           "\"0\",\r\n" +
          "\"\"\r\n" +
       "],\r\n" +
        "\"loggingEnabled\": false\r\n" +
      "}\r\n" +
"},"
  );
            var json = JsonConvert.SerializeObject(Keys);
            var obj = JsonConvert.DeserializeObject(json);
            var f = JsonConvert.SerializeObject(obj, Formatting.Indented);
            writer2.WriteLine(f.TrimStart('['));
            writer2.WriteLine("}");
            writer2.Close();

        }
    }
}
