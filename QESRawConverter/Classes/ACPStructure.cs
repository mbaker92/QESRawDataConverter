using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QESRawConverter.Classes
{
    public class ACPStructure
    {
        #region Variables
        [Name("StIDSecID ")]
        public string StIDSecID { get; set; }

        [Name(" SampleNumber ")]
        public int SampleNumber { get; set; }

        [Name( " StreetName ")]
        public string StreetName { get; set; }

        [Name(" BegLocatio ")]
        public string BegLocatio { get; set; }

        [Name(" EndLocatio ")]
        public string EndLocatio { get; set; }

        [Name(" Sample Length ")]
        public int SampleLength { get; set; }

        [Name(" Sample Width")]
        public int SampleWidth { get; set; }

        [Name(" Sample Area ")]
        public int SampleArea { get; set; }

        [Name(" Sample Notes ")]
        public string SampleNotes { get; set; }

        [Name(" Alligator L ")]
        public int AlligatorL { get; set; }

        [Name(" Alligator M ")]
        public int AlligatorM { get; set; }

        [Name(" Alligator H ")]
        public int AlligatorH { get; set; }

        [Name(" Block L ")]
        public int BlockL { get; set; }

        [Name(" Block M")]
        public int BlockM { get; set; }

        [Name(" Block H ")]
        public int BlockH { get; set; }

        [Name(" Distortion L")]
        public int DistortionL { get; set; }

        [Name(" Distortion M")]
        public int DistortionM { get; set; }

        [Name(" Distortion H ")]
        public int DistortionH { get; set; }

        [Name(" LongTrans L")]
        public int LTCL { get; set; }

        [Name(" LongTrans M ")]
        public int LTCM { get; set; }

        [Name(" LongTrans H ")]
        public int LTCH { get; set; }

        [Name(" Patch L ")]
        public int PatchL { get; set; }

        [Name(" Patch M")]
        public int PatchM { get; set; }

        [Name(" Patch H ")]
        public int PatchH { get; set; }

        [Name(" Raveling L")]
        public int RavL { get; set; }

        [Name(" Raveling M")]
        public int RavM { get; set; }

        [Name(" Raveling H")]
        public int RavH { get; set; }

        [Name(" RuttingDepression L")]
        public int RutL { get; set; }

        [Name(" RuttingDepression M")]
        public int RutM { get; set; }

        [Name(" RuttingDepression H")]
        public int RutH { get; set; }

        [Name(" Weathering L")]
        public int WeatL { get; set; }

        [Name(" Weathering M")]
        public int WeatM { get; set; }

        [Name(" Weather H")]
        public int WeatH { get; set; }

   //     [Name(" Special ")]
     //   public string Special { get; set; }

       // [Name(" Date ")]
     //   public string date { get; set; }
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

        [Name("Inspection Area")]
        public int InspectionArea { get; set; }

        [Name("Inspection Length")]
        public int InspectionLength { get; set; }

        [Name("DistressType")]
        public string DistressType { get; set; }

        [Name("Severity")]
        public char Severity { get; set; }

        [Name("DistressSize")]
        public int DistressSize { get; set; }

        [Name("NoDistresses")]
        public string NoDistresses { get; set; }

        [Name("Special")]
        public string Special { get; set; }

        public static List<ACPOutput> GetNewRecords(ACPStructure Infile)
        {
            bool WillHaveDistress = false;
            List<ACPOutput> temp = new List<ACPOutput>();
            #region Alligator
            if (Infile.AlligatorL > 0)
            {
                temp.Add(new ACPOutput(Infile) {DistressType = "Alligator Cracking", Severity ='L', DistressSize= Infile.AlligatorL, NoDistresses="No" });
                WillHaveDistress = true;
            }
            if (Infile.AlligatorM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Alligator Cracking", Severity = 'M', DistressSize = Infile.AlligatorM, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.AlligatorH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Alligator Cracking", Severity = 'H', DistressSize = Infile.AlligatorH, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            #endregion
            #region Block
            if (Infile.BlockL> 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Block Cracking", Severity = 'L', DistressSize = Infile.BlockL, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.BlockM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Block Cracking", Severity = 'M', DistressSize = Infile.BlockM, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.BlockH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Block Cracking", Severity = 'H', DistressSize = Infile.BlockH, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            #endregion
            #region Distortions
            if (Infile.DistortionL> 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Distortions", Severity = 'L', DistressSize = Infile.DistortionL, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.DistortionM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Distortions", Severity = 'M', DistressSize = Infile.DistortionM, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.DistortionH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Distortions", Severity = 'H', DistressSize = Infile.DistortionH, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            #endregion
            #region LTC
            if (Infile.LTCL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Long & Trans. Cracking", Severity = 'L', DistressSize = Infile.LTCL, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.LTCM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Long & Trans. Cracking", Severity = 'M', DistressSize = Infile.LTCM, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.LTCH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Long & Trans. Cracking", Severity = 'H', DistressSize = Infile.LTCH, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            #endregion
            #region Patching
            if (Infile.PatchL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Patch & Util. Cut Patch", Severity = 'L', DistressSize = Infile.PatchL, NoDistresses = "No" });
                WillHaveDistress = true;
            }

            if (Infile.PatchM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Patch & Util. Cut Patch", Severity = 'M', DistressSize = Infile.PatchM, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.PatchH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Patch & Util. Cut Patch", Severity = 'H', DistressSize = Infile.PatchH, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            #endregion
            #region Rutting/Depression
            if (Infile.RutL > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Rutting/Depression", Severity = 'L', DistressSize = Infile.RutL, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.RutM > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Rutting/Depression", Severity = 'M', DistressSize = Infile.RutM, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if (Infile.RutH > 0)
            {
                temp.Add(new ACPOutput(Infile) { DistressType = "Rutting/Depression", Severity = 'H', DistressSize = Infile.RutH, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            #endregion
            #region Weathering And Raveling
            if((Infile.WeatL >0) || (Infile.RavL > 0))
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Weating & Raveling", Severity = 'L', DistressSize = Infile.WeatL + Infile.RavL, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if ((Infile.WeatM > 0) || (Infile.RavM > 0))
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Weating & Raveling", Severity = 'M', DistressSize = Infile.WeatM + Infile.RavM, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            if ((Infile.WeatH > 0) || (Infile.RavH > 0))
            {

                temp.Add(new ACPOutput(Infile) { DistressType = "Weating & Raveling", Severity = 'H', DistressSize = Infile.WeatH + Infile.RavH, NoDistresses = "No" });
                WillHaveDistress = true;
            }
            #endregion
            if( WillHaveDistress == false)
            {
                temp.Add(new ACPOutput(Infile) { NoDistresses = "Yes" });
            }

            return temp;
        }
        public ACPOutput(ACPStructure Infile)
        {
            StreetID = Infile.StIDSecID;
            StreetID.Substring(0, StreetID.IndexOf("-"));
            SectionID = Infile.StIDSecID;
            SectionID.Substring(SectionID.IndexOf("-"));
            SampleNumber = Infile.SampleNumber;
            InspectionDate = "";
            Special = "";
           // InspectionDate = Infile.date;
            InspectionArea = Infile.SampleArea;
            InspectionLength = Infile.SampleLength;
         //   if(Infile.Special != "0")
        //    {
        //        Special = "Yes";
        //    }
        //    else
        //    {
         //       Special = "No";
         //   }

        }

    }
}
