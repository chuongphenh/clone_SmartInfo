<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.Notifications.DisplayUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .description-detail {
        height: 39px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
        overflow: hidden;
    }

    .view-all {
        font-size: 12px;
        color: white;
        float: right;
        padding: 1px;
        padding-right: 15px;
        padding-left: 15px;
        border: 1px solid #597EF7;
        background: #597EF7;
        border-radius: 100px;
    }

        .view-all:hover {
            cursor: pointer;
        }
</style>

<div style="width: 100%; padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px; text-align: left">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <%--CONTENT--%>
    <div class="row">
        <asp:LinkButton ID="btnSendMessage" OnClick="btnSendMessage_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn gửi thông báo?')" runat="server" Style="float: right; margin-top: -5px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-left: 0px; padding-right: 10px; border-radius: 4px;">
            <i class="fa fa-paper-plane" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 0px; border-radius: 4px;" title="Gửi ý kiến chỉ đạo"></i>
            <span style="color: #595959;font-weight:600;font-size:16px;">Gửi</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Style="float: right; margin-top: -5px; margin-left: 5px;  background: #F2F3F8; padding-bottom: 5px; padding-left: 0px; padding-right: 10px; border-radius: 4px;">
            <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 0px; border-radius: 4px;" title="Sửa"></i>
            <span style="color: #595959;font-weight:600;font-size:16px;">Sửa</span>
        </asp:LinkButton>
    </div>
    <div class="row">
        <span style="font-size: 20px; font-weight: 700;color:#141ed2;">
            <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
        </span>
    </div>
    <div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="far fa-clock" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-weight:600;font-size:16px;">Ngày diễn ra sự kiện</span>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <p style="font-size: 14px; color: #262626">
                <asp:Literal ID="ltrDoDTG" runat="server"></asp:Literal>
            </p>
        </div>
    </div>
    <div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="far fa-chart-bar" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-weight:600;font-size:16px;">Ghi chú</span>
            <span class="view-all" id="view-all-note">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <p style="font-size: 14px; color: #262626; min-height: 39px" class="description-detail" id="pNote">
                <asp:Literal ID="ltrNote" runat="server"></asp:Literal>
            </p>
        </div>
    </div>
    <div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fas fa-thumbtack" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-weight:600;font-size:16px;">Ý kiến chỉ đạo</span>
            <span class="view-all" id="view-all-comment">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <p style="font-size: 14px; color: #262626; min-height: 39px" class="description-detail" id="pComment">
                <asp:Literal ID="ltrComment" runat="server"></asp:Literal>
            </p>
        </div>
    </div>
    <%--END CONTENT--%>
    <hr style="margin-bottom: 10px; margin-top: 10px;" />
    <div class="row" style="margin-top: 10px; padding-bottom: 10px">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fa fa-comments" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-weight:600;font-size:16px;">Bình luận</span>
        </div>
    </div>
    <uc:CommentUC ID="ucComment" runat="server" />
</div>

<script>
    $(document).ready(function () {
        $('#view-all-comment').on('click', function () {
            if ($('#pComment').hasClass('description-detail')) {
                $('#pComment').removeClass('description-detail');
                $('#view-all-comment').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
            }
            else {
                $('#pComment').addClass('description-detail');
                $('#view-all-comment').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
            }
        });

        $('#view-all-note').on('click', function () {
            if ($('#pNote').hasClass('description-detail')) {
                $('#pNote').removeClass('description-detail');
                $('#view-all-note').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
            }
            else {
                $('#pNote').addClass('description-detail');
                $('#view-all-note').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
            }
        });
    });
</script>