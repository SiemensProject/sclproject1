using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class PropertyValueExtractor
    {/// <summary>
    /// FindPropertyKey extracts the values for properties of EDC class, creates list of EDC objects from the collection of input & output variables. 
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
            string[] dotNetDataTypes = { "System.Double", "System.Boolean","System.String","System.Int16","System.UInt32","System.Byte"};
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
                        edcId =id,
                        name = propertyKey[0].Trim(),
                        dotNetDataType = ""
                    };
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
                           DefaultValueAssigner(Contents[i+1],props, dotNetDataTypes[0],edc);
                        }
                        else if (Contents[i + 1].Contains("BOOL"))
                        {
                            DefaultValueAssigner(Contents[i + 1], props, dotNetDataTypes[1],edc);
                        }
                       else if (Contents[i + 1].Contains("AnaValFF")||Contents[i+1].Contains("AnaVal"))
                        {
                            edc.dotNetDataType = dotNetDataTypes[0];
                            props.defaultValue = "0.0";
                        }
                       else if (Contents[i + 1].Contains("DigValFF") || Contents[i + 1].Contains("DigVal"))
                        {
                            edc.dotNetDataType = dotNetDataTypes[1];
                            props.defaultValue = "false";
                        }
                        else if (Contents[i + 1].Contains("INT"))
                        {
                            DefaultValueAssigner(Contents[i + 1], props, dotNetDataTypes[3],edc);
                        }
                        else if(Contents[i+1].Contains("STRING[32]"))
                        {
                            edc.dotNetDataType = dotNetDataTypes[2];
                        }
                        else if (Contents[i + 1].Contains("BYTE"))
                        {    
                            DefaultValueAssigner_hexatodecimal(Contents[i + 1], props, dotNetDataTypes[5],edc);
                        }
                       else if (Contents[i + 1].Contains("DWORD"))
                        {
                            DefaultValueAssigner_hexatodecimal(Contents[i + 1], props, dotNetDataTypes[4],edc);
                        }
                       else if (Contents[i + 1].Contains("STRUCT"))
                        {
                            if (Contents[i + 2].Contains("REAL") )
                            {
                                DefaultValueAssigner(Contents[i + 2], props, dotNetDataTypes[0],edc);
                            }
                            if( Contents[i + 2].Contains("BOOL"))
                            { 
                                DefaultValueAssigner(Contents[i + 2], props, dotNetDataTypes[1],edc);
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
        public void DefaultValueAssigner(string line,Property props,string dotNetdataType,EDC edc)
        {
            edc.dotNetDataType = dotNetdataType;
            string[] dvalue =line.Split('=');
            string[] values = dvalue[1].Split(';');
            props.defaultValue = values[0];
        }
        public void DefaultValueAssigner_hexatodecimal(string line, Property props, string dotNetdataType, EDC edc)
        {
            edc.dotNetDataType = dotNetdataType;
            string[] dvalue = line.Split('=');
            string[] values = dvalue[1].Split(';');
            string[] hex_value = values[0].Split('#');  
            long int_value = Convert.ToInt64(hex_value[1], 16); //logic to convert from hexa to decimal
            props.defaultValue = int_value.ToString();
        }
    }
}
