using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
{
    public partial class RelationshipWithMBUC : UserControl
    {
        public event EventHandler RequestSave_PressAgency;

        #region Events

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) - 1);
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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
                var item = GetDataFromPopup();

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SaveRelationshipWithMB);
                param.RelationshipWithMB = item;
                MainController.Provider.Execute(param);

                popEdit.Hide();
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestSave_PressAgency != null && string.IsNullOrWhiteSpace(hidPressAgencyID.Value))
                {
                    RequestSave_PressAgency(null, null);
                    return;
                }

                ClearPopup();
                popEdit.Show();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void popEdit_PopupClosed(object sender, EventArgs e)
        {
            popEdit.Hide();
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeater(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                var item = GetCurrentRowData(e.Item);

                switch (e.CommandName)
                {
                    case SMX.ActionEdit:
                        BindObjectToPopup(item);
                        popEdit.Show();
                        break;
                    case SMX.ActionDelete:
                        DeleteItem(item);
                        BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ucPager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPage.Value = e.NewPageIndex.ToString();
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            hidPage.Value = "1";
            UIUtility.BindDicToDropDownList(ddlRelationship, SMX.RelationshipWithMB.dicDesc);
        }

        public void BindData(int? pressAgencyID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyID.Value = Utility.GetString(pressAgencyID);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thEdit.Visible = btnAddNew.Visible = footerEdit.Visible = isEdit.GetValueOrDefault(false);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListRelationshipWithMB_ByPressAgencyID);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniTen
            };
            param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptData.DataSource = param.ListRelationshipWithMB;
            rptData.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidRelationshipWithMBID.Value = ddlRelationship.SelectedValue = string.Empty;
            dpkFromDTG.SelectedDate = dpkToDTG.SelectedDate = null;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_RelationshipWithMB item = rptItem.DataItem as agency_RelationshipWithMB;

            HiddenField hidRelationshipWithMBID = rptItem.FindControl("hidRelationshipWithMBID") as HiddenField;
            hidRelationshipWithMBID.Value = Utility.GetString(item.RelationshipWithMBID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrFromDTG", Utility.GetDateString(item.FromDTG));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrToDTG", Utility.GetDateString(item.ToDTG));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrRelationship", Utility.GetDictionaryValue(SMX.RelationshipWithMB.dicDesc, item.Relationship));

            DatePicker dpkFromDTG = rptItem.FindControl("dpkFromDTG") as DatePicker;
            DatePicker dpkToDTG = rptItem.FindControl("dpkToDTG") as DatePicker;
            HiddenField hidRelationship = rptItem.FindControl("hidRelationship") as HiddenField;

            hidRelationship.Value = Utility.GetString(item.Relationship);
            dpkFromDTG.SelectedDate = item.FromDTG;
            dpkToDTG.SelectedDate = item.ToDTG;

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private agency_RelationshipWithMB GetCurrentRowData(RepeaterItem rptItem)
        {
            agency_RelationshipWithMB result = new agency_RelationshipWithMB();

            HiddenField hidRelationshipWithMBID = rptItem.FindControl("hidRelationshipWithMBID") as HiddenField;
            DatePicker dpkFromDTG = rptItem.FindControl("dpkFromDTG") as DatePicker;
            DatePicker dpkToDTG = rptItem.FindControl("dpkToDTG") as DatePicker;
            HiddenField hidRelationship = rptItem.FindControl("hidRelationship") as HiddenField;

            result.RelationshipWithMBID = Utility.GetNullableInt(hidRelationshipWithMBID.Value);
            result.FromDTG = dpkFromDTG.SelectedDate;
            result.ToDTG = dpkToDTG.SelectedDate;
            result.Relationship = Utility.GetNullableInt(hidRelationship.Value);

            return result;
        }

        private void BindObjectToPopup(agency_RelationshipWithMB item)
        {
            hidRelationshipWithMBID.Value = Utility.GetString(item.RelationshipWithMBID);
            dpkFromDTG.SelectedDate = item.FromDTG;
            dpkToDTG.SelectedDate = item.ToDTG;
            ddlRelationship.SelectedValue = Utility.GetString(item.Relationship);
        }

        private agency_RelationshipWithMB GetDataFromPopup()
        {
            agency_RelationshipWithMB result = new agency_RelationshipWithMB();

            result.RelationshipWithMBID = Utility.GetNullableInt(hidRelationshipWithMBID.Value);
            result.PressAgencyID = Utility.GetNullableInt(hidPressAgencyID.Value);
            result.FromDTG = dpkFromDTG.SelectedDate;
            result.ToDTG = dpkToDTG.SelectedDate;
            result.Relationship = Utility.GetNullableInt(ddlRelationship.SelectedValue);

            return result;
        }

        private void DeleteItem(agency_RelationshipWithMB item)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeleteRelationshipWithMB);
            param.RelationshipWithMB = item;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}