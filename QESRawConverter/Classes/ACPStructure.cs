using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace QESRawConverter.Classes
{
    public class ACPStructure
    {
        // Values From Input CSV
        #region Variables
        [Name("StIDSecID")]
        public string StIDSecID { get; set; }

        [Name("SampleNumber")]
        public int SampleNumber { get; set; }

        [Name("StreetName")]
        public string StreetName { get; set; }

        [Name("BegLocatio")]
        public string BegLocatio { get; set; }

        [Name("EndLocatio")]
        public string EndLocatio { get; set; }

        [Name("Sample Length")]
        public int SampleLength { get; set; }

        [Name("Sample Width")]
        public int SampleWidth { get; set; }

        [Name("Sample Area")]
        public int SampleArea { get; set; }

        [Name("Alligator L")]
        public int AlligatorL { get; set; }

        [Name("Alligator M")]
        public int AlligatorM { get; set; }

        [Name("Alligator H")]
        public int AlligatorH { get; set; }

        [Name("Block L")]
        public int BlockL { get; set; }

        [Name("Block M")]
        public int BlockM { get; set; }

        [Name("Block H")]
        public int BlockH { get; set; }

        [Name("Distortion L")]
        public int DistortionL { get; set; }

        [Name("Distortion M")]
        public int DistortionM { get; set; }

        [Name("Distortion H")]
        public int DistortionH { get; set; }

        [Name("LongTrans L")]
        public int LTCL { get; set; }

        [Name("LongTrans M")]
        public int LTCM { get; set; }

        [Name("LongTrans H")]
        public int LTCH { get; set; }

        [Name("Patch L")]
        public int PatchL { get; set; }

        [Name("Patch M")]
        public int PatchM { get; set; }

        [Name("Patch H")]
        public int PatchH { get; set; }

        [Name("Raveling L")]
        public int RavL { get; set; }

        [Name("Raveling M")]
        public int RavM { get; set; }

        [Name("Raveling H")]
        public int RavH { get; set; }

        [Name("RuttingDepression L")]
        public int RutL { get; set; }

        [Name("RuttingDepression M")]
        public int RutM { get; set; }

        [Name("RuttingDepression H")]
        public int RutH { get; set; }

        [Name("Weathering L")]
        public int WeatL { get; set; }

        [Name("Weathering M")]
        public int WeatM { get; set; }

        [Name("Weather H")]
        public int WeatH { get; set; }

        [Name("Special")]
        public string Special { get; set; }

        [Name("Date")]
        public string date { get; set; }
        #endregion
    }
    public class ACPOutput
    {
        // Values for Output CSV
        [Name("StreetID")]
        public string StreetID { get; set; }

        [Name("SectionID")]
        public string SectionID { get; set; }

        [Name("InspectionUnit#")]
        public int SampleNumber { get; set; }

        [Name("InspectionDate")]
        public string InspectionDate { get; set; }

        [Name("InspectionArea")]
        public int InspectionArea { get; set; }

        [Name("InspectionLength")]
        public int InspectionLength { get; set; }

        [Name("DistressType")]
        public string DistressType { get; set; }

        [Name("Severity")]
        public char Severity { get; set; }

        [Name("DistressSize")]
        public int? DistressSize { get; set; }

        [Name("NoDistresses")]
        public string NoDistresses { get; set; }

        [Name("Special")]
        public string Special { get; set; }

        [Name("Error")]
        public string Error { get; set; }

        public static List<ACPOutput> GetNewRecords(ACPStructure Infile)
        {
            bool WillHaveDistress = false;
            List<ACPOutput> temp = new List<ACPOutput>();

            // value that will be in the error column
            string ErrorMessage = "";

            // Error Check if Alligator, Block, and Patching values are greater than the Sample's Area
            int CheckAll = Infile.AlligatorL + Infile.AlligatorM + Infile.AlligatorH + Infile.PatchL + Infile.PatchM + Infile.PatchH + Infile.BlockL + Infile.BlockM + Infile.BlockH;
            if (CheckAll > Infile.SampleArea)
            {
                ErrorMessage = "Alligator/Block/Patch > Sample Area.";
            }

            // Error Check if the Sample's Area is within bounds
            if ((Infile.SampleArea > 4000) || (Infile.SampleArea < 1000))
            {
                ErrorMessage = ErrorMessage + " Sample Area Outside Bounds.";
            }

            // Error Check if the Sample has a valid Sample Number
            if (Infile.SampleNumber == 0)
            {
                ErrorMessage = ErrorMessage + " Sample Number Missing.";
            }

            // Error Check if Block cracking is 100% of Sample area and if there is Longitudinal and Transverse Cracking too.
            int checkblock = Infile.BlockL + Infile.BlockM + Infile.BlockH;
            if (checkblock == Infile.SampleArea)
            {
                if ((Infile.LTCL > 0) || (Infile.LTCM > 0) || (Infile.LTCH > 0))
                {
                    ErrorMessage = ErrorMessage + " Block 100% with Long/Trans.";
                }
            }

            // Error Check if Weathering and Raveling are greater than Sample's Area
            int CheckWeatherRavel = Infile.WeatL + Infile.WeatM + Infile.WeatH + Infile.RavL + Infile.RavM + Infile.RavH;
            if (CheckWeatherRavel > Infile.SampleArea)
            {
                ErrorMessage = ErrorMessage + " Weather+Ravel > Sample Area.";
            }

            // Error Check if there is a value for Raveling Low.
            if (Infile.RavL > 0)
            {
                ErrorMessage = ErrorMessage + " Low Raveling - " + Infile.RavL;
            }


            #region Alligator
            // Add to List for Low, Medium, or High Alligator Cracking if the Sample had values for those distresses
            if (Infile.AlligatorL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Alligator Cracking", Severity = 'L', DistressSize = Infile.AlligatorL, NoDistresses = "No", Error = ErrorMessage });

                WillHaveDistress = true;
            }
            if (Infile.AlligatorM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Alligator Cracking", Severity = 'M', DistressSize = Infile.AlligatorM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.AlligatorH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Alligator Cracking", Severity = 'H', DistressSize = Infile.AlligatorH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion
            #region Block

            // Add to List for Low, Medium, or High Block Cracking if the Sample had values for those distresses
            if (Infile.BlockL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Block Cracking", Severity = 'L', DistressSize = Infile.BlockL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.BlockM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Block Cracking", Severity = 'M', DistressSize = Infile.BlockM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.BlockH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Block Cracking", Severity = 'H', DistressSize = Infile.BlockH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion
            #region Distortions

            // Add to List for Low, Medium, or High Distortions if the Sample had values for those distresses
            if (Infile.DistortionL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Distortions", Severity = 'L', DistressSize = Infile.DistortionL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.DistortionM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Distortions", Severity = 'M', DistressSize = Infile.DistortionM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.DistortionH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Distortions", Severity = 'H', DistressSize = Infile.DistortionH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion
            #region LTC

            // Add to List for Low, Medium, or High Longitudinal/Transverse Cracking if the Sample had values for those distresses
            if (Infile.LTCL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Long. & Trans. Cracking", Severity = 'L', DistressSize = Infile.LTCL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.LTCM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Long. & Trans. Cracking", Severity = 'M', DistressSize = Infile.LTCM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.LTCH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Long. & Trans. Cracking", Severity = 'H', DistressSize = Infile.LTCH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion
            #region Patching

            // Add to List for Low, Medium, or High Patching if the Sample had values for those distresses
            if (Infile.PatchL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Patch & Util. Cut Patch", Severity = 'L', DistressSize = Infile.PatchL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }

            if (Infile.PatchM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Patch & Util. Cut Patch", Severity = 'M', DistressSize = Infile.PatchM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.PatchH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Patch & Util. Cut Patch", Severity = 'H', DistressSize = Infile.PatchH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion
            #region Rutting/Depression

            // Add to List for Low, Medium, or High Rutting/Depression if the Sample had values for those distresses
            if (Infile.RutL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Rutting/Depression", Severity = 'L', DistressSize = Infile.RutL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.RutM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Rutting/Depression", Severity = 'M', DistressSize = Infile.RutM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.RutH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Rutting/Depression", Severity = 'H', DistressSize = Infile.RutH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion
            #region Raveling

            // Add to List for Low, Medium, or High Raveling if the Sample had values for those distresses
            if (Infile.RavL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Raveling", Severity = 'L', DistressSize = Infile.RavL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.RavM > 0)
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Raveling", Severity = 'M', DistressSize = Infile.RavM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.RavH > 0)
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Raveling", Severity = 'H', DistressSize = Infile.RavH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion

            // Add to List if there are no distresses
            if (WillHaveDistress == false)
            {
                temp.Add(new ACPOutput(Infile) { NoDistresses = "Yes", DistressSize = null });
            }

            return temp;
        }
        public ACPOutput(ACPStructure Infile)
        {
            // Get the Street ID
            StreetID = Infile.StIDSecID;
            StreetID = StreetID.Substring(0, StreetID.IndexOf('-'));

            // Get the Section ID
            SectionID = Infile.StIDSecID;
            SectionID = SectionID.Substring(SectionID.IndexOf('-') + 2);

            // Get the Sample Number
            SampleNumber = Infile.SampleNumber;
            try
            {
                // Convert Date in Original CSV to the format required in the new CSV
                var dt = DateTime.ParseExact(Infile.date, "MM-dd-yyyy  HH:mm:ss", CultureInfo.InvariantCulture);

                InspectionDate = dt.ToString("MM/dd/yyyy");
            }
            catch
            {
                InspectionDate = Infile.date;
            }

            // Get Inspection Area and Length
            InspectionArea = Infile.SampleArea;
            InspectionLength = Infile.SampleLength;

            // Get if Sample is considered Special
            if (Infile.Special == "0")
            {
                Special = "No";
            }
            else if (Infile.Special == " False ")
            {
                Special = "No";
            }
            else
            {
                Special = "Yes";
            }
        }
    }
}