using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class EDC
    {
        public int Id { get; set; }
        public int EDCId { get; set; }
        public string Name { get; set; }
        public string dotNetDataType { get; set; }
        public bool hmiVisible { get; set; }
        public bool signalStatus { get; set; }
        public Property Props = new Property();
    }
}
