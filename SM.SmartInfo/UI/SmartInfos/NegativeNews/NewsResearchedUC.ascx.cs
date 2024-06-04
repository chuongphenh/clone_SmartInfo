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

namespace SM.SmartInfo.UI.SmartInfos.NegativeNews
{
    public partial class NewsResearchedUC : BaseUserControl
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

                NewsParam param = new NewsParam(FunctionType.NewsResearched.SaveNewsResearched);
                param.NewsResearched = item;
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

        protected void rptNewsResearched_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptNewsResearched_ItemCommand(object source, RepeaterCommandEventArgs e)
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

        private void ClearPopup()
        {
            hidNewsResearchedID.Value = txtObjectContact.Text = txtResult.Text = txtTypeOfContact.Text = txtContent.Text = string.Empty;
            dtpCreatedDTG.SelectedDate = null;
        }

        public void BindData(int? newsID, bool? isEdit)
        {
            hidID.Value = Utility.GetString(newsID);
            hidIsEdit.Value = Utility.GetString(isEdit);
            thEdit.Visible = btnAddNew.Visible = isEdit.GetValueOrDefault(false);

            NewsParam param = new NewsParam(FunctionType.NewsResearched.GetListNewsResearchedByNewsID);
            param.NewsID = newsID;
            MainController.Provider.Execute(param);

            rptNewsResearched.DataSource = param.ListNewsResearched;
            rptNewsResearched.DataBind();
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            NewsResearched item = rptItem.DataItem as NewsResearched;

            HiddenField hidNewsResearchedID = rptItem.FindControl("hidNewsResearchedID") as HiddenField;
            hidNewsResearchedID.Value = Utility.GetString(item.NewsResearchedID);

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

        private NewsResearched GetCurrentRowData(RepeaterItem rptItem)
        {
            NewsResearched result = new NewsResearched();

            HiddenField hidNewsResearchedID = rptItem.FindControl("hidNewsResearchedID") as HiddenField;
            Literal ltrTypeOfContact = rptItem.FindControl("ltrTypeOfContact") as Literal;
            Literal ltrObjectContact = rptItem.FindControl("ltrObjectContact") as Literal;
            Literal ltrContent = rptItem.FindControl("ltrContent") as Literal;
            Literal ltrResult = rptItem.FindControl("ltrResult") as Literal;
            DatePicker dpkCreatedDTG = rptItem.FindControl("dpkCreatedDTG") as DatePicker;

            result.NewsResearchedID = Utility.GetNullableInt(hidNewsResearchedID.Value);
            result.TypeOfContact = ltrTypeOfContact.Text;
            result.ObjectContact = ltrObjectContact.Text;
            result.Content = ltrContent.Text;
            result.Result = ltrResult.Text;
            result.CreatedDTG = dpkCreatedDTG.SelectedDate;

            return result;
        }

        private void BindObjectToPopup(NewsResearched item)
        {
            hidNewsResearchedID.Value = Utility.GetString(item.NewsResearchedID);
            txtTypeOfContact.Text = item.TypeOfContact;
            txtContent.Text = item.Content;
            dtpCreatedDTG.SelectedDate = item.CreatedDTG;
            txtObjectContact.Text = item.ObjectContact;
            txtResult.Text = item.Result;

        }

        private NewsResearched GetDataFromPopup()
        {
            NewsResearched result = new NewsResearched();

            result.NewsResearchedID = Utility.GetNullableInt(hidNewsResearchedID.Value);
            result.NewsID = Utility.GetNullableInt(hidID.Value);
            result.ObjectContact = txtObjectContact.Text;
            result.TypeOfContact = txtTypeOfContact.Text;
            result.Content = txtContent.Text;
            result.Result = txtResult.Text;
            result.CreatedDTG = dtpCreatedDTG.SelectedDate;

            return result;
        }

        private void DeleteItem(NewsResearched item)
        {
            NewsParam param = new NewsParam(FunctionType.NewsResearched.DeleteNewsResearched);
            param.NewsResearchedID = item.NewsResearchedID;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}