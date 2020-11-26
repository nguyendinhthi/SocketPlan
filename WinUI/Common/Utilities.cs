using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Globalization;
using Edsa.AutoCadProxy;
using System.Drawing.Drawing2D;

namespace SocketPlan.WinUI
{
    public class Utilities
    {
        /// <summary>0102Cみたいな文字列を1-2Cにする。0102は1-2にする。0900は9-0にしなきゃ。</summary>
        public static string ConvertPlanNo(string planNoWithZero)
        {
            var first = planNoWithZero.Substring(0, 2);
            var second = planNoWithZero.Substring(2);

            if (first.StartsWith("0"))
                first = first.Remove(0, 1);

            if (second.StartsWith("0"))
                second = second.Remove(0, 1);

            return first + "-" + second;
        }

        /// <summary>ミリメートルをメートルに換算し、少数第3位を切り上げる。1.23456を1.24にする</summary>
        public static decimal ToMeter(decimal mm)
        {
            return Math.Ceiling(mm / 10) / 100;
        }

        /// <summary>数値をアルファベットに変換する。1→A,26→Z</summary>
        public static string ToAlphabet(int number)
        {
            return ((char)('A' + number - 1)).ToString();
        }
        /// <summary>アルファベットを数値に変換する。A→1,Z→26</summary>
        public static string AlphabetToNumber(char alphabet)
        {
            return ((alphabet - (char)'A') + 1).ToString();
        }

        /// <summary>数値を○付き数字に変換する。1→①,20→⑳</summary>
        public static string ToMaruNumber(int number)
        {
            if (20 < number) //21からは○数字がない。
                return number.ToString();

            return ((char)('①' + number - 1)).ToString();
        }
        /// <summary>○付き数字から○を取って返す。変換できない場合はそのまま返す。</summary>
        public static string DecodeMaruNumber(string number)
        {
            int ret = 0;
            if (string.IsNullOrEmpty(number))
                return number;

            if (Int32.TryParse(number, out ret))
                return number;

            if (number.Length > 1)
                return number;

            var chr = number[0];
            for (var hex = (char)('①'); hex <= (char)('⑳'); hex++)
            {
                ret++;
                if (chr == hex)
                    return ret.ToString();
            }
            return number;
        }

        /// <summary>[全角/半角]、[かな/カナ]、[大文字/小文字]を無視して文字列を比較する</summary>
        public static bool EqualIgnoreWidthCase(string a, string b)
        {
            var cultureInfo = new CultureInfo("ja-JP");
            var compareInfo = cultureInfo.CompareInfo;
            int diff = compareInfo.Compare(a, b, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase);

            return diff == 0;
        }

        /// <summary>半角を全角に変換する</summary>
        public static string ConvertToZenkaku(string val)
        {
            return Strings.StrConv(val, VbStrConv.Wide, 0x0411);
        }

        /// <summary>val内の（２）のような全角括弧つき数字を○つき数字に変換する</summary>
        public static string ConvertToMaruNumber(string val)
        {
            var temp = val;

            for (int i = 1; i <= 20; i++)
            {
                var zenkakuNumber = Utilities.ConvertToZenkaku(i.ToString());
                var maruNumber = Utilities.ToMaruNumber(i);

                temp = temp.Replace("（" + zenkakuNumber + "）", maruNumber);
            }

            return temp;
        }

        /// <summary>targetがconditionsのいずれかに一致したらtrueを返す。SQLのIN句的な。</summary>
        public static bool In(string target, params string[] conditions)
        {
            //3.0だったら拡張メソッドで実装できるのに・・・
            foreach (var condition in conditions)
            {
                if (target == condition)
                    return true;
            }

            return false;
        }

        public static double GetDistance(PointD from, PointD to)
        {
            return Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2));
        }

        //座標を一定角度回転させる
        public static PointD GetRotatePoint(PointD p, double angle) 
        {
            PointD result = new PointD();

            double x1, y1, x, y, a;
            x1 = Double.Parse(p.X.ToString());
            y1 = Double.Parse(p.Y.ToString());
            a = Math.PI * angle /180;
            x = x1 * Math.Cos(a) - y1 * Math.Sin(a);
            y = x1 * Math.Sin(a) + y1 * Math.Cos(a);

            return new PointD(x, y);
        }

        //indexつき座標のKeyValuePairをX座標順に整列して戻す
        public static int ComparePointwithIndexHorizontal(KeyValuePair<int, PointD> p, KeyValuePair<int, PointD> q)
        {
            return ((PointD)p.Value).X.CompareTo(((PointD)q.Value).X);
        }
        public static int ComparePointwithIndexVertical(KeyValuePair<int, PointD> p, KeyValuePair<int, PointD> q)
        {
            return ((PointD)p.Value).Y.CompareTo(((PointD)q.Value).Y);
        }

        public static int GetLastObjectId()
        {
            int? objectId;
            int loopCount = 0;

            while (true)
            {
                objectId = AutoCad.Selection.GetLastObjectId();
                if (objectId.HasValue && AutoCad.Db.BlockReference.IsType(objectId.Value))
                    return objectId.Value;

                loopCount++;
                if (5 < loopCount)
                    throw new ApplicationException(Messages.HeightNotSet());
            }
        }

        public static void Rename(string oldName, string newName)
        {
            AutoCad.Command.SendLineEsc(
                "-RENAME\nB\n" + oldName + "\n" + newName + "\n");
            AutoCad.Command.SendLineEsc("");
        }

        public static SocketPlanDirection GetSocketPlanDirection(PointD position, PointD centerposition)
        {
            var radian = AutoCad.Db.BlockReference.GetAnglePointToPoint(centerposition, position);
            var angle = radian * 180 / Math.PI;

            if (0 <= angle && angle < 90)
                return SocketPlanDirection.RightUp;
            else if (90 <= angle && angle < 180)
                return SocketPlanDirection.LeftUp;
            else if (180 <= angle && angle < 270)
                return SocketPlanDirection.LeftDown;
            else if (270 <= angle && angle < 360)
                return SocketPlanDirection.RightDown;
            else
                throw new ApplicationException("Failed to sceparate in each area");
        }

        /// <summary>
        /// 誤差を許容する座標一致
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="margin"></param>
        /// <returns></returns>
        public static bool EqualPointWithMargin(PointD p, PointD q, double margin)
        {
            if (Math.Abs(Math.Round(p.X) - Math.Round(q.X)) > margin)
                return false;

            if (Math.Abs(Math.Round(p.Y) - Math.Round(q.Y)) > margin)
                return false;

            return true;
        }
    }
}
