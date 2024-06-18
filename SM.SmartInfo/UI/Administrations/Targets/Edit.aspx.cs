
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
    public partial class Edit : BasePage, ISMFormEdit<Target>
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
            //bind radcombobox control
            UIUtility.BindListToDropDownList(ddTargetType, SMX.TargetType.dctTargetTypes.ToList(), false);
            //UIUtility.BindListToDropDownList(ddTransformType, SMX.TransformType.dctName.ToList(), false);
            //UIUtility.BindListToDropDownList(ddStatus, SMX.Status.dctStatus.ToList(), false);
            //UIUtility.BindDicToDropDownList<int?, string>(ddlTriggerType, SMX.TriggerType.dicDes, true);
        }

        public void LoadData()
        {
            TargetParam param = new TargetParam(FunctionType.Administration.Target.LoadDataEdit);
            param.TargetID = base.GetIntIdParam();
            MainController.Provider.Execute(param);

            BindObjectToForm(param.Target);
        }

        public void UpdateItem()
        {
            TargetParam param = new TargetParam(FunctionType.Administration.Target.UpdateItem);
            param.Target = BindFormToObject();
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specifix

        public void BindObjectToForm(Target item)
        {
            lblCode.Text = item.TargetCode;
            txtName.Text = item.Name;
            ddTargetType.SelectedValue = Utils.Utility.GetString(item.TargetType);

            txtDescription.Text = item.Description;

            hidId.Value = Utility.GetString(item.TargetID);
            hidVersion.Value = Utility.GetString(item.Version);
        }

        public Target BindFormToObject()
        {
            Target item = new Target();

            item.TargetID = Utility.GetNullableInt(hidId.Value);
            item.Version = Utility.GetNullableInt(hidVersion.Value);
            item.TargetCode = lblCode.Text;
            item.Name = txtName.Text.Trim();
            item.TargetType = Utils.Utility.GetNullableInt(ddTargetType.SelectedValue);
            item.Description = txtDescription.Text.Trim();

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