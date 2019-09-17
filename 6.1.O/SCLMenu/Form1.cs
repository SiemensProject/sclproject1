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
    public partial class Form1 : Form
    {
        public string FileToBeSearched { get; set; }
        public string OutputFileLocation { get; set; }
        public string PropertyKey { get; set; }
        public string PropertyValue { get; set; }
        public List<EDC> LstEDc = new List<EDC>();
        public Dictionary<string, List<string>> SCL_InputOutputContents;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("S7_m_c");
            //string[] keys = { "e", "f", "g" };
            //comboBox1.Items.AddRange(keys);
            comboBox2.Items.Add("true");
            comboBox2.Items.Add("false");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyKey = comboBox1.SelectedItem.ToString();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            // Prompt user to select input SCL file 
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.ShowDialog();

            // Copy absolute path of the SCL file selected by user
            FileToBeSearched = openFileDialog1.FileName;
            try
            {
                // Read SCL file and populate input and output parameter contents for filtering the expected output 
                SCL_FileExtractor sclFileExtractor = new SCL_FileExtractor();
                SCL_InputOutputContents = sclFileExtractor.GetInputVarOutputVarContents(FileToBeSearched);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File Not Found !!!");
            }
            //Extracts required fields from the input and ouput parameter list
            PropertyValueExtractor extractor = new PropertyValueExtractor();
            LstEDc = extractor.FindPropertyKey(SCL_InputOutputContents, PropertyKey, PropertyValue);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //To select value
            PropertyValue = comboBox2.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Wr
            DialogResult result;
            if (!File.Exists(OutputFileLocation + "\\names1.csv"))
            {
                FileWriter writer = new FileWriter();
                writer.WriteCSV(OutputFileLocation, "names1.csv", LstEDc);
                MessageBox.Show("Created  " + OutputFileLocation + "\\names1.csv");
            }
            else
            {
                result = MessageBox.Show("Do you want to create a new file?", "Confirmation", MessageBoxButtons.YesNo);

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            OutputFileLocation = folderBrowserDialog1.SelectedPath;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        // When the user selects json file to be created this method is called
        private void btnJson_Click(object sender, EventArgs e)
        {
            string outputFileName = "\\names1.json"; // Final output json file name
            DialogResult result; // To display message box to the user for prompting if file has been created before in the previous execution

            // Check if the result json file already is present
            if (!File.Exists(OutputFileLocation + outputFileName))
            {
                FileWriter writer = new FileWriter();
                writer.WriteJson(OutputFileLocation, "names1.json", LstEDc);
                MessageBox.Show("Created " + OutputFileLocation + "\\names1.json");
            }
            else
            {
                result = MessageBox.Show("Do you want to create a new file?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {

                    FileWriter writer = new FileWriter();
                    writer.WriteJson(OutputFileLocation, "names1.json", LstEDc);
                    MessageBox.Show("Created  " + OutputFileLocation + "\\names1.json");
                }
                else
                {
                    string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default.json", -1, -1);
                    FileWriter writer = new FileWriter();
                    writer.WriteJson(OutputFileLocation, f, LstEDc);
                    MessageBox.Show("Created  " + OutputFileLocation + "\\" + f);

                }
            }

        }
    }
}
