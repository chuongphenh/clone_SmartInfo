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

namespace SM.SmartInfo.UI.Administrations.Categories
{
    public partial class Edit : BasePage, ISMFormEdit<Category>
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

        #endregion

        #region Common



        public void SetupForm()
        {
            lnkExit.NavigateUrl = PageURL.Default;
        }

        public void LoadData()
        {
            CategoryParam param = new CategoryParam(FunctionType.Administration.Category.LoadDataEdit);
            param.CategoryID = base.GetIntIdParam();
            MainController.Provider.Execute(param);

            BindObjectToForm(param.Category);
        }

        public void UpdateItem()
        {
            CategoryParam param = new CategoryParam(FunctionType.Administration.Category.UpdateItem);
            param.Category = BindFormToObject();
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specifix

        public void BindObjectToForm(Category item)
        {
            txtName.Text = item.CategoryName;

            hidId.Value = Utility.GetString(item.CategoryID);
            hidVersion.Value = Utility.GetString(item.Version);
        }

        public Category BindFormToObject()
        {
            Category item = new Category();

            item.CategoryID = Utility.GetNullableInt(hidId.Value);
            item.Version = Utility.GetNullableInt(hidVersion.Value);
            item.CategoryName = txtName.Text.Trim();

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