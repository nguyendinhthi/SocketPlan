using System.Collections.Generic;

namespace Edsa.AutoCadProxy
{
    public class Curve : Entity
    {
        protected override ObjectType ObjectType { get { return ObjectType.AcDbCurve; } }

        public PointD GetStartPoint(int objectId)
        {
            Result<double[]> result = this.Get<double[]>(objectId, "始点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get start point of line.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        public PointD GetEndPoint(int objectId)
        {
            Result<double[]> result = this.Get<double[]>(objectId, "終点取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get end point of line.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        /// <summary>プラスだと線の右側に、マイナスだと線の左側にオフセットを作る。</summary>
        public List<int> MakeOffsetLine(int objectId, double offset)
        {
            Result<int[]> result = this.Get<int[], double>(objectId, "オフセット図形作成", offset);
            if (!result.Success) //オフセットを描くスペースが無い時はオフセットを作らない。
                return new List<int>();

            return new List<int>(result.Value);
        }

        /// <summary>
        /// 【注意】このメソッドはCurveで呼び出してください。継承先から呼び出すとエラーになります。
        /// 図形上の点から、パラメータを取得します。
        /// </summary>
        public double GetParameter(int objectId, PointD point)
        {
            Result<double> result = this.Get<double, double[]>(objectId, "点からパラメータ取得", new double[] { point.X, point.Y, 0 });
            if (!result.Success)
                throw new AutoCadException("Failed to get parameter by point.");

            return result.Value;
        }

        /// <summary>パラメータによって特定される図形上の点を取得します。</summary>
        public PointD GetPoint(int objectId, double parameter)
        {
            Result<double[]> result = this.Get<double[], double>(objectId, "パラメータから点取得", parameter);
            if (!result.Success)
                throw new AutoCadException("Failed to get point by parameter.");

            return new PointD(result.Value[0], result.Value[1]);
        }

        /// <summary>指定した座標が指定した図形上にあればTrueを返す</summary>
        public bool IsOn(PointD point, int objectId)
        {
            //メソッドの成否で戻り値を決めるので、エラーが出ることを前提としている。そのため、一時的にエラー出力を切る。
            AutoCad.vbcom.SetErrorType(2);
            Result<double> result = this.Get<double, double[]>(objectId, "点から距離取得", new double[] { point.X, point.Y, 0 });
            AutoCad.vbcom.SetErrorType(1); //エラー出力を戻す

            return result.Success;
        }

        /// <summary>線を分割する。パラメータの指定方法は線の種類によって異なる</summary>
        public List<int> GetSplitCurves(int objectId, List<double> parameter)
        {
            //parameterが1つの時にエラーが出る。なんだそれ！　0を先頭に詰めれば回避できた。
            parameter.Insert(0, 0);

            Result<int[]> result = this.Get<int[], double[]>(objectId, "分割図形作成", parameter.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to get split curves.");

            return new List<int>(result.Value);
        }

        public virtual double GetLength(int objectId)
        {
            PointD start = this.GetStartPoint(objectId);
            PointD end = this.GetEndPoint(objectId);

            if (start.Equals(end))
                return 0d; //長さが0の線に対して距離取得すると失敗するので、回避だ！

            Result<double> result = this.Get<double, double[]>(objectId, "点から距離取得", end.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to get length of curve.");

            return result.Value;
        }

        public double GetLength(int objectId, double parameter)
        {
            if (parameter <= 0)
                return 0;

            Result<double> result = this.Get<double, double>(objectId, "パラメータから距離取得", parameter);
            if (!result.Success)
                throw new AutoCadException("Failed to get length of polyline.");

            return result.Value;
        }
        
        /// <summary>始点から、指定する図形上の位置（点）までの図形に沿った距離を取得する</summary>
        public double GetLength(int objectId, PointD point)
        {
            Result<double> result = this.Get<double, double[]>(objectId, "点から距離取得", point.ToArray());
            if (!result.Success)
                throw new AutoCadException("Failed to get length of polyline.");

            return result.Value;
        }

        public bool IsClosed(int objectId)
        {
            var result = this.Get<short>(objectId, "閉じている？");
            if (!result.Success)
                throw new AutoCadException("<LT VB-COM> Failed to get isClosed.");

            return result.Value == 1;
        }

        public double GetArea(int objectId)
        {
            var result = this.Get<double>(objectId, "面積取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get area.");

            return result.Value;
        }
    }
}