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

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class PressAgencyHRRelativesUC : UserControl
    {
        public event EventHandler RequestSave_PressAgencyHR;

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetDataFromPopup();

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgencyHRRelatives);
                param.PressAgencyHRRelatives = item;
                MainController.Provider.Execute(param);

                popEdit.Hide();
                BindData(Utility.GetNullableInt(hidPressAgencyHRID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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
                if (RequestSave_PressAgencyHR != null && (string.IsNullOrWhiteSpace(hidPressAgencyHRID.Value) || Utility.GetNullableInt(hidPressAgencyHRID.Value) == 0))
                {
                    RequestSave_PressAgencyHR(null, null);
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
                        BindData(Utility.GetNullableInt(hidPressAgencyHRID.Value), Utility.GetNullableBool(hidIsEdit.Value));
                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void BindData(int? pressAgencyHRID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyHRID.Value = Utility.GetString(pressAgencyHRID);
            btnAddNew.Visible = isEdit.GetValueOrDefault(false);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHRRelatives_ByPressAgencyHRID);
            param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = pressAgencyHRID };
            MainController.Provider.Execute(param);

            rptHistory.DataSource = param.ListPressAgencyHRRelatives;
            rptHistory.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidPressAgencyHRRelativesID.Value = txtRelationship.Text = txtFullName.Text = txtOtherNote.Text = string.Empty;
            dpkDOB.SelectedDate = null;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyHRRelatives item = rptItem.DataItem as agency_PressAgencyHRRelatives;

            HiddenField hidPressAgencyHRRelativesID = rptItem.FindControl("hidPressAgencyHRRelativesID") as HiddenField;
            hidPressAgencyHRRelativesID.Value = Utility.GetString(item.PressAgencyHRRelativesID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrRelationship", item.Relationship);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrFullName", item.FullName);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAge", item.DOB.HasValue ? Utility.GetString(DateTime.Now.Year - item.DOB.Value.Year) : string.Empty);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrDOB", Utility.GetDateString(item.DOB));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrOtherNote", item.OtherNote);

            DatePicker dpkDOB = rptItem.FindControl("dpkDOB") as DatePicker;
            dpkDOB.SelectedDate = item.DOB;

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;

            btnEdit.Visible = btnDelete.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);
        }

        private agency_PressAgencyHRRelatives GetCurrentRowData(RepeaterItem rptItem)
        {
            agency_PressAgencyHRRelatives result = new agency_PressAgencyHRRelatives();

            HiddenField hidPressAgencyHRRelativesID = rptItem.FindControl("hidPressAgencyHRRelativesID") as HiddenField;
            Literal ltrRelationship = rptItem.FindControl("ltrRelationship") as Literal;
            Literal ltrFullName = rptItem.FindControl("ltrFullName") as Literal;
            Literal ltrOtherNote = rptItem.FindControl("ltrOtherNote") as Literal;
            DatePicker dpkDOB = rptItem.FindControl("dpkDOB") as DatePicker;

            result.PressAgencyHRRelativesID = Utility.GetNullableInt(hidPressAgencyHRRelativesID.Value);
            result.Relationship = ltrRelationship.Text;
            result.FullName = ltrFullName.Text;
            result.OtherNote = ltrOtherNote.Text;
            result.DOB = dpkDOB.SelectedDate;

            return result;
        }

        private void BindObjectToPopup(agency_PressAgencyHRRelatives item)
        {
            hidPressAgencyHRRelativesID.Value = Utility.GetString(item.PressAgencyHRRelativesID);
            txtRelationship.Text = item.Relationship;
            txtFullName.Text = item.FullName;
            txtOtherNote.Text = item.OtherNote;
            dpkDOB.SelectedDate = item.DOB;
        }

        private agency_PressAgencyHRRelatives GetDataFromPopup()
        {
            agency_PressAgencyHRRelatives result = new agency_PressAgencyHRRelatives();

            result.PressAgencyHRRelativesID = Utility.GetNullableInt(hidPressAgencyHRRelativesID.Value);
            result.PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value);
            result.Relationship = txtRelationship.Text;
            result.FullName = txtFullName.Text;
            result.OtherNote = txtOtherNote.Text;
            result.DOB = dpkDOB.SelectedDate;

            return result;
        }

        private void DeleteItem(agency_PressAgencyHRRelatives item)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyHRRelatives);
            param.PressAgencyHRRelatives = item;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}