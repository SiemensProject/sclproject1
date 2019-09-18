using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    public class SCL_FileExtractor
    {
        /// <summary>
        /// GetInputVarOutputVarContents method takes the SCL filePath selected by the user, creates collection of input and output variables.
        /// It also strores the collections as values for keys "INPUT" & "OUTPUT" in the IODictionary and returns the same.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetInputVarOutputVarContents(string filePath)
        {
            // to populate input property contents
            List<string> InputContents = new List<string>(); 

            // to populate input property contents
            List<string> OutputContents = new List<string>(); 

            // Create a key value pairing of input and output parameters contents
            Dictionary<string, List<string>> IODictionary = new Dictionary<string, List<string>>();

            // Ensuring the user has selected a file
            ///<exception cref="FileNotFoundException">Ensuring the user has selected a file</exception>
            if (!File.Exists(filePath))
                throw new Exception("File not found....");

            // Read SCL file logic
            StreamReader reader = new StreamReader(filePath);
            string str = string.Empty;
            int i = 0;
            while (!reader.EndOfStream) // Read until file reaches end of file
            {
                str = reader.ReadLine(); // read a line
                if (str.Contains("VAR_INPUT")) // check if the line containts VAR_INPUT in SCL file
                {
                    i = 1;
                }
                if (str.Contains("VAR_OUTPUT")) // check if the line containts VAR_OUTPUT in SCL file
                {
                    i = 2;
                }
                if (str.Contains("END_VAR")) // check if the line containts VAR_INPUT in SCL file
                {
                    i = 0;
                }
                if (i == 0)
                    continue;
                else if (i == 1)
                {

                    InputContents.Add(str);
                }
                else if (i == 2)
                {

                    OutputContents.Add(str);
                }
            }
            IODictionary.Add("INPUT", InputContents);
            IODictionary.Add("OUTPUT", OutputContents);
            reader.Close();
            return IODictionary;
        }
        public string GetNamePropertypValue(string filePath)
        {
            // Helper variable that holds current line on stream read operation
            string currentLine = string.Empty;

            // Helper variable to hold the trim value of name property
            string[] nameSplit = null;

            // Read the file until end of file
            StreamReader reader = new StreamReader(filePath);
            while (!reader.EndOfStream) 
            {
                currentLine = reader.ReadLine();

                // Check if current line contains property name
                if (currentLine.Contains("NAME"))
                {
                    nameSplit = currentLine.Split(':'); 
                    currentLine = nameSplit[1].Trim(); 
                    break; // Found the name property and hence terminating the loop
                }
            }
            return currentLine;
        }
    }
}
