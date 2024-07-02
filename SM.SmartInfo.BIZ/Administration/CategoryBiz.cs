using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Kernel.Exceptions;

namespace SM.SmartInfo.BIZ.Administration
{
    class CategoryBiz : BizBase, ISMFormCRUDBiz<CategoryParam>
    {
        private CategoryDAO _dao = new CategoryDAO();

        public void SetupAddNewForm(CategoryParam param)
        {

        }

        public void AddNewItem(CategoryParam param)
        {
            //01. Validate input data
            this.ValidateItem(param);

            //02. Insert emailTemplate
            Category item = param.Category;
            item.CreatedBy = Profiles.MyProfile.UserName;
            item.CreatedDTG = System.DateTime.Now;
            item.Version = SMX.smx_FirstVersion;
            item.Deleted = SMX.smx_IsNotDeleted;

            _dao.InsertCategory(item);
        }

        public void ValidateItem(CategoryParam param)
        {
            List<string> lsmMess = new List<string>();

            if (param.Category == null)
                lsmMess.Add(Messages.Category.NoItemCategory);

            if (param.Category != null && string.IsNullOrWhiteSpace(param.Category.CategoryName))
                lsmMess.Add(Messages.Category.NoName);

            //if (param.Category != null && param.EmailTemplate.TemplateType == null)
            //    lsmMess.Add(Messages.Category.NoTemplateType);

            //if (param.Category != null && param.EmailTemplate.TransformType == null)
            //    lsmMess.Add(Messages.Emailtemplate.NoTransformType);

            if (lsmMess.Count > 0)
                throw new SMXException(lsmMess);
        }

        public void LoadDataDisplay(CategoryParam param)
        {
            param.Category = _dao.GetCategoryByID(param.CategoryID);
        }

        public void SetupEditForm(CategoryParam param)
        {
            //param.EmailTemplate = _dao.GetEmailTemplateByID(param.EmailTemplateID);
        }

        public void LoadDataEdit(CategoryParam param)
        {
            param.Category = _dao.GetCategoryByID(param.CategoryID);
        }

        public void UpdateItem(CategoryParam param)
        {
            //01. Validate input data
            this.ValidateItem(param);

            //02. Update emailTemplate
            Category item = param.Category;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = System.DateTime.Now;

            _dao.UpdateCategory(item);
        }

        public void SetupViewForm(CategoryParam param)
        {
            //_dao.GetEmailTemplates(param);
        }

        public void DeleteItems(CategoryParam param)
        {
            Category item = param.Category;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = System.DateTime.Now;
            item.Deleted = SMX.smx_IsDeleted;
            //if (item.Status == SMX.Status.Active)
            //    throw new SMXException("Bản ghi đang được sử dụng không được phép xóa !");
            _dao.UpdateCategory(item);


        }

        public void SearchItemsForView(CategoryParam param)
        {
            _dao.GetCategoryTemplates(param);
        }

        public void ApproveRejectEmailTemplate(CategoryParam param)
        {
            Category item = param.Category;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            _dao.UpdateCategory(item);
        }
    }

}