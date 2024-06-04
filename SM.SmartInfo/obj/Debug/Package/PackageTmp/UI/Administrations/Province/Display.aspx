<%@ Page Title="Chi tiết tỉnh thành" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Province.Display" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdID" runat="server" />
    <asp:HiddenField ID="HiddenVersion" runat="server" />
    <div class="toolbar">
        CHI TIẾT TỈNH THÀNH
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Visible="false">
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16pt;" title="Sửa"> Sửa</i>
                </asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tỉnh thành không?')" Visible="false">
                    <i class="fa fa-trash" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Xóa"> Xóa</i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Thoát"> Thoát</i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table cellpadding="0" cellspacing="0" class="tableDisplay" width="100%">
            <tr>
                <th>Mã
                </th>
                <td>
                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                </td>
                <th>Tên
                </th>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Miền <span class="star">*</span>
                </th>
                <td>
                    <asp:Label ID="lblZone" runat="server"></asp:Label>
                </td>
                <th>Trạng thái
                </th>
                <td>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Thứ tự hiển thị
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDisplayOrder" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>Ghi chú
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
