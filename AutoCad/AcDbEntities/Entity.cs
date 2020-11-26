using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Entity : Object
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbEntity; } }

        public List<int> GetAll(string layerName)
        {
            Filter filter = new Filter();
            filter.Add(new FilterOption.LayerName(layerName));
            return filter.Execute();
        }

        /// <summary>オブジェクトに描画を促す。機能しない？致命的なエラーが発生する</summary>
        public void Redraw(int objectId)
        {
            if (!this.Do(objectId, "描画"))
                throw new AutoCadException("Failed to redraw");
        }

        public int GetLayerId(int objectId)
        {
            Result<int> result = this.Get<int>(objectId, "画層ID取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get layer id.");

            return result.Value;
        }

        public string GetLayerName(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "画層取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get layer name.");

            return result.Value;
        }

        public void SetLayerName(int objectId, string name)
        {
            if (!this.Set<string>(objectId, "画層設定", name))
                throw new AutoCadException("Failed to set layer name.");
        }

        public string GetLineType(int objectId)
        {
            Result<string> result = this.Get<string>(objectId, "線種取得");

            if (!result.Success)
                throw new AutoCadException("Failed to get line type.");

            return result.Value;
        }

        public void SetLineType(int objectId, string lineType)
        {
            if (!this.Set<string>(objectId, "線種設定", lineType))
                throw new AutoCadException("Failed to set line type.");
        }

        public void SetLineTypeScale(int objectId, double scale)
        {
            if (!this.Set<double>(objectId, "線種尺度設定", scale))
                throw new AutoCadException("Failed to set line type scale.");
        }

        //平面的に見て交差している座標を取得する
        public List<PointD> GetIntersect2D(int objectIdA, int objectIdB)
        { //objectAとobjectBの交点を取得する。複数点で交わっていた場合は、交わっている分だけ座標を返す。
            var args = new object[5];
            args[0] = objectIdB;    //対象図形のオブジェクトID
            args[1] = (short)0;     //交差タイプ「基本交差」=0

            var basePoint = new double[3];
            basePoint[0] = 0;
            basePoint[1] = 0;
            basePoint[2] = 0;
            var vectorU = new double[3];
            vectorU[0] = 1;
            vectorU[1] = 0;
            vectorU[2] = 0;
            var vectorV = new double[3];
            vectorV[0] = 0;
            vectorV[1] = 1;
            vectorV[2] = 0;

            //この3つを引数に加えると、投影平面上で交差を判定してくれる。
            //配管のRをつけた所で交差してる場合、なぜか交点を取得できない。それへの対処です。
            args[2] = basePoint;    //「投影平面」の原点座標値	
            args[3] = vectorU;      //「投影平面」のu方向ベクトル
            args[4] = vectorV;      //「投影平面」のv方向ベクトル	

            Result<object[]> result = this.Get<object[], object[]>(objectIdA, "交点取得", args);

            if (!result.Success)
                throw new AutoCadException("Failed to get intersect of line.");

            if (result.Value == null)
                return new List<PointD>();

            List<PointD> list = new List<PointD>();
            foreach (object point in result.Value)
            {
                double[] dPoint = (double[])point;
                list.Add(new PointD(dPoint[0], dPoint[1]));
            }

            return list;
        }

        public bool IsIntersect(int objectIdA, int objectIdB)
        {
            return this.GetIntersect2D(objectIdA, objectIdB).Count > 0;
        }

        //立体的に見て交差している座標を取得する。しかし、ポリラインのカーブ部分との交差判定がされなかったり、ブロックの属性にあたり判定があったりして使い物にならない。
        //public List<PointD> GetIntersect3D(int objectIdA, int objectIdB)
        //{
        //    var args = new List<object>();
        //    args.Add(objectIdB);
        //    args.Add((short)0);

        //    var result = this.Get<object[], object[]>(objectIdA, "交点取得", args.ToArray());

        //    if (!result.Success)
        //        throw new AutoCadException("Failed to get intersect.");

        //    if (result.Value == null)
        //        return new List<PointD>();

        //    List<PointD> list = new List<PointD>();
        //    foreach (object point in result.Value)
        //    {
        //        double[] dPoint = (double[])point;
        //        list.Add(new PointD(dPoint[0], dPoint[1]));
        //    }

        //    return list;
        //}

        public void SetColor(int objectId, CadColor color)
        {
            if (!this.Set<int>(objectId, "色設定", color.Code))
                throw new AutoCadException("Failed to set color.");
        }

        public short GetColor(int objectId)
        {
            Result<short> result = this.Get<short>(objectId, "色取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get color.");

            return result.Value;
        }

        public List<PointD> GetEntityBound(int objectId)
        {
            var result = this.Get<object[]>(objectId, "矩形範囲取得");

            if (!result.Success)
                throw new AutoCadException("Failed to get entity bound.");

            if (result.Value == null)
                return new List<PointD>();

            List<PointD> list = new List<PointD>();
            foreach (object point in result.Value)
            {
                double[] dPoint = (double[])point;
                list.Add(new PointD(dPoint[0], dPoint[1]));
            }

            return list;
        }

        public double GetWidth(int objectId)
        {
            var bound = this.GetEntityBound(objectId);
            return bound[1].X - bound[0].X;
        }

        public double GetHeight(int objectId)
        {
            var bound = this.GetEntityBound(objectId);
            return bound[1].Y - bound[0].Y;
        }

        public PointD GetSize(int objectId)
        {
            var bound = this.GetEntityBound(objectId);

            return new PointD(bound[1].X - bound[0].X, bound[1].Y - bound[0].Y);
        }

        public PointD GetCenter(int objectId)
        {
            var bound = this.GetEntityBound(objectId);
            return new PointD((bound[0].X + bound[1].X) / 2, (bound[0].Y + bound[1].Y) / 2);
        }

        public void SetLineWeight(int objectId, int weight)
        {
            if (!this.Set<short>(objectId, "線の太さ設定", (short)weight))
                throw new AutoCadException("Failed to set line weight.");
        }

        public void Highlight(int objectId)
        {
            if (!this.Do(objectId, "ハイライト"))
                throw new AutoCadException("Failed to highlight");
        }

        public void Unhighlight(int objectId)
        {
            if (!this.Do(objectId, "ハイライト解除"))
                throw new AutoCadException("Failed to unhighlight");
        }

        public List<int> Explode(int objectId)
        {
            var result = this.Get<int[]>(objectId, "分解");
            if (!result.Success)
                throw new AutoCadException("Failed to explode block.");

            return new List<int>(result.Value);
        }

        public void SetVisible(int objectId, bool visible)
        {
            if (!this.Set<short>(objectId, "表示属性設定", visible ? (short)0 : (short)1))
                throw new AutoCadException("Failed to set entity visible.");
        }

        public int Copy(int objectId)
        {
            var unitMatrix = Calc.GetUnitMatrix();

            Result<int> result = this.Get<int, double[,]>(objectId, "変換複写", unitMatrix);
            if (!result.Success)
                throw new AutoCadException("Failed to copy object.");

            return result.Value;
        }
    }
}