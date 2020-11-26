using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public partial class Validation
    {
        #region 認証可能

        /// <summary>DenkiWireの両端について、片方でもシンボルがない場合エラーとする。</summary>
        public static void ValidateWireConnectionSymbol(List<Wire> wires, List<Symbol> symbols)
        {
            string messageId = @"There is a wire which is not connected to symbol.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorWires = new List<Wire>();

                foreach (var wire in wires)
                {
                    if (wire is RisingWire)
                        continue;

                    int count = 0;
                    foreach (var symbol in symbols)
                    {
                        if (wire.IsConnected(symbol))
                            count++;
                    }

                    if (count < 2)
                        errorWires.Add(wire);
                }

                if (errorWires.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errorWires.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        /// <summary>Ceiling Panelの線上にJoint Boxシンボルが重なっていればエラー</summary>
        public static void ValidateJBOnCeilingPanel(List<Symbol> symbols, List<CeilingPanel> panels)
        {
            string messageId = 
@"Joint Box was installed at the edge of the ceiling panel.
Move it to other.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errors = new List<Symbol>();
                var jointBoxes = symbols.FindAll(p => p.IsJointBox);

                if (jointBoxes.Count == 0)
                    return null;

                foreach (var jointBox in jointBoxes)
                {
                    var ceilingPanels = panels.FindAll(p => p.Floor == jointBox.Floor);

                    if (ceilingPanels.Count == 0)
                        continue;

                    foreach (var ceilingPanel in ceilingPanels)
                    {
                        var entity = new Edsa.AutoCadProxy.Entity();
                        var crossPoint = entity.GetIntersect2D(jointBox.ObjectId, ceilingPanel.ObjectId);

                        if (0 < crossPoint.Count)
                            errors.Add(jointBox);
                    }
                }
                
                if (errors.Count == 0)
                    return null;

                var error = new ErrorDialog(messageId);
                errors.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        #endregion

        #region 認証不可

        /// <summary>DenkiWireの両端がスイッチである場合エラーとする。</summary>
        public static void ValidateWireConnection(List<Wire> wires, List<Symbol> symbols)
        {
            string messageId = @"No Wire Connection for switch to switch.";

            var validator = new Validator();
            validator.Validate = delegate()
            {
                var errorWires = new List<Wire>();
                var error = new ErrorDialog(messageId, false);

                var switches = symbols.FindAll(p => p.IsSwitch);

                foreach (var swSymbol in switches)
                {
                    var denkiWires = wires.FindAll(p => p.IsConnected(swSymbol));

                    foreach (var wire in denkiWires)
                    {
                        var connectSymbol = switches.Find(p => wire.IsConnected(p)
                                                            && swSymbol.ObjectId != p.ObjectId);

                        if (connectSymbol == null)
                            return null;

                        if (connectSymbol.IsSensorSwitch)
                            return null;

                        if (connectSymbol.IsKatteniSwitch)
                            return null;

                        errorWires.Add(wire);
                    }
                }

                if (errorWires.Count == 0)
                    return null;

                errorWires.ForEach(p => error.AddInfo(p));
                return error;
            };

            validator.Run(messageId);
        }

        #endregion

        #region 警告

        #endregion
    }
}
