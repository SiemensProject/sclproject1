using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// This class holds the json structure for the actual ecd object with property name props
    /// </summary>
   public class Property
    {
        public string address { get; set; }
        public string defaultValue { get; set; }
        public bool loggingEnabled { get; set; }
    }
}
