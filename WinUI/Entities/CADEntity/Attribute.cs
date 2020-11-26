using System;
using System.Collections.Generic;
using System.Text;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class Attribute
    {
        public int ObjectId { get; set; }
        public string Tag { get; set; }
        public string Value { get; set; }
        public UpdateStatus UpdateStatus { get; set; }

        public int BlockId { get; set; } //冗長だが、ブロックに属性を追加する時に便利なので。

        //TODO ★ここリファクタリング最優先★
        public static Attribute New(int blockId, string tag, string value, Attribute oldA)
        {
            Attribute newA = new Attribute();
            newA.BlockId = blockId;
            newA.Tag = tag;
            newA.Value = value;

            if (oldA == null)
            {
                if (string.IsNullOrEmpty(value))
                    newA.UpdateStatus = UpdateStatus.None;
                else
                    newA.UpdateStatus = UpdateStatus.Insert;
            }
            else
            {
                newA.ObjectId = oldA.ObjectId;
                if (string.IsNullOrEmpty(value))
                {
                    newA.UpdateStatus = UpdateStatus.Delete;
                    if (tag == Const.AttributeTag.HEIGHT)
                    { //高さ属性は削除させない。
                        newA.Value = "H=0";
                        newA.UpdateStatus = UpdateStatus.Update;
                    }
                }
                else
                    newA.UpdateStatus = UpdateStatus.Update;
            }

            return newA;
        }

        public void Update()
        {
            AutoCad.Db.Attribute.SetText(this.ObjectId, this.Value);
        }

        public void Store(PointD relativePosition, bool pointFrom左下)
        {
            if (this.UpdateStatus == UpdateStatus.Insert)
            {
                this.ObjectId = Attribute.Make(this.BlockId, this.Tag, this.Value, relativePosition, pointFrom左下);
            }
            else if (this.UpdateStatus == UpdateStatus.Update)
            {
                AutoCad.Db.Attribute.SetText(this.ObjectId, this.Value);
            }
            else if (this.UpdateStatus == UpdateStatus.Delete)
            {
                AutoCad.Db.Object.Erase(this.ObjectId);
            }
        }

        public static int Make(int blockRefId, string title, string value, PointD relativePoint, bool pointFrom左下)
        {
            var drawing = Drawing.GetCurrent();

            if (drawing.IsElevation)
                return AutoCad.Db.Attribute.Make(blockRefId, title, value, relativePoint, pointFrom左下, Const.TEXT_HEIGHT_E);
            else
                return AutoCad.Db.Attribute.Make(blockRefId, title, value, relativePoint, pointFrom左下, Const.TEXT_HEIGHT);
        }

        public void Store(PointD relativePosition, bool pointFrom左下, double textHeight)
        {
            if (this.UpdateStatus == UpdateStatus.Insert)
            {
                this.ObjectId = Attribute.Make(this.BlockId, this.Tag, this.Value, relativePosition, pointFrom左下, textHeight);                
            }
            else if (this.UpdateStatus == UpdateStatus.Update)
            {
                AutoCad.Db.Attribute.SetText(this.ObjectId, this.Value);
            }
            else if (this.UpdateStatus == UpdateStatus.Delete)
            {
                AutoCad.Db.Object.Erase(this.ObjectId);
            }
        }

        public static int Make(int blockRefId, string title, string value, PointD relativePoint, bool pointFrom左下, double textHeight)
        {
            return AutoCad.Db.Attribute.Make(blockRefId, title, value, relativePoint, pointFrom左下, textHeight);
        }

        public static void Move(string blockName, int attId, string attValue)
        {
            var attBound = AutoCad.Db.Entity.GetEntityBound(attId);

            WindowController2.BringAutoCadToTop();

            string command = "-attedit" + "\n";
            command += "y" + "\n";
            command += blockName + "\n";
            command += "*" + "\n"; //タグに小文字が含まれていると、なぜかひっかからないので。
            command += attValue + "\n";
            command += "fence\n" + attBound[0].X + "," + attBound[0].Y + "\n" + (attBound[1].X) + "," + (attBound[1].Y) + "\n" + "\n" + "\n";
            command += "p";
            AutoCad.Command.SendLineEsc(command);

            AutoCad.Status.WaitHistory("文字列の新しい挿入位置を指定 <変更しない>:", "Specify new text insertion point <no change>:");
            if (AutoCad.Status.IsCanceled())
                return;

            AutoCad.Command.SendLine(string.Empty);
        }

        public void Move(string blockName)
        {
            Attribute.Move(blockName, this.ObjectId, this.Value);
        }

        public static List<Attribute> GetAll(int blockRefId)
        {
            var attributeIds = AutoCad.Db.BlockReference.GetAttributes(blockRefId);

            var list = new List<Attribute>();
            foreach (var id in attributeIds)
            {
                var attribute = new Attribute();
                attribute.BlockId = blockRefId;
                attribute.ObjectId = id;
                attribute.Tag = AutoCad.Db.Attribute.GetTag(id);
                attribute.Value = AutoCad.Db.Text.GetText(id);
                list.Add(attribute);
            }

            return list;
        }

        public static void UpdateAttributes(List<Attribute> attributes)
        {
            AutoCad.Db.LayerTableRecord.Make(Const.Layer.電気_コメント, CadColor.BlackWhite, Const.LineWeight.Default);
            AutoCad.Command.SetCurrentLayer(Const.Layer.電気_コメント);

            PointD relativePosition = new PointD(0, 0);

            foreach (var attribute in attributes)
            {
                if (attribute.Tag == Const.AttributeTag.OTHER)
                    relativePosition.Y -= 150;

                attribute.Store(relativePosition, true);
            }

            AutoCad.Command.Refresh();
        }

        public void ChangeVisible()
        {
            var visible = AutoCad.Db.Attribute.GetVisible(this.ObjectId);
            AutoCad.Db.Attribute.SetVisible(this.ObjectId, !visible);

            AutoCad.Command.Refresh();
        }
    }

    public enum UpdateStatus
    {
        Insert,
        Update,
        Delete,
        None    
    }
}
