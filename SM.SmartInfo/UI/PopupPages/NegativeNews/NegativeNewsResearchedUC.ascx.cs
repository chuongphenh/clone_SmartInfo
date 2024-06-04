using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.PopupPages.NegativeNews
{
    public partial class NegativeNewsResearchedUC : BaseUserControl
    {
        public event EventHandler RequestSave_NegativeNews;

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetDataFromPopup();

                NewsParam param = new NewsParam(FunctionType.NegativeNewsResearched.SaveNegativeNewsResearched);
                param.NegativeNewsResearched = item;
                MainController.Provider.Execute(param);

                popEdit.Hide();
                BindData(Utility.GetNullableInt(hidID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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
                if (RequestSave_NegativeNews != null && (string.IsNullOrWhiteSpace(hidID.Value) || Utility.GetNullableInt(hidID.Value) == 0))
                {
                    RequestSave_NegativeNews(null, null);
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

        private void ClearPopup()
        {
            dtpCreatedDTG.SelectedDate = null;
            hidNegativeNewsResearchedID.Value = txtObjectContact.Text = txtResult.Text = txtTypeOfContact.Text = txtContent.Text = string.Empty;
        }

        protected void rptNegativeNewsResearched_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptNegativeNewsResearched_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                        BindData(Utility.GetNullableInt(hidID.Value), Utility.GetNullableBool(hidIsEdit.Value));

                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Methods

        public void BindData(int? negativeNewsID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidID.Value = Utility.GetString(negativeNewsID);
            thEdit.Visible = btnAddNew.Visible = isEdit.GetValueOrDefault(false);

            NewsParam param = new NewsParam(FunctionType.NegativeNewsResearched.GetListNegativeNewsResearchedByNegativeNewsID);
            param.NegativeNewsID = negativeNewsID;
            MainController.Provider.Execute(param);

            rptNegativeNewsResearched.DataSource = param.ListNegativeNewsResearched;
            rptNegativeNewsResearched.DataBind();
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            NegativeNewsResearched item = rptItem.DataItem as NegativeNewsResearched;

            HiddenField hidNegativeNewsResearchedID = rptItem.FindControl("hidNegativeNewsResearchedID") as HiddenField;
            hidNegativeNewsResearchedID.Value = Utility.GetString(item.NegativeNewsResearchedID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrTypeOfContact", item.TypeOfContact);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrObjectContact", item.ObjectContact);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrContent", item.Content);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrResult", item.Result);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrCreatedDTG", Utility.GetDateString(item.CreatedDTG));

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            DatePicker dpkCreatedDTG = rptItem.FindControl("dpkCreatedDTG") as DatePicker;
            dpkCreatedDTG.SelectedDate = item.CreatedDTG;

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private NegativeNewsResearched GetCurrentRowData(RepeaterItem rptItem)
        {
            NegativeNewsResearched result = new NegativeNewsResearched();

            HiddenField hidNegativeNewsResearchedID = rptItem.FindControl("hidNegativeNewsResearchedID") as HiddenField;
            Literal ltrTypeOfContact = rptItem.FindControl("ltrTypeOfContact") as Literal;
            Literal ltrObjectContact = rptItem.FindControl("ltrObjectContact") as Literal;
            Literal ltrContent = rptItem.FindControl("ltrContent") as Literal;
            Literal ltrResult = rptItem.FindControl("ltrResult") as Literal;
            DatePicker dpkCreatedDTG = rptItem.FindControl("dpkCreatedDTG") as DatePicker;

            result.NegativeNewsResearchedID = Utility.GetNullableInt(hidNegativeNewsResearchedID.Value);
            result.TypeOfContact = ltrTypeOfContact.Text;
            result.ObjectContact = ltrObjectContact.Text;
            result.Content = ltrContent.Text;
            result.Result = ltrResult.Text;
            result.CreatedDTG = dpkCreatedDTG.SelectedDate;

            return result;
        }

        private void BindObjectToPopup(NegativeNewsResearched item)
        {
            hidNegativeNewsResearchedID.Value = Utility.GetString(item.NegativeNewsResearchedID);
            txtTypeOfContact.Text = item.TypeOfContact;
            txtContent.Text = item.Content;
            dtpCreatedDTG.SelectedDate = item.CreatedDTG;
            txtObjectContact.Text = item.ObjectContact;
            txtResult.Text = item.Result;

        }

        private NegativeNewsResearched GetDataFromPopup()
        {
            NegativeNewsResearched result = new NegativeNewsResearched();

            result.NegativeNewsResearchedID = Utility.GetNullableInt(hidNegativeNewsResearchedID.Value);
            result.NegativeNewsID = Utility.GetNullableInt(hidID.Value);
            result.ObjectContact = txtObjectContact.Text;
            result.TypeOfContact = txtTypeOfContact.Text;
            result.Content = txtContent.Text;
            result.Result = txtResult.Text;
            result.CreatedDTG = dtpCreatedDTG.SelectedDate;


            return result;
        }

        private void DeleteItem(NegativeNewsResearched item)
        {
            NewsParam param = new NewsParam(FunctionType.NegativeNewsResearched.DeleteNegativeNewsResearched);
            param.NegativeNewsResearchedID = item.NegativeNewsResearchedID;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}