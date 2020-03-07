using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QESRawConverter.Classes
{
    public class ACPStructure
    {

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

    //    [Name("Sample Notes")]
  //      public string SampleNotes { get; set; }

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

            int CheckAll = 0;
            string ErrorMessage = "";
            CheckAll = Infile.AlligatorL + Infile.AlligatorM + Infile.AlligatorH + Infile.PatchL + Infile.PatchM + Infile.PatchH + Infile.BlockL + Infile.BlockM + Infile.BlockH;
            int checkblock = 0;
            if (CheckAll > Infile.SampleArea)
            {
                ErrorMessage = "Alligator/Block/Patch > Sample Area.";
            }

            checkblock = Infile.BlockL + Infile.BlockM + Infile.BlockH;

            if ((Infile.SampleArea > 4000) || (Infile.SampleArea < 1000))
            {
                ErrorMessage = ErrorMessage + " Sample Area Outside Bounds.";
            }
            if (Infile.SampleNumber == 0)
            {
                ErrorMessage = ErrorMessage + " Sample Number Missing.";
            }

            if (checkblock == Infile.SampleArea)
            {
                if ((Infile.LTCL > 0) || (Infile.LTCM > 0) || (Infile.LTCH > 0))
                {
                    ErrorMessage = ErrorMessage + " Block 100% with Long/Trans.";
                }
            }
            int CheckWeatherRavel = 0;
            CheckWeatherRavel = Infile.WeatL + Infile.WeatM + Infile.WeatH + Infile.RavL + Infile.RavM + Infile.RavH;
            if (CheckWeatherRavel > Infile.SampleArea)
            {
                ErrorMessage = ErrorMessage + " Weather+Ravel > Sample Area.";
            }

            if (Infile.RavL > 0)
            {
                ErrorMessage = ErrorMessage + " Low Raveling - " + Infile.RavL;
            }


            #region Alligator
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
            #region Weathering And Raveling
            /*
            if ((Infile.WeatL > 0) || (Infile.RavL > 0))
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Weathering & Raveling", Severity = 'L', DistressSize = Infile.WeatL + Infile.RavL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if ((Infile.WeatM > 0) || (Infile.RavM > 0))
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Weathering & Raveling", Severity = 'M', DistressSize = Infile.WeatM + Infile.RavM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if ((Infile.WeatH > 0) || (Infile.RavH > 0))
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Weathering & Raveling", Severity = 'H', DistressSize = Infile.WeatH + Infile.RavH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            */
            #region Weathering
            if (Infile.WeatL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Weathering", Severity = 'L', DistressSize = Infile.WeatL, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.WeatM > 0)
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Weathering", Severity = 'M', DistressSize = Infile.WeatM, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            if (Infile.WeatH > 0)
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Weathering", Severity = 'H', DistressSize = Infile.WeatH, NoDistresses = "No", Error = ErrorMessage });
                WillHaveDistress = true;
            }
            #endregion
            #region Raveling
            if  (Infile.RavL > 0)
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
            #endregion
            if (WillHaveDistress == false)
            {
                temp.Add(new ACPOutput(Infile) { NoDistresses = "Yes" , DistressSize=null});
            }

            return temp;
        }
        public ACPOutput(ACPStructure Infile)
        {
            StreetID = Infile.StIDSecID;
            StreetID= StreetID.Substring(0, StreetID.IndexOf('-'));
            SectionID = Infile.StIDSecID;
            SectionID=SectionID.Substring(SectionID.IndexOf('-')+2);
            SampleNumber = Infile.SampleNumber;
            try
            {
                var dt = DateTime.ParseExact(Infile.date, "MM-dd-yyyy  HH:mm:ss", CultureInfo.InvariantCulture);

                //  InspectionDate = Infile.date;
                InspectionDate = dt.ToString("MM/dd/yyyy");
            }
            catch 
            {
                InspectionDate = Infile.date;
            }
            InspectionArea = Infile.SampleArea;
            InspectionLength = Infile.SampleLength;
          //  System.Diagnostics.Debug.Write("Special Value = " + Infile.Special);
            if(Infile.Special == "0")
            {
                Special = "No";
            }
            else if (Infile.Special == " False ")
            {
                Special = "No";
            }
            else
            {
                Special = "No";
            }

        }

    }
}