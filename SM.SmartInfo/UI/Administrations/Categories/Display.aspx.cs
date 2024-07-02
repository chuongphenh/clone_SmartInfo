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

namespace SM.SmartInfo.UI.Administrations.Categories
{
    public partial class Display : BasePage, ISMFormDisplay<Category>
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

        //protected void lbtImage_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(hidBinaryContent.Value))
        //        {
        //            string fileType = string.Empty;
        //            int? transformType = Utility.GetNullableInt(hiTransformType.Value);
        //            if (transformType.HasValue)
        //            {
        //                switch (transformType.Value)
        //                {
        //                    case SMX.TransformType.TransformByWord:
        //                        {
        //                            fileType = ".doc";
        //                            break;
        //                        }
        //                    case SMX.TransformType.TransformByXslt:
        //                        {
        //                            fileType = ".xsl";
        //                            break;
        //                        }
        //                    default:
        //                        break;
        //                }
        //            }
        //            string displayName = lblCode.Text + fileType;//.xls: Extension of 4 templates
        //            byte[] bytes = Convert.FromBase64String(hidBinaryContent.Value);

        //            SoftMart.Core.Utilities.DownloadHelper.PushBinaryContent(SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_TEXT, bytes, displayName);
        //        }
        //    }
        //    catch (SMXException ex)
        //    {
        //        ucErr.ShowError(ex);
        //    }
        //}

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
            CategoryParam param = new CategoryParam(FunctionType.Administration.Category.LoadDataDisplay);
            param.CategoryID = Utility.GetNullableInt(hiID.Value);
            MainController.Provider.Execute(param);

            BindObjectToForm(param.Category);
        }

        public void DeleteItems()
        {
            CategoryParam param = new CategoryParam(FunctionType.Administration.Category.DeleteItem);
            Category category = new Category();
            category.CategoryID = Utility.GetNullableInt(hiID.Value);
            //target.Status = Utility.GetInt(hidStatus.Value);
            param.Category = category;
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specifix

        public void BindObjectToForm(Category item)
        {
            // Button edit
            //lnkEdit.Visible = lnkEdit.Visible && (item.Status == SMX.Status.EmailTemplate.Updating || item.Status == SMX.Status.EmailTemplate.Final);
            if (lnkEdit.Visible)
            {
                lnkEdit.NavigateUrl = string.Format(PageURL.Edit, item.CategoryID);
            }

            // Record data
            lblName.Text = item.CategoryName;
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkEdit   , FunctionCode.EDIT     },
                    //{ btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}