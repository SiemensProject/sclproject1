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
        public string FileToBeSearched {get; set; }
        public string OutputFileLocation { get; set; }
        public string PropertyKey { get; set; }
        public string PropertyValue { get; set; }
        public List<string> IKeys = new List<string>();
        public List<string> OKeys = new List<string>();
        public Dictionary<string, List<string>> IODictionary;
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("S7_m_c");
            string[] keys = {"e", "f","g"};
            comboBox1.Items.AddRange(keys);
            comboBox2.Items.Add("true");
            comboBox2.Items.Add("false");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyKey = comboBox1.SelectedItem.ToString();
        }
        
        private void BtnSelect_Click(object sender, EventArgs e)
        {
           
                this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.ShowDialog();
                FileToBeSearched=openFileDialog1.FileName;
                try
                {
                       SCL_FileExtractor s = new SCL_FileExtractor();
                        IODictionary=s.CreateInputVarOutputVarFiles(FileToBeSearched);
                }
                catch(FileNotFoundException)
                {
                MessageBox.Show("File Not Found !!!");
                }
            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyValue = comboBox2.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            PropertyKeyFinder v = new PropertyKeyFinder();
            if (!File.Exists(OutputFileLocation+"\\names1.xls"))
            {
                IKeys = v.FindPropertyKey(IODictionary["INPUT"], PropertyKey, PropertyValue);
                FileWriter writer=new FileWriter();
                writer.WriteCSV(OutputFileLocation, "names1.xls", IKeys);
                MessageBox.Show("Created INPUT_XL in " + OutputFileLocation);
            }
            else
            {
                result = MessageBox.Show("Do you want to create a new file?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    IKeys = v.FindPropertyKey(IODictionary["INPUT"], PropertyKey, PropertyValue);
                    FileWriter writer = new FileWriter();
                    writer.WriteCSV(OutputFileLocation, "names1.xls", IKeys);
                    MessageBox.Show("Created INPUT_XL in " + OutputFileLocation);
                }
                else
                {
                    string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default", -1, -1);
                    IKeys = v.FindPropertyKey(IODictionary["INPUT"], PropertyKey, PropertyValue);
                    FileWriter writer = new FileWriter();
                    writer.WriteCSV(OutputFileLocation,f, IKeys);
                    MessageBox.Show("Created INPUT_XL in "+OutputFileLocation);

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            OutputFileLocation = folderBrowserDialog1.SelectedPath;
           // MessageBox.Show(Folder);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result;
            PropertyKeyFinder v = new PropertyKeyFinder();
            if (!File.Exists(OutputFileLocation+"\\names2.xls"))
            {
                OKeys = v.FindPropertyKey(IODictionary["OUTPUT"], PropertyKey, PropertyValue);
                FileWriter writer = new FileWriter();
                writer.WriteCSV(OutputFileLocation, "names2.xls", OKeys);
                MessageBox.Show("Created OUTPUT_XL in "+OutputFileLocation);
            }
            else
            {
                result = MessageBox.Show("Do you want to create a new file?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    OKeys = v.FindPropertyKey(IODictionary["OUTPUT"], PropertyKey, PropertyValue);
                    FileWriter writer = new FileWriter();
                    writer.WriteCSV(OutputFileLocation, "names2.xls", OKeys);
                    MessageBox.Show("Created OUTPUT_XL in "+OutputFileLocation);
                }
                else
                {
                    string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default", -1, -1);
                    OKeys = v.FindPropertyKey(IODictionary["OUTPUT"], PropertyKey, PropertyValue);
                    FileWriter writer = new FileWriter();
                    writer.WriteCSV(OutputFileLocation, f, OKeys);
                    MessageBox.Show("Created OUTPUT_XL in "+ OutputFileLocation);

                }
            }
        }
    }
}
