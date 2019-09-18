using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleApp2;
using System.IO;
using Library;

namespace SCLMenu
{
    /// <summary>
    ///Form1 is the UI where user can select source file. UI contains option to select  output format.
    ///It also allows user to select output target folder.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Capture the input file location which is selected by the user
        /// </summary>
        public string InputFileLocation { get; set; }

        /// <summary>
        /// Capture the output file location where user wants to store the output contents after converting to XLS or JSON formats
        /// </summary>
        public string OutputFileLocation { get; set; }

        /// <summary>
        /// Input file properties key holder
        /// </summary>
        public string PropertyKey { get; set; }

        /// <summary>
        /// Input file properties value holder
        /// </summary>
        public string PropertyValue { get; set; }

        /// <summary>
        /// Holds the collection of property values of the processed input file 
        /// </summary>
        public List<EDC> LstEDc = new List<EDC>();

        /// <summary>
        /// Helper collection for holder the intermediate properties scanned within var_input and var_output
        /// </summary>
        public Dictionary<string, List<string>> Source_InputOutputContents;

    /// <summary>
    /// contains a property called "name " of input file
    /// </summary>
        public string propertyName { get; set; }
        /// <summary>
        /// Default Constructor initializing with Combo box
        /// </summary>   
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("S7_m_c");
            comboBox2.Items.Add("true");
            comboBox2.Items.Add("false");
        }
        /// <summary>
        /// Method for retrieving selected item(Key) in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PropertyKey = comboBox1.SelectedItem.ToString();
            PropertyKey = comboBox1.Text;
        }
        /// <summary>
        /// Method allows user to browse the input file.
        /// It calls SCL_FileExtractor.GetNamePropertypValue() to extract input and output variables from the input file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            // Prompt user to select input SCL file 
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.ShowDialog();

            // Copy absolute path of the SCL file selected by user
            InputFileLocation = openFileDialog1.FileName;
            textBox1.Text = InputFileLocation;
            try
            {
                // Read SCL file and populate input and output parameter contents for filtering the expected output 
                Source_FileExtractor srcFileExtractor = new Source_FileExtractor();
                propertyName = srcFileExtractor.GetNamePropertypValue(InputFileLocation);
                Source_InputOutputContents = srcFileExtractor.GetInputVarOutputVarContents(InputFileLocation);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File Not Found !!!");
            }
            //Extracts required fields from the input and ouput parameter list
           
        }
        /// <summary>
        /// Method for retrieving selected item(Value) in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //To select value
            // PropertyValue = comboBox2.SelectedItem.ToString();
            PropertyValue = comboBox2.Text;
        }
       /// <summary>
       /// Generating output file (csv or json)
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string[] FilepathSplit=InputFileLocation.Split('\\');
            string FileName = FilepathSplit[FilepathSplit.Length - 1];
            //Executes when output file format is Xl and input file is a .AWL File
          if(checkBox2.Checked && FileName.Contains(".AWL"))
            {
                PropertyValueExtractorForAWL extractor = new PropertyValueExtractorForAWL();
                LstEDc = extractor.FindPropertyKey(Source_InputOutputContents, PropertyKey, PropertyValue);
                DialogResult result;
                if (!File.Exists(OutputFileLocation + "\\names1.csv"))
                {
                    FileWriter writer = new FileWriter();
                    writer.WriteCSV(OutputFileLocation, "names1.csv", LstEDc);
                    MessageBox.Show("Created  " + OutputFileLocation + "\\names1.csv");
                }
                else
                {
                    result = MessageBox.Show("Do you want to create a new file for XL?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {

                        FileWriter writer = new FileWriter();
                        writer.WriteCSV(OutputFileLocation, "names1.csv", LstEDc);
                        MessageBox.Show("Created  " + OutputFileLocation + "\\names1.csv");
                    }
                    else
                    {
                        string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default", -1, -1);
                        FileWriter writer = new FileWriter();
                        writer.WriteCSV(OutputFileLocation, f, LstEDc);
                        MessageBox.Show("Created " + OutputFileLocation + "\\" + f);

                    }
                }
            }
            //Executes when output file format is JSON and input file is a .AWL File
            if (checkBox1.Checked && FileName.Contains(".AWL"))
            {
                PropertyValueExtractorForAWL extractor = new PropertyValueExtractorForAWL();
                LstEDc = extractor.FindPropertyKey(Source_InputOutputContents, PropertyKey, PropertyValue);
                string outputFileName = "\\names1.json"; // Final output json file name
                DialogResult result; // To display message box to the user for prompting if file has been created before in the previous execution

                // Check if the result json file already is present
                if (!File.Exists(OutputFileLocation + outputFileName))
                {
                    FileWriter writer = new FileWriter();
                    writer.WriteJson(OutputFileLocation, "names1.json", LstEDc, propertyName);
                    MessageBox.Show("Created " + OutputFileLocation + "\\names1.json");
                }
                else
                {
                    result = MessageBox.Show("Do you want to create a new file for JSON?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {

                        FileWriter writer = new FileWriter();
                        writer.WriteJson(OutputFileLocation, "names1.json", LstEDc, propertyName);
                        MessageBox.Show("Created  " + OutputFileLocation + "\\names1.json");
                    }
                    else
                    {
                        string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default.json", -1, -1);
                        FileWriter writer = new FileWriter();
                        writer.WriteJson(OutputFileLocation, f, LstEDc, propertyName);
                        MessageBox.Show("Created  " + OutputFileLocation + "\\" + f);

                    }
                }
            }
            //Executes when output file format is JSON and input file is a .SCL File
            if (checkBox1.Checked && FileName.Contains(".SCL"))
            {
                PropertyValueExtractorForSCL extractor = new PropertyValueExtractorForSCL();
                LstEDc = extractor.FindPropertyKey(Source_InputOutputContents, PropertyKey, PropertyValue);
                string outputFileName = "\\names1.json"; // Final output json file name
                DialogResult result; // To display message box to the user for prompting if file has been created before in the previous execution

                // Check if the result json file already is present
                if (!File.Exists(OutputFileLocation + outputFileName))
                {
                    FileWriter writer = new FileWriter();
                    writer.WriteJson(OutputFileLocation, "names1.json", LstEDc, propertyName);
                    MessageBox.Show("Created " + OutputFileLocation + "\\names1.json");
                }
                else
                {
                    result = MessageBox.Show("Do you want to create a new file for JSON?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {

                        FileWriter writer = new FileWriter();
                        writer.WriteJson(OutputFileLocation, "names1.json", LstEDc, propertyName);
                        MessageBox.Show("Created  " + OutputFileLocation + "\\names1.json");
                    }
                    else
                    {
                        string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default.json", -1, -1);
                        FileWriter writer = new FileWriter();
                        writer.WriteJson(OutputFileLocation, f, LstEDc, propertyName);
                        MessageBox.Show("Created  " + OutputFileLocation + "\\" + f);

                    }
                } 
            }
            //Executes when output file format is Xl and input file is a .SCL File
            if (checkBox2.Checked && FileName.Contains(".SCL"))
            {
                PropertyValueExtractorForSCL extractor = new PropertyValueExtractorForSCL();
                LstEDc = extractor.FindPropertyKey(Source_InputOutputContents, PropertyKey, PropertyValue);
                DialogResult result;
                if (!File.Exists(OutputFileLocation + "\\names1.csv"))
                {
                    FileWriter writer = new FileWriter();
                    writer.WriteCSV(OutputFileLocation, "names1.csv", LstEDc);
                    MessageBox.Show("Created  " + OutputFileLocation + "\\names1.csv");
                }
                else
                {
                    result = MessageBox.Show("Do you want to create a new file for XL?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {

                        FileWriter writer = new FileWriter();
                        writer.WriteCSV(OutputFileLocation, "names1.csv", LstEDc);
                        MessageBox.Show("Created  " + OutputFileLocation + "\\names1.csv");
                    }
                    else
                    {
                        string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default", -1, -1);
                        FileWriter writer = new FileWriter();
                        writer.WriteCSV(OutputFileLocation, f, LstEDc);
                        MessageBox.Show("Created " + OutputFileLocation + "\\" + f);

                    }
                }
            }       

        }
        private void SelectFolder_click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            OutputFileLocation = folderBrowserDialog1.SelectedPath;
            textBox2.Text = OutputFileLocation;
        }
    }
}
