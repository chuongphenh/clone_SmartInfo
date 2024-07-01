using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.UIControls;
using SM.SmartInfo.Utils;
using System.Linq;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class TargetSelectorUC : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;

        #region Public Properties

        //public EmployeeSelectorMode Mode
        //{
        //    get { return string.IsNullOrEmpty(hidMode.Value) ? EmployeeSelectorMode.All : (EmployeeSelectorMode)Enum.Parse(typeof(EmployeeSelectorMode), hidMode.Value); }
        //    set { hidMode.Value = value.ToString(); }
        //}

        public string ComboboxClientID
        {
            get { return rcbTarget.ClientID; }
        }

        public string HidOrganizationID
        {
            get { return hidOrganizationID.Value; }
            set { hidOrganizationID.Value = value; }
        }

        public Unit Width
        {
            get { return rcbTarget.Width; }
            set { rcbTarget.Width = value; }
        }

        public string SelectedValue
        {
            get { return rcbTarget.SelectedValue; }
            set { rcbTarget.SelectedValue = value; }
        }

        public string Text
        {
            get { return rcbTarget.Text; }
            set { rcbTarget.Text = value; }
        }

        public string DataValueField
        {
            get { return rcbTarget.DataValueField; }
            set { rcbTarget.DataValueField = value; }
        }

        public string DataTextField
        {
            get { return rcbTarget.DataTextField; }
            set { rcbTarget.DataTextField = value; }
        }

        public bool AutoPostBack
        {
            get { return rcbTarget.AutoPostBack; }
            set { rcbTarget.AutoPostBack = value; }
        }

        //public int? OrganizationType
        //{
        //    get { return Utility.GetNullableInt(hidOrganizationType.Value); }
        //    set { hidOrganizationType.Value = value.ToString(); }
        //}

        /// <summary>
        /// List of RoleID separated by ','
        /// </summary>
        //public List<int> RoleIDs
        //{
        //    get
        //    {
        //        return hidRoleIDs.Value.Split(',')
        //            .Select(en => Utility.GetNullableInt(en))
        //            .Where(en => en.HasValue)
        //            .Select(en => en.Value).ToList();
        //    }
        //    set
        //    {
        //        hidRoleIDs.Value = string.Join(",", value);
        //    }
        //}

        #endregion

        #region Event

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        protected void rcbTarget_ItemDataBound(object sender, SoftMart.Core.UIControls.SearchBoxItemEventArgs e)
        {
            Target enTarget = (Target)e.Item.DataItem;

            Literal ltrTargetCode = (Literal)e.Item.FindControl("ltrTargetCode");
            ltrTargetCode.Text = enTarget.TargetCode;

            Literal ltrTargetName = (Literal)e.Item.FindControl("ltrTargetName");
            ltrTargetName.Text = enTarget.TargetName;
        }

        protected void rcbTarget_TextChanged(object sender, string newText)
        {
            CommonParam param = new CommonParam(FunctionType.Common.TargetSelectorSearch);
            param.TargetName = Utility.NullIfEmptyString(newText);


            MainController.Provider.Execute(param);

            BindData(param.Targets.Take(30).ToList());
        }

        protected void rcbTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(sender, e);
            }
        }

        #endregion

        #region Public methods

        public void SetSelectedItem(int? selectedValue, string text)
        {
            //Luon phai set cung luc. Neu ko, khi select lan thu 2 se ko co value.
            this.SelectedValue = Utility.GetString(selectedValue);
            this.Text = text;
        }

        public void BindData(Target Target)
        {
            List<Target> lst = new List<Target>() { Target };
            BindData(lst);
        }

        public void BindData(List<Target> lstTarget)
        {
            rcbTarget.DataSource = lstTarget;
            rcbTarget.DataBind();
        }

        #endregion
    }
}