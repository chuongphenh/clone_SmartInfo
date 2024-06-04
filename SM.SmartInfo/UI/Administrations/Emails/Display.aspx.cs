
using System;

using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.Utils;
using System.IO;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.BIZ;

using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.PermissionManager.Shared;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.Administrations.Emails
{
    public partial class Display : BasePage, ISMFormDisplay<Flex_EmailTemplate>
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItems();
                Response.Redirect(PageURL.Default);
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

        #endregion

        #region Private

        private void ApproveReject(int status)
        {
            EmailTemplateParam param = new EmailTemplateParam(FunctionType.Administration.EmailTemplate.ApproveRejectEmailTemplate);

            Flex_EmailTemplate email = new Flex_EmailTemplate();
            email.EmailTemplateID = base.GetIntIdParam(SMX.Parameter.ID);
            email.Status = status;
            email.Version = Utility.GetNullableInt(hidVersion.Value);

            param.EmailTemplate = email;

            MainController.Provider.Execute(param);
        }

        #endregion

        #region Common

        public void SetupForm()
        {
            hiID.Value = Utility.GetString(base.GetIntIdParam());
            lnkExit.NavigateUrl = PageURL.Default;
            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, GetIntIdParam());
        }

        public void LoadData()
        {
            EmailTemplateParam param = new EmailTemplateParam(FunctionType.Administration.EmailTemplate.LoadDataDisplay);
            param.EmailTemplateID = Utility.GetNullableInt(hiID.Value);
            MainController.Provider.Execute(param);

            BindObjectToForm(param.EmailTemplate);
        }

        public void DeleteItems()
        {
            EmailTemplateParam param = new EmailTemplateParam(FunctionType.Administration.EmailTemplate.DeleteItem);
            Flex_EmailTemplate email = new Flex_EmailTemplate();
            email.EmailTemplateID = Utility.GetNullableInt(hiID.Value);
            email.Status = Utility.GetInt(hidStatus.Value);
            param.EmailTemplate = email;
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specifix

        public void BindObjectToForm(Flex_EmailTemplate item)
        {
            // Button edit
            lnkEdit.Visible = lnkEdit.Visible && (item.Status == SMX.Status.EmailTemplate.Updating || item.Status == SMX.Status.EmailTemplate.Final);
            if (lnkEdit.Visible)
            {
                lnkEdit.NavigateUrl = string.Format(PageURL.Edit, item.EmailTemplateID);
            }

            // Record data
            lblCode.Text = item.Code;
            lblName.Text = item.Name;
            lblTemplateType.Text = Utils.Utility.GetDictionaryValue(SMX.TemplateType.dctTemplateTypes, item.TemplateType);
            hiTransformType.Value = Utility.GetString(item.TransformType);
            lblTransformType.Text = Utils.Utility.GetDictionaryValue(SMX.TransformType.dctName, item.TransformType);
            lblProperties.Text = item.Properties;
            lblSubject.Text = item.Subject;
            lblContent.Text = item.Content;
            lblStatus.Text = Utils.Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
            hidStatus.Value = Utility.GetString(item.Status);
            hidVersion.Value = item.Version.ToString();

            if (item.TransformType == SMX.TransformType.Map)
                lblContent.Visible = true;

            if (item.TriggerType.HasValue)
            {
                lblTriggerType.Text = Utility.GetDictionaryValue<int?>(SMX.TriggerType.dicDes, item.TriggerType);
                switch (item.TriggerType)
                {
                    case SMX.TriggerType.Event:
                        break;
                    case SMX.TriggerType.Daily:
                        break;
                    case SMX.TriggerType.Weekly:
                        {
                            switch (item.TriggerTime.Value.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    lblTriggerTime.Text = "Thứ 2";
                                    break;
                                case DayOfWeek.Tuesday:
                                    lblTriggerTime.Text = "Thứ 3";
                                    break;
                                case DayOfWeek.Wednesday:
                                    lblTriggerTime.Text = "Thứ 4";
                                    break;
                                case DayOfWeek.Thursday:
                                    lblTriggerTime.Text = "Thứ 5";
                                    break;
                                case DayOfWeek.Friday:
                                    lblTriggerTime.Text = "Thứ 6";
                                    break;
                                case DayOfWeek.Saturday:
                                    lblTriggerTime.Text = "Thứ 7";
                                    break;
                                case DayOfWeek.Sunday:
                                    lblTriggerTime.Text = "Chủ nhật";
                                    break;
                            }
                        }
                        break;
                    case SMX.TriggerType.Monthly:
                        lblTriggerTime.Text = item.TriggerTime.Value.Day.ToString();
                        break;
                }
            }

            //Photo
            if (item.ContentBinary != null && item.ContentBinary.Length > 0 && item.TransformType != SMX.TransformType.Map)
            {
                hidBinaryContent.Value = Convert.ToBase64String(item.ContentBinary);
                lbtImage.Visible = true;
            }
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkEdit   , FunctionCode.EDIT     },
                    { btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}