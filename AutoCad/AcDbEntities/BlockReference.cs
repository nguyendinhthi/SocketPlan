using System.Collections.Generic;
using System.IO;

namespace Edsa.AutoCadProxy
{
    public class BlockReference : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbBlockReference; } }

        public int Make(string blockName, PointD point)
        {
            var setData = new List<object>();
            setData.Add(new double[] { point.X, point.Y, 0 });
            setData.Add(blockName);

            var result = this.Make(setData.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to add block reference.");

            return result.Value;
        }

        public int Make(int blockId, PointD point)
        {
            var setData = new List<object>();
            setData.Add(new double[] { point.X, point.Y, 0 });
            setData.Add(blockId);

            var result = this.Make(setData.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to add block reference.");

            return result.Value;
        }

        public int AddAttribute(int blockId, string tag, string value, PointD point)
        {
            var setData = new List<object>();
            ////ブロックと属性の高さを同じにしていると、ブロック同士のIsIntersectをした時に属性にあたり判定が出来てしまう。
            setData.Add(new double[] { point.X, point.Y, 1000 });
            setData.Add(value);
            setData.Add(tag);

            //追加した属性のObjectIdが戻り値
            var result = this.Get<int, object>(blockId, "属性追加", setData.ToArray());

            if (!result.Success)
                throw new AutoCadException("Failed to add attribute.");

            return result.Value;
        }

        public List<int> GetAttributes(int objectId)
        {
            var result = this.Get<int[]>(objectId, "属性取得");

            if (!result.Success)
                return new List<int>();

            return new List<int>(result.Value);
        }

        public double GetRotation(int objectId)
        {
            Result<double> result = this.Get<double>(objectId, "回転角度取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get rotation of block.");

            return result.Value;
        }

        public void SetRotation(int objectId, double rotation)
        {
            if (!Set<double>(objectId, "回転角度設定", rotation))
                throw new AutoCadException("Failed to set rotation.");
        }

        public PointD GetPosition(int objectId)
        {
            Result<double[]> result = this.Get<double[]>(objectId, "基点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get position of block.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public void SetPosition(int blockRefId, PointD position)
        {
            if (!Set<double[]>(blockRefId, "基点設定", position.ToArray()))
                throw new AutoCadException("Failed to set position.");
        }

        /// <summary>ブロック図形を囲む最小の矩形の左下と右上の座標を返す</summary>
        public List<PointD> GetBlockBound(int objectId)
        {
            var result = this.Get<object[]>(objectId, "ジオメトリ範囲最適フィット取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get block bound.");

            var list = new List<PointD>();
            foreach (var vertex in result.Value)
            {
                var point = (double[])vertex;

                list.Add(new PointD(point[0], point[1]));
            }

            return list;
        }

        public PointD GetBlockCenterPosition(int objectId)
        {
            var points = this.GetBlockBound(objectId);

            return new PointD((points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2);
        }

        public int GetBlockId(int blockReferenceId)
        {
            var result = this.Get<int>(blockReferenceId, "ブロックテーブルレコード取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get block id.");

            return result.Value;
        }

        public string GetBlockName(int blockReferenceId)
        {
            var blockId = AutoCad.Db.BlockReference.GetBlockId(blockReferenceId);
            return AutoCad.Db.BlockTableRecord.GetBlockName(blockId);
        }

        public void SetScaleFactor(int blockReferenceId, double scale)
        {
            this.SetScaleFactor(blockReferenceId, scale, scale, 1);
        }

        public void SetScaleFactor(int blockReferenceId, double scaleX, double scaleY, double scaleZ)
        {
            if (!Set<double[]>(blockReferenceId, "尺度設定", new double[] { scaleX, scaleY, scaleZ }))
                throw new AutoCadException("Failed to set scale factor.");
        }

        //TODO 使われていないようだが・・・
        public int InsertToModel(string blockPath, PointD position, double rotation)
        {
            return this.InsertToModel(blockPath, position, rotation, false);
        }

        public int InsertToModel(string blockPath, PointD position, double rotation, bool isMirror)
        {
            var blockName = Path.GetFileNameWithoutExtension(blockPath);
            if (!AutoCad.Db.BlockTable.Exist(blockName))
            {
                //erase last で消しそびれないように、画面中央に挿入する
                var center = AutoCad.Db.ViewportTableRecord.GetCenterPointOfModelLayout();
                AutoCad.Command.SendLine("-insert\n" + blockPath + "\n" + center.ToString() + "\n1.0\n\n0.0");
                AutoCad.Command.SendLine("erase\nlast\n");
            }

            var id = AutoCad.Db.BlockReference.Make(blockName, position);
            AutoCad.Db.BlockReference.SetRotation(id, rotation);
            if (isMirror)
                AutoCad.Db.BlockReference.SetScaleFactor(id, -1, 1, 1);

            return id;
        }

        /// <summary>Makeで挿入するとブロック定義内にある属性がなくなるようだ・・・気をつけて使いましょう</summary>
        public int Insert(string filePath, PointD position)
        {
            var blockName = Path.GetFileNameWithoutExtension(filePath);
            if (!AutoCad.Db.BlockTable.Exist(blockName))
            {
                //たまに登録に失敗するので、最大10回試行する
                for (int i = 0; i < 10; i++)
                {
                    this.RegisterBlock(filePath);
                    if (AutoCad.Db.BlockTable.Exist(blockName))
                        break;
                }
            }

            return AutoCad.Db.BlockReference.Make(blockName, position);
        }

        public int Insert(string filePath, double x, double y)
        {
            return this.Insert(filePath, new PointD(x, y));
        }

        /// <summary>座標0,0が画面内に表示されていないと動かないかも</summary>
        private void RegisterBlock(string blockPath)
        {
            AutoCad.Command.InsertBlock(blockPath, new PointD(0, 0));
            AutoCad.Command.EraseLast();
        }
    }
}