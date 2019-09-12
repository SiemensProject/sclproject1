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

namespace SCLMenu
{
    public partial class Form1 : Form
    {
        public string FileName,Folder;
        public string Key;
        public string Value;
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
            Key = comboBox1.SelectedItem.ToString();
        }
        
        private void BtnSelect_Click(object sender, EventArgs e)
        {
           
                this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.ShowDialog();
                FileName=openFileDialog1.FileName;
                SCLSplit s = new SCLSplit();
                s.SplitFile(FileName);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Value = comboBox2.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            VariableName v = new VariableName();
            if (!File.Exists(Folder+"\\names1.xls"))
            {
                v.GetVariables(Folder + "\\INPUT.txt", Key, Value, Folder + "\\names1.xls");
                MessageBox.Show("Created INPUT_XL in " + Folder);
            }
            else
            {
                result = MessageBox.Show("Do you want to create a new file?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    v.GetVariables(Folder + "\\INPUT.txt", Key, Value, Folder+"\\names1.xls");
                    MessageBox.Show("Created INPUT_XL in " + Folder);
                }
                else
                {
                    string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default", -1, -1);
                    v.GetVariables(Folder+"\\INPUT.txt", Key, Value, Folder + "\\" + f);
                   MessageBox.Show("Created INPUT_XL in "+Folder);

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            Folder = folderBrowserDialog1.SelectedPath;
           // MessageBox.Show(Folder);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result;
            VariableName v = new VariableName();
            if (!File.Exists(Folder+"\\names2.xls"))
            {
                v.GetVariables(Folder+"\\OUTPUT.txt", Key, Value, Folder+"\\names2.xls");
                MessageBox.Show("Created OUTPUT_XL in "+Folder);
            }
            else
            {
                result = MessageBox.Show("Do you want to create a new file?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    v.GetVariables(Folder+"\\OUTPUT.txt", Key, Value, Folder+"\\names2.xls");
                    MessageBox.Show("Created OUTPUT_XL in "+Folder);
                }
                else
                {
                    string f = Microsoft.VisualBasic.Interaction.InputBox("Enter new FileName with required format", "FileName Prompt", "desired default", -1, -1);
                    v.GetVariables(Folder+"\\OUTPUT.txt", Key, Value, Folder +"\\"+ f);
                    MessageBox.Show("Created OUTPUT_XL in "+ Folder);

                }
            }
        }
    }
}
