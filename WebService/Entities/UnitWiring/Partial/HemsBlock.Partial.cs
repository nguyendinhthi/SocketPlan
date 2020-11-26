using System.Collections.Generic;
using System;
namespace SocketPlan.WebService
{
    public partial class HemsBlock
    {
        // 特殊回路リスト用
        public int RequestYear { get; set; }
        public int RequestWeek { get; set; }

        public string RequestYearText
        {
            get
            {
                if (this.RequestYear == 0)
                    return string.Empty;

                return this.RequestYear.ToString();
            }
            set { }
        }

        public string RequestWeekText
        {
            get
            {
                if (this.RequestWeek == 0)
                    return string.Empty;

                return this.RequestWeek.ToString();
            }
            set { }
        }

        public string CircuitPlanCreatedDateText { get; set; }

        public string FloorRoom { get; set; }

        private string kairoNo;
        public string KairoNo
        {
            get
            {
                if (!string.IsNullOrEmpty(kairoNo))
                    return kairoNo;

                if (this.Name.IndexOf("branch_") == -1)
                    return string.Empty;

                var no = this.Name.Substring(7);
                int i;
                if (!int.TryParse(no, out i))
                    return string.Empty;
                
                this.kairoNo = i.ToString();
                return this.kairoNo;   
            }
            set
            {
                this.kairoNo = value;
            }
        }

        public string EnglishName { get; set; }

        public static void Delete(string constructionCode)
        {
            string sql = @"
                DELETE FROM HemsBlocks
                WHERE
                    ConstructionCode = '" + constructionCode + @"'";

            var db = HemsBlock.GetDatabase();
            db.ExecuteNonQuery(sql);
        }

        public static List<HemsBlock> Get(List<string> constructionCodes)
        {
            if(constructionCodes.Count == 0)
                return new List<HemsBlock>();

            var codes = string.Empty;
            constructionCodes.ForEach(p => codes += "'" + p + "',");
            codes = codes.Substring(0, codes.Length - 1);

            var sql = @"
            SELECT *
            FROM HemsBlocks
            WHERE
                ConstructionCode IN (" + codes + @")";

            var db = HemsBlock.GetDatabase();
            return db.ExecuteQuery<HemsBlock>(sql);
        }

        public static List<HemsBlock> Get(string constructionCode)
        {
            var code = constructionCode.Replace("'", "''");
            var sql = @"SELECT * FROM HemsBlocks WHERE ConstructionCode = '" + code + "'";
            var db = HemsBlock.GetDatabase();
            return db.ExecuteQuery<HemsBlock>(sql);
        }

        public void FillFloorRoom()
        {
            var sql = @"
            SELECT R.Location + ' - ' + R.Name AS FloorRoom
            FROM HemsRoomBlocks B
            JOIN HemsRooms R ON
                B.ConstructionCode = R.ConstructionCode AND
                B.RoomId = R.Id
            WHERE
                B.ConstructionCode = '" + this.ConstructionCode + @"' AND
                B.DeviceId = " + this.DeviceId + @" AND
                B.BlockId = " + this.Id;

            var db = HemsBlock.GetDatabase();
            var floorRoom = db.ExecuteScalar(sql);

            if (floorRoom == null || floorRoom == DBNull.Value)
                return;

            this.FloorRoom = floorRoom.ToString();
        }

        public List<HemsBlock> GetEcwill1(string constructionCode)
        {
            string sql = string.Empty;

            sql = @"SELECT * FROM HemsBlocks 
                    WHERE ConstructionCode = '" + constructionCode + @"'
                            AND Name = 'branch_44'
                            AND DisplayName = 'エコウィル'";

            var db = HemsBlock.GetDatabase();
            var blocks = db.ExecuteQuery<HemsBlock>(sql);

            return blocks;
        }

        public List<HemsBlock> GetEcwill2(string constructionCode)
        {
            string sql = string.Empty;

            sql = @"SELECT * FROM HemsBlocks 
                    WHERE ConstructionCode = '" + constructionCode + @"'
                            AND Name = 'branch_40'
                            AND DisplayName = 'エコウィル'";

            var db = HemsBlock.GetDatabase();
            var blocks = db.ExecuteQuery<HemsBlock>(sql);
            return blocks;
        }

        public void FillEnglishName()
        {
            if (this.DisplayName == "エコキュート")
            {
                this.EnglishName = "Eco";
                return;
            }

            if (this.DisplayName == "エコウィル")
            {
                if (this.KairoNo == "42" || this.KairoNo == "38")
                {
                    this.EnglishName = "E/S";
                    return;
                }

                if (this.KairoNo == "44" || this.KairoNo == "40")
                {
                    this.EnglishName = "E/H";
                    return;
                }
            }

            if (this.DisplayName == "蓄電池")
            {
                if (this.KairoNo == "42" || this.KairoNo == "38")
                {
                    this.EnglishName = "C/J";
                    return;
                }

                if (this.KairoNo == "44" || this.KairoNo == "40")
                {
                    this.EnglishName = "C/H";
                    return;
                }
            }

            // 上記以外は蓄電池の非常用になる
            this.EnglishName = "Backup";
        }

        public void FillCircuitPlanCreatedDateTime()
        {
            var log = HemsLog.Get(this.ConstructionCode, this.PlanNo, this.RevisionNo);
            if (log == null || !log.KairoPlanOutputDateTime.HasValue)
                return;

            this.CircuitPlanCreatedDateText = log.KairoPlanOutputDateTime.Value.ToString("yyyy/MM/dd H:mm");
        }
    }
}
