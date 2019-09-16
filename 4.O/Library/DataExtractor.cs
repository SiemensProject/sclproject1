using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DataExtractor
    {/// <summary>
    /// FindPropertyKey extracts the values for properties of EDC class, creates list of EDC objects from the collection of input & output variables.
    /// 
    /// </summary>
    /// <param name="EDCs">EDCs dictionary containing collections of input & output variables.</param>
    /// <param name="key">key is the property selected by the user.</param>
    /// <param name="value">value is the value of the selected property</param>
    /// <returns></returns>
        public List<EDC> FindPropertyKey(Dictionary<string,List<string>>EDCs, string key, string value)
        {
            string[] propertyKey;
            List<EDC> LstEDCs = new List<EDC>();
            int id = 2;
            List<string> Contents = EDCs["INPUT"];
            Contents.AddRange(EDCs["OUTPUT"]);
            for(int i=0;i<Contents.Count();i++)
            {
                ///Logic to check whether the variable has specified key value pair.
                if (Contents[i].Contains(key + ":=" + "'" + value + "'") || Contents[i].Contains(key + " :=" + "'" + value + "'") || Contents[i].Contains(key + ":= " + "'" + value + "'") || Contents[i].Contains(key + " := " + "'" + value + "'"))
                {
                    id = id + 1;
                    propertyKey = Contents[i].Split('{');
                    Property props = new Property
                    {
                        address = "",
                        defaultValue="",
                        loggingEnabled = false
                    };
                    EDC edc = new EDC
                    {
                        id = id,
                        edcId = id,
                        name = propertyKey[0].Trim(),
                        dotNetDataType = ""
                        
                    };
                    ///
                    /// Logic to write value of property "hmiVisible" 
                    if (Contents[i].Contains("S7_visible:='false'")|| Contents[i].Contains("S7_visible :='false'")|| Contents[i].Contains("S7_visible:= 'false'")|| Contents[i].Contains("S7_visible := 'false'"))
                    {
                        edc.hmiVisible = false;
                    }
                    else
                    {
                        edc.hmiVisible = true;
                    }
                    //Logic to write value of "signalStatus" property 
                    if (Contents[i].Contains("S7_xm_c:='Value,true;'")|| Contents[i].Contains("S7_xm_c :='Value,true;'")|| Contents[i].Contains("S7_xm_c:= 'Value,true;'")|| Contents[i].Contains("S7_xm_c := 'Value,true;'"))
                    {
                        edc.signalStatus = true;
                    }
                    else
                    {
                        edc.signalStatus = false;
                    }
                    //Logic to extract "defaultvalue " and "dotNetDataType" from SCL file
                    try {
                        if (Contents[i + 1].Contains("REAL"))
                        {
                            edc.dotNetDataType = "System.Double";
                            string[] dvalue = Contents[i + 1].Split('=');
                            string[] values = dvalue[1].Split(';');
                            props.defaultValue = values[0];
                        }
                        else if (Contents[i + 1].Contains("BOOL"))
                        {
                            edc.dotNetDataType = "System.Boolean";
                            string[] dvalue = Contents[i + 1].Split('=');
                            string[] values = dvalue[1].Split(';');
                            props.defaultValue = values[0];
                        }
                       else if (Contents[i + 1].Contains("AnaValFF")||Contents[i+1].Contains("AnaVal"))
                        {
                            edc.dotNetDataType = "System.Double";
                            props.defaultValue = "0.0";
                        }
                       else if (Contents[i + 1].Contains("DigValFF") || Contents[i + 1].Contains("DigVal"))
                        {
                            edc.dotNetDataType = "System.Boolean";
                            props.defaultValue = "false";
                        }
                        else if (Contents[i + 1].Contains("INT"))
                        {
                            edc.dotNetDataType = "System.Int16";
                            string[] dvalue = Contents[i + 1].Split('=');
                            string[] values = dvalue[1].Split(';');
                            props.defaultValue = values[0];
                        }
                        else if(Contents[i+1].Contains("STRING[32]"))
                        {
                            edc.dotNetDataType = "System.String";
                            //props.defaultValue = "";
                        }
                        else if (Contents[i + 1].Contains("BYTE"))
                        {
                            edc.dotNetDataType = "System.Byte";
                            //logic to convert from hexa to decimal
                            string[] dvalue = Contents[i + 1].Split('=');
                            string[] values = dvalue[1].Split(';');
                            string[] hex_value = values[0].Split('#');
                            int int_value = Convert.ToInt32(hex_value[1],16);
                            props.defaultValue = int_value.ToString();
                        }
                       else if (Contents[i + 1].Contains("DWORD"))
                        {
                            edc.dotNetDataType = "System.UInt32";
                            //logic to convert from hexa to decimal
                            string[] dvalue = Contents[i + 1].Split('=');
                            string[] values = dvalue[1].Split(';');
                            string[] hex_value = values[0].Split('#');
                            Decimal int_value = Convert.ToInt32(hex_value[1],16);
                            props.defaultValue = int_value.ToString();
                        }
                       else if (Contents[i + 1].Contains("STRUCT"))
                        {
                            if (Contents[i + 2].Contains("REAL") )
                            {
                                edc.dotNetDataType = "System.Double";
                                string[] dvalue = Contents[i + 2].Split('=');
                                string[] values = dvalue[1].Split(';');
                                props.defaultValue = values[0];
                            }
                            if( Contents[i + 2].Contains("BOOL"))
                            {
                                edc.dotNetDataType = "System.Boolean";
                                string[] dvalue = Contents[i + 2].Split('=');
                                string[] values = dvalue[1].Split(';');
                                props.defaultValue = values[0];
                            }
                        }
                    }catch(Exception)
                    {
                        props.defaultValue = "0.0";
                    }
                    edc.props = props;
                    LstEDCs.Add(edc);
                }  
            }
            return LstEDCs;
        }
    }
}
