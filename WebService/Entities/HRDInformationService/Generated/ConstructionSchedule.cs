//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.3643
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocketPlan.WebService
{
    using System;
    using System.Collections.Generic;
    using Edsa.Data;
    using Edsa.Data.Attributes;
    
    
    [Serializable()]
    [DbTable(Name="ConstructionSchedule", DatabaseType=DatabaseType.SqlServer, ConnectionSettingKeyName="SocketPlan.WebService.Properties.Settings.ConnectionStringOfHRDInformationService4")]
    public partial class ConstructionSchedule : DataEntity<ConstructionSchedule>
    {
        
        private string _ConstructionCode;
        
        private System.Nullable<System.DateTime> _InitialCameDate;
        
        private System.Nullable<System.DateTime> _InitialVisitedDate;
        
        private System.Nullable<System.DateTime> _DecidedLandDate;
        
        private System.Nullable<System.DateTime> _InspectedSoilDate;
        
        private System.Nullable<System.DateTime> _InspectedSiteDate;
        
        private System.Nullable<System.DateTime> _AccomplishPaymentDate;
        
        private System.Nullable<System.DateTime> _FirstRequestedPlanDate;
        
        private System.Nullable<System.DateTime> _DecidedBasicSpecificationDate;
        
        private System.Nullable<System.DateTime> _IssuedContractDate;
        
        private System.Nullable<System.DateTime> _ExpectedFinalSpecificationConfirmDate;
        
        private System.Nullable<System.DateTime> _ConfirmedFinalSpecificationDate;
        
        private System.Nullable<System.DateTime> _ConsentedStartingDate;
        
        private System.Nullable<System.DateTime> _ExpectedObtainPermissionOfApplicationDate;
        
        private System.Nullable<System.DateTime> _ObtainedPermissionOfApplicationDate;
        
        private System.Nullable<System.DateTime> _ReceiptedApplicationDate;
        
        private System.Nullable<System.DateTime> _SentApplicationDate;
        
        private System.Nullable<System.DateTime> _SentProcessRequestDate;
        
        private System.Nullable<System.DateTime> _ReceivedSpecificationsDate;
        
        private System.Nullable<System.DateTime> _ReceiptedProcessRequestNo1Date;
        
        private System.Nullable<System.DateTime> _ReceiptedProcessRequestNo2Date;
        
        private System.Nullable<System.DateTime> _ProcessingRequestAcceptedDate;
        
        private System.Nullable<System.DateTime> _ReceiptedOriginalProductOrderDate;
        
        private System.Nullable<System.DateTime> _KibiroiFlagFinishedDate;
        
        private System.Nullable<System.DateTime> _StartedFoundationWorkDate;
        
        private System.Nullable<System.DateTime> _ExpectedHouseRaisingDate;
        
        private int _HouseRaisingDateTypeAtRequested;
        
        private System.Nullable<System.DateTime> _ExpectedHouseRaisingDateAtRequested;
        
        private System.Nullable<System.DateTime> _HouseRaisingDate;
        
        private System.Nullable<System.DateTime> _ExpectedCarpenterWorkFinishDate;
        
        private System.Nullable<System.DateTime> _FinishedCarpenterWorkDate;
        
        private System.Nullable<System.DateTime> _TestedEnduranceDate;
        
        private System.Nullable<System.DateTime> _TestedMiddleConstructionDate;
        
        private System.Nullable<System.DateTime> _TestedFinishConstructionDate;
        
        private System.Nullable<System.DateTime> _IssuedBillDate;
        
        private System.Nullable<System.DateTime> _DepositedDate;
        
        private System.Nullable<System.DateTime> _ExpectedReleaseDate;
        
        private System.Nullable<System.DateTime> _ReleasedDate;
        
        private System.Nullable<System.DateTime> _StartedPerformanceGuaranteeDate;
        
        private System.Nullable<System.DateTime> _InitialInspectedDate;
        
        private System.Nullable<System.DateTime> _StoppedProcessingDate;
        
        private System.Nullable<System.DateTime> _ResumedProcessingDate;
        
        private System.Nullable<System.DateTime> _ReceiptedFinalElectricPlanRequestDate;
        
        private System.Nullable<System.DateTime> _PlanSentOutDate;
        
        private System.Nullable<System.DateTime> _FoundationPlanReceivedDate;
        
        private System.Nullable<System.DateTime> _StructualDesignFinishedDate;
        
        private System.Nullable<System.DateTime> _StructualDesignReceivedDate;
        
        private System.Nullable<System.DateTime> _StructualDesignConfirmedDate;
        
        private System.Nullable<System.DateTime> _MetalGoodsOrderMakedDate;
        
        // 建方図面発送日
        private System.Nullable<System.DateTime> _ErectionPlanSentOutDate;
        
        // エコポイント申請依頼受付日
        private System.Nullable<System.DateTime> _EcoPointRequestReceivedDate;
        
        private System.Nullable<System.DateTime> _UpdatedDate;
        
        [DbColumn(Name="ConstructionCode", TypeName="varchar", IsPrimaryKey=true, Length=12)]
        public virtual string ConstructionCode
        {
            get
            {
                return this._ConstructionCode;
            }
            set
            {
                this._ConstructionCode = value;
            }
        }
        
        [DbColumn(Name="InitialCameDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> InitialCameDate
        {
            get
            {
                return this._InitialCameDate;
            }
            set
            {
                this._InitialCameDate = value;
            }
        }
        
        [DbColumn(Name="InitialVisitedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> InitialVisitedDate
        {
            get
            {
                return this._InitialVisitedDate;
            }
            set
            {
                this._InitialVisitedDate = value;
            }
        }
        
        [DbColumn(Name="DecidedLandDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> DecidedLandDate
        {
            get
            {
                return this._DecidedLandDate;
            }
            set
            {
                this._DecidedLandDate = value;
            }
        }
        
        [DbColumn(Name="InspectedSoilDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> InspectedSoilDate
        {
            get
            {
                return this._InspectedSoilDate;
            }
            set
            {
                this._InspectedSoilDate = value;
            }
        }
        
        [DbColumn(Name="InspectedSiteDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> InspectedSiteDate
        {
            get
            {
                return this._InspectedSiteDate;
            }
            set
            {
                this._InspectedSiteDate = value;
            }
        }
        
        [DbColumn(Name="AccomplishPaymentDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> AccomplishPaymentDate
        {
            get
            {
                return this._AccomplishPaymentDate;
            }
            set
            {
                this._AccomplishPaymentDate = value;
            }
        }
        
        [DbColumn(Name="FirstRequestedPlanDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> FirstRequestedPlanDate
        {
            get
            {
                return this._FirstRequestedPlanDate;
            }
            set
            {
                this._FirstRequestedPlanDate = value;
            }
        }
        
        [DbColumn(Name="DecidedBasicSpecificationDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> DecidedBasicSpecificationDate
        {
            get
            {
                return this._DecidedBasicSpecificationDate;
            }
            set
            {
                this._DecidedBasicSpecificationDate = value;
            }
        }
        
        [DbColumn(Name="IssuedContractDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> IssuedContractDate
        {
            get
            {
                return this._IssuedContractDate;
            }
            set
            {
                this._IssuedContractDate = value;
            }
        }
        
        [DbColumn(Name="ExpectedFinalSpecificationConfirmDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ExpectedFinalSpecificationConfirmDate
        {
            get
            {
                return this._ExpectedFinalSpecificationConfirmDate;
            }
            set
            {
                this._ExpectedFinalSpecificationConfirmDate = value;
            }
        }
        
        [DbColumn(Name="ConfirmedFinalSpecificationDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ConfirmedFinalSpecificationDate
        {
            get
            {
                return this._ConfirmedFinalSpecificationDate;
            }
            set
            {
                this._ConfirmedFinalSpecificationDate = value;
            }
        }
        
        [DbColumn(Name="ConsentedStartingDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ConsentedStartingDate
        {
            get
            {
                return this._ConsentedStartingDate;
            }
            set
            {
                this._ConsentedStartingDate = value;
            }
        }
        
        [DbColumn(Name="ExpectedObtainPermissionOfApplicationDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ExpectedObtainPermissionOfApplicationDate
        {
            get
            {
                return this._ExpectedObtainPermissionOfApplicationDate;
            }
            set
            {
                this._ExpectedObtainPermissionOfApplicationDate = value;
            }
        }
        
        [DbColumn(Name="ObtainedPermissionOfApplicationDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ObtainedPermissionOfApplicationDate
        {
            get
            {
                return this._ObtainedPermissionOfApplicationDate;
            }
            set
            {
                this._ObtainedPermissionOfApplicationDate = value;
            }
        }
        
        [DbColumn(Name="ReceiptedApplicationDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ReceiptedApplicationDate
        {
            get
            {
                return this._ReceiptedApplicationDate;
            }
            set
            {
                this._ReceiptedApplicationDate = value;
            }
        }
        
        [DbColumn(Name="SentApplicationDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> SentApplicationDate
        {
            get
            {
                return this._SentApplicationDate;
            }
            set
            {
                this._SentApplicationDate = value;
            }
        }
        
        [DbColumn(Name="SentProcessRequestDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> SentProcessRequestDate
        {
            get
            {
                return this._SentProcessRequestDate;
            }
            set
            {
                this._SentProcessRequestDate = value;
            }
        }
        
        [DbColumn(Name="ReceivedSpecificationsDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ReceivedSpecificationsDate
        {
            get
            {
                return this._ReceivedSpecificationsDate;
            }
            set
            {
                this._ReceivedSpecificationsDate = value;
            }
        }
        
        [DbColumn(Name="ReceiptedProcessRequestNo1Date", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ReceiptedProcessRequestNo1Date
        {
            get
            {
                return this._ReceiptedProcessRequestNo1Date;
            }
            set
            {
                this._ReceiptedProcessRequestNo1Date = value;
            }
        }
        
        [DbColumn(Name="ReceiptedProcessRequestNo2Date", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ReceiptedProcessRequestNo2Date
        {
            get
            {
                return this._ReceiptedProcessRequestNo2Date;
            }
            set
            {
                this._ReceiptedProcessRequestNo2Date = value;
            }
        }
        
        [DbColumn(Name="ProcessingRequestAcceptedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ProcessingRequestAcceptedDate
        {
            get
            {
                return this._ProcessingRequestAcceptedDate;
            }
            set
            {
                this._ProcessingRequestAcceptedDate = value;
            }
        }
        
        [DbColumn(Name="ReceiptedOriginalProductOrderDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ReceiptedOriginalProductOrderDate
        {
            get
            {
                return this._ReceiptedOriginalProductOrderDate;
            }
            set
            {
                this._ReceiptedOriginalProductOrderDate = value;
            }
        }
        
        [DbColumn(Name="KibiroiFlagFinishedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> KibiroiFlagFinishedDate
        {
            get
            {
                return this._KibiroiFlagFinishedDate;
            }
            set
            {
                this._KibiroiFlagFinishedDate = value;
            }
        }
        
        [DbColumn(Name="StartedFoundationWorkDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> StartedFoundationWorkDate
        {
            get
            {
                return this._StartedFoundationWorkDate;
            }
            set
            {
                this._StartedFoundationWorkDate = value;
            }
        }
        
        [DbColumn(Name="ExpectedHouseRaisingDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ExpectedHouseRaisingDate
        {
            get
            {
                return this._ExpectedHouseRaisingDate;
            }
            set
            {
                this._ExpectedHouseRaisingDate = value;
            }
        }
        
        [DbColumn(Name="HouseRaisingDateTypeAtRequested", TypeName="int", Length=4)]
        public virtual int HouseRaisingDateTypeAtRequested
        {
            get
            {
                return this._HouseRaisingDateTypeAtRequested;
            }
            set
            {
                this._HouseRaisingDateTypeAtRequested = value;
            }
        }
        
        [DbColumn(Name="ExpectedHouseRaisingDateAtRequested", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ExpectedHouseRaisingDateAtRequested
        {
            get
            {
                return this._ExpectedHouseRaisingDateAtRequested;
            }
            set
            {
                this._ExpectedHouseRaisingDateAtRequested = value;
            }
        }
        
        [DbColumn(Name="HouseRaisingDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> HouseRaisingDate
        {
            get
            {
                return this._HouseRaisingDate;
            }
            set
            {
                this._HouseRaisingDate = value;
            }
        }
        
        [DbColumn(Name="ExpectedCarpenterWorkFinishDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ExpectedCarpenterWorkFinishDate
        {
            get
            {
                return this._ExpectedCarpenterWorkFinishDate;
            }
            set
            {
                this._ExpectedCarpenterWorkFinishDate = value;
            }
        }
        
        [DbColumn(Name="FinishedCarpenterWorkDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> FinishedCarpenterWorkDate
        {
            get
            {
                return this._FinishedCarpenterWorkDate;
            }
            set
            {
                this._FinishedCarpenterWorkDate = value;
            }
        }
        
        [DbColumn(Name="TestedEnduranceDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> TestedEnduranceDate
        {
            get
            {
                return this._TestedEnduranceDate;
            }
            set
            {
                this._TestedEnduranceDate = value;
            }
        }
        
        [DbColumn(Name="TestedMiddleConstructionDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> TestedMiddleConstructionDate
        {
            get
            {
                return this._TestedMiddleConstructionDate;
            }
            set
            {
                this._TestedMiddleConstructionDate = value;
            }
        }
        
        [DbColumn(Name="TestedFinishConstructionDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> TestedFinishConstructionDate
        {
            get
            {
                return this._TestedFinishConstructionDate;
            }
            set
            {
                this._TestedFinishConstructionDate = value;
            }
        }
        
        [DbColumn(Name="IssuedBillDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> IssuedBillDate
        {
            get
            {
                return this._IssuedBillDate;
            }
            set
            {
                this._IssuedBillDate = value;
            }
        }
        
        [DbColumn(Name="DepositedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> DepositedDate
        {
            get
            {
                return this._DepositedDate;
            }
            set
            {
                this._DepositedDate = value;
            }
        }
        
        [DbColumn(Name="ExpectedReleaseDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ExpectedReleaseDate
        {
            get
            {
                return this._ExpectedReleaseDate;
            }
            set
            {
                this._ExpectedReleaseDate = value;
            }
        }
        
        [DbColumn(Name="ReleasedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ReleasedDate
        {
            get
            {
                return this._ReleasedDate;
            }
            set
            {
                this._ReleasedDate = value;
            }
        }
        
        [DbColumn(Name="StartedPerformanceGuaranteeDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> StartedPerformanceGuaranteeDate
        {
            get
            {
                return this._StartedPerformanceGuaranteeDate;
            }
            set
            {
                this._StartedPerformanceGuaranteeDate = value;
            }
        }
        
        [DbColumn(Name="InitialInspectedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> InitialInspectedDate
        {
            get
            {
                return this._InitialInspectedDate;
            }
            set
            {
                this._InitialInspectedDate = value;
            }
        }
        
        [DbColumn(Name="StoppedProcessingDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> StoppedProcessingDate
        {
            get
            {
                return this._StoppedProcessingDate;
            }
            set
            {
                this._StoppedProcessingDate = value;
            }
        }
        
        [DbColumn(Name="ResumedProcessingDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ResumedProcessingDate
        {
            get
            {
                return this._ResumedProcessingDate;
            }
            set
            {
                this._ResumedProcessingDate = value;
            }
        }
        
        [DbColumn(Name="ReceiptedFinalElectricPlanRequestDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> ReceiptedFinalElectricPlanRequestDate
        {
            get
            {
                return this._ReceiptedFinalElectricPlanRequestDate;
            }
            set
            {
                this._ReceiptedFinalElectricPlanRequestDate = value;
            }
        }
        
        [DbColumn(Name="PlanSentOutDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> PlanSentOutDate
        {
            get
            {
                return this._PlanSentOutDate;
            }
            set
            {
                this._PlanSentOutDate = value;
            }
        }
        
        [DbColumn(Name="FoundationPlanReceivedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> FoundationPlanReceivedDate
        {
            get
            {
                return this._FoundationPlanReceivedDate;
            }
            set
            {
                this._FoundationPlanReceivedDate = value;
            }
        }
        
        [DbColumn(Name="StructualDesignFinishedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> StructualDesignFinishedDate
        {
            get
            {
                return this._StructualDesignFinishedDate;
            }
            set
            {
                this._StructualDesignFinishedDate = value;
            }
        }
        
        [DbColumn(Name="StructualDesignReceivedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> StructualDesignReceivedDate
        {
            get
            {
                return this._StructualDesignReceivedDate;
            }
            set
            {
                this._StructualDesignReceivedDate = value;
            }
        }
        
        [DbColumn(Name="StructualDesignConfirmedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> StructualDesignConfirmedDate
        {
            get
            {
                return this._StructualDesignConfirmedDate;
            }
            set
            {
                this._StructualDesignConfirmedDate = value;
            }
        }
        
        [DbColumn(Name="MetalGoodsOrderMakedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> MetalGoodsOrderMakedDate
        {
            get
            {
                return this._MetalGoodsOrderMakedDate;
            }
            set
            {
                this._MetalGoodsOrderMakedDate = value;
            }
        }
        
        [DbColumn(Name="ErectionPlanSentOutDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8, Remarks="建方図面発送日")]
        public virtual System.Nullable<System.DateTime> ErectionPlanSentOutDate
        {
            get
            {
                return this._ErectionPlanSentOutDate;
            }
            set
            {
                this._ErectionPlanSentOutDate = value;
            }
        }
        
        [DbColumn(Name="EcoPointRequestReceivedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8, Remarks="エコポイント申請依頼受付日")]
        public virtual System.Nullable<System.DateTime> EcoPointRequestReceivedDate
        {
            get
            {
                return this._EcoPointRequestReceivedDate;
            }
            set
            {
                this._EcoPointRequestReceivedDate = value;
            }
        }
        
        [DbColumn(Name="UpdatedDate", TypeName="datetime", Nullable=true, DecimalPlace=3, Length=8)]
        public virtual System.Nullable<System.DateTime> UpdatedDate
        {
            get
            {
                return this._UpdatedDate;
            }
            set
            {
                this._UpdatedDate = value;
            }
        }
        
        public static ConstructionSchedule Get(string _ConstructionCode)
        {
            ConstructionSchedule entity = new ConstructionSchedule();
            entity.ConstructionCode = _ConstructionCode;
            if (!entity.Fill())
            {
                return null;
            }
            return entity;
        }
    }
}
