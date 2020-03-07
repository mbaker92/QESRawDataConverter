using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

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
                // Show Input file on GUI
                label1.Text = "Input File: "+ openFileDialog1.SafeFileName;
                string OutputFile = "";

                if (openFileDialog1.FileName.Contains("ACP"))
                {
                    using (var reader = new StreamReader(openFileDialog1.FileName))
                    {
                        using (var csv = new CsvHelper.CsvReader(reader))
                        {
                            // Get Input CSV Rows
                            var records = csv.GetRecords<Classes.ACPStructure>();

                            // Create Output file path
                            OutputFile = Path.GetDirectoryName(openFileDialog1.FileName) + @"\" + openFileDialog1.SafeFileName.Replace("Raw Data", "Converted");

                            using (var s = new StreamWriter(OutputFile))
                            {
                                using (var cs = new CsvHelper.CsvWriter(s))
                                {
                                    // Write new records to Output CSV file.
                                    try
                                    {
                                        foreach (var Rec in records)
                                        {
                                            cs.WriteRecords(Classes.ACPOutput.GetNewRecords(Rec));
                                            cs.Flush();
                                        }
                                    }catch (CsvHelper.CsvHelperException ex){
                                        Console.WriteLine("Exception" + ex.Data.Values);
                                    }
                                    s.Close();
                                }
                            }

                        }
                        reader.Close();
                    }
                    // Show Output file location on the GUI
                    label2.Text = "Output File: " + openFileDialog1.SafeFileName.Replace("Raw Data", "Converted");

                    // create another copy of the output file with Sheet1.csv as the name.
                    System.IO.File.Copy(OutputFile, Path.GetDirectoryName(openFileDialog1.FileName) + @"\Sheet1.csv", true);

                    // Show popup that tells the user the CSV file was converted.
                    DialogResult result = MessageBox.Show("File Location: " + OutputFile, "Conversion Done", MessageBoxButtons.OK);
                }
            }

        }

    }
}
