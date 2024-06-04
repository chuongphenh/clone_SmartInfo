using System;
using System.Collections.Generic;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SoftMart.Core.Dao;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.BIZ.Commons;
using System.Linq;

namespace SM.SmartInfo.BIZ.Administration
{
    class RegionBiz : BizBase
    {
        private RegionDao _dao = new RegionDao();
        public void SearchItemsForView(RegionParam param)
        {
            param.Listadm_Region = _dao.GetRegionsForView(param);
        }

        public void LoadDataEdit(RegionParam param)
        {
            int? regionID = param.RegionID;
            param.adm_Region = _dao.GetItemByID<adm_Region>(regionID);
            param.Listadm_RegionProvince = _dao.GetListRegionProvince(regionID);
        }

        public void AddNewData(RegionParam param)
        {
            var region = param.adm_Region;
            var lstRegionProvince = param.Listadm_RegionProvince;
            ValidateRegion(region);
            SetSystemInfo(region);
            _dao.InsertItem(region);
            if (lstRegionProvince != null && lstRegionProvince.Count > 0)
            {
                lstRegionProvince.ForEach(c => c.RegionID = region.RegionID);
                _dao.InsertItems(lstRegionProvince);
            }

        }

        public void SaveData(RegionParam param)
        {
            var region = param.adm_Region;
            var lstRegionProvince = param.Listadm_RegionProvince;
            ValidateRegion(region);
            SetSystemInfo(region);
            _dao.UpdateItem(region);
            _dao.Delete_adm_RegionProvinceByRegionID(region.RegionID);

            if (lstRegionProvince != null && lstRegionProvince.Count > 0)
            {
                lstRegionProvince.ForEach(c => c.RegionID = region.RegionID);
                _dao.InsertItems(lstRegionProvince);
            }

        }

        public void DeleteRegion(RegionParam param)
        {
            var regionID = param.RegionID;
            _dao.Delete_adm_RegionByRegionID(regionID);
        }

        private void ValidateRegion(adm_Region item)
        {
            List<string> lstErr = new List<string>();
            if (string.IsNullOrEmpty(item.Name))
                lstErr.Add("[Tên vùng] không để trống");
            if (item.EmployeeID == null)
                lstErr.Add("[Cán bộ quản lý] không để trống");
            if (lstErr != null && lstErr.Count > 0)
                throw new SMXException(lstErr);
        }


    }
}
