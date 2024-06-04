
using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;

using SoftMart.Kernel.Exceptions;

using SM.SmartInfo.PermissionManager.Shared;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Plans
{
    public partial class AddNew : BasePage, ISMFormAddNew<Flex_EmailTemplate>
    {
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    SetupForm();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                object itemID = AddNewItem();
                Response.Redirect(string.Format(PageURL.Display, itemID.ToString()));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ddTransformType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int type = Utils.Utility.GetInt(ddTransformType.SelectedValue);
                txtContent.Visible = type == SMX.TransformType.Map;
                fileUpload.Visible = !txtContent.Visible;
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ddlTriggerType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int? triggerType = Utility.GetNullableInt(ddlTriggerType.SelectedValue);
            ddlTriggerTime.Items.Clear();
            ddlTriggerTime.Visible = false;

            switch (triggerType)
            {
                case SMX.TriggerType.Event:
                    lblTriggerTimeNote.Text = "Gửi khi xảy ra các sự kiện";
                    break;
                case SMX.TriggerType.Daily:
                    lblTriggerTimeNote.Text = "Gửi vào thời điểm giờ cấu hình hàng ngày";
                    break;
                case SMX.TriggerType.Weekly:
                    ddlTriggerTime.Visible = true;
                    ddlTriggerTime.Items.AddRange(new ListItem[]{
                        new ListItem("Thứ 2", ((int)DayOfWeek.Monday).ToString()),
                        new ListItem("Thứ 3", ((int)DayOfWeek.Tuesday).ToString()),
                        new ListItem("Thứ 4", ((int)DayOfWeek.Wednesday).ToString()),
                        new ListItem("Thứ 5", ((int)DayOfWeek.Thursday).ToString()),
                        new ListItem("Thứ 6", ((int)DayOfWeek.Friday).ToString()),
                        new ListItem("Thứ 7", ((int)DayOfWeek.Saturday).ToString()),
                        new ListItem("Chủ nhật", ((int)DayOfWeek.Sunday).ToString())
                    });

                    lblTriggerTimeNote.Text = "Gửi vào thời điểm giờ cấu hình tại ngày được chọn trong tuần";
                    break;
                case SMX.TriggerType.Monthly:
                    ddlTriggerTime.Visible = true;
                    for (int i = 1; i <= 31; i++)
                    {
                        ddlTriggerTime.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    lblTriggerTimeNote.Text = "Gửi vào thời điểm giờ cấu hình tại ngày được chọn trong tháng";
                    break;
            }
        }
        #endregion

        #region Common

        public void SetupForm()
        {
            lnkExit.NavigateUrl = PageURL.Default;
            //bind radcombobox control
            UIUtility.BindListToDropDownList(ddTemplateType, SMX.TemplateType.dctTemplateTypes.ToList(), false);
            UIUtility.BindListToDropDownList(ddTransformType, SMX.TransformType.dctName.ToList(), false);
            UIUtility.BindListToDropDownList(ddStatus, SMX.Status.dctStatus.ToList(), false);
            UIUtility.BindDicToDropDownList<int?, string>(ddlTriggerType, SMX.TriggerType.dicDes, true);

            int type = Utility.GetInt(ddTransformType.SelectedValue);
            txtContent.Visible = type == SMX.TransformType.Map;
            fileUpload.Visible = !txtContent.Visible;
        }

        public object AddNewItem()
        {
            EmailTemplateParam param = new EmailTemplateParam(FunctionType.Administration.EmailTemplate.AddNewItem);

            param.EmailTemplate = BindFormToObject();
            MainController.Provider.Execute(param);

            return param.EmailTemplate.EmailTemplateID;
        }

        #endregion

        #region Specifix

        public Flex_EmailTemplate BindFormToObject()
        {
            Flex_EmailTemplate item = new Flex_EmailTemplate();

            item.Code = txtCode.Text;
            item.Name = txtName.Text;
            item.TemplateType = Utils.Utility.GetNullableInt(ddTemplateType.SelectedValue);
            item.TransformType = Utils.Utility.GetNullableInt(ddTransformType.SelectedValue);
            item.Properties = txtProperties.Text;
            item.Subject = txtSubject.Text;
            item.Content = (item.TemplateType == SMX.TemplateType.SMS) ? txtContent.Text : txtContent.Text;
            item.Content = UIUtility.ConvertBreakLine2Html(item.Content, false);
            item.Status = int.Parse(ddStatus.SelectedValue);
            item.TriggerType = Utility.GetNullableInt(ddlTriggerType.SelectedValue);

            switch (item.TriggerType)
            {
                case SMX.TriggerType.Event:
                    item.TriggerTime = null;
                    break;
                case SMX.TriggerType.Daily:
                    item.TriggerTime = null;
                    break;
                case SMX.TriggerType.Weekly:
                    item.TriggerTime = DateTime.Now;
                    int selectedDW = Utility.GetInt(ddlTriggerTime.SelectedValue);
                    int nowDW = (int)item.TriggerTime.Value.DayOfWeek;
                    item.TriggerTime = item.TriggerTime.Value.AddDays(selectedDW - nowDW);
                    break;
                case SMX.TriggerType.Monthly:
                    item.TriggerTime = new DateTime(DateTime.Now.Year, 12, Utility.GetInt(ddlTriggerTime.SelectedValue));
                    break;
            }

            if (fileUpload.HasFile)
                item.ContentBinary = fileUpload.FileBytes;

            return item;
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { btnSave   , FunctionCode.ADD  },
                };
            }
        }
    }
}