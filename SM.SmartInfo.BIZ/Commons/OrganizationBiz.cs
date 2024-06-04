using System.Linq;
using SM.SmartInfo.DAO.Commons;
using SM.SmartInfo.CacheManager;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.SmartInfo.BIZ.Commons
{
    class OrganizationBiz : BizBase
    {
        private OrganizationDAO _dao = new OrganizationDAO();

        #region Get

        public void OrganizationSelectorSearch(CommonParam param)
        {
            switch (param.OrganizationSelectorTreeMode)
            {
                case OrganizationSelectorTreeMode.All:
                    {
                        //param.Organizations = _dao.GetAllShortOrganization();
                        param.Organizations = GlobalCache.GetAllShortOrganization();
                        break;
                    }
                case OrganizationSelectorTreeMode.Type:
                    {
                        param.Organizations = _dao.GetOrganizationByType(param.OrganizationType.Value);
                        break;
                    }
                default:
                    throw new SMXException("Chua ho tro Mode: " + param.OrganizationSelectorTreeMode.ToString());
            }
        }

        public void GetZoneIDByOrganizationID(CommonParam param)
        {
            param.Organization.ZoneID = _dao.GetZoneIDByOrganizationID(param.Organization.OrganizationID.Value);
        }

        public void GetOrganizationByType(CommonParam param)
        {
            param.Organizations = _dao.GetOrganizationByType(param.Organization.Type.Value);
        }

        public void GetOrganizationByListDirectManagingOrganizationID(CommonParam param)
        {
            param.Organizations = _dao.GetOrganizationByListDirectManagingOrganizationID(Profiles.MyProfile.ListDirectManagingOrganizationID);
        }

        public void GetListItemByProvinceId(CommonParam param)
        {
            if (!param.ProvinceId.HasValue)
            {
                param.Organizations = new List<Organization>();
                return;
            }
            OrganizationDAO dao = new OrganizationDAO();

            int provinceId = param.ProvinceId.Value;
            param.Organizations = dao.GetListOrganizationByProvinceId(provinceId);
        }

        #endregion

        public void GetOrganizationByID(CommonParam param)
        {
            param.Organization = _dao.GetOrganizationByID(param.OrganizationID.Value);
        }

        public void GetListOrganizationByZoneId(CommonParam param)
        {
            param.Organizations = _dao.GetListOrganizationByZoneId(param.ZoneId);
        }

        public void GetOrganizationByTypeAndCommitteeCode(CommonParam param)
        {
            List<int> lstType = new List<int>() { param.Organization.Type.Value };
            param.Organizations = _dao.GetOrganizationByTypeAndCommitteeCode(lstType, param.Organization.CommitteeCode);
        }

        public void GetOrganizationByParentID(CommonParam param)
        {
            param.Organizations = _dao.GetOrganizationByParentID(param.Organization.ParentID.Value);
        }

        public void SearchBranchByName(CommonParam param)
        {
            string searchName = param.Organization.Name;
            List<int> lstType = new List<int>() { SMX.OrganizationType.ManagementUnit };
            List<Organization> lstBranch = _dao.GetOrganizationByTypeAndCommitteeCode(lstType, SMX.CommitteeCode.CKS_TrungTam);
            if (!string.IsNullOrWhiteSpace(searchName))
                lstBranch = lstBranch.Where(c => c.Name.ToLower().Contains(searchName.ToLower())).ToList();

            param.Organizations = lstBranch;
        }

        public void SearchOrganization(CommonParam param)
        {
            param.Organizations = _dao.SearchOrganization(param.OrganizationName, param.OrganizationType);
        }
    }
}