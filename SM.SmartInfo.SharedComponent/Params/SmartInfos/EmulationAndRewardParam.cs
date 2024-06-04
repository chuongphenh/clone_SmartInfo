using System;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.SmartInfos
{
    public class EmulationAndRewardParam : BaseParam
    {
        public EmulationAndRewardParam(string functionType)
            : base(Constants.BusinessType.EmulationAndReward, functionType)
        {
        }

        public er_EmulationAndReward er_EmulationAndReward { get; set; }

        public List<er_EmulationAndReward> ListEmulationAndReward { get; set; }

        public er_EmulationAndRewardSubject er_EmulationAndRewardSubject { get; set; }

        public List<er_EmulationAndRewardSubject> ListEmulationAndRewardSubject { get; set; }

        public er_EmulationAndRewardHistory er_EmulationAndRewardHistory { get; set; }

        public List<er_EmulationAndRewardHistory> ListEmulationAndRewardHistory { get; set; }

        public adm_Attachment Attachment { get; set; }

        public adm_Attachment AttachmentCaNhan { get; set; }

        public adm_Attachment AttachmentDonVi { get; set; }

        public List<adm_Attachment> ListAttachment { get; set; }

        public bool IsSaveComplete { get; set; }


        // Awarding Catalog
        public AwardingCatalog AwardingCatalog { get; set; }

        public List<AwardingCatalog> ListAwardingCatalog { get; set; }

        public int AwardingCatalogId { get; set; }
        public string AwardingCatalogName { get; set; }


        // Awarding Level
        public AwardingLevel AwardingLevel { get; set; }
        public List<AwardingLevel> ListAwardingLevel { get; set; }
        public int AwardingLevelId { get; set; }
        public string Level { get; set; }
        public string AwardingLevelDescription { get; set; }
        public int AwardingLevelCategory { get; set; }

        // Awarding Type
        public AwardingType AwardingType { get; set; }
        public List<AwardingType> ListAwardingType { get; set; }
        public int AwardingTypeId { get; set; }
        public string AwardingTypeName { get; set; }

        // Awarding Period
        public AwardingPeriod AwardingPeriod { get; set; }
        public List<AwardingPeriod> ListAwardingPeriod { get; set; }
        public int AwardingPeriodId { get; set; }
        public DateTime AwardingTime { get; set; }
        public string AwardingPeriodName { get; set; }
    }
}