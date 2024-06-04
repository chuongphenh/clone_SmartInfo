﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressAgencyHistoryUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.PressAgencies.PressAgencyHistoryUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .table > tbody > tr {
        height: 50px;
    }

        .table > tbody > tr > td {
            vertical-align: middle;
            word-break: break-word;
        }

    .table > thead > tr {
        height: 50px;
    }

        .table > thead > tr > th {
            color: #595959;
            background: transparent !important;
            font-size: 14px;
            font-weight: 600;
            text-align: center;
            vertical-align: middle;
        }

    .center {
        font-size: 14px;
        text-align: center !important;
    }

    .aspNetDisabled {
        background: unset !important;
        color: darkgray !important;
        border-color: unset !important;
    }

    .flex_popup-content > .table {
        font-size: 14px;
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
        <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="font-size: 16pt; float: right; padding-right: 5px">
            <i class="fa fa-plus" title="Thêm mới lịch sử thay đổi nhân sự"></i>
        </asp:LinkButton>
    </div>
    <div class="data-table">
        <table class="table">
            <thead>
                <tr>
                    <th class="center">#</th>
                    <th>Vị trí thay đổi</th>
                    <th>Nhân sự cũ</th>
                    <th>Nhân sự mới</th>
                    <th class="center">Ngày thay đổi</th>
                    <th id="thEdit" runat="server" style="display: none;"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptHistory" runat="server" OnItemDataBound="rptHistory_ItemDataBound" OnItemCommand="rptHistory_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td class="center">
                                <%# (Container.ItemIndex + 1) + ((SM.SmartInfo.Utils.Utility.GetNullableInt(hidPage.Value) - 1) * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniTen) %>
                                <asp:HiddenField ID="hidPressAgencyHistoryID" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="ltrPositionChange" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="ltrOldEmployee" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="ltrNewEmployee" runat="server"></asp:Literal>
                            </td>
                            <td class="center">
                                <asp:Literal ID="ltrChangeDTG" runat="server"></asp:Literal>
                            </td>
                            <tk:DatePicker ID="dpkChangeDTG" runat="server" Width="100%" DateFormat="DMY" Visible="false" />
                            <td id="tdEdit" runat="server" class="center">
                                <asp:LinkButton ID="btnEdit" runat="server" Style="margin-right: 5px"><i class="fas fa-pencil-alt" title="Sửa"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash" title="Xóa"></i></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr style="background: #fff;">
                    <td colspan="2" style="text-align: left">
                        <asp:LinkButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click"><i class="fa fa-angle-left"></i></asp:LinkButton>
                        <span style="padding-right: 20px; padding-left: 20px">Trang <%= hidPage.Value %></span>
                        <asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click"><i class="fa fa-angle-right"></i></asp:LinkButton>
                    </td>
                    <td></td>
                    <td id="footerEdit" runat="server"></td>
                    <td colspan="2" style="text-align: right">
                        <span></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>

<tk:PopupPanel ID="popEdit" runat="server" Title="THÊM MỚI/CẬP NHẬT LỊCH SỬ" Width="800" CancelButton="btnCancel" OnPopupClosed="popEdit_PopupClosed">
    <PopupTemplate>
        <table class="table" style="width: 100%; margin-bottom: 0px">
            <colgroup>
                <col width="150" />
                <col />
            </colgroup>
            <tr>
                <th>Vị trí thay đổi</th>
                <td>
                    <asp:HiddenField ID="hidPressAgencyHistoryID" runat="server"></asp:HiddenField>
                    <asp:TextBox ID="txtPositionChange" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Nhân sự cũ
                </th>
                <td>
                    <asp:TextBox ID="txtOldEmployee" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Nhân sự mới
                </th>
                <td>
                    <asp:TextBox ID="txtNewEmployee" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Ngày thay đổi
                </th>
                <td>
                    <tk:DatePicker ID="dpkChangeDTG" runat="server" Width="100%" DateFormat="DMY" />
                </td>
            </tr>
        </table>
        <div class="popup-toolbar" style="text-align: center">
            <asp:Button runat="server" ID="btnSave" Text="Lưu" OnClick="btnSave_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>