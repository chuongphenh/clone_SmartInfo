using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.Web.UI.HtmlControls;
using SoftMart.Core.UIControls;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
{
    public partial class PressAgencyHistoryUC : UserControl
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

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgencyHistory);
                param.PressAgencyHistory = item;
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

        protected void rptHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                        break;
                }

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
        }

        public void BindData(int? pressAgencyID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyID.Value = Utility.GetString(pressAgencyID);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thEdit.Visible = btnAddNew.Visible = footerEdit.Visible = isEdit.GetValueOrDefault(false);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHistory_ByPressAgencyID);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniTen
            };
            param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptHistory.DataSource = param.ListPressAgencyHistory;
            rptHistory.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidPressAgencyHistoryID.Value = txtNewEmployee.Text = txtOldEmployee.Text = txtPositionChange.Text = string.Empty;
            dpkChangeDTG.SelectedDate = null;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyHistory item = rptItem.DataItem as agency_PressAgencyHistory;

            HiddenField hidPressAgencyHistoryID = rptItem.FindControl("hidPressAgencyHistoryID") as HiddenField;
            hidPressAgencyHistoryID.Value = Utility.GetString(item.PressAgencyHistoryID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrPositionChange", item.PositionChange);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrOldEmployee", item.OldEmployee);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrNewEmployee", item.NewEmployee);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrChangeDTG", Utility.GetDateString(item.ChangeDTG));

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            DatePicker dpkChangeDTG = rptItem.FindControl("dpkChangeDTG") as DatePicker;
            dpkChangeDTG.SelectedDate = item.ChangeDTG;

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private agency_PressAgencyHistory GetCurrentRowData(RepeaterItem rptItem)
        {
            agency_PressAgencyHistory result = new agency_PressAgencyHistory();

            HiddenField hidPressAgencyHistoryID = rptItem.FindControl("hidPressAgencyHistoryID") as HiddenField;
            Literal ltrPositionChange = rptItem.FindControl("ltrPositionChange") as Literal;
            Literal ltrOldEmployee = rptItem.FindControl("ltrOldEmployee") as Literal;
            Literal ltrNewEmployee = rptItem.FindControl("ltrNewEmployee") as Literal;
            DatePicker dpkChangeDTG = rptItem.FindControl("dpkChangeDTG") as DatePicker;

            result.PressAgencyHistoryID = Utility.GetNullableInt(hidPressAgencyHistoryID.Value);
            result.PositionChange = ltrPositionChange.Text;
            result.OldEmployee = ltrOldEmployee.Text;
            result.NewEmployee = ltrNewEmployee.Text;
            result.ChangeDTG = dpkChangeDTG.SelectedDate;

            return result;
        }

        private void BindObjectToPopup(agency_PressAgencyHistory item)
        {
            hidPressAgencyHistoryID.Value = Utility.GetString(item.PressAgencyHistoryID);
            txtPositionChange.Text = item.PositionChange;
            txtOldEmployee.Text = item.OldEmployee;
            txtNewEmployee.Text = item.NewEmployee;
            dpkChangeDTG.SelectedDate = item.ChangeDTG;
        }

        private agency_PressAgencyHistory GetDataFromPopup()
        {
            agency_PressAgencyHistory result = new agency_PressAgencyHistory();

            result.PressAgencyHistoryID = Utility.GetNullableInt(hidPressAgencyHistoryID.Value);
            result.PressAgencyID = Utility.GetNullableInt(hidPressAgencyID.Value);
            result.PositionChange = txtPositionChange.Text;
            result.OldEmployee = txtOldEmployee.Text;
            result.NewEmployee = txtNewEmployee.Text;
            result.ChangeDTG = dpkChangeDTG.SelectedDate;

            return result;
        }

        private void DeleteItem(agency_PressAgencyHistory item)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyHistory);
            param.PressAgencyHistory = item;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}