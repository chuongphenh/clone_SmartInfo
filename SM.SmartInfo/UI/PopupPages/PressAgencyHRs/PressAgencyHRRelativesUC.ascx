﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressAgencyHRRelativesUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.PressAgencyHRRelativesUC" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    ::-webkit-scrollbar-track {
        /*-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);*/
        border-radius: 4px;
        background-color: #fff;
    }

    ::-webkit-scrollbar {
        width: 5px;
        height: 5px;
        background-color: #F5F5F5;
    }

    ::-webkit-scrollbar-thumb {
        border-radius: 10px;
        /*-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);*/
        background-color: #E8E8E8;
    }
</style>

<asp:HiddenField ID="hidPage" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidPressAgencyHRID" runat="server" />
<div class="err">
    <uc:ErrorMessage ID="ucErr" runat="server" />
</div>

<div class="pa-popup">
    <div class="pa-popup-title">
        <span>&nbsp;</span>
        Thông tin gia đình
        <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="font-size: 16pt; float: right; padding-right: 5px"><i class="fa fa-plus"></i></asp:LinkButton>
    </div>
    <ul style="max-height: 303px; min-height: 303px; overflow-y: scroll;">
        <asp:Repeater ID="rptHistory" runat="server" OnItemDataBound="rptHistory_ItemDataBound" OnItemCommand="rptHistory_ItemCommand">
            <ItemTemplate>
                <li style="padding-right: 5px;">
                    <asp:HiddenField ID="hidPressAgencyHRRelativesID" runat="server" />
                    <p class="pa-date" style="font-weight: bold">
                        <asp:Literal ID="ltrRelationship" runat="server"></asp:Literal>
                        <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                    </p>
                    <asp:LinkButton style="float: right" ID="btnDelete" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash" title="Xóa"></i></asp:LinkButton>
                    <asp:LinkButton style="float: right; margin-right: 10px" ID="btnEdit" runat="server"><i class="fas fa-pencil-alt" title="Sửa"></i></asp:LinkButton>
                </li>
                <li>
                    <p class="pa-date">Ngày sinh:</p>
                    <p>
                        <asp:Literal ID="ltrDOB" runat="server"></asp:Literal>
                        <tk:DatePicker ID="dpkDOB" runat="server" Width="100%" DateFormat="DMY" Visible="false" />
                    </p>
                </li>
                <li>
                    <p class="pa-date">Tuổi</p>
                    <p>
                        <asp:Literal ID="ltrAge" runat="server"></asp:Literal>
                    </p>
                </li>
                <li>
                    <p class="pa-date">Thông tin khác</p>
                    <p>
                        <asp:Literal ID="ltrOtherNote" runat="server"></asp:Literal>
                    </p>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>


<%--<!--Pagination-->
<div style="text-align: center">
    <div class="text-center" style="display: inline-block">
        <ul class="pagination">
            <uc:PagerUC runat="server" ID="Pager" OnPageIndexChanged="ucPager_PageIndexChanged" />
        </ul>
    </div>
</div>
<!--End Pagination-->--%>

<tk:PopupPanel ID="popEdit" runat="server" Title="THÊM MỚI/CẬP NHẬT NHÂN THÂN" Width="800" CancelButton="btnCancel" OnPopupClosed="popEdit_PopupClosed">
    <PopupTemplate>
        <table class="table" style="width: 100%; margin-bottom: 0px">
            <colgroup>
                <col width="150" />
                <col />
            </colgroup>
            <tr>
                <th>Quan hệ
                </th>
                <td>
                    <asp:HiddenField ID="hidPressAgencyHRRelativesID" runat="server" />
                    <asp:TextBox ID="txtRelationship" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Họ tên
                </th>
                <td>
                    <asp:TextBox ID="txtFullName" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Ngày sinh
                </th>
                <td>
                    <tk:DatePicker ID="dpkDOB" runat="server" Width="100%" DateFormat="DMY" />
                </td>
            </tr>
            <tr>
                <th>Thông tin liên quan khác
                </th>
                <td>
                    <asp:TextBox ID="txtOtherNote" runat="server" Width="100%" MaxLength="512"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="popup-toolbar" style="text-align: center">
            <asp:Button runat="server" ID="btnSave" Text="Lưu" OnClick="btnSave_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>