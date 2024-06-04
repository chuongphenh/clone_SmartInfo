using System;
using System.Collections.Generic;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.BIZ
{
    abstract class BizBase
    {
        protected void SetSystemInfo(SharedComponent.Interfaces.ISystemEntity item)
        {
            if (item.ItemID.HasValue)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;
            }
            else
            {
                item.UpdatedBy = item.CreatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = item.CreatedDTG = DateTime.Now;
                item.Deleted = SMX.smx_IsNotDeleted;
                item.Version = SMX.smx_FirstVersion;
            }
        }

        protected void ReloadGlobalCache(SystemParameter item)
        {
            GlobalCache.UpdateItem(item);

            //// sync cache - lay link service, method name
            //List<string> lstServiceLink = new List<string>();
            //string arrReloadServiceURL = Utils.ConfigUtils.GetConfig("ReloadCacheServiceLink");
            //if (!string.IsNullOrWhiteSpace(arrReloadServiceURL))
            //    lstServiceLink = arrReloadServiceURL.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            //// khong co thong tin cau hinh de sync => bo qua
            //string serviceMethod = Utils.ConfigUtils.GetConfig("ReloadCacheServiceMethod");
            //string serviceKey = Utils.ConfigUtils.GetConfig("CMVServiceKey");
            //if (item.SystemParameterID == null || string.IsNullOrWhiteSpace(serviceKey) ||
            //    string.IsNullOrWhiteSpace(serviceMethod) || lstServiceLink.Count == 0)
            //    return;

            //serviceMethod = serviceMethod + "Item";
            //foreach (string serviceLink in lstServiceLink)
            //{
            //    try
            //    {
            //        Service.ECM.Service.ServiceWrapper.CallWebService<string>(serviceLink, serviceMethod,
            //            new object[] { serviceKey, item.SystemParameterID.ToString() });
            //    }
            //    catch { }
            //}
        }

        protected void AddRequireErrorMessage(string title, List<string> lstErr)
        {
            lstErr.Add(GetRequireErrorMessage(title));
        }

        protected string GetRequireErrorMessage(string title)
        {
            return string.Format(Messages.FieldNotEmpty, title);
        }

        protected string GetFullAddress(int? province, int? district, int? town, int? street, string houseNumber)
        {
            List<string> lstAddress = new List<string>();

            if (!string.IsNullOrWhiteSpace(houseNumber))
                lstAddress.Add(houseNumber);

            if (street.HasValue)
                lstAddress.Add(GlobalCache.GetNameByID(street));

            if (town.HasValue)
                lstAddress.Add(GlobalCache.GetNameByID(town));

            if (district.HasValue)
                lstAddress.Add(GlobalCache.GetNameByID(district));

            if (province.HasValue)
                lstAddress.Add(GlobalCache.GetNameByID(province));

            return string.Join(", ", lstAddress);
        }
        protected string GetDescriptionVehicle(int? model, int? brand, int? namsx)
        {
            List<string> lstDescription = new List<string>();
            if (brand.HasValue)
                lstDescription.Add(GlobalCache.GetNameByID(brand));
            if (model.HasValue)
                lstDescription.Add(GlobalCache.GetNameByID(model));
            if (namsx.HasValue)
                lstDescription.Add(namsx + "");
            return string.Join(", ", lstDescription);
        }
        protected string GetDescriptionVessel(int? vesselType, string shipType, string name, string infactProducedYear)
        {
            List<string> lstDescription = new List<string>();
            if (vesselType.HasValue)
                lstDescription.Add(GlobalCache.GetNameByID(vesselType));
            if (!string.IsNullOrWhiteSpace(shipType))
                lstDescription.Add(shipType + "");
            if (!string.IsNullOrWhiteSpace(name))
                lstDescription.Add(name);
            if (!string.IsNullOrWhiteSpace(infactProducedYear))
                lstDescription.Add(infactProducedYear);
            return string.Join(", ", lstDescription);
        }

        protected string GetDescriptionEquipment(string type, string brand, string model, int? producedYear)
        {
            List<string> lstDescription = new List<string>();
            if (!string.IsNullOrWhiteSpace(type))
                lstDescription.Add(type);
            if (!string.IsNullOrWhiteSpace(brand))
                lstDescription.Add(brand);
            if (!string.IsNullOrWhiteSpace(model))
                lstDescription.Add(model);
            if (producedYear.HasValue)
                lstDescription.Add(producedYear + "");
            return string.Join(", ", lstDescription);
        }
        protected string GetFullAddressWithMapNo(string plotNo, string mapNo, string address)
        {
            List<string> lstAddress = new List<string>();

            if (!string.IsNullOrWhiteSpace(plotNo))
                lstAddress.Add("Thửa đất số " + plotNo);

            if (!string.IsNullOrWhiteSpace(mapNo))
                lstAddress.Add("tờ bản đồ " + mapNo);

            if (!string.IsNullOrWhiteSpace(address))
                lstAddress.Add(address);

            return string.Join(", ", lstAddress);
        }

        protected string GetDescriptionEquipment(string type, string brand, string model, string color, int? producedYear, int? manufacturedCountry)
        {
            List<string> lstDescription = new List<string>();

            if (!string.IsNullOrWhiteSpace(type))
                lstDescription.Add(type);

            if (!string.IsNullOrWhiteSpace(brand))
                lstDescription.Add("nhãn hiệu " + brand);

            if (!string.IsNullOrWhiteSpace(model))
                lstDescription.Add("số loại " + model);

            if (!string.IsNullOrWhiteSpace(color))
                lstDescription.Add("màu " + color);

            if (producedYear.HasValue)
                lstDescription.Add("sản xuất năm " + producedYear);

            if (manufacturedCountry.HasValue)
                lstDescription.Add("tại " + GlobalCache.GetNameByID(manufacturedCountry));

            return string.Join(", ", lstDescription);
        }
    }
}
