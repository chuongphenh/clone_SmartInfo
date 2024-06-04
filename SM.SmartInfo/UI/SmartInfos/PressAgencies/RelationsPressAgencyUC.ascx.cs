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
    public partial class RelationsPressAgencyUC : UserControl
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

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SaveRelationsPressAgency);
                param.RelationsPressAgency = item;
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
        }

        public void BindData(int? pressAgencyID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyID.Value = Utility.GetString(pressAgencyID);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thEdit.Visible = btnAddNew.Visible = footerEdit.Visible = isEdit.GetValueOrDefault(false);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListRelationsPressAgency_ByPressAgencyID);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniTen
            };
            param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptHistory.DataSource = param.ListRelationsPressAgency;
            rptHistory.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidRelationsPressAgencyID.Value = txtName.Text = txtRelationship.Text = txtNote.Text = string.Empty;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_RelationsPressAgency item = rptItem.DataItem as agency_RelationsPressAgency;

            HiddenField hidRelationsPressAgencyID = rptItem.FindControl("hidRelationsPressAgencyID") as HiddenField;
            hidRelationsPressAgencyID.Value = Utility.GetString(item.RelationsPressAgencyID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrName", item.Name);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrRelationship", item.Relationship);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrNote", item.Note);

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private agency_RelationsPressAgency GetCurrentRowData(RepeaterItem rptItem)
        {
            agency_RelationsPressAgency result = new agency_RelationsPressAgency();

            HiddenField hidRelationsPressAgencyID = rptItem.FindControl("hidRelationsPressAgencyID") as HiddenField;
            Literal ltrName = rptItem.FindControl("ltrName") as Literal;
            Literal ltrRelationship = rptItem.FindControl("ltrRelationship") as Literal;
            Literal ltrNote = rptItem.FindControl("ltrNote") as Literal;

            result.RelationsPressAgencyID = Utility.GetNullableInt(hidRelationsPressAgencyID.Value);
            result.Name = ltrName.Text;
            result.Relationship = ltrRelationship.Text;
            result.Note = ltrNote.Text;

            return result;
        }

        private void BindObjectToPopup(agency_RelationsPressAgency item)
        {
            hidRelationsPressAgencyID.Value = Utility.GetString(item.RelationsPressAgencyID);
            txtName.Text = item.Name;
            txtNote.Text = item.Note;
            txtRelationship.Text = item.Relationship;
        }

        private agency_RelationsPressAgency GetDataFromPopup()
        {
            agency_RelationsPressAgency result = new agency_RelationsPressAgency();

            result.RelationsPressAgencyID = Utility.GetNullableInt(hidRelationsPressAgencyID.Value);
            result.PressAgencyID = Utility.GetNullableInt(hidPressAgencyID.Value);
            result.Name = txtName.Text;
            result.Note = txtNote.Text;
            result.Relationship = txtRelationship.Text;

            return result;
        }

        private void DeleteItem(agency_RelationsPressAgency item)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeleteRelationsPressAgency);
            param.RelationsPressAgency = item;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}