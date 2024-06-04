using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.CatalogNews;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.BIZ.CatalogNews
{
    class CatalogNewsBiz: BizBase
    {
        private CatalogNewsDao _dao = new CatalogNewsDao();

        public void GetCatalogNewsTreeData(NewsParam param)
        {
            param.ListCatalogNews = _dao.GetAllShortCatalogNews();
        }

        public void LoadDataDisplay(NewsParam param)
        {
            int? catalogNewsID = param.CatalogNewsID;

            // Get organization info
            SharedComponent.Entities.CatalogNews item = _dao.GetItemByID<SharedComponent.Entities.CatalogNews>(catalogNewsID);
            if (item == null)
                throw new SMXException(Messages.ItemNotExisted);

            param.CatalogNews = item;

        }

        public void DeleteItems(NewsParam param)
        {
            SharedComponent.Entities.CatalogNews item = param.CatalogNews;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;
            item.Deleted = SMX.smx_IsDeleted;

            int catalogNewsID = item.CatalogNewsID.Value;

            // Validate
            int count = _dao.GetChildrenCount(catalogNewsID);
            if (count > 0)
                throw new SMXException("Danh mục đang có danh mục con trực thuộc. Không được phép xóa.");
            _dao.UpdateItem(item);
        }

        public void AddNewItem(NewsParam param)
        {
            ValidateItem(param);

            #region Prepare system data

            SharedComponent.Entities.CatalogNews item = param.CatalogNews;
            item.Deleted = SMX.smx_IsNotDeleted;
            item.Version = SMX.smx_FirstVersion;
            item.CreatedBy = Profiles.MyProfile.UserName;
            item.CreatedDTG = DateTime.Now;
            #endregion

            // Validate in database
            ValidateOrganizationInDatabase(param);
            _dao.InsertNews(item);

        }

        public void UpdateItem(NewsParam param)
        {
            // Validate
            ValidateItem(param);

            // Prepare system data
            SharedComponent.Entities.CatalogNews item = param.CatalogNews;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            //Validate on database
            ValidateOrganizationInDatabase(param);
            _dao.UpdateNews(item);
        }

        public void ValidateItem(NewsParam param)
        {
           SharedComponent.Entities.CatalogNews item = param.CatalogNews;
            List<string> lstMsg = new List<string>();

            bool isAddNew = item.CatalogNewsID == null;

            if (string.IsNullOrWhiteSpace(item.Code))
            {
                lstMsg.Add("[Mã danh mục] chưa được nhập");
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                lstMsg.Add("[Tên danh mục] chưa được nhập");
            }

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }

        private void ValidateOrganizationInDatabase(NewsParam param)
        {
            SharedComponent.Entities.CatalogNews item = param.CatalogNews;
            List<string> lstMsg = new List<string>();

            // Validate Code
            bool isDuplicatedCode = _dao.CheckDuplicatedCode(item.CatalogNewsID, item.Code);
            if (isDuplicatedCode)
                lstMsg.Add("Mã Code đã tồn tại trong danh mục, vui lòng chọn mã khác");

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }
    }
}
