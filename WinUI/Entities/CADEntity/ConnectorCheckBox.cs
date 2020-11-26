using System;
using System.Collections.Generic;
using Edsa.AutoCadProxy;

namespace SocketPlan.WinUI
{
    public class ConnectorCheckBox
    {
        public string ItemNo { get; set; }
        public int floor { get; set; }
        public int seq { get; set; }
        public List<string> ConnectorNums { get; set; }

        public static List<ConnectorCheckBox> CreateConnectorCheckboxes(List<Symbol> checkboxSymbols)
        {
            var connectorBoxs = new List<ConnectorCheckBox>();
            foreach (var box in checkboxSymbols)
            {
                var attributes = box.Attributes;
                var connector = new ConnectorCheckBox();
                connector.ConnectorNums = new List<string>();
                connector.floor = box.Floor;
                foreach (var attribute in attributes)
                {
                    if (string.IsNullOrEmpty(attribute.Value))
                        continue;

                    if (attribute.Value.StartsWith("■"))
                        connector.ConnectorNums.Add(attribute.Value.Replace("■", ""));
                    else
                        connector.ItemNo = attribute.Value;
                }

                if (!string.IsNullOrEmpty(connector.ItemNo))
                    connectorBoxs.Add(connector);
            }

            return connectorBoxs;
        }

    }
}
