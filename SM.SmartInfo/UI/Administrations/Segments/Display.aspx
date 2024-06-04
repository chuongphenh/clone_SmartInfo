<%@ Page Title="Chi tiết Đoạn đường" Language="C#"  MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Segments.Display" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <div class="toolbar">
        CHI TIẾT ĐOẠN ĐƯỜNG
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="icon-edit" Text="Sửa" Visible="true"></asp:HyperLink>
            </li>
             <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="icon-delete"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa đoạn đường không?')" Visible="true">Xóa
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink CssClass="icon-exit" ID="lnkExit" runat="server">Thoát</asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table class="tableDisplay">
            <col width="250" />
            <col width="250" />
            <col width="250" />
            <col />
            <tr>
                <th>Mã
                </th>
                <td>
                    <asp:Literal ID="ltrCode" runat="server"></asp:Literal>
                </td>
                <th>Tên
                </th>
                <td>
                    <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>Tỉnh/Thành phố
                </th>
                <td>
                    <asp:Literal ID="ltrProvince" runat="server"></asp:Literal>
                </td>
                <th>Quận/Huyện
                </th>
                <td>
                    <asp:Literal ID="ltrDistrict" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>Đường/Phố
                </th>
                <td>
                    <asp:Literal ID="ltrStreet" runat="server"></asp:Literal>
                </td>
                <th>Trạng thái
                </th>
                <td>
                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>Đoạn từ
                </th>
                <td>
                    <asp:Literal ID="ltrSegmentFrom" runat="server"></asp:Literal>
                </td>
                <th>Đoạn đến
                </th>
                <td>
                    <asp:Literal ID="ltrSegmentTo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>Ghi chú
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>