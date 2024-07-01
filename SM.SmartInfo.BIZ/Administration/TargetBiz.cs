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
    class TargetBiz : BizBase, ISMFormCRUDBiz<TargetParam>
    {
        private TargetDao _dao = new TargetDao();

        public void SetupAddNewForm(TargetParam param)
        {

        }

        public void AddNewItem(TargetParam param)
        {
            //01. Validate input data
            this.ValidateItem(param);

            //02. Insert emailTemplate
            Target item = param.Target;
            item.CreatedBy = Profiles.MyProfile.UserName;
            item.CreatedDTG = System.DateTime.Now;
            item.Version = SMX.smx_FirstVersion;
            item.Deleted = SMX.smx_IsNotDeleted;

            _dao.InsertTarget(item);
        }

        public void ValidateItem(TargetParam param)
        {
            List<string> lsmMess = new List<string>();

            if (param.Target == null)
                lsmMess.Add(Messages.Target.NoItemTarget);

            if (param.Target != null && string.IsNullOrWhiteSpace(param.Target.TargetCode))
                lsmMess.Add(Messages.Target.NoCode);

            if (param.Target != null && string.IsNullOrWhiteSpace(param.Target.TargetName))
                lsmMess.Add(Messages.Target.NoName);

            //if (param.Target != null && param.EmailTemplate.TemplateType == null)
            //    lsmMess.Add(Messages.Target.NoTemplateType);

            //if (param.Target != null && param.EmailTemplate.TransformType == null)
            //    lsmMess.Add(Messages.Emailtemplate.NoTransformType);

            if (lsmMess.Count > 0)
                throw new SMXException(lsmMess);
        }

        public void LoadDataDisplay(TargetParam param)
        {
            param.Target = _dao.GetTargetByID(param.TargetID);
        }

        public void SetupEditForm(TargetParam param)
        {
            //param.EmailTemplate = _dao.GetEmailTemplateByID(param.EmailTemplateID);
        }

        public void LoadDataEdit(TargetParam param)
        {
            param.Target = _dao.GetTargetByID(param.TargetID);
        }

        public void UpdateItem(TargetParam param)
        {
            //01. Validate input data
            this.ValidateItem(param);

            //02. Update emailTemplate
            Target item = param.Target;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = System.DateTime.Now;

            _dao.UpdateTarget(item);
        }

        public void SetupViewForm(TargetParam param)
        {
            //_dao.GetEmailTemplates(param);
        }

        public void DeleteItems(TargetParam param)
        {
            Target item = param.Target;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = System.DateTime.Now;
            item.Deleted = SMX.smx_IsDeleted;
            //if (item.Status == SMX.Status.Active)
            //    throw new SMXException("Bản ghi đang được sử dụng không được phép xóa !");
            _dao.UpdateTarget(item);


        }

        public void SearchItemsForView(TargetParam param)
        {
            _dao.GetEmailTemplates(param);
        }

        public void ApproveRejectEmailTemplate(TargetParam param)
        {
            Target item = param.Target;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            _dao.UpdateTarget(item);
        }
    }
}
