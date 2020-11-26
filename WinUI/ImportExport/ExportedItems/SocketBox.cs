using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI.SocketPlanServiceReference
{
    public partial class SocketBox
    {
        public string patternName { get; set; }

        public int Size { get; set; }
        public string DwgPath { get; set; }
        public PointD ShownLocation { get; set; }
        public SocketPlanDirection SPDirection { get; set; }
        public int ObjectId { get; set; }
        public bool IsExistsDwg { get; set; }

        public bool IsLeftSide
        {
            get { return this.SPDirection == SocketPlanDirection.LeftUp ||
                            this.SPDirection == SocketPlanDirection.LeftDown; }
        }

        public PointD ActualSymbolLocation
        {
            get { return new PointD((double)this.ActualLocationX, (double)this.ActualLocationY); }
            set
            {
                this.ActualLocationX = (decimal)value.X;
                this.ActualLocationY = (decimal)value.Y;
            }
        }

        public PointD SymbolLocation
        {
            get { return new PointD((double)this.SymbolLocationX, (double)this.SymbolLocationY); }
            set
            {
                this.SymbolLocationX = (decimal)value.X;
                this.SymbolLocationY = (decimal)value.Y;
            }
        }

        public PointD BoxLeftLocation
        {
            get { return new PointD((double)this.BoxLeftLocationX, (double)this.BoxLeftLocationY); }
            set
            {
                this.BoxLeftLocationX = (decimal)value.X;
                this.BoxLeftLocationY = (decimal)value.Y;
            }
        }

        public PointD BoxRightLocation
        {
            get { return new PointD((double)this.BoxRightLocationX, (double)this.BoxRightLocationY); }
            set
            {
                this.BoxRightLocationX = (decimal)value.X;
                this.BoxRightLocationY = (decimal)value.Y;
            }
        }


        public string ToCSVRow()
        {
            return
                this.Floor.ToString() + "," +
                this.Size.ToString() + "," +
                this.Height.ToString() + "," +
                this.ActualLocationX.ToString("0.0000") + "," +
                this.ActualLocationY.ToString("0.0000");
        }
    }
}
