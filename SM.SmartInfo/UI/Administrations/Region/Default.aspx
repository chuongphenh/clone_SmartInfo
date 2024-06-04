<%@ Page Title="Danh sách vùng" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" Inherits="SM.SmartInfo.UI.Administrations.Region.Default" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="toolbar">
        DANH SÁCH VÙNG
        <ul class="icon_toolbar">
            <li><a id="hypAddNew" runat="server" class="icon-new" href="AddNew.aspx">Tạo mới</a> </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table width="100%">
            <tr>
                <td>
                    <asp:DataGrid ID="grdMain" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="true"
                        AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid-main" OnItemDataBound="grdMain_ItemDataBound"
                        OnItemCommand="grdMain_ItemCommand" OnPageIndexChanged="grdMain_PageIndexChanged">
                        <HeaderStyle CssClass="grid-header" />
                        <ItemStyle CssClass="grid-item-even" />
                        <AlternatingItemStyle CssClass="grid-item-odd" />
                        <FooterStyle CssClass="grid-footer" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Tên vùng">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hiID" runat="server" />
                                    <asp:HiddenField ID="hiVersion" runat="server" />
                                    <asp:HyperLink runat="server" ID="hplName"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cán bộ quản lý">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrEmployeeName" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Trạng thái">
                                <HeaderStyle Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Literal ID="ltrStatus" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Sửa">
                                <HeaderStyle Width="30px" />
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="hypEdit" CssClass="grid-edit">
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="10" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
