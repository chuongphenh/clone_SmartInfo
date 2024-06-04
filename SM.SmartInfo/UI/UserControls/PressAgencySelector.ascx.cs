using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Common;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class PressAgencySelector : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;

        #region Public Properties


        public string ComboboxClientID
        {
            get { return rcbPressAgency.ClientID; }
        }

        public Unit Width
        {
            get { return rcbPressAgency.Width; }
            set { rcbPressAgency.Width = value; }
        }

        public string SelectedValue
        {
            get { return rcbPressAgency.SelectedValue; }
            set { rcbPressAgency.SelectedValue = value; }
        }

        public string Text
        {
            get { return rcbPressAgency.Text; }
            set { rcbPressAgency.Text = value; }
        }

        public string DataValueField
        {
            get { return rcbPressAgency.DataValueField; }
            set { rcbPressAgency.DataValueField = value; }
        }

        public string DataTextField
        {
            get { return rcbPressAgency.DataTextField; }
            set { rcbPressAgency.DataTextField = value; }
        }

        public bool AutoPostBack
        {
            get { return rcbPressAgency.AutoPostBack; }
            set { rcbPressAgency.AutoPostBack = value; }
        }

        #endregion


        #region Event


        protected void rcbPressAgency_ItemDataBound(object sender, SoftMart.Core.UIControls.SearchBoxItemEventArgs e)
        {
            agency_PressAgency enPressAgency = (agency_PressAgency)e.Item.DataItem;

            Literal ltrName = (Literal)e.Item.FindControl("ltrName");
            ltrName.Text = enPressAgency.Name;
        }

        protected void rcbPressAgency_TextChanged(object sender, string newText)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencySelector);
            param.Name = Utility.NullIfEmptyString(newText);

            MainController.Provider.Execute(param);

            BindData(param.ListPressAgency.ToList());
        }

        protected void rcbPressAgency_SelectedIndexChanged(object sender, EventArgs e)
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

        public void BindData(agency_PressAgency pressAgency)
        {
            List<agency_PressAgency> lst = new List<agency_PressAgency>() { pressAgency };
            BindData(lst);
        }

        public void BindData(List<agency_PressAgency> lstpressAgency)
        {
            rcbPressAgency.DataSource = lstpressAgency;
            rcbPressAgency.DataBind();
        }

        #endregion

        
    }
}