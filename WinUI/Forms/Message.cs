using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;
using SocketPlan.WinUI.Entities.CADEntity;

namespace SocketPlan.WinUI
{
    public class Message
    {
        private List<string[]> messages = new List<string[]>();

        /// <summary>英語のメッセージ</summary>
        public string EnglishText { get; set; }

        /// <summary>日本語のメッセージ</summary>
        public string JapaneseText { get; set; }

        /// <summary>問題の特定に繋がる情報を付け加える</summary>
        public void AddInfo(string text)
        {
            this.messages.Add(new string[] { text, string.Empty });
        }

        /// <summary>問題の特定に繋がる情報を付け加える</summary>
        public void AddInfo(string name, string value)
        {
            this.messages.Add(new string[] { name, value });
        }

        /// <summary>問題の特定に繋がるシンボル情報を付け加える</summary>
        public void AddInfo(Symbol symbol)
        {
            this.AddInfo("◆Symbol◆");
            this.AddInfo("Name", symbol.BlockName);
            this.AddInfo("Floor", symbol.Floor.ToString());
            this.AddInfo("Location", symbol.Position.X.ToString("0.000") + ", " + symbol.Position.Y.ToString("0.000"));
        }

        /// <summary>問題の特定に繋がる配線情報を付け加える</summary>
        public void AddInfo(Wire wire)
        {
            var infoWires = wire.GetAllChildrenWire();
            foreach (var infoWire in infoWires)
            {
                this.AddInfo("■Wire■");
                this.AddInfo("Floor", infoWire.Floor.ToString());
                this.AddInfo("StartPoint", infoWire.StartPoint.X.ToString("0.000") + ", " + infoWire.StartPoint.Y.ToString("0.000"));
                this.AddInfo("EndPoint", infoWire.EndPoint.X.ToString("0.000") + ", " + infoWire.EndPoint.Y.ToString("0.000"));
            }
        }

        public void AddInfo(PointD startPoint, PointD endPoint)
        {
            this.AddInfo("◆Line◆");
            this.AddInfo("StartPoint", startPoint.X.ToString("0.000") + ", " + startPoint.Y.ToString("0.000"));
            this.AddInfo("EndPoint", endPoint.X.ToString("0.000") + ", " + endPoint.Y.ToString("0.000"));
        }

        /// <summary>プロジェクトで決められたメッセージ書式に変換する</summary>
        public override string ToString()
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(this.EnglishText))
                msg += this.EnglishText + Environment.NewLine;
            if (!string.IsNullOrEmpty(this.JapaneseText))
                msg += this.JapaneseText + Environment.NewLine;

            if (this.messages.Count == 0)
                return msg;

            msg += Environment.NewLine;
            msg += "   === Information ===" + Environment.NewLine;
            foreach (var item in this.messages)
            {
                if (item[1] == string.Empty)
                    msg += item[0];
                else
                    msg += item[0] + " : " + item[1];

                if (this.messages.IndexOf(item) == this.messages.Count - 1)
                    break; //最終行は改行しないで抜ける。

                msg += Environment.NewLine;
            }

            return msg;
        }
    }
}
