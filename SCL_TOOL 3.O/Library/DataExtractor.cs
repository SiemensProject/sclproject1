using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DataExtractor
    {
        public List<EDC> FindPropertyKey(Dictionary<string,List<string>>EDCs, string key, string value)
        {
            string[] propertyKey;
            List<EDC> LstEDCs = new List<EDC>();
            int id = 2;
            List<string> Contents = EDCs["INPUT"];
            Contents.AddRange(EDCs["OUTPUT"]);
            for(int i=0;i<Contents.Count();i++)
            {

                if (Contents[i].Contains(key + ":=" + "'" + value + "'") || Contents[i].Contains(key + " :=" + "'" + value + "'") || Contents[i].Contains(key + ":= " + "'" + value + "'") || Contents[i].Contains(key + " := " + "'" + value + "'"))
                {
                    id = id + 1;
                    propertyKey = Contents[i].Split('{');
                    Property props = new Property
                    {
                        Address = "",
                        DefaultValue="0.0",
                        LoggingEnabled = false
                    };
                    EDC edc = new EDC
                    {
                        Id = id,
                        EDCId = id,
                        Name = propertyKey[0].Trim(),
                        dotNetDataType = ""
                        
                    };
                    //hmivisible logic
                    if (Contents[i].Contains("S7_visible:='false'")|| Contents[i].Contains("S7_visible :='false'")|| Contents[i].Contains("S7_visible:= 'false'")|| Contents[i].Contains("S7_visible := 'false'"))
                    {
                        edc.hmiVisible = false;
                    }
                    else
                    {
                        edc.hmiVisible = true;
                    }
                    //signal status logic
                    if (Contents[i].Contains("S7_xm_c:='Value,true;'")|| Contents[i].Contains("S7_xm_c :='Value,true;'")|| Contents[i].Contains("S7_xm_c:= 'Value,true;'")|| Contents[i].Contains("S7_xm_c := 'Value,true;'"))
                    {
                        edc.signalStatus = true;
                    }
                    else
                    {
                        edc.signalStatus = false;
                    }
                    //defaultvalue finding logic
                    try {
                        if (Contents[i + 1].Contains("REAL") || Contents[i + 1].Contains("BOOL"))
                        {
                            string[] dvalue = Contents[i + 1].Split('=');
                            string[] values = dvalue[1].Split(';');
                            props.DefaultValue = values[0];
                        }
                        if (Contents[i + 1].Contains("STRUCT"))
                        {
                            if (Contents[i + 2].Contains("REAL") || Contents[i + 2].Contains("BOOL"))
                            {
                                string[] dvalue = Contents[i + 2].Split('=');
                                string[] values = dvalue[1].Split(';');
                                props.DefaultValue = values[0];
                            }
                        }
                    }catch(Exception)
                    {
                        props.DefaultValue = "0.0";
                    }
                    edc.Props = props;
                    LstEDCs.Add(edc);
                }  
            }
            return LstEDCs;
        }
    }
}
