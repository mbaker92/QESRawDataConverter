﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace QESRawConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        // For Browse Button
        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Console.WriteLine(openFileDialog1.FileName);
                Console.WriteLine(Path.GetDirectoryName(openFileDialog1.FileName));
                label1.Text = "Input File: "+ openFileDialog1.SafeFileName;
                string OutputFile = "";
                if (openFileDialog1.FileName.Contains("ACP"))
                {
                    using (var reader = new StreamReader(openFileDialog1.FileName))
                    {
                        using (var csv = new CsvHelper.CsvReader(reader))
                        {
                            var records = csv.GetRecords<Classes.ACPStructure>();
                            OutputFile = Path.GetDirectoryName(openFileDialog1.FileName) + @"\" + openFileDialog1.SafeFileName.Replace("Raw Data", "Converted");
                            using (var s = new StreamWriter(OutputFile))
                            {
                                using (var cs = new CsvHelper.CsvWriter(s))
                                {
                                    foreach (var Rec in records)
                                    {
                                        cs.WriteRecords(Classes.ACPOutput.GetNewRecords(Rec));
                                    }

                                    s.Close();
                                }
                            }

                        }
                        reader.Close();
                    }
                    label2.Text = "Output File: " + openFileDialog1.SafeFileName.Replace("Raw Data", "Converted");

                    DialogResult result = MessageBox.Show("File Location: " + OutputFile, "Conversion Done", MessageBoxButtons.OK);
                }
            }

        }

    }
}
