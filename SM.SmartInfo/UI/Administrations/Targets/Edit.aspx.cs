
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Targets
{
    public partial class Edit : BasePage, ISMFormEdit<Flex_EmailTemplate>
    {
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    LoadData();
                }
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
                UpdateItem();
                Response.Redirect(string.Format(PageURL.Display, hidId.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void lbtImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(hidBinaryContent.Value))
                {
                    string fileType = string.Empty;
                    int? transformType = Utility.GetNullableInt(hiTransformType.Value);
                    if (transformType.HasValue)
                    {
                        switch (transformType.Value)
                        {
                            case SMX.TransformType.TransformByWord:
                                {
                                    fileType = ".doc";
                                    break;
                                }
                            case SMX.TransformType.TransformByXslt:
                                {
                                    fileType = ".xsl";
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    string displayName = lblCode.Text + fileType;//.xls: Extension of 4 templates
                    byte[] bytes = Convert.FromBase64String(hidBinaryContent.Value);

                    SoftMart.Core.Utilities.DownloadHelper.PushBinaryContent(SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_TEXT, bytes, displayName);
                }
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
                fileUpload.Visible = lbtImage.Visible = !txtContent.Visible;
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
        }

        public void LoadData()
        {
            EmailTemplateParam param = new EmailTemplateParam(FunctionType.Administration.EmailTemplate.LoadDataEdit);
            param.EmailTemplateID = base.GetIntIdParam();
            MainController.Provider.Execute(param);

            BindObjectToForm(param.EmailTemplate);
        }

        public void UpdateItem()
        {
            EmailTemplateParam param = new EmailTemplateParam(FunctionType.Administration.EmailTemplate.UpdateItem);
            param.EmailTemplate = BindFormToObject();
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specifix

        public void BindObjectToForm(Flex_EmailTemplate item)
        {
            lblName.Text = item.Name;
            lblProperties.Text = item.Properties;
            txtSubject.Text = item.Subject;
            txtContent.Text = UIUtility.ConvertHtml2BreakLine(item.Content, false);
            lblCode.Text = item.Code;
            ddTemplateType.SelectedValue = Utils.Utility.GetString(item.TemplateType);
            ddTransformType.SelectedValue = Utils.Utility.GetString(item.TransformType);
            ddStatus.SelectedValue = Utils.Utility.GetString(item.Status);
            hiTransformType.Value = Utility.GetString(item.TransformType);
            txtContent.Visible = item.TransformType == SMX.TransformType.Map;
            fileUpload.Visible = lbtImage.Visible = !txtContent.Visible;
            if (item.TriggerType.HasValue)
                ddlTriggerType.SelectedValue = Utility.GetString(item.TriggerType);

            ddlTriggerType_OnSelectedIndexChanged(null, null);
            switch (item.TriggerType)
            {
                case SMX.TriggerType.Event:
                    break;
                case SMX.TriggerType.Daily:
                    break;
                case SMX.TriggerType.Weekly:
                    ddlTriggerTime.SelectedValue = ((int)item.TriggerTime.Value.DayOfWeek).ToString();
                    break;
                case SMX.TriggerType.Monthly:
                    ddlTriggerTime.SelectedValue = item.TriggerTime.Value.Day.ToString();
                    break;
            }

            //Photo
            if (item.ContentBinary != null && item.ContentBinary.Length > 0)
            {
                hidBinaryContent.Value = Convert.ToBase64String(item.ContentBinary);
                fileUpload.Visible = lbtImage.Visible = true;
            }
            else
                lbtImage.Visible = false;

            hidId.Value = Utility.GetString(item.EmailTemplateID);
            hidVersion.Value = Utility.GetString(item.Version);
        }

        public Flex_EmailTemplate BindFormToObject()
        {
            Flex_EmailTemplate item = new Flex_EmailTemplate();

            item.EmailTemplateID = Utility.GetNullableInt(hidId.Value);
            item.Version = Utility.GetNullableInt(hidVersion.Value);
            item.Code = lblCode.Text;
            item.Name = lblName.Text;
            item.Properties = lblProperties.Text;
            item.Subject = txtSubject.Text;
            item.TemplateType = Utils.Utility.GetNullableInt(ddTemplateType.SelectedValue);
            item.TransformType = Utils.Utility.GetNullableInt(ddTransformType.SelectedValue);
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
                    { this      , FunctionCode.EDIT },
                    { btnSave   , FunctionCode.EDIT },
                };
            }
        }
    }
}