<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressAgencyHRUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.PressAgencies.PressAgencyHRUC" %>

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

    .active-li > .div-link > .row > .col-sm-4 {
        font-size: 14px;
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
<div class="row">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="headline" style="text-align: center; position: relative; top: -49px; right: 0px;">
        <asp:LinkButton ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" Style="font-size: 16pt; float: right; padding-right: 5px">
            <i class="fa fa-plus" title="Thêm mới danh sách liên hệ"></i>
        </asp:LinkButton>
    </div>
    <div class="home-block">
        <ul class="press-agency-hr">
            <asp:Button ID="btnReloadAppendix" runat="server" Text="Tải lại trang" OnClick="btnReloadAppendix_Click" Style="display: none;" />
            <asp:Repeater ID="rptHR" runat="server" OnItemDataBound="rptHR_ItemDataBound" OnItemCommand="rptHR_ItemCommand">
                <ItemTemplate>
                    <li class="active-li">
                        <div class="div-link" id="divLink" runat="server">
                            <div class="row">
                                <div class="col-sm-4">
                                    <a id="linkItem" runat="server" href="#">
                                        <img id="img" runat="server" class="img-responsive" src="" alt="" style="height: 60px; width: 60px; border-radius: 50%; object-fit: cover">
                                    </a>
                                </div>
                                <div class="col-sm-8">
                                    <p class="p-title" style="margin: 15px 0 0">
                                        <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                                    </p>
                                    <p style="color: #1D39C4;font-size:13px;">
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
                                    <asp:Label ID="ltrAttitude" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">Ghi nhớ</div>
                                <div class="col-sm-8 description-detail">
                                    <span class="circle-note">&nbsp;</span>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div class="col-sm-12">
                                <ul class="list-unstyled list-inline blog-info">
                                    <li style="float: right">
                                        <asp:LinkButton ID="btnDelete" runat="server" Style="font-size: 16px; float: right;" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </li>
                                    <li style="float: right">
                                        <asp:HyperLink ID="lnkEdit" runat="server" Style="font-size: 16px"></asp:HyperLink>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>