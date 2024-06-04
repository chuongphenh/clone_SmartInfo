<%@ Page Title="Danh sách nhóm người dùng" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.Administrations.Roles.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <style>
                .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <div class="toolbar">
        DANH SÁCH NHÓM NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="hypAdd" runat="server">
                    <i class="fa fa-plus" aria-hidden="true" style="color: black; font-size: 16px; font-weight:700;" title="Tạo mới"></i>
                    <span style="color:black;font-weight:700">Tạo</span>
                </asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" Visible="false">
                    <i class="fa fa-file-excel-o" aria-hidden="true" style="color: black; font-size: 16px; font-weight:700;" title="Xuất Excel"></i>
                    <span style="color:black;font-weight:700">Xuất Excel</span>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="content table-group-list-user">
        <asp:DataGrid ID="grdMain" runat="server" ShowFooter="false"
            AutoGenerateColumns="False" CssClass="grid-main" OnItemDataBound="grdMain_ItemDataBound">
            <HeaderStyle CssClass="grid-header" HorizontalAlign="Center" />
            <ItemStyle CssClass="grid-item-even" />
            <AlternatingItemStyle CssClass="grid-item-odd" />
            <FooterStyle CssClass="grid-footer" />
            <Columns>
                <asp:TemplateColumn HeaderText="STT" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# grdMain.CurrentPageIndex * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageSize + (Container.DataSetIndex + 1) %>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Vai trò">
                    <ItemTemplate>
                        <asp:HyperLink ID="hypName" runat="server"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Mô tả">
                    <ItemTemplate>
                        <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Trạng thái">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Sửa">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HyperLink ID="hypCode" runat="server">
                            <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; font-size: 16px; margin-left: 5px;" title="Sửa"></i>
                            <span style="color:black;font-weight:700">Sửa</span>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
</asp:Content>
