using System;
using SM.SmartInfo.BIZ;
using System.Web.UI.WebControls;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.Linq;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class CommentUC : BaseUserControl
    {
        private int? _CommentID { get { return (int?)ViewState["_CommentID"]; } set { ViewState["_CommentID"] = value; } }
        private int? _CommentedByID { get { return (int?)ViewState["_CommentedByID"]; } set { ViewState["_CommentedByID"] = value; } }
        private int? _Version { get { return (int?)ViewState["_Version"]; } set { ViewState["_Version"] = value; } }

        #region Events

        protected void btnComment_Click(object sender, EventArgs e)
        {
            try
            {
                var cm = GetComment();
                if (cm.CommentID.HasValue && cm.CommentedByID != Profiles.MyProfile.EmployeeID)
                    throw new SMXException("Không được quyền cập nhật comment của người khác.");
                CommentParam param = new CommentParam(FunctionType.Comment.InsertUpdateCommentByRefIDAndRefType);
                param.Comment = cm;
                MainController.Provider.Execute(param);
                PushNotiComment(cm);
                LoadData(Utils.Utility.GetNullableInt(hidRefID.Value), Utils.Utility.GetNullableInt(hidRefType.Value), Utils.Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        private void PushNotiComment(Comment itemComment)
        {
            ntf_Notification itemNoti = new ntf_Notification();
            NotificationParam pushNotiPram = new NotificationParam(FunctionType.Notification.PushNotification);
            itemNoti.NotificationID = itemComment?.RefID ?? 0;
            itemNoti.Content = itemComment.Content;
            itemNoti.Type = (int)itemComment.RefType;
            pushNotiPram.Notification = itemNoti;
            pushNotiPram.TypeNoti = "Comment";
            MainController.Provider.Execute(pushNotiPram);
        }
        protected void rptComment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObject2RepeaterItem(e.Item);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void rptComment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionEdit:
                        {
                            var commentID = Utils.Utility.GetNullableInt((e.Item.FindControl("hidCMID") as HiddenField).Value);
                            var commentedByID = Utils.Utility.GetNullableInt((e.Item.FindControl("hidCommentedByID") as HiddenField).Value);
                            if (commentID.HasValue && Profiles.MyProfile.EmployeeID != commentedByID)
                                throw new SMXException("Không được quyền cập nhật comment của người khác.");
                            BindObject2Footer(e.Item);
                        }
                        break;
                    case SMX.ActionDelete:
                        {
                            var commentID = Utils.Utility.GetNullableInt((e.Item.FindControl("hidCMID") as HiddenField).Value);
                            var commentedByID = Utils.Utility.GetNullableInt((e.Item.FindControl("hidCommentedByID") as HiddenField).Value);
                            var version = Utils.Utility.GetNullableInt((e.Item.FindControl("hidVersion") as HiddenField).Value);
                            var rate = Utils.Utility.GetNullableInt((e.Item.FindControl("hidRate") as HiddenField).Value);
                            if (Profiles.MyProfile.EmployeeID != commentedByID)
                                throw new SMXException("Không được quyền xóa comment của người khác.");
                            CommentParam param = new CommentParam(FunctionType.Comment.DeleteCommentByID);
                            param.CommentID = commentID;
                            param.Comment = new Comment()
                            {
                                CommentID = commentID,
                                Version = version,
                                CommentedByID = commentedByID,
                                Rate = rate
                            };
                            MainController.Provider.Execute(param);

                            LoadData(Utils.Utility.GetNullableInt(hidRefID.Value), Utils.Utility.GetNullableInt(hidRefType.Value), Utils.Utility.GetNullableBool(hidIsEdit.Value));
                            break;
                        }
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            UIUtility.BindDicToDropDownList(ddlRate, SMX.Rate.dicDesc, false);
            ddlRate.SelectedValue = Utils.Utility.GetString(SMX.Rate.BinhThuong);

            this._CommentID = null;
            this._Version = null;
            this._CommentedByID = null;
            txtCommentContent.Text = string.Empty;
           

        }
        public void LoadData(int? refID, int? refType, bool? isEdit = false, int? typeNoti = 1)
        {
            hidRefID.Value = Utils.Utility.GetString(refID);
            hidIsEdit.Value = Utils.Utility.GetString(isEdit);
            hidRefType.Value = Utils.Utility.GetString(refType);
            
            CommentParam param = new CommentParam(FunctionType.Comment.GetAllCommentByRefIDAndRefType);
            param.RefID = refID;
            param.RefType = refType;
            param.TypeNoti = typeNoti;
            MainController.Provider.Execute(param);

            rptComment.DataSource = param.ListComment;
            rptComment.DataBind();

            trComment.Visible = isEdit.GetValueOrDefault(false);
            this._CommentID = null;
            this._Version = null;
            this._CommentedByID = null;
            txtCommentContent.Text = string.Empty;
            ddlRate.SelectedValue = Utils.Utility.GetString(SMX.Rate.BinhThuong);
        }

        #endregion

        #region Private Methods

        private Comment GetComment()
        {
            Comment result = new Comment();
            int Type = 1;
            //if (!string.IsNullOrEmpty(Request.Params["T"]))
            //{
            //    Type = int.Parse(Request.Params["T"]);
            //}
            int typeValue;
            if (!string.IsNullOrEmpty(Request.QueryString["T"]) && int.TryParse(Request.QueryString["T"], out typeValue))
            {
                Type = typeValue;
            }


            result.CommentID = this._CommentID;
            result.RefID = Utils.Utility.GetNullableInt(hidRefID.Value);
            result.RefType = Utils.Utility.GetNullableInt(hidRefType.Value);
            result.Content = txtCommentContent.Text;
            result.Rate = Utils.Utility.GetNullableInt(ddlRate.SelectedValue);
            result.TypeNoti = Type;
            
            var commentByID = this._CommentedByID;
            if (commentByID.HasValue)
                result.CommentedByID = commentByID;
            else
                result.CommentedByID = Profiles.MyProfile.EmployeeID;
            result.CommentedByName = Profiles.MyProfile.FullName;
            result.CommentedByUserName = Profiles.MyProfile.UserName;
            result.Version = this._Version;

            return result;
        }

        private void BindObject2RepeaterItem(RepeaterItem rptItem)
        {
            Comment item = rptItem.DataItem as Comment;

            UIUtility.SetRepeaterItemIText(rptItem, "ltrName", item.CommentedByName);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrContent", UIUtility.ConvertBreakLine2Html(item.Content));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrCommentedDTG", Utils.Utility.GetDateTimeString(item.CommentedDTG, "HH:mm - dd/MM/yyyy"));

            HiddenField hidCMID = rptItem.FindControl("hidCMID") as HiddenField;
            hidCMID.Value = Utils.Utility.GetString(item.CommentID);

            HiddenField hidRate = rptItem.FindControl("hidRate") as HiddenField;
            hidRate.Value = Utils.Utility.GetString(item.Rate);

            HiddenField hidVersion = rptItem.FindControl("hidVersion") as HiddenField;
            hidVersion.Value = Utils.Utility.GetString(item.Version);

            HiddenField hidCommentedByID = rptItem.FindControl("hidCommentedByID") as HiddenField;
            hidCommentedByID.Value = Utils.Utility.GetString(item.CommentedByID);


            HiddenField hidTypeNoti = rptItem.FindControl("hidTypeNoti") as HiddenField;
            hidTypeNoti.Value = Utils.Utility.GetString(item.TypeNoti);

            HtmlGenericControl divComment = rptItem.FindControl("divComment") as HtmlGenericControl;
            switch (item.Rate)
            {
                case SMX.Rate.BinhThuong:
                    divComment.Attributes["class"] = "comment-content normal";
                    break;
                case SMX.Rate.QuanTrong:
                    divComment.Attributes["class"] = "comment-content important";
                    break;
            }

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;

            btnEdit.Visible = btnDelete.Visible = Profiles.MyProfile.EmployeeID == item.CommentedByID && Utils.Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);
        }

        private void BindObject2Footer(RepeaterItem rptItem)
        {
            var commentID = Utils.Utility.GetNullableInt((rptItem.FindControl("hidCMID") as HiddenField).Value);
            var content = UIUtility.ConvertHtml2BreakLine((rptItem.FindControl("ltrContent") as Literal).Text);
            var rate = Utils.Utility.GetNullableInt((rptItem.FindControl("hidRate") as HiddenField).Value);
            var commentedByID = Utils.Utility.GetNullableInt((rptItem.FindControl("hidCommentedByID") as HiddenField).Value);
            var version = Utils.Utility.GetNullableInt((rptItem.FindControl("hidVersion") as HiddenField).Value);
            this._CommentID = commentID;
            this._CommentedByID = commentedByID;
            this._Version = version;
            ddlRate.SelectedValue = Utils.Utility.GetString(rate);
            txtCommentContent.Text = content;
            hidTypeNotification = rptItem.FindControl("hidTypeNoti") as HiddenField ;
        }

        #endregion
    }
}