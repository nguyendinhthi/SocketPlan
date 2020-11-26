using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SocketPlan.WebService.Entities
{
    public class OptionPickingResult
    {
        // Regular

        private List<OptionPickingItem> optionPickingItems = new List<OptionPickingItem>();
        public List<OptionPickingItem> OptionPickingItems
        {
            get { return this.optionPickingItems; }
            set { this.optionPickingItems = value; }
        }

        private List<ProcessLog> processLogs = new List<ProcessLog>();
        public List<ProcessLog> ProcessLogs
        {
            get { return this.processLogs; }
            set { this.processLogs = value; }
        }

        private List<SekisanMaterialDetail> sekisanMaterialDetails = new List<SekisanMaterialDetail>();
        public List<SekisanMaterialDetail> SekisanMaterialDetails
        {
            get { return this.sekisanMaterialDetails; }
            set { this.sekisanMaterialDetails = value; }
        }

        private List<SekisanMaterial> sekisanMaterials = new List<SekisanMaterial>();
        public List<SekisanMaterial> SekisanMaterials
        {
            get { return this.sekisanMaterials; }
            set { this.sekisanMaterials = value; }
        }

        // Hems

        private List<CtBox> ctBoxes = new List<CtBox>();
        public List<CtBox> CtBox
        {
            get { return this.ctBoxes; }
            set { this.ctBoxes = value; }
        }

        private List<HemsRoomBlock> hemsRoomBlocks = new List<HemsRoomBlock>();
        public List<HemsRoomBlock> HemsRoomBlocks
        {
            get { return this.hemsRoomBlocks; }
            set { this.hemsRoomBlocks = value; }
        }

        private List<HemsBlock> hemsBlocks = new List<HemsBlock>();
        public List<HemsBlock> HemsBlocks
        {
            get { return this.hemsBlocks; }
            set { this.hemsBlocks = value; }
        }

        private List<HemsDevice> hemsDevices = new List<HemsDevice>();
        public List<HemsDevice> HemsDevices
        {
            get { return this.hemsDevices; }
            set { this.hemsDevices = value; }
        }

        private List<HemsRoom> hemsRooms = new List<HemsRoom>();
        public List<HemsRoom> HemsRooms
        {
            get { return this.hemsRooms; }
            set { this.hemsRooms = value; }
        }

        private List<HemsLog> hemsLogs = new List<HemsLog>();
        public List<HemsLog> HemsLogs
        {
            get { return this.hemsLogs; }
            set { this.hemsLogs = value; }
        }

        // RN

        private List<RimokonNiche> rimokonNiches = new List<RimokonNiche>();
        public List<RimokonNiche> RimokonNiches
        {
            get { return this.rimokonNiches; }
            set { this.rimokonNiches = value; }
        }

        private List<RimokonNicheDetail> rimokonNicheDetails = new List<RimokonNicheDetail>();
        public List<RimokonNicheDetail> RimokonNicheDetails
        {
            get { return this.rimokonNicheDetails; }
            set { this.rimokonNicheDetails = value; }
        }
    }
}
