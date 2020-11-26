using System.Collections.Generic;
using System.IO;
using SocketPlan.WebService.Properties;
using System.Drawing;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace SocketPlan.WebService
{
    public partial class RimokonNicheDetail
    {
        public RimokonNichePattern Pattern { get; set; }
        public Equipment Equipment { get; set; }

        public string EquipmentNameAtSelection
        {
            get
            {
                if (this.Equipment == null)
                    return string.Empty;

                if (this.Equipment.IsSwitch)
                    return "SW";

                return this.Equipment.NameAtSelection.Replace("(new)", "");
            }
        }

        public string RimokonNichePatternName
        {
            get
            {
                if (this.Pattern == null)
                    return string.Empty;

                return this.Pattern.PatternName;
            }
        }

        public string PatternNameForReport
        {
            get
            {
                return "[" + this.RimokonNichePatternName + "]";
            }
        }

        public string PatternImagePath
        {
            get
            {
                if (this.Pattern == null)
                    return string.Empty;

                return Path.Combine(
                    Settings.Default.RimokonNichePatternImagePath,
                    this.Pattern.PatternName + ".jpg");
            }
        }

        public byte[] PatternImage
        {
            get
            {
                if(string.IsNullOrEmpty(this.PatternImagePath))
                    return null;

                var image = Image.FromFile(this.PatternImagePath);
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                return stream.GetBuffer();
            }
        }

        //public string KindName
        //{
        //    get
        //    {
        //        if(this.Equipment == null)
        //            return string.Empty;

        //        if (this.Equipment.EquipmentKindId == 3) // Switch
        //            return "スイッチ";

        //        if (this.Equipment.NameAtSelection.Contains("CV"))
        //            return "ロスガード";

        //        if (this.Equipment.NameAtSelection.Contains("YR"))
        //            return "床暖";

        //        if (this.Equipment.NameAtSelection.Contains("PVR"))
        //            return "太陽光";

        //        if(this.Equipment.Name == "int-01") // interphone
        //            return "インターホン";

        //        if(this.Equipment.Name == "ﾘﾓｺﾝ12" || this.Equipment.Name == "ﾘﾓｺﾝ13")
        //            return "風呂";

        //        return string.Empty;
        //    }
        //}

        public string Hinban { get; set; }

        public static void Delete(string constructionCode)
        {
            var sql = @"DELETE FROM RimokonNicheDetails WHERE ConstructionCode = '" + constructionCode + "'";
            var db = RimokonNicheDetail.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static List<RimokonNicheDetail> Get(string constructionCode, int seqNo)
        {
            var sql = @"
            SELECT *
            FROM RimokonNicheDetails
            WHERE
                ConstructionCode = '" + constructionCode + @"' AND
                SeqNo = " + seqNo;

            var db = RimokonNicheDetail.GetDatabase();
            return db.ExecuteQuery<RimokonNicheDetail>(sql);
        }

        /// <summary>スイッチの数を数えて○連って文字をHinbanに入れる</summary>
        public void SetHinbanForSwitch(List<RimokonNicheDetail> switches)
        {
            if (switches.Count == 0)
                return;

            if (this.PositionNo == "2")
            {
                this.Hinban = "2連";
                return;
            }

            if (switches.FindAll(p => p.PositionNo == this.PositionNo).Count == 1)
            {
                this.Hinban = "1連";
                return;
            }

            if (this.PositionNo == "1")
            {
                if (switches.Exists(p => p.PositionNo == "2" && p.Equipment.IsSwitch))
                    this.Hinban = "1連";
                else
                    this.Hinban = "2連";
            }
        }

        /// <summary>重複してる図面表示を消す</summary>
        public static void DistinctSwitches(List<RimokonNicheDetail> details)
        {
            var switches = details.FindAll(p => p.Equipment.IsSwitch);
            if (switches.Count == 0 || switches.Count == 1)
                return;

            RimokonNicheDetail sw1 = switches.Find(p => p.PositionNo == "1");
            RimokonNicheDetail sw2 = switches.Find(p => p.PositionNo == "2");

            //if(2 <= switches.Count && switches.Count <= 6)
            //{
            //    sw1 = switches[0];
            //}
            //else if(7 <= switches.Count)
            //{
            //    sw1 = switches.Find(p => p.PositionNo == 1);
            //    sw2 = switches.Find(p => p.PositionNo == 2);
            //}

            details.RemoveAll(p => p.Equipment.IsSwitch);

            var index = 0;
            if (sw1 != null)
            {
                details.Insert(index, sw1);
                index++;
            }

            if (sw2 != null)
            {
                details.Insert(index, sw2);
                index++;
            }
        }
    }
}
