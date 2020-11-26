using System;
using System.Collections.Generic;
using System.Text;
using SocketPlan.WinUI.SocketPlanServiceReference;

namespace SocketPlan.WinUI
{
    public enum SocketPlanType { Individual, Pattern }

    /// <summary>
    /// システムのいろんなところから呼ばれる変数、メソッドを持つクラス
    /// staticにしたい変数は全部ここに入れちゃえ！
    /// </summary>
    public class UnitWiring
    {
        #region マスタを保持するクラス

        public class Masters
        {
            //マスタの取得タイミングをばらけさせる為にシングルトンにして初利用時に取得するようにしている。
            //以前、起動時にマスタを一括で取得していたら、朝にアクセスが集中してサーバーのCPUが100%になってたんですよ。。。

            private static List<Block> blocks;
            public static List<Block> Blocks
            {
                get
                {
                    if (blocks == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            blocks = new List<Block>(service.GetBlocks());
                        }
                    }

                    return blocks;
                }
            }

            private static List<Comment> comments;
            public static List<Comment> Comments
            {
                get
                {
                    if (comments == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            comments = new List<Comment>(service.GetComments());
                        }

                        comments.Sort((a, b) => a.SortNo.CompareTo(b.SortNo));

                        foreach (var comment in comments)
                        {
                            var list = new List<Specification>();
                            foreach (var detail in UnitWiring.Masters.CommentSpecifications.FindAll(p => p.CommentId == comment.Id))
                            {
                                list.AddRange(UnitWiring.Masters.Specifications.FindAll(p => p.Id == detail.SpecificationId));
                            }
                            comment.Specifications = list.ToArray();
                        }
                    }

                    return comments;
                }
            }

            private static List<CommentCategory> commentCategories;
            public static List<CommentCategory> CommentCategories
            {
                get
                {
                    if (commentCategories == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            commentCategories = new List<CommentCategory>(service.GetCommentCategories());
                        }

                        commentCategories.Sort((a, b) => a.SortNo.CompareTo(b.SortNo));

                        foreach (var category in commentCategories)
                        {
                            category.Comments = UnitWiring.Masters.Comments.FindAll(p => p.CategoryId == category.Id).ToArray();
                        }
                    }

                    return commentCategories;
                }
            }

            private static List<CommentSpecification> commentSpecifications;
            private static List<CommentSpecification> CommentSpecifications
            {
                get
                {
                    if (commentSpecifications == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            commentSpecifications = new List<CommentSpecification>(service.GetCommentSpecifications());
                        }
                    }

                    return commentSpecifications;
                }
            }

            private static List<Equipment> equipments;
            public static List<Equipment> Equipments
            {
                get
                {
                    if (equipments == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            equipments = new List<Equipment>(service.GetEquipments());
                        }

                        foreach (var equipment in equipments)
                        {
                            equipment.Block = UnitWiring.Masters.Blocks.Find(p => p.Id == equipment.BlockId);
                            equipment.Texts = UnitWiring.Masters.Texts.FindAll(p => p.EquipmentId == equipment.Id).ToArray();
                            equipment.Kind = UnitWiring.Masters.EquipmentKinds.Find(p => p.Id == equipment.EquipmentKindId);
                            equipment.RelatedEquipments = UnitWiring.Masters.RelatedEquipments.FindAll(p => p.EquipmentId == equipment.Id).ToArray();

                            var list = new List<Specification>();
                            foreach (var detail in UnitWiring.Masters.EquipmentSpecifications.FindAll(p => p.EquipmentId == equipment.Id))
                            {
                                list.AddRange(UnitWiring.Masters.Specifications.FindAll(p => p.Id == detail.SpecificationId));
                            }
                            equipment.Specifications = list.ToArray();

                            equipment.CustomActions = UnitWiring.Masters.EquipmentCustomActions.FindAll(p => p.EquipmentId == equipment.Id).ToArray();
                        }
                    }

                    return equipments;
                }
            }

            private static List<EquipmentCustomAction> equipmentCustomActions;
            private static List<EquipmentCustomAction> EquipmentCustomActions
            {
                get
                {
                    if (equipmentCustomActions == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            equipmentCustomActions = new List<EquipmentCustomAction>(service.GetEquipmentCustomActions());
                        }
                    }

                    return equipmentCustomActions;
                }
            }

            private static List<EquipmentKind> equipmentKinds;
            public static List<EquipmentKind> EquipmentKinds
            {
                get
                {
                    if (equipmentKinds == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            equipmentKinds = new List<EquipmentKind>(service.GetEquipmentKinds());
                        }
                    }

                    return equipmentKinds;
                }
            }

            private static List<EquipmentSpecification> equipmentSpecifications;
            private static List<EquipmentSpecification> EquipmentSpecifications
            {
                get
                {
                    if (equipmentSpecifications == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            equipmentSpecifications = new List<EquipmentSpecification>(service.GetEquipmentSpecifications());

                        }
                    }

                    return equipmentSpecifications;
                }
            }

            private static List<InteriorEstimateRoom> interiorEstimateRooms;
            public static List<InteriorEstimateRoom> InteriorEstimateRooms
            {
                get
                {
                    if (interiorEstimateRooms == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            interiorEstimateRooms = new List<InteriorEstimateRoom>(service.GetInteriorEstimateRooms());
                        }
                    }

                    return interiorEstimateRooms;
                }
            }

            private static List<Layout> layouts;
            public static List<Layout> Layouts
            {
                get
                {
                    if (layouts == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            layouts = new List<Layout>(service.GetLayouts());
                        }
                    }

                    return layouts;
                }
            }

            private static List<LightSerial> lightSerialsWithSetteigai;
            public static List<LightSerial> LightSerialsWithSetteigai
            {
                get
                {
                    if (lightSerialsWithSetteigai == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            lightSerialsWithSetteigai = new List<LightSerial>(service.GetLightSerialsWithSetteigai());
                        }
                    }

                    return lightSerialsWithSetteigai;
                }
            }

            private static List<LightSerial> lightSerials;
            private static List<LightSerial> LightSerials
            {
                get
                {
                    if (lightSerials == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            lightSerials = new List<LightSerial>(service.GetLightSerials());
                        }
                    }

                    return lightSerials;
                }
            }

            private static List<LightSerialCategory> lightSerialCategories;
            public static List<LightSerialCategory> LightSerialCategories
            {
                get
                {
                    if (lightSerialCategories == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            lightSerialCategories = new List<LightSerialCategory>(service.GetLightSerialCategories());
                        }

                        foreach (var category in lightSerialCategories)
                        {
                            category.LightSerials = UnitWiring.Masters.LightSerials.FindAll(p => p.CategoryId == category.Id).ToArray();
                        }
                    }

                    return lightSerialCategories;
                }
            }

            private static List<RelatedEquipment> relatedEquipments;
            private static List<RelatedEquipment> RelatedEquipments
            {
                get
                {
                    if (relatedEquipments == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            relatedEquipments = new List<RelatedEquipment>(service.GetRelatedEquipments());
                        }
                    }

                    return relatedEquipments;
                }
            }

            private static List<Room> rooms;
            public static List<Room> Rooms
            {
                get
                {
                    if (rooms == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            rooms = new List<Room>(service.GetRooms());
                        }

                        foreach (var room in rooms)
                        {
                            room.StandardItems = UnitWiring.Masters.RoomStandardItems.FindAll(p => p.RoomId == room.Id).ToArray();
                        }
                    }

                    return rooms;
                }
            }

            private static List<RoomStandardItem> roomStandardItems;
            private static List<RoomStandardItem> RoomStandardItems
            {
                get
                {
                    if (roomStandardItems == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            string planNo = string.Empty;
                            if (Static.Drawing != null)
                                planNo = Static.Drawing.PlanNoWithHyphen;

                            roomStandardItems = new List<RoomStandardItem>(service.GetRoomStandardItems(Static.ConstructionCode, planNo));
                        }
                    }

                    return roomStandardItems;
                }
            }

            private static List<Room> newRooms;
            public static List<Room> NewRooms
            {
                get
                {
                    if (newRooms == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            newRooms = new List<Room>(service.GetRooms());
                        }

                        foreach (var room in newRooms)
                        {
                            room.StandardItems = UnitWiring.Masters.RoomStandardNewItems.FindAll(p => p.RoomId == room.Id).ToArray();
                        }
                    }

                    return newRooms;
                }
            }

            private static List<RoomStandardItem> roomStandardNewItems;
            private static List<RoomStandardItem> RoomStandardNewItems
            {
                get
                {
                    if (roomStandardNewItems == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            roomStandardNewItems = new List<RoomStandardItem>(service.GetRoomStandardNewItems());
                        }
                    }

                    return roomStandardNewItems;
                }
            }
            private static List<Room> oldRooms;
            public static List<Room> OldRooms
            {
                get
                {
                    if (oldRooms == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            oldRooms = new List<Room>(service.GetRooms());
                        }

                        foreach (var room in oldRooms)
                        {
                            room.StandardItems = UnitWiring.Masters.RoomStandardOldItems.FindAll(p => p.RoomId == room.Id).ToArray();
                        }
                    }

                    return oldRooms;
                }
            }

            private static List<RoomStandardItem> roomStandardOldItems;
            private static List<RoomStandardItem> RoomStandardOldItems
            {
                get
                {
                    if (roomStandardOldItems == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            roomStandardOldItems = new List<RoomStandardItem>(service.GetRoomStandardOldItems());
                        }
                    }

                    return roomStandardOldItems;
                }
            }

            private static List<SelectionCategory> selectionCategories;
            public static List<SelectionCategory> SelectionCategories
            {
                get
                {
                    if (selectionCategories == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            selectionCategories = new List<SelectionCategory>(service.GetSelectionCategories());
                        }

                        selectionCategories.Sort((a, b) => a.SortNo.CompareTo(b.SortNo));

                        foreach (var category in selectionCategories)
                        {
                            var list = new List<Equipment>();
                            foreach (var detail in UnitWiring.Masters.SelectionCategoryDetails.FindAll(p => p.SelectionCategoryId == category.Id))
                            {
                                list.AddRange(UnitWiring.Masters.Equipments.FindAll(p => p.Id == detail.EquipmentId));
                            }
                            category.Equipments = list.ToArray();
                        }
                    }

                    return selectionCategories;
                }
            }

            private static List<SelectionCategoryDetail> selectionCategoryDetails;
            public static List<SelectionCategoryDetail> SelectionCategoryDetails
            {
                get
                {
                    if (selectionCategoryDetails == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            selectionCategoryDetails = new List<SelectionCategoryDetail>(service.GetSelectionCategoryDetails());
                        }

                        selectionCategoryDetails.Sort((a, b) =>
                        {
                            if (a.SelectionCategoryId != b.SelectionCategoryId)
                                return a.SelectionCategoryId.CompareTo(b.SelectionCategoryId);

                            return a.SortNo.CompareTo(b.SortNo);
                        });
                    }

                    return selectionCategoryDetails;
                }
            }

            private static List<Specification> specifications;
            private static List<Specification> Specifications
            {
                get
                {
                    if (specifications == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            specifications = new List<Specification>(service.GetSpecifications());
                        }
                    }

                    return specifications;
                }
            }

            private static List<Text> texts;
            private static List<Text> Texts
            {
                get
                {
                    if (texts == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            texts = new List<Text>(service.GetTexts());
                        }
                    }

                    return texts;
                }
            }

            private static List<TextItem> textItems;
            public static List<TextItem> TextItems
            {
                get
                {
                    if (textItems == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            textItems = new List<TextItem>(service.GetTextItems());
                        }
                    }

                    return textItems;
                }
            }

            private static List<WireItem> wireItems;
            public static List<WireItem> WireItems
            {
                get
                {
                    if (wireItems == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            wireItems = new List<WireItem>(service.GetWireItems());
                        }
                    }

                    return wireItems;
                }
            }

            private static List<FloorText> floorTexts;
            public static List<FloorText> FloorTexts
            {
                get
                {
                    if (floorTexts == null)
                    {
                        using (var service = new SocketPlanServiceNoTimeout())
                        {
                            floorTexts = new List<FloorText>(service.GetFloorTexts());
                        }
                    }

                    return floorTexts;
                }
            }

            //カップボードのマスター取得
            private static List<CupboardSerial> cupboardSerials;
            public static List<CupboardSerial> CupboardSerials
            {
                get
                {
                    if (cupboardSerials == null)
                    {
                        using (var service = new SocketPlanService())
                        {
                            cupboardSerials = new List<CupboardSerial>(service.getCupboardSerial());

                        }
                    }

                    return cupboardSerials;
                }
            }

            private static List<LightElectrical> lightElectricals;
            public static List<LightElectrical> LightElectricals
            {
                get
                {
                    if (lightElectricals == null)
                    {
                        using (var service = new SocketPlanService())
                        {
                            lightElectricals = new List<LightElectrical>(service.GetLightElectricals());
                        }
                    }

                    return lightElectricals;
                }
            }

            private static List<LightElectricalWireKind> lightElectricalWireKinds;
            public static List<LightElectricalWireKind> LightElectricalWireKinds
            {
                get
                {
                    if (lightElectricalWireKinds == null)
                    {
                        using (var service = new SocketPlanService())
                        {
                            lightElectricalWireKinds = new List<LightElectricalWireKind>(service.GetLightElectricalWireKinds());
                        }
                    }

                    return lightElectricalWireKinds;
                }
            }

            // PS5
            private static List<SocketBoxPattern> socketBoxPatterns;
            public static List<SocketBoxPattern> SocketBoxPatterns
            {
                get
                {
                    if (socketBoxPatterns == null)
                    {
                        using (var service = new SocketPlanService())
                        {
                            socketBoxPatterns = new List<SocketBoxPattern>(service.GetAllSocketBoxPatterns());
                            socketBoxPatterns.RemoveAll(p => p.CategoryId < 0);
                        }
                    }

                    return socketBoxPatterns;
                }
            }

            private static List<SocketBoxSpecificCategory> socketBoxSpecificCategories;
            public static List<SocketBoxSpecificCategory> SocketBoxSpecificCategories
            {
                get
                {
                    if (socketBoxSpecificCategories == null)
                    {
                        using (var service = new SocketPlanService())
                        {
                            socketBoxSpecificCategories = new List<SocketBoxSpecificCategory>(service.GetAllSocketBoxSpecificCategories());
                        }
                    }

                    return socketBoxSpecificCategories;
                }
            }

            private static List<SingleSocketBoxEquipment> singleSocketBoxEquipments;
            public static List<SingleSocketBoxEquipment> SingleSocketBoxEquipments
            {
                get
                {
                    if (singleSocketBoxEquipments == null)
                    {
                        using(var service = new SocketPlanService())
                        {
                            singleSocketBoxEquipments = new List<SingleSocketBoxEquipment>(service.GetAllSingleSocketBoxEquipments());
                        }
                    }

                    return singleSocketBoxEquipments;
                }
            }

            private static List<SubeteExempleEquipment> subeteExempleEquipments;
            public static List<SubeteExempleEquipment> SubeteExempleEquipments
            {
                get
                {
                    if (subeteExempleEquipments == null)
                    {
                        using (var service = new SocketPlanService())
                        {
                            subeteExempleEquipments = new List<SubeteExempleEquipment>(service.GetAllSubeteExempleEquipments());
                        }
                    }

                    return subeteExempleEquipments;
                }
            }

            private static List<NotNameHotaruSwitch> notNameHotaruSwitches;
            public static List<NotNameHotaruSwitch> NotNameHotaruSwitches
            {
                get
                {
                    if (notNameHotaruSwitches == null)
                    {
                        using(var service = new SocketPlanService())
                        {
                            notNameHotaruSwitches = new List<NotNameHotaruSwitch>(service.GetNotNameHotaruSwitches());
                        }
                    }

                    return notNameHotaruSwitches;
                }
            }

            private static List<NotNameHotaruSwitchSerial> notNameHotaruSwitchSerials;
            public static List<NotNameHotaruSwitchSerial> NotNameHotaruSwitchSerials
            {
                get
                {
                    if (notNameHotaruSwitchSerials == null)
                    {
                        using (var service = new SocketPlanService())
                        {
                            notNameHotaruSwitchSerials = new List<NotNameHotaruSwitchSerial>(service.GetNotNameHotaruSwitchSerials());
                        }
                    }

                    return notNameHotaruSwitchSerials;
                }
            }

            public static void UpdateSocketBoxPatterns()
            {
                using (var service = new SocketPlanService())
                {
                    socketBoxPatterns = new List<SocketBoxPattern>(service.GetAllSocketBoxPatterns());
                }
            }

            public static void UpdateSocketBoxSpecificCategories()
            {
                using (var service = new SocketPlanService())
                {
                    socketBoxSpecificCategories = new List<SocketBoxSpecificCategory>(service.GetAllSocketBoxSpecificCategories());
                }
            }

            public static void UpdateSingleSocketBoxEquipments()
            {
                using (var service = new SocketPlanService())
                {
                    singleSocketBoxEquipments = new List<SingleSocketBoxEquipment>(service.GetAllSingleSocketBoxEquipments());
                }
            }

            public static void UpdateNotNameHotaruSwitches()
            {
                using (var service = new SocketPlanService())
                {
                    notNameHotaruSwitches = new List<NotNameHotaruSwitch>(service.GetNotNameHotaruSwitches());
                }
            }

            public static void UpdateNotNameHotaruSwitchSerials()
            {
                using (var service = new SocketPlanService())
                {
                    notNameHotaruSwitchSerials = new List<NotNameHotaruSwitchSerial>(service.GetNotNameHotaruSwitchSerials());
                }
            }

            public static void DisposeRooms()
            {
                roomStandardItems = null;
                rooms = null;
                roomStandardNewItems = null;
                newRooms = null;
                roomStandardOldItems = null;
                oldRooms = null;
            }

            public static void DisposeInteriorEstimateRooms()
            {
                interiorEstimateRooms = null;
            }

            public static void DisposeTextItems()
            {
                textItems = null;
            }

            public static void DisposeComments()
            {
                comments = null;
                commentCategories = null;
                commentSpecifications = null;
            }

            public static void DisposeEquipments()
            {
                equipmentCustomActions = null;
                selectionCategoryDetails = null;
                selectionCategories = null;
                relatedEquipments = null;
                texts = null;
                equipmentSpecifications = null;
                equipments = null;
                blocks = null;
            }
        }

        public static string GetSocketPlanFileName(string originalName, SocketPlanType type)
        {
            var s = originalName.Split('-');
            if (s.Length != 6)
                throw new ApplicationException("Invalid file name. Cannot create ");

            if (type == SocketPlanType.Individual)
                return s[0] + "-" + s[1] + "-" + s[2] + "-" + s[3] + "-" + s[4] + "-SP_Individual-" + s[5];
            else if (type == SocketPlanType.Pattern)
                return s[0] + "-" + s[1] + "-" + s[2] + "-" + s[3] + "-" + s[4] + "-SP_Pattern-" + s[5];
            else
                throw new ApplicationException("Invalid socket plan type.");
        }

        #endregion

        #region 承認機能

        /// <summary>承認したことのある内容を記録し、同じ内容を2度承認しなくて良いようにする。</summary>
        private static List<string> approvedHistory = new List<string>();

        public static bool AlreadyApproved(string messageId)
        {
            return UnitWiring.approvedHistory.Contains(messageId);
        }

        public static void AddApprovedHistory(string messageId)
        {
            UnitWiring.approvedHistory.Add(messageId);
        }

        public static void ClearApprovedHistory()
        {
            UnitWiring.approvedHistory.Clear();
        }

        #endregion
    }
}
