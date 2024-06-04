<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressAgencyHRUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.PressAgencyHRUC" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .content-address {
        height: 20px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 1;
        overflow: hidden;
    }

    .active-li > .div-link > .row > .col-sm-8 > .p-title {
        font-size: 14px;
        font-weight: 600;
        color: #1D39C4;
    }

        .active-li > .div-link > .row > .col-sm-8 > .p-title > a {
            font-size: 14px;
            font-weight: 600;
            color: #1D39C4;
        }

    .active-li > .div-link > .row > .col-sm-4 {
        font-size: 13px;
        font-weight: 600;
    }

    .description-detail {
        font-size: 13px;
        height: 18px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 1;
        overflow: hidden;
    }

    .description-address {
        font-size: 13px;
        height: 35px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
        overflow: hidden;
    }

    .div-link:hover {
        cursor: pointer;
    }
</style>
<asp:HiddenField ID="hidPage" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidPressAgencyID" runat="server" />
<asp:HiddenField ID="hidAttitude" runat="server" />
<div class="row">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="home-block" style="width: 100%">
        <ul class="press-agency-hr">
            <asp:Repeater ID="rptHR" runat="server" OnItemDataBound="rptHR_ItemDataBound">
                <ItemTemplate>
                    <li class="active-li" style="padding: 10px">
                        <div class="div-link" id="divLink" runat="server">
                            <div class="row">
                                <div class="col-sm-4">
                                    <a id="linkItem" runat="server" href="#">
                                        <img id="img" runat="server" class="img-responsive" src="" alt="" style="height: 60px; width: 60px; border-radius: 50%;">
                                    </a>
                                </div>
                                <div class="col-sm-8">
                                    <p class="p-title" style="margin: 15px 0 0">
                                        <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                                    </p>
                                    <p style="color: #1D39C4">
                                        <asp:Literal ID="ltrPosition" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Tuổi</div>
                                <div class="col-sm-8 description-detail">
                                    <asp:Literal ID="ltrAge" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Ngày sinh</div>
                                <div class="col-sm-8 description-detail">
                                    <asp:Literal ID="ltrDOB" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Điện thoại</div>
                                <div class="col-sm-8 description-detail">
                                    <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Email</div>
                                <div class="col-sm-8 description-detail">
                                    <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Địa chỉ</div>
                                <div class="col-sm-8 description-address">
                                    <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Thái độ</div>
                                <div class="col-sm-8 description-detail">
                                    <asp:Label ID="ltrAttitude" runat="server" Style="font-size: 13px; padding-bottom: 0px; padding-top: 0px"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Ghi nhớ</div>
                                <div class="col-sm-8 description-detail">
                                    <span class="circle-note">&nbsp;</span>
                                </div>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>