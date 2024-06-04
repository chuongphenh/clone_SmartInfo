<%@ Page Title="Chi tiết vùng" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" Language="C#" AutoEventWireup="true"
    CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Region.Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidRegionID" runat="server" />
    <div class="toolbar">
        CHI TIẾT VÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" CssClass="icon-edit" Text="Sửa"></asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="icon-delete"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa người dùng không?')">Xóa
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="hypExit" runat="server" CssClass="icon-exit">Thoát</asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <table cellpadding="0" cellspacing="0" class="tableDisplay" width="100%">
            <col width="30%" />
            <col />
            <tr>
                <th>Tên vùng
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>Cán bộ quản lý</th>
                <td colspan="3">
                    <asp:Literal ID="ltrEmployee" runat="server" />
                </td>
            </tr>
            <tr>
                <th>Trạng thái
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltrStatus" runat="server" />
                </td>
            </tr>
        </table>
        <h3>Danh sách các tỉnh</h3>
        <table id="list-results" data-role="table" width="100%" class="tableDisplay">
            <tr style="text-align: center">
                <th style="text-align: left">Tên tỉnh
                </th>
            </tr>
            <asp:Repeater ID="rptRegionProvince" runat="server" OnItemDataBound="rptRegionProvince_ItemDataBound">
                <ItemTemplate>
                    <tr style="background: #fff;">
                        <td>
                            <asp:Literal ID="ltrProvince" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
