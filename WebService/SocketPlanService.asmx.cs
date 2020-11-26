using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Services;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Drawing;
using SocketPlan.WebService.Properties;
using System.Data;

namespace SocketPlan.WebService
{
    /// <summary>
    /// SocketPlanService の概要の説明です
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // この Web サービスを、スクリプトから ASP.NET AJAX を使用して呼び出せるようにするには、次の行のコメントを解除します。
    // [System.Web.Script.Services.ScriptService]
    public class SocketPlanService : System.Web.Services.WebService
    {
        #region マスタ取得系

        [WebMethod]
        public HouseTypeGroupDetail GetHouseTypeGroupDetail(string constructionTypeCode)
        {
            return HouseTypeGroupDetail.Get(constructionTypeCode);
        }

        [WebMethod]
        public List<Block> GetBlocks()
        {
            return Block.GetAll();
        }

        [WebMethod]
        public List<Comment> GetComments()
        {
            return Comment.GetAll();
        }

        [WebMethod]
        public List<CommentCategory> GetCommentCategories()
        {
            return CommentCategory.GetAll();
        }

        [WebMethod]
        public List<CommentSpecification> GetCommentSpecifications()
        {
            return CommentSpecification.GetAll();
        }

        [WebMethod]
        public List<Equipment> GetEquipments()
        {
            return Equipment.GetAll();
        }

        [WebMethod]
        public List<EquipmentCustomAction> GetEquipmentCustomActions()
        {
            return EquipmentCustomAction.GetAll();
        }

        [WebMethod]
        public List<EquipmentKind> GetEquipmentKinds()
        {
            return EquipmentKind.GetAll();
        }

        [WebMethod]
        public List<EquipmentSpecification> GetEquipmentSpecifications()
        {
            return EquipmentSpecification.GetAll();
        }

        [WebMethod]
        public List<InteriorEstimateRoom> GetInteriorEstimateRooms()
        {
            return InteriorEstimateRoom.GetAll();
        }

        [WebMethod]
        public List<Layout> GetLayouts()
        {
            return Layout.GetAll();
        }

        [WebMethod]
        public List<LightSerial> GetLightSerialsWithSetteigai()
        {
            return LightSerial.GetAll();
        }

        [WebMethod]
        public List<LightSerial> GetLightSerials()
        {
            return LightSerial.GetAll().FindAll(p => !p.RequireApproval);
        }

        [WebMethod]
        public List<LightSerial> GetEmptyStockLightSerials()
        {
            return LightSerial.GetEmptyStockLightSerials();
        }

        [WebMethod]
        public List<LightSerialCategory> GetLightSerialCategories()
        {
            return LightSerialCategory.GetAll();
        }

        [WebMethod]
        public List<RelatedEquipment> GetRelatedEquipments()
        {
            return RelatedEquipment.GetAll();
        }

        [WebMethod]
        public List<Room> GetRooms()
        {
            return Room.GetAll();
        }

        [WebMethod]
        public List<RoomStandardItem> GetRoomStandardItems(string constructionCode, string planNo)
        {
            return RoomStandardItem.GetAll(constructionCode, planNo);
        }

        [WebMethod]
        public List<RoomStandardItem> GetRoomStandardNewItems()
        {
            return RoomStandardItem.GetAll(true);
        }

        [WebMethod]
        public List<RoomStandardItem> GetRoomStandardOldItems()
        {
            return RoomStandardItem.GetAll(false);
        }

        [WebMethod]
        public List<SelectionCategory> GetSelectionCategories()
        {
            return SelectionCategory.GetAll();
        }

        [WebMethod]
        public List<SelectionCategoryDetail> GetSelectionCategoryDetails()
        {
            return SelectionCategoryDetail.GetAll();
        }

        [WebMethod]
        public List<Specification> GetSpecifications()
        {
            return Specification.GetAll();
        }

        [WebMethod]
        public List<TextItem> GetTextItems()
        {
            return TextItem.GetAll();
        }

        [WebMethod]
        public List<Text> GetTexts()
        {
            return Text.GetAll();
        }

        [WebMethod]
        public List<WireItem> GetWireItems()
        {
            return WireItem.GetAll();
        }

        [WebMethod]
        public List<LightSerialItem> GetLightSerialItems()
        {
            return LightSerialItem.GetAll().OrderBy(p => p.Serial)
                                           .ThenBy(p => p.Priority)
                                           .ToList();
        }

        [WebMethod]
        public List<FloorText> GetFloorTexts()
        {
            return FloorText.GetAll();
        }

        [WebMethod]
        public List<CupboardSerial> getCupboardSerial()
        {
            return CupboardSerial.GetAll();
        }

        [WebMethod]
        public List<KansenBuzaiItem> getKansenBuzaiItems()
        {
            return KansenBuzaiItem.GetAll();
        }

        [WebMethod]
        public List<LightElectrical> GetLightElectricals()
        {
            return LightElectrical.GetAll();
        }

        [WebMethod]
        public List<LightElectricalWireKind> GetLightElectricalWireKinds()
        {
            return LightElectricalWireKind.GetAll();
        }

        [WebMethod]
        public List<JboxItem> GetJboxItems()
        {
            return JboxItem.GetAll();
        }

        [WebMethod]
        public List<RemoconItem> GetRemoconItems()
        {
            return RemoconItem.GetAll();
        }

        [WebMethod]
        public List<RemoconZumenItem> GetRemoconZumenItems()
        {
            return RemoconZumenItem.GetAll();
        }

        [WebMethod]
        public List<UsableLightSerial> GetUsableLightSerials(string constructionCode)
        {
            return UsableLightSerial.Get(constructionCode);
        }

        [WebMethod]
        public List<UsableBracketLightSerial> GetUsableBracketLightSerials(string constructionCode)
        {
            return UsableBracketLightSerial.Get(constructionCode);
        }

        [WebMethod]
        public RimokonNicheHolePattern GetRimokonNicheHolePattern(string patternName)
        {
            return RimokonNicheHolePattern.Get(patternName);
        }

        [WebMethod]
        public List<ConvertionLightSerial> GetConvertionLightSerials(string constructionCode)
        {
            return ConvertionLightSerial.GetSerials(constructionCode);
        }

        [WebMethod]
        public List<SwitchLightSerial> GetSwitchLightSerials()
        {
            return SwitchLightSerial.GetAll();
        }

        [WebMethod]
        public List<UsableRohmOfLightSerial> GetUsableRohmOfLightSerial(string constructionCode)
        {
            return UsableRohmOfLightSerial.Get(constructionCode);
        }

        [WebMethod]
        public bool IsUsingIrisohyamaLightConstructions(string constructionCode)
        {
            var code = UsingIrisohyamaLightConstruction.Get(constructionCode);
            if (code == null || string.IsNullOrEmpty(code.ConstructionCiode))
                return false;

            return true;
        }

        [WebMethod]
        public SpcadPlanInfos_Extract GetSpcadPlanInfos_Extract(string constructionCode, string planNo)
        {
            return SpcadPlanInfos_Extract.Get(constructionCode, planNo);
        }

        [WebMethod]
        public List<InvalidDownLightSerial> GetInvalidDownLightSerials()
        {
            return InvalidDownLightSerial.GetAll();
        }

        #endregion

        #region 仕様書参照系

        [WebMethod]
        public int GetSiyoCode(string constructionCode, string planNo)
        {
            return tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
        }

        [WebMethod]
        public string GetKanabakari(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.GetKanabakari(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool IsKanabakari240(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.IsKanabakari240(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool IsKanabakari240Plus(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.IsKanabakari240Plus(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool CanDrawGlassWool(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return BasicSpecificationDetail.CanDrawGlassWool(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool ExistSolar(string constructionCode)
        {
            return SpecificationProductDetail.ExistSolar(constructionCode);
        }

        [WebMethod]
        public bool IsZehConstructionCode(string constructionCode)
        {
            var companyCode = ShikakuTableEntry.GetCompanyCode(constructionCode);
            if (!string.IsNullOrEmpty(companyCode))
            {
                if (companyCode == "02" || companyCode == "03" || companyCode == "05")
                    return false;
            }

            var code = NotElectricControlConstructions.Get(constructionCode);
            if (code != null)
                return false;

            return true;
        }

        [WebMethod]
        public string GetElectricCompanyCode(string constructionCode)
        {
            return ShikakuTableEntry.GetCompanyCode(constructionCode);
        }

        [WebMethod]
        public int GetPowerConditionerCount(string constructionCode)
        {
            return SpecificationProductDetail.GetPowerConditionerCount(constructionCode);
        }

        [WebMethod]
        public List<SiyoHeya> GetSiyoHeyas(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return SiyoHeya.Get(constructionCode, siyoCode);
        }

        [WebMethod]
        public string GetConstructionTypeName(string constructionCode, string planNo)
        {
            return tbl_siyo_kanri.Get(constructionCode, planNo).printType;
        }

        [WebMethod]
        public bool IsBeforeProcessRequest(string constructionCode)
        {
            return ConstructionSchedule.IsBeforeProcessRequest(constructionCode);
        }

        [WebMethod]
        public bool IsKanreiArea(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.IsKanreiArea(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool IsExportingXMlData(string constructionCode)
        {
            return ProcessLog.ExistExportHemsData(constructionCode);
        }

        [WebMethod]
        public bool IsTenjijyoConstruction(string constructionCode)
        {
            return Construction.IsTenjijyo(constructionCode);
        }

        #region ダウンライト対応
        [WebMethod]
        public ConstructionSchedule GetSchedule(string constructionCode)
        {
            return ConstructionSchedule.Get(constructionCode);
        }
        #endregion

        [WebMethod]
        public string GetHouseTypeCode(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return BasicSpecificationDetail.GetHouseTypeCode(constructionCode, siyoCode);
        }

        [WebMethod]
        public string GetConstructionTypeCode(string constructionCode, string planNo)
        {
            return House.GetConstructionTypeCode(constructionCode, planNo);
        }

        [WebMethod]
        public bool IsISmart(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.IsISmart(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool IsICube(string constructionCode, int siyoCode)
        {
            return BasicSpecificationDetail.IsICubeOrISmileOrIPalette(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool IsISmartICubeIHead4(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return BasicSpecificationDetail.IsISmartICubeISmileIPaletteIHead4(constructionCode, siyoCode);
        }

        [WebMethod]
        public string GetPlannerLicence(string constructionCode)
        {
            var construction = Construction.Get(constructionCode);
            var plannerCode = construction.PlannerCode;

            var firstClassArchitect = EmployeeLisence.Get(plannerCode, "003");
            if (firstClassArchitect != null && !string.IsNullOrEmpty(firstClassArchitect.RegistrationNumber))
                return "一級建築士" + this.GetLicensePrefectureName(firstClassArchitect) + "登録第" + firstClassArchitect.RegistrationNumber + "号";

            var secondClassArchitect = EmployeeLisence.Get(plannerCode, "004");
            if (secondClassArchitect != null)
                return "二級建築士" + this.GetLicensePrefectureName(secondClassArchitect) + "登録第" + secondClassArchitect.RegistrationNumber + "号";

            return string.Empty;
        }

        private string GetLicensePrefectureName(EmployeeLisence license)
        {
            if (string.IsNullOrEmpty(license.PrefectureCode))
                return string.Empty;

            var prefecture = Prefecture.Get(license.PrefectureCode);
            if (prefecture == null || string.IsNullOrEmpty(prefecture.PrefectureName))
                return string.Empty;

            return prefecture.PrefectureName + "知事";
        }

        private string GetPrefectureCode(string constructionCode)
        {
            var construction = Construction.Get(constructionCode);
            if (construction == null)
                return string.Empty;

            return construction.SiteAddressPrefectureCode;
        }

        [WebMethod]
        public string GetConstructionStory(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return BasicSpecificationDetail.GetHouseStoryCode(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool IsHemsPlan(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return BasicSpecificationDetail.IsHemsPlan(constructionCode, siyoCode);
        }

        [WebMethod]
        public bool IsZehPlan(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            if (BasicSpecificationDetail.IsZehPlan(constructionCode, siyoCode))
                return true;

            return false;
        }

        [WebMethod]
        public string GetInsulationRegion(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return BasicSpecificationDetail.GetInsulationRegionCode(constructionCode, siyoCode);
        }

        #endregion

        #region 四角テーブル系

        [WebMethod]
        public List<ShikakuTableItem> GetShikakuTableNewItems()
        {
            return ShikakuTableNewItem.GetAll();
        }

        [WebMethod]
        public List<ShikakuTableItem> GetShikakuTableOldItems()
        {
            return ShikakuTableOldItem.GetAll();
        }

        [WebMethod]
        public List<ShikakuTableItem> GetShikakuTableNewIPaletteItems()
        {
            return ShikakuTableNewIPaletteItem.GetAll();
        }

        [WebMethod]
        public List<ShikakuTableItem> GetShikakuTableOldIPaletteItems()
        {
            return ShikakuTableOldIPaletteItem.GetAll();
        }

        [WebMethod]
        public List<ShikakuTableEntry> GetShikakuTableEntries(string constructionCode)
        {
            return ShikakuTableEntry.Get(constructionCode);
        }

        [WebMethod]
        public ShikakuTableEntry GetShikakuTableEntry(string constructionCode, int itemId)
        {
            return ShikakuTableEntry.Get(constructionCode, itemId);
        }

        [WebMethod]
        public void RegisterShikakuTableEntries(List<ShikakuTableEntry> entries)
        {
            if (entries.Count == 0)
                return;

            using (var transaction = new TransactionScope())
            {
                var constructionCode = entries[0].ConstructionCode;
                ShikakuTableEntry.Delete(constructionCode);
                var now = DateTime.Now;
                foreach (var entry in entries)
                {
                    entry.UpdatedDateTime = now;
                    entry.Store();
                }

                transaction.Complete();
            }
        }

        [WebMethod]
        public void RegisterLightSerialCsvs(List<LightSerialCsv> lightSerialCsvs, string constructionCode)
        {
            using (var transaction = new TransactionScope())
            {
                LightSerialCsv.Delete(constructionCode);

                var now = DateTime.Now;
                var db = LightSerialCsv.GetDatabase();
                foreach (var lightSerialCsv in lightSerialCsvs)
                {
                    lightSerialCsv.UpdatedDate = now;
                    db.Insert<LightSerialCsv>(lightSerialCsv);
                }

                transaction.Complete();
            }
        }

        [WebMethod]
        public bool HasHemsBundenban(string constructionCode)
        {
            return ShikakuTableEntry.HasHemsBundenban(constructionCode);
        }

        #endregion

        #region 電気部品色系

        [WebMethod]
        public List<PartsColor> GetPartsColors()
        {
            return PartsColor.GetAll();
        }

        [WebMethod]
        public List<PartsColorEntry> GetPartsColorEntries(string constructionCode)
        {
            return PartsColorEntry.Get(constructionCode);
        }

        [WebMethod]
        public void RegisterPartsColorEntries(List<PartsColorEntry> entries)
        {
            if (entries.Count == 0)
                return;

            using (var transaction = new TransactionScope())
            {
                var constructionCode = entries[0].ConstructionCode;
                PartsColorEntry.Delete(constructionCode);
                var now = DateTime.Now;
                foreach (var entry in entries)
                {
                    entry.UpdatedDateTime = now;
                    entry.Store();
                }

                transaction.Complete();
            }
        }

        [WebMethod]
        public List<DefaultPartsColor> GetDefaultPartsColors()
        {
            return DefaultPartsColor.GetAll();
        }

        [WebMethod]
        public List<JboxColor> GetJboxColors()
        {
            return JboxColor.GetAll();
        }

        private List<JboxColorEntry> GetJboxColorEntries(string constructionCode, string orderColumn)
        {
            var entries = JboxColorEntry.Get(constructionCode, orderColumn);
            var equipments = Equipment.GetAll();

            foreach (var entry in entries)
            {
                entry.Equipment = equipments.Find(p => p.Name == entry.JboxEquipmentName);
            }

            return entries;
        }

        [WebMethod]
        public List<JboxColorEntry> GetJboxColorEntries(string constructionCode, bool orderBy)
        {
            var orderString = "";
            if (orderBy)
                orderString = "Floor, RoomName";
            else
                orderString = "Seq";

            return GetJboxColorEntries(constructionCode, orderString);
        }

        [WebMethod]
        public void RegisterJboxColorEntries(List<JboxColorEntry> entries)
        {
            if (entries.Count == 0)
                return;

            using (var transaction = new TransactionScope())
            {
                var constructionCode = entries[0].ConstructionCode;
                JboxColorEntry.Delete(constructionCode);
                var now = DateTime.Now;
                foreach (var entry in entries)
                {
                    entry.UpdatedDateTime = now;
                    entry.Store();
                }

                transaction.Complete();
            }
        }

        [WebMethod]
        public void DeleteJboxColorEntries(string constructionCode)
        {
            using (var transaction = new TransactionScope())
            {
                JboxColorEntry.Delete(constructionCode);
                transaction.Complete();
            }
        }

        #endregion

        #region 承認用

        [WebMethod]
        public bool ValidateUser(string userId, string password, string machineName, string message)
        {
            var user = SocketPlan.WebService.User.Get(userId);
            if (user == null)
                return false;

            if (user.Password != password)
                return false;

            this.RegisterApprovalLog(userId, machineName, message);

            return true;
        }

        [WebMethod]
        public bool ValidateUserWithoutMessage(string userId, string password, bool canMasterEdit)
        {
            var user = SocketPlan.WebService.User.Get(userId);
            if (user == null)
                return false;

            if (user.Password != password)
                return false;

            if (canMasterEdit && !user.CanMasterMaintenance)
                return false;

            return true;
        }

        private void RegisterApprovalLog(string userId, string machineName, string message)
        {
            var encoding = Encoding.GetEncoding("Shift_JIS");
            var maxByte = 980; //DB定義が1000なので、余裕を持って980にした

            var byteCount = encoding.GetByteCount(message);
            if (maxByte < byteCount)
            {
                byte[] bytes = encoding.GetBytes(message);
                message = encoding.GetString(bytes, 0, maxByte);
            }

            using (var transaction = new TransactionScope())
            {
                var log = new ApprovalLog();
                log.ApprovedDateTime = DateTime.Now;
                log.UserId = userId;
                log.MachineName = machineName;
                log.Message = message;
                log.UpdatedDateTime = DateTime.Now;

                log.Store();

                transaction.Complete();
            }
        }

        #endregion

        #region マスタメンテ用

        #region Equipments

        [WebMethod]
        public void RegisterSelectionCategory(SelectionCategory category)
        {
            using (var transaction = new TransactionScope())
            {
                if (category.Id == 0)
                {
                    category.SortNo = SelectionCategory.GetAll().Max(p => p.SortNo) + 1;
                    category.UpdatedDateTime = DateTime.Now;
                }

                category.Store();

                transaction.Complete();
            }
        }

        [WebMethod]
        public void DeleteSelectionCategory(SelectionCategory category)
        {
            using (var transaction = new TransactionScope())
            {
                //UIで制御して、Detailsを先に削除させている

                category.Drop();

                transaction.Complete();
            }
        }

        [WebMethod]
        public void UpdateSelectionCategorySortNo(List<SelectionCategory> categories)
        {
            using (var transaction = new TransactionScope())
            {
                var now = DateTime.Now;
                var sortNo = 1;
                foreach (var category in categories)
                {
                    category.SortNo = sortNo;
                    category.UpdatedDateTime = now;
                    category.Store();
                    sortNo++;
                }

                transaction.Complete();
            }
        }

        [WebMethod]
        public void UpdateEquipment(int categoryId, Equipment equipment)
        {
            using (var transaction = new TransactionScope())
            {
                equipment.Store(categoryId);

                transaction.Complete();
            }
        }

        [WebMethod]
        public void DeleteEquipment(Equipment equipment)
        {
            using (var transaction = new TransactionScope())
            {
                equipment.Drop();

                transaction.Complete();
            }
        }

        [WebMethod]
        public void UpdateEquipmentSortNo(int categoryId, List<Equipment> equipments)
        {
            using (var transaction = new TransactionScope())
            {
                SelectionCategoryDetail.Delete(categoryId);

                var now = DateTime.Now;
                var sortNo = 1;
                foreach (var equipment in equipments)
                {
                    var detail = new SelectionCategoryDetail();
                    detail.SelectionCategoryId = categoryId;
                    detail.EquipmentId = equipment.Id;
                    detail.SortNo = sortNo;
                    detail.UpdatedDateTime = now;
                    detail.Store();

                    sortNo++;
                }

                transaction.Complete();
            }
        }

        #endregion

        #region Comments

        [WebMethod]
        public void UpdateComments(List<CommentCategory> categories)
        {
            using (var transaction = new TransactionScope())
            {
                CommentCategory.Update(categories);

                transaction.Complete();
            }
        }

        #endregion

        #region Rooms

        [WebMethod]
        public Room RegisterRoom(Room room, bool isUsingNew)
        {
            using (var transaction = new TransactionScope())
            {
                room.Store(isUsingNew);

                transaction.Complete();
            }

            return room;
        }

        [WebMethod]
        public void DeleteRoom(Room room)
        {
            using (var transaction = new TransactionScope())
            {
                room.Drop();

                transaction.Complete();
            }
        }


        #endregion

        #region ProductVas

        [WebMethod]
        public List<ProductVa> GetAllProductVas()
        {
            return ProductVa.GetAll();
        }

        [WebMethod]
        public void RegisterProductVas(ProductVa[] productVas)
        {
            using (var transaction = new TransactionScope())
            {
                ProductVa.Delete();

                var now = DateTime.Now;
                foreach (var product in productVas)
                {
                    product.UpdatedDateTime = now;
                    product.Store();
                }

                transaction.Complete();
            }
        }

        #endregion

        #region InteriorEstimateRooms

        [WebMethod]
        public void UpdateInteriorEstimateRooms(List<InteriorEstimateRoom> rooms)
        {
            using (var transaction = new TransactionScope())
            {
                InteriorEstimateRoom.DeleteAll();

                foreach (var room in rooms)
                {
                    room.Store();
                }

                transaction.Complete();
            }
        }

        #endregion

        #region TextItems

        [WebMethod]
        public void UpdateTextItems(List<TextItem> textItems)
        {
            using (var transaction = new TransactionScope())
            {
                TextItem.DeleteAll();

                foreach (var textItem in textItems)
                {
                    textItem.Store();
                }

                transaction.Complete();
            }
        }

        #endregion

        #region ProductItems

        [WebMethod]
        public List<ProductItem> GetProductItemsAll()
        {
            return ProductItem.GetAll();
        }

        [WebMethod]
        public void UpdateProductItems(List<ProductItem> productItems)
        {
            using (var transaction = new TransactionScope())
            {
                ProductItem.DeleteAll();

                foreach (var productItem in productItems)
                {
                    productItem.Store();
                }

                transaction.Complete();
            }
        }

        #endregion

        #region PendantLightSerials

        [WebMethod]
        public List<PendantLightSerial> GetPendantLightSerialsAll()
        {
            return PendantLightSerial.GetAll();
        }

        [WebMethod]
        public void RegisterPendantLightSerial(PendantLightSerial[] pendantLightSerials)
        {
            using (var transaction = new TransactionScope())
            {
                PendantLightSerial.Delete();

                var now = DateTime.Now;
                foreach (var pendantLightSerial in pendantLightSerials)
                {
                    pendantLightSerial.UpdatedDateTime = now;
                    pendantLightSerial.Store();
                }

                transaction.Complete();
            }
        }

        #endregion

        #endregion

        #region その他

        [WebMethod]
        public List<LayoutText> GetLayoutTexts(string constructionCode, int layoutId)
        {
            return LayoutText.Get(constructionCode, layoutId);
        }

        [WebMethod]
        public List<LayoutText> GetLayoutTextsBySiyoCode(string constructionCode, int siyoCode)
        {
            return LayoutText.GetBySiyoCode(constructionCode, siyoCode);
        }

        [WebMethod]
        public List<LayoutText> GetLayoutTextsByPlanNo(string constructionCode, string planNo, int layoutId)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);
            return LayoutText.Get(constructionCode, siyoCode, layoutId);
        }

        [WebMethod]
        public List<Setting> GetSettings(string constructionCode, int siyoCode)
        {
            return Setting.Get(constructionCode, siyoCode);
        }

        [WebMethod]
        public List<ElectricContract> GetElectricContracts()
        {
            return ElectricContract.GetAll();
        }

        [WebMethod]
        public List<HouseTypeGroupStandardItem> GetHouseTypeGroupStandardItems(string constructionCode, string planNo)
        {
            return HouseTypeGroupStandardItem.Get(constructionCode, planNo);
        }

        [WebMethod]
        public int GetApplianceSpecificationCode(string constructionCode, string planNo)
        {
            var siyoCode = tbl_siyo_boss.GetSiyoCode(constructionCode, planNo);

            if (BasicSpecificationDetail.IsISmart(constructionCode, siyoCode))
                return 10;

            if (BasicSpecificationDetail.IsICubeOrISmileOrIPalette(constructionCode, siyoCode))
                return 10;

            var prefectureCode = this.GetPrefectureCode(constructionCode);
            if (prefectureCode == "01") //I-HEAD,一般（北海道）
                return 12;

            return 11; //I-HEAD,一般
        }

        [WebMethod]
        public string GetContractVa(decimal soVa)
        {
            return ContractVa.GetContractVa(soVa);
        }

        [WebMethod]
        public List<ProductVa> GetProductVas(string constructionCode, string planNo)
        {
            return ProductVa.Get(constructionCode, planNo);
        }

        [WebMethod]
        public void UpdateOptionPickingItemWithLogs(List<OptionPickingItem> items, SekisanMaterial sekisanMaterial, string revisionNo, OptionPickingLog log)
        {
            using (var transaction = new TransactionScope())
            {
                OptionPickingItem.Update(items, log.ConstructionCode);
                ProcessLog.Update(log.ConstructionCode, log.PlanNo, revisionNo);
                SekisanMaterial.Update(sekisanMaterial);

                log.OptionPickingDateTime = DateTime.Now;

                var db = OptionPickingLog.GetDatabase();
                db.Insert<OptionPickingLog>(log);

                transaction.Complete();
            }
        }

        [WebMethod]
        public void UpdateOptionPickingItemEx(List<OptionPickingItemsEx> exItems, string constructionCode)
        {
            using (var transaction = new TransactionScope())
            {
                OptionPickingItemsEx.Update(exItems, constructionCode);
                transaction.Complete();
            }
        }

        [WebMethod]
        public List<OptionPickingItemsEx> GetOptionPickingItemEx(string constructionCode)
        {
            return OptionPickingItemsEx.Get(constructionCode);
        }

        [WebMethod]
        public void UpdateRemoconDetail(List<RemoconDetail> remocons)
        {
            using (var transaction = new TransactionScope())
            {
                RemoconDetail.Update(remocons);

                transaction.Complete();
            }
        }

        [WebMethod]
        public List<ProductItem> GetProductItems(string constructionCode, string planNo)
        {
            return ProductItem.Get(constructionCode, planNo);
        }

        [WebMethod]
        public bool IsBatteryTypeFireAlarm(string constructionCode, string planNo)
        {
            //マスタ化した方がいいのかなぁ

            var detail = HouseTypeGroupDetail.Get(constructionCode, planNo);

            if (detail.HouseTypeGroupId == 3 || detail.HouseTypeGroupId == 4)
                return true;

            if (detail.HouseTypeGroupId == 1)
                return false;

            if (detail.HouseTypeGroupId == 2)
            {
                if (detail.ConstructionTypeCode == "100")
                    return false; //ブリアール
                else
                    return true; //アシュレ
            }

            return false; //未知の家タイプが出てきてエラーで処理が止まったら困るので適当に値を設定しておこう。
        }

        [WebMethod]
        public void LogKairoPlan(string constructionCode, string planNo, string revisionNo)
        {
            using (var transaction = new TransactionScope())
            {
                var planNoWithHyphen = HemsLog.GetPlanNoWithHyphen(planNo);
                var log = HemsLog.Get(constructionCode, planNoWithHyphen, revisionNo);
                if (log == null)
                {
                    log = new HemsLog();
                    log.ConstructionCode = constructionCode;
                    log.PlanNo = planNoWithHyphen;
                    log.RevisionNo = revisionNo;
                    log.PlanNoWithoutHyphen = planNo;
                }

                var now = DateTime.Now;
                log.KairoPlanOutputDateTime = now;
                log.UpdatedDateTime = now;
                log.Store();

                transaction.Complete();
            }
        }

        [WebMethod]
        public void LogBundenbanSpecification(string constructionCode, string planNo, string revisionNo)
        {
            using (var transaction = new TransactionScope())
            {
                var planNoWithHyphen = HemsLog.GetPlanNoWithHyphen(planNo);
                var log = HemsLog.Get(constructionCode, planNoWithHyphen, revisionNo);
                if (log == null)
                {
                    log = new HemsLog();
                    log.ConstructionCode = constructionCode;
                    log.PlanNo = planNoWithHyphen;
                    log.RevisionNo = revisionNo;
                    log.PlanNoWithoutHyphen = planNo;
                }

                var now = DateTime.Now;
                log.BundenbanSpecificationOutputDateTime = now;
                log.UpdatedDateTime = now;
                log.Store();

                transaction.Complete();
            }
        }

        private byte[] GetPowerConditionerNumberSettingImage(string constructionCode, int count)
        {
            string path = string.Empty;

            if (count == 0)
                path = Path.Combine(Settings.Default.PowerConditionerNumberImageFolderPath, "NotPc.jpg");
            else
                path = Path.Combine(Settings.Default.PowerConditionerNumberImageFolderPath, "pcs1" + count + ".jpg");

            var image = Image.FromFile(path);

            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        private bool NeedElectricControl(string constructionCode)
        {
            var shikaku = this.GetShikakuTableEntry(constructionCode, ShikakuTableEntry.HEMS_ARI);
            if (shikaku == null || !Convert.ToBoolean(shikaku.Value))
                return false;

            return this.IsZehConstructionCode(constructionCode);
        }

        [WebMethod]
        public Construction GetConstruction(string constructionCode)
        {
            return Construction.Get(constructionCode);
        }

        [WebMethod]
        public RimokonNichePattern GetRimokonNichePattern(
            bool isJikugumi, string side, int interphoneCount, int mrCount, int switchCount, int rimokonCount)
        {
            return RimokonNichePattern.Get(isJikugumi, side, interphoneCount, mrCount, switchCount, rimokonCount, false);
        }

        [WebMethod]
        public RimokonNichePattern GetRimokonNichePatternForMT81(
            bool isJikugumi, string side, int interphoneCount, int mrCount, int switchCount, int rimokonCount, bool hasMT81_83)
        {
            return RimokonNichePattern.Get(isJikugumi, side, interphoneCount, mrCount, switchCount, rimokonCount, hasMT81_83);
        }

        [WebMethod]
        public void UpdateRimokonNiches(string constructionCode, List<RimokonNiche> niches)
        {
            using (var transaction = new TransactionScope())
            {
                RimokonNicheDetail.Delete(constructionCode);
                RimokonNiche.Delete(constructionCode);

                foreach (var niche in niches)
                {
                    niche.StoreSimple();
                    niche.Details.ForEach(p => p.StoreSimple());
                }

                transaction.Complete();
            }
        }

        [WebMethod]
        public List<RimokonNicheEntry> GetRimokonNicheEntries(string constructionCode)
        {
            return RimokonNicheEntry.Get(constructionCode);
        }

        [WebMethod]
        public void UpdateRimokonNicheEntries(string constructionCode, List<RimokonNicheEntry> entries, int nicheSeq)
        {
            using (var transaction = new TransactionScope())
            {
                RimokonNicheEntry.Update(constructionCode, entries, nicheSeq);
                transaction.Complete();
            }
        }

        [WebMethod]
        public void DeleteRemainedRimokonNicheEntries(string constructionCode, int maxSeq)
        {
            RimokonNicheEntry.DeleteRemainedEntries(constructionCode, maxSeq);
        }

        [WebMethod]
        public List<SolarComposition> getSolarComposition(string powerStr)
        {
            return SolarComposition.GetComposition(powerStr);
        }

        [WebMethod]
        public List<SolarComposition> getSolarCompositionByPC(List<string> powerConTexts)
        {
            return SolarComposition.GetComposition(powerConTexts);
        }

        [WebMethod]
        public decimal getSolarPowerMax()
        {
            return SolarComposition.GetMaxPower();
        }

        [WebMethod]
        public decimal getSolarPowerMin()
        {
            return SolarComposition.GetMinPower();
        }

        [WebMethod]
        public DrawingParameter getDrawingParameter(string constructionCode, string planNo, string revisionNo, string keyString)
        {
            return DrawingParameter.GetParameter(constructionCode, planNo, revisionNo, keyString);
        }

        [WebMethod]
        public void registerDrawingParameter(string constructionCode, string planNo, string revisionNo, string keyString, string value)
        {
            DrawingParameter.Insert(constructionCode, planNo, revisionNo, keyString, value);
        }

        [WebMethod]
        public KansenAllowance getKansenAllowance(string kind, string kanabakari, bool isAbove)
        {
            return KansenAllowance.GetAllowance(kind, kanabakari, isAbove);
        }

        [WebMethod]
        public string getKansenBuzaiItemName(string buzaiCode)
        {
            return KansenBuzaiItem.GetName(buzaiCode);
        }

        [WebMethod]
        public ContractAmpere getContractAmpere(string contract)
        {
            return ContractAmpere.GetAmpere(contract);
        }

        [WebMethod]
        public ContractAmpere getExtensionContractAmpere(string ampere)
        {
            return ContractAmpere.GetExtensionAmpere(ampere);
        }

        [WebMethod]
        public string getKaiheikiHinban(decimal ampere)
        {
            return ContractAmpere.GetHinban(ampere);
        }

        [WebMethod]
        public List<SolarBreaker> getSolarBreaker(decimal main, decimal pcSize)
        {
            return SolarBreaker.GetBreaker(main, pcSize);
        }

        [WebMethod]
        public string getKaiheikiBoxSerial(int first, int second)
        {
            var ret = KaiheikiSerial.Get(first, second);

            if (ret == null)
                return "品番未設定";
            else if (string.IsNullOrEmpty(ret.BoxSerial))
                return "品番未設定";
            else
                return ret.BoxSerial;
        }

        [WebMethod]
        public List<JboxItem> getEntryJboxItems(string constructionCode)
        {
            return JboxItem.GetItems(constructionCode);
        }

        [WebMethod]
        public JboxItem getEntryJboxItem(JboxColorEntry entry)
        {
            var ret = JboxItem.GetItem(entry);
            if (ret.Count > 0)
                return ret[0];

            return null;
        }

        [WebMethod]
        public bool IsUsingNewRuleConstruction(string constructionCode)
        {
            return Construction.IsNewStandardQtyRule(constructionCode);
        }

        [WebMethod]
        public List<RoomLayoutsForKibiroi> GetRoomLayouts(string constructionCode)
        {
            return RoomLayoutsForKibiroi.Get(constructionCode);
        }
        [WebMethod]
        public List<ShipmentRequest> GetShipmentRequestForXGWSettings(int year, int week)
        {
            var margeShipmentRequests = new List<ShipmentRequest>();
            margeShipmentRequests.AddRange(ShipmentRequest.Get(year, week, "HH"));
            margeShipmentRequests.AddRange(ShipmentRequest.Get(year, week + 2, "IQ"));
            margeShipmentRequests.AddRange(ShipmentRequest.Get(year, week + 1, "SA"));

            return margeShipmentRequests;
        }

        [WebMethod]
        public OptionPickingLog GetOptionPickingLog(string constructionCode)
        {
            return OptionPickingLog.Get(constructionCode);
        }
        [WebMethod]
        public bool ExistHemsData(string constructionCode)
        {
            var hemsDevices = HemsDevice.Get(constructionCode);
            var hemsRoomBlocks = HemsRoomBlock.Get(constructionCode);
            var hemsRooms = HemsRoom.Get(constructionCode);
            var hemsBlocks = HemsBlock.Get(constructionCode);

            if (hemsDevices.Count == 0 || hemsRooms.Count == 0 ||
                hemsBlocks.Count == 0 || hemsRoomBlocks.Count == 0)
            {
                return false;
            }
            return true;
        }

        [WebMethod]
        public List<string> GetXGWSettingReportLogOfBeforeConstructionCodes(int year, int week, List<string> constuctionCodes)
        {
            List<string> constructionCodes = new List<string>();
            var log = XGWSettingReportLog.GetBeforeConstructionCodesforDistinct(year, week, constuctionCodes);
            log.ForEach(p => constructionCodes.Add(p.ConstructionCode));
            return constructionCodes;
        }
        [WebMethod]
        public void RegisterXGWSettingReportLogs(List<XGWSettingReportLog> insertLogs)
        {
            var now = DateTime.Now;
            using (var transaction = new TransactionScope())
            {
                foreach (var item in insertLogs)
                {
                    item.InsertTime = now;
                    item.Store();
                }
                transaction.Complete();
            }
        }
        #endregion

        public void RegisterWirePickingReportLog(WirePickingReportLog log)
        {
            log.WirePickingReportDateTime = DateTime.Now;
            log.StoreSimple();
        }

        [WebMethod]
        public List<WireSerial> GetWireSerials(bool isJiku)
        {
            var wireSerials = WireSerial.GetAll();

            return wireSerials.FindAll(p => p.IsJikugumi == isJiku);
        }

        [WebMethod]
        public bool IsUnitWiring(string constructionCode)
        {
            return HouseConstructionMaterial.IsUnitWiring(constructionCode);
        }

        [WebMethod]
        public void UpdateElectricPower(int kairo1, string constructionCode)
        {
            //回路数×最大アンペア×需要率
            decimal va1 = kairo1 * 1600m * 0.3m / 1000m;

            ShikakuTableEntry.UpdateElectricPower(constructionCode, va1);
        }

        // 以下、SocketPlan

        [WebMethod]
        public List<SocketBoxPattern> GetAllSocketBoxPatterns()
        {
            var patterns = SocketBoxPattern.GetAll().FindAll(p => !p.DeletedDate.HasValue);
            var details = SocketBoxPatternDetail.GetAll();
            var detailComments = SocketBoxDetailComment.GetAll();
            var colors = SocketBoxPatternColor.GetAll();
            var categories = SocketBoxCategory.GetAll();
            var comments = Comment.GetAll();
            var equipments = Equipment.GetAll();
            var texts = Text.GetAll();

            foreach (var pattern in patterns)
            {
                pattern.Details = details.FindAll(p => p.PatternId == pattern.Id);
                pattern.Colors = colors.FindAll(p => p.PatternId == pattern.Id);
                foreach (var detail in pattern.Details)
                {
                    detail.Equipment = equipments.Find(p => p.Id == detail.EquipmentId);
                    if (detail.Equipment != null)
                        detail.Equipment.Texts = texts.FindAll(p => p.EquipmentId == detail.Equipment.Id);

                    detail.Comments = detailComments.FindAll(p =>
                        p.PatternId == detail.PatternId &&
                        p.DetailSeq == detail.Seq);

                    foreach (var comment in detail.Comments)
                    {
                        comment.Comment = comments.Find(p => p.Id == comment.CommentId);
                    }
                }
            }

            return patterns;
        }

        [WebMethod]
        public void RegisterSocketBoxes(string constructionCode, string planNo, string zumenNo, List<SocketBox> boxes)
        {
            using (var transaction = new TransactionScope())
            {
                SocketBox.Delete(constructionCode);

                foreach (var box in boxes)
                {
                    box.Store();
                }

                this.LogSocketPlanAutoGenerated(constructionCode, planNo, zumenNo);

                transaction.Complete();
            }
        }

        [WebMethod]
        public void DeleteSocketBoxes(string constructionCode, List<string> seqs)
        {
             SocketBox.DeleteByDrawing(constructionCode, seqs);
        }

        [WebMethod]
        public void RegisterSocketBoxBySpecific(string constructionCode, List<SocketBox> boxes)
        {
            using (var transaction = new TransactionScope())
            {
                var seq = SocketBox.GetMaxSeq(constructionCode) + 1;
                foreach (var box in boxes)
                {
                    box.Seq = seq;
                    box.SetSeq = boxes.IndexOf(box) + 1;
                    box.Store();
                }
                transaction.Complete();
            }
        }

        [WebMethod]
        public int GetNextSocketBoxSeq(string constructionCode)
        {
            return SocketBox.GetMaxSeq(constructionCode) + 1;
        }

        [WebMethod]
        public List<SocketBoxCategory> GetAllSocketBoxCategories()
        {
            var categories = SocketBoxCategory.GetAll().FindAll(p => !p.DeletedDate.HasValue);
            categories = categories.OrderBy(p => p.SortOrder).ToList();

            var patterns = SocketBoxPattern.GetAll().FindAll(p => !p.DeletedDate.HasValue);

            foreach (var category in categories)
            {
                category.Patterns = patterns.FindAll(p => p.CategoryId == category.Id);
            }

            return categories;
        }

        [WebMethod]
        public List<SocketBoxCategory> GetAllSocketBoxCategoriesSimple()
        {
            return SocketBoxCategory.GetAll().FindAll(p => !p.DeletedDate.HasValue);
        }

        [WebMethod]
        public List<SocketBoxPatternDetail> GetSocketBoxDetails(int patternId)
        {
            var details = SocketBoxPatternDetail.Get(patternId);
            var detailComments = SocketBoxDetailComment.Get(patternId);
            var comments = SocketBoxDetailComment.GetComments(detailComments);
            var equipments = Equipment.GetAll();

            foreach (var detail in details)
            {
                detail.Equipment = equipments.Find(p => p.Id == detail.EquipmentId);
                detail.Comments = detailComments.FindAll(p => p.DetailSeq == detail.Seq);
                foreach (var comment in detail.Comments)
                {
                    comment.Comment = comments.Find(p => p.Id == comment.CommentId);
                }
            }

            return details;
        }

        [WebMethod]
        public List<SocketBoxPatternColor> GetSocketBoxColors(int patternId)
        {
            return SocketBoxPatternColor.Get(patternId);
        }

        [WebMethod]
        public int RegisterSocketBoxCategory(SocketBoxCategory category)
        {
            using (var transaction = new TransactionScope())
            {
                if (category.Id == 0)
                    category.Id = SocketBoxCategory.GetNewId();

                category.Store();

                transaction.Complete();
            }

            return category.Id;
        }

        [WebMethod]
        public int RegisterSocketBoxPattern(SocketBoxPattern pattern, string cpuName)
        {
            using (var transaction = new TransactionScope())
            {
                if (pattern.Id == 0)
                {
                    pattern.Id = SocketBoxPattern.GetNewId();
                }
                else
                {
                    SocketBoxDetailComment.Delete(pattern.Id);
                    SocketBoxPatternDetail.Delete(pattern.Id);
                    SocketBoxPatternColor.Delete(pattern.Id);
                }

                pattern.Store();

                var detailSeq = 1;
                foreach (var detail in pattern.Details)
                {
                    detail.PatternId = pattern.Id;
                    detail.Seq = detailSeq;
                    detail.Store();

                    var commentSeq = 1;
                    foreach (var comment in detail.Comments)
                    {
                        comment.PatternId = pattern.Id;
                        comment.DetailSeq = detailSeq;
                        comment.CommentSeq = commentSeq;
                        comment.Store();

                        commentSeq++;
                    }

                    detailSeq++;
                }

                foreach (var color in pattern.Colors)
                {
                    color.PatternId = pattern.Id;
                    color.Store();
                }

                #region ログ出力
                SocketPlanMaintenanceLog log = new SocketPlanMaintenanceLog();
                log.Id = SocketPlanMaintenanceLog.GetNewId();
                log.PatternId = pattern.Id;
                log.CpuName = cpuName;
                log.UpdatedDateTime = DateTime.Now;
                log.Store();

                detailSeq = 1;
                foreach (var detail in pattern.Details)
                {
                    SocketPlanMaintenance_EquipmentLog equipmentLog = new SocketPlanMaintenance_EquipmentLog();
                    equipmentLog.LogId = log.Id;
                    equipmentLog.Seq = detailSeq;
                    equipmentLog.EquipmentId = detail.Equipment.Id;
                    equipmentLog.EquipmentName = detail.Equipment.Name;
                    equipmentLog.Store();

                    var commentSeq = 1;
                    foreach (var comment in detail.Comments)
                    {
                        SocketPlanMaintenance_CommentLog commentLog = new SocketPlanMaintenance_CommentLog();
                        commentLog.LogId = log.Id;
                        commentLog.EquipmentSeq = equipmentLog.Seq;
                        commentLog.Seq = commentSeq;
                        commentLog.Comment = comment.Comment.Text;
                        commentLog.Store();

                        commentSeq++;
                    }

                    detailSeq++;
                }
                #endregion

                transaction.Complete();
            }

            return pattern.Id;
        }

        [WebMethod]
        public void DeleteSocketBoxCategory(int categoryId)
        {
            using (var transaction = new TransactionScope())
            {
                var category = SocketBoxCategory.Get(categoryId);
                if (category == null)
                    throw new ApplicationException("This category is already deleted.");

                var patterns = SocketBoxPattern.GetAll().FindAll(p => p.CategoryId == categoryId);
                foreach (var pattern in patterns)
                {
                    pattern.DeletedDate = DateTime.Now;
                    pattern.Store();
                }

                category.DeletedDate = DateTime.Now;
                category.Store();

                transaction.Complete();
            }
        }

        [WebMethod]
        public void DeleteSocetBoxPattern(int patternId)
        {
            using (var transaction = new TransactionScope())
            {
                var pattern = SocketBoxPattern.Get(patternId);
                if (pattern == null)
                    throw new ApplicationException("This socket box pattern is already deleted.");

                pattern.DeletedDate = DateTime.Now;
                pattern.Store();

                transaction.Complete();
            }
        }

        [WebMethod]
        public List<NotNameHotaruSwitch> GetNotNameHotaruSwitches()
        {
            var switches = NotNameHotaruSwitch.GetAll();
            var equipments = Equipment.GetAll();

            foreach (var sw in switches)
            {
                sw.Equipment = equipments.Find(p => p.Id == sw.EquipmentId);
            }

            return switches;
        }

        [WebMethod]
        public void RegisterNotNameHotaruSwitches(List<NotNameHotaruSwitch> switches)
        {
            using (var transaction = new TransactionScope())
            {
                NotNameHotaruSwitch.DeleteAll();
                switches.ForEach(p => p.Store());

                transaction.Complete();
            }
        }

        [WebMethod]
        public List<NotNameHotaruSwitchSerial> GetNotNameHotaruSwitchSerials()
        {
            return NotNameHotaruSwitchSerial.GetAll();
        }

        [WebMethod]
        public void RegisterNotNameHotaruSwitchSerials(List<NotNameHotaruSwitchSerial> serials)
        {
            using (var transaction = new TransactionScope())
            {
                NotNameHotaruSwitchSerial.DeleteAll();
                serials.ForEach(p => p.Store());

                transaction.Complete();
            }
        }

        [WebMethod]
        public void ChangeSocketBoxColor(
            string constructionCode, decimal symbolX, decimal symbolY, int patternId, string colorName)
        {
            var box = SocketBox.Get(constructionCode, symbolX, symbolY, patternId);
            if (box == null)
                throw new ApplicationException("Failed to get socket box data.");

            box.Color = colorName;

            using (var transaction = new TransactionScope())
            {
                box.Store();
                transaction.Complete();
            }
        }

        [WebMethod]
        public List<SocketBox> GetSocketBoxes(string constructionCode)
        {
            return SocketBox.Get(constructionCode);
        }

        [WebMethod]
        public SocketBox GetLocation(string constructionCode, int specificId)
        {
            return SocketBox.Get(constructionCode, specificId);
        }

        [WebMethod]
        public SocketBoxSpecific GetBlockPath(int patternId)
        {
            return SocketBoxSpecific.GetSpecifics( patternId);
        }

        [WebMethod]
        public static SocketBoxSpecific GetPatternName(string blockPath, int patternId)
        {
            return SocketBoxSpecific.GetPatternName(patternId);
        }

        [WebMethod]
        public List<SocketBoxPickingItem> RegisterSocketBoxPickingItems(string constructionCode, string planNo, string zumenNo)
        {
            var items = new List<SocketBoxPickingItem>();

            using (var transaction = new TransactionScope())
            {
                SocketBoxPickingItem.Delete(constructionCode);

                var boxes = SocketBox.Get(constructionCode);
                if (boxes.Count == 0)
                {
                    this.LogSocketPlanExported(constructionCode, planNo, zumenNo);
                    transaction.Complete();
                    throw new ApplicationException("Socket plan is not generated yet.");
                }

               // boxes = boxes.OrderBy(p => p.ConstructionCode).ThenBy(p => p.PlanNo).ThenBy(p => p.ZumenNo).ThenBy(p => p.Floor).ToList(); //なくす

                var patternBoxes = boxes.FindAll(p => p.PatternId >= 0);
                var specificBoxes = boxes.FindAll(p => p.PatternId < 0);

                var patterns = SocketBoxPattern.GetAll();
                var seq = 1;
                foreach (var box in patternBoxes)
                {
                    var newItem = new SocketBoxPickingItem();
                    var now = DateTime.Now;

                    newItem.Quantity = 1;
                    newItem.UpdatedDateTime = now;
                    newItem.ConstructionCode = constructionCode;
                    newItem.PlanNo = planNo;
                    newItem.ZumenNo = zumenNo;
                    newItem.Floor = box.Floor;
                    newItem.Seq = seq;
                    newItem.SetSeq = box.SetSeq;
                    newItem.RoomCode = box.RoomCode;
                    newItem.RoomName = box.RoomName;
                    newItem.Color = box.Color;
                    newItem.Shape = box.Shape;

                    var pattern = patterns.Find(p => p.Id == box.PatternId);
                    if (pattern == null)
                        throw new ApplicationException("Invalid socket box pattern ID registered.");

                    newItem.SocketBoxName = pattern.Name;
                    newItem.IsSpecificItem = false;
                    newItem.SocketBoxSize = pattern.SocketBoxSize;

                    var item = items.Find(p =>
                        p.Floor == newItem.Floor &&
                        p.RoomCode == newItem.RoomCode &&
                        p.RoomName == newItem.RoomName &&
                        p.Color == newItem.Color &&
                        p.Shape == newItem.Shape &&
                        p.SocketBoxName == newItem.SocketBoxName &&
                        p.IsSpecificItem == newItem.IsSpecificItem);

                    if (item == null)
                    {
                        items.Add(newItem);
                        seq++;
                    }
                    else
                    {
                        item.Quantity++;
                    }
                }

                //バラ品は数量サマリをしない
                var specifics = SocketBoxSpecific.GetAll();
                specificBoxes.OrderBy(p => p.Seq);
                seq++;
                foreach (var box in specificBoxes)
                {
                    var newItem = new SocketBoxPickingItem();
                    var now = DateTime.Now;

                    var idx = specificBoxes.IndexOf(box);
                    newItem.Quantity = 1;
                    newItem.UpdatedDateTime = now;
                    newItem.ConstructionCode = constructionCode;
                    newItem.PlanNo = planNo;
                    newItem.ZumenNo = zumenNo;
                    newItem.Floor = box.Floor;
                    newItem.Seq = seq;
                    newItem.SetSeq = box.SetSeq;
                    newItem.RoomCode = box.RoomCode;
                    newItem.RoomName = box.RoomName;
                    newItem.Color = box.Color;
                    newItem.Shape = box.Shape;

                    var specific = specifics.Find(p => p.Id == box.SpecificId);
                    if (specific == null)
                        throw new ApplicationException("Invalid socket box specific ID registered.");

                    newItem.SocketBoxName = specific.Serial;
                    newItem.IsSpecificItem = true;
                    newItem.SocketBoxSize = specific.SocketBoxSize == null ? 0 : Int32.Parse(specific.SocketBoxSize.ToString()); //nullを許容しない

                    items.Add(newItem);

                    if (idx < specificBoxes.Count - 1 && box.Seq != specificBoxes[idx + 1].Seq)
                        seq++;
                }

                foreach(var item in items)
                {
                    item.SizeQuantity = item.SocketBoxSize == 0 ? 0 : item.Quantity;
                }

                items.ForEach(p => p.Store());

                this.LogSocketPlanExported(constructionCode, planNo, zumenNo);
                transaction.Complete();
            }

            return items;
        }

        [WebMethod]
        public List<SocketBoxSpecificCategory> GetAllSocketBoxSpecificCategories()
        {
            var categories = SocketBoxSpecificCategory.GetAll().FindAll(p => !p.DeletedDate.HasValue);
            var specifics = SocketBoxSpecific.GetAll().FindAll(p => !p.DeletedDate.HasValue);
            var relations = SocketBoxSpecificRelation.GetAll();

            foreach (var category in categories)
            {
                category.Specifics.AddRange(specifics.FindAll(p => p.SpecificCategoryId == category.Id));
                foreach (var specific in category.Specifics)
                {
                    var currentRelations = relations.FindAll(p => p.SocketBoxSpecificId == specific.Id);
                    specific.Relations.AddRange(currentRelations);
                }
            }

            return categories;
        }

        [WebMethod]
        public void DeleteSocketBoxSpecificCategory(int specificCategoryId)
        {
            using (var transaction = new TransactionScope())
            {
                var category = SocketBoxSpecificCategory.Get(specificCategoryId);
                if (category == null)
                    throw new ApplicationException("This category is already deleted.");

                var specifics = SocketBoxSpecific.GetAll().FindAll(p => p.SpecificCategoryId == category.Id);
                foreach (var specific in specifics)
                {
                    SocketBoxSpecificRelation.DeleteIncludeRelated(specific.Id);
                    specific.DeletedDate = DateTime.Now;
                    specific.Store();
                }

                category.DeletedDate = DateTime.Now;
                category.Store();

                transaction.Complete();
            }
        }

        [WebMethod]
        public void DeleteSocketBoxesAutoMatically(string constructionCode)
        {
            SocketBox.DeleteOld(constructionCode);
        }

        [WebMethod]
        public void DeleteSocetBoxSpecific(int specificId)
        {
            using (var transaction = new TransactionScope())
            {
                var specific = SocketBoxSpecific.Get(specificId);
                if (specific == null)
                    throw new ApplicationException("This socket box specific is already deleted.");

                SocketBoxSpecificRelation.DeleteIncludeRelated(specific.Id);
                specific.DeletedDate = DateTime.Now;
                specific.Store();

                transaction.Complete();
            }
        }

        [WebMethod]
        public int RegisterSocketBoxSpecific(SocketBoxSpecific specific)
        {
            int id;

            using (var transaction = new TransactionScope())
            {
                if (specific.Id == 0)
                    specific.Id = SocketBoxSpecific.GetNewId();

                specific.Store();
                id = specific.Id;

                SocketBoxSpecificRelation.Delete(specific.Id);
                foreach (var relation in specific.Relations)
                {
                    relation.SocketBoxSpecificId = specific.Id;
                    relation.Store();
                }

                transaction.Complete();
            }

            return id;
        }

        [WebMethod]
        public int RegisterSocketBoxSpecificCategory(SocketBoxSpecificCategory category)
        {
            using (var transaction = new TransactionScope())
            {
                if (category.Id == 0)
                    category.Id = SocketBoxSpecificCategory.GetNewId();

                category.Store();

                transaction.Complete();
            }

            return category.Id;
        }

        [WebMethod]
        public List<SingleSocketBoxEquipment> GetAllSingleSocketBoxEquipments()
        {
            var singles = SingleSocketBoxEquipment.GetAll();
            var equipments = Equipment.GetAll();
            foreach (var single in singles)
            {
                single.Equipment = equipments.Find(p => p.Id == single.EquipmentId);
            }

            return singles;
        }

        [WebMethod]
        public void RegisterSingleSocketBoxEquipments(List<SingleSocketBoxEquipment> equipments)
        {
            using (var transaction = new TransactionScope())
            {
                SingleSocketBoxEquipment.DeleteAll();
                equipments.ForEach(p => p.Store());

                transaction.Complete();
            }
        }

        [WebMethod]
        public List<SubeteExempleEquipment> GetAllSubeteExempleEquipments()
        {
            var exemples = SubeteExempleEquipment.GetAll();
            var equipments = Equipment.GetAll();
            foreach (var exemple in exemples)
            {
                exemple.Equipment = equipments.Find(p => p.Id == exemple.EquipmentId);
            }

            return exemples;
        }

        [WebMethod]
        public void RegisterSubeteExempleEquipments(List<SubeteExempleEquipment> equipments)
        {
            using (var transaction = new TransactionScope())
            {
                SubeteExempleEquipment.DeleteAll();
                equipments.ForEach(p => p.Store());

                transaction.Complete();
            }
        }

        private void LogSocketPlanAutoGenerated(string constructionCode, string planNo, string zumenNo)
        {
            var log = SocketPlanLog.Get(constructionCode, planNo, zumenNo);
            if (log == null)
            {
                log = new SocketPlanLog();
                log.ConstructionCode = constructionCode;
                log.PlanNo = planNo;
                log.ZumenNo = zumenNo;
            }

            log.AutoGeneratedDateTime = DateTime.Now;
            log.Store();
        }

        [WebMethod]
        public void LogSocketPlanFramed(string constructionCode, string planNo, string zumenNo)
        {
            var log = SocketPlanLog.Get(constructionCode, planNo, zumenNo);
            if (log == null)
            {
                log = new SocketPlanLog();
                log.ConstructionCode = constructionCode;
                log.PlanNo = planNo;
                log.ZumenNo = zumenNo;
            }

            log.FramedDateTime = DateTime.Now;
            log.Store();
        }

        private void LogSocketPlanExported(string constructionCode, string planNo, string zumenNo)
        {
            var log = SocketPlanLog.Get(constructionCode, planNo, zumenNo);
            if (log == null)
            {
                log = new SocketPlanLog();
                log.ConstructionCode = constructionCode;
                log.PlanNo = planNo;
                log.ZumenNo = zumenNo;
            }

            log.ExportedDateTime = DateTime.Now;
            log.DenkiOrderImportStatus = false;
            log.Store();
        }
    }
}
