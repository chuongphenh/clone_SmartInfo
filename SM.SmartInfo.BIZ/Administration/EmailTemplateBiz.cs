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
    class EmailTemplateBiz : BizBase, ISMFormCRUDBiz<EmailTemplateParam>
    {
        private EmailTemplateDao dao = new EmailTemplateDao();

        public void SetupAddNewForm(EmailTemplateParam param)
        {

        }

        public void AddNewItem(EmailTemplateParam param)
        {
            //01. Validate input data
            this.ValidateItem(param);

            //02. Insert emailTemplate
            Flex_EmailTemplate item = param.EmailTemplate;
            item.CreatedBy = Profiles.MyProfile.UserName;
            item.CreatedDTG = System.DateTime.Now;
            item.Version = SMX.smx_FirstVersion;
            item.Deleted = SMX.smx_IsNotDeleted;
            item.IsManually = SMX.smx_Manually;

            dao.InsertEmailTemplate(item);
        }

        public void ValidateItem(EmailTemplateParam param)
        {
            List<string> lsmMess = new List<string>();

            if (param.EmailTemplate == null)
                lsmMess.Add(Messages.Emailtemplate.NoItemEmailtemplate);

            if (param.EmailTemplate != null && string.IsNullOrWhiteSpace(param.EmailTemplate.Code))
                lsmMess.Add(Messages.Emailtemplate.NoCode);

            if (param.EmailTemplate != null && string.IsNullOrWhiteSpace(param.EmailTemplate.Name))
                lsmMess.Add(Messages.Emailtemplate.NoName);

            if (param.EmailTemplate != null && param.EmailTemplate.TemplateType == null)
                lsmMess.Add(Messages.Emailtemplate.NoTemplateType);

            if (param.EmailTemplate != null && param.EmailTemplate.TransformType == null)
                lsmMess.Add(Messages.Emailtemplate.NoTransformType);

            if (lsmMess.Count > 0)
                throw new SMXException(lsmMess);
        }

        public void LoadDataDisplay(EmailTemplateParam param)
        {
            param.EmailTemplate = dao.GetEmailTemplateByID(param.EmailTemplateID);
        }

        public void SetupEditForm(EmailTemplateParam param)
        {
            //param.EmailTemplate = dao.GetEmailTemplateByID(param.EmailTemplateID);
        }

        public void LoadDataEdit(EmailTemplateParam param)
        {
            param.EmailTemplate = dao.GetEmailTemplateByID(param.EmailTemplateID);
        }

        public void UpdateItem(EmailTemplateParam param)
        {
            //01. Validate input data
            this.ValidateItem(param);

            //02. Update emailTemplate
            Flex_EmailTemplate item = param.EmailTemplate;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = System.DateTime.Now;

            dao.UpdateEmailTemplate(item);
        }

        public void SetupViewForm(EmailTemplateParam param)
        {
            //dao.GetEmailTemplates(param);
        }

        public void DeleteItems(EmailTemplateParam param)
        {
            Flex_EmailTemplate item = param.EmailTemplate;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = System.DateTime.Now;
            item.Deleted = SMX.smx_IsDeleted;
            if (item.Status == SMX.Status.Active)
                throw new SMXException("Bản ghi đang được sử dụng không được phép xóa !");
            dao.UpdateEmailTemplate(item);


        }

        public void SearchItemsForView(EmailTemplateParam param)
        {
            dao.GetEmailTemplates(param);
        }

        public void ApproveRejectEmailTemplate(EmailTemplateParam param)
        {
            Flex_EmailTemplate item = param.EmailTemplate;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            dao.UpdateEmailTemplate(item);
        }
    }
}
