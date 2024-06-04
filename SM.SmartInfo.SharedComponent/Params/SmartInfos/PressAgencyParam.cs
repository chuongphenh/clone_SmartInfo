using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.SmartInfos
{
    public class PressAgencyParam : BaseParam
    {
        public PressAgencyParam(string functionType)
            : base(Constants.BusinessType.PressAgency, functionType)
        {
        }
        public ntf_Notification Notification { get; set; }
        public string PressAgencyTypeCode { get; set; }
        public int PressAgencyTypeID { get; set; }
        public string PressAgencyName { get; set; }
        public string PressAgencyHRName { get; set; }
        public List<AgencyType> ListAgencyType { get; set; }

        public agency_PressAgency PressAgency { get; set; }

        public int PressAgencyType { get; set; }

        public List<adm_Attachment> ListOtherImage { get; set; }

        public List<agency_PressAgency> ListPressAgency { get; set; }

        public agency_PressAgencyHR PressAgencyHR { get; set; }

        public List<agency_PressAgencyHR> ListPressAgencyHR { get; set; }

        public agency_PressAgencyHistory PressAgencyHistory { get; set; }

        public List<agency_PressAgencyHistory> ListPressAgencyHistory { get; set; }

        public agency_PressAgencyMeeting PressAgencyMeeting { get; set; }

        public List<agency_PressAgencyMeeting> ListPressAgencyMeeting { get; set; }

        public agency_RelationsPressAgency RelationsPressAgency { get; set; }

        public List<agency_RelationsPressAgency> ListRelationsPressAgency { get; set; }

        public agency_RelationshipWithMB RelationshipWithMB { get; set; }

        public List<agency_RelationshipWithMB> ListRelationshipWithMB { get; set; }

        public agency_PressAgencyHRHistory PressAgencyHRHistory { get; set; }

        public List<agency_PressAgencyHRHistory> ListPressAgencyHRHistory { get; set; }

        public agency_PressAgencyHRAlert PressAgencyHRAlert { get; set; }

        public List<agency_PressAgencyHRAlert> ListPressAgencyHRAlert { get; set; }

        public agency_PressAgencyHRRelatives PressAgencyHRRelatives { get; set; }

        public List<agency_PressAgencyHRRelatives> ListPressAgencyHRRelatives { get; set; }

        public adm_Attachment Attachment { get; set; }

        public List<adm_Attachment> ListAttachment { get; set; }

        public AgencyType AgencyType { get; set; }

        public string Name { get; set; }

        public bool IsSaveComplete { get; set; }

        public int UserId { get; set; }
        public int pressAgencyHRID { get; set; }

        public List<Employee> listUserShared { get; set; }
        public string txtSearchUserShared { get; set; }
    }
}