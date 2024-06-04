<%@ Page Title="Chi tiết nước sản xuất" Language="C#"  MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Country.Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdID" runat="server" />
    <asp:HiddenField ID="HiddenVersion" runat="server" />
    <div class="toolbar">
        CHI TIẾT NƯỚC SẢN XUẤT
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="icon-edit" Text="Sửa" Visible="false"></asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="icon-delete"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa quốc gia không?')" Visible="false">Xóa
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink CssClass="icon-exit" ID="lnkExit" runat="server">Thoát</asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
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
                <th>Trạng thái
                </th>
                <td>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
                <th>Thứ tự hiển thị</th>
                <td>
                    <asp:Literal ID="ltrDisplayOrder" runat="server"/>
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

