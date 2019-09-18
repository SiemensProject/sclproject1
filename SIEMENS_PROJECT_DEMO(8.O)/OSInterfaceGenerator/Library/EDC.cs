using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// This class represents the json required to be converted as per SCL file for EDC
    /// </summary>
    public class EDC
    {
        public int id { get; set; }
        public int edcId { get; set; }
        public string name { get; set; }
        public string dotNetDataType { get; set; }
        public bool hmiVisible { get; set; }
        public bool signalStatus { get; set; }
        public Property props { get; set; }
    }
}
