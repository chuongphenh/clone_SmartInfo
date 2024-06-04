<%@ Page Title="Tạo mới vùng" Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs"
    MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" Inherits="SM.SmartInfo.UI.Administrations.Region.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/EmployeeSelectorUC.ascx" TagName="Employee" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="toolbar">
        TẠO MỚI VÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="icon-save">Lưu</asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server" CssClass="icon-exit">Thoát</asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <table cellpadding="0" cellspacing="0" class="tableDisplay" width="100%">
            <col width="30%" />
            <col />
            <tr>
                <th>Tên vùng <span class="star">*</span>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtName" runat="server" MaxLength="64" Width="220"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Cán bộ quản lý<span class="star">*</span></th>
                <td colspan="3">
                    <uc:Employee ID="ucEmployee" runat="server" DataTextField="Name" IsSearchAll="true" />
                </td>
            </tr>
            <tr>
                <th>Trạng thái
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="220" />
                </td>
            </tr>
        </table>
        <h3>Danh sách các tỉnh</h3>
        <table id="list-results" data-role="table" width="100%" class="tableDisplay">
            <col width="70%" />
            <col />
            <tr style="text-align: center">
                <th style="text-align: center">Tên tỉnh
                </th>
                <th style="text-align: center">Chức năng
                </th>
            </tr>
            <tbody>
                <asp:Repeater ID="rptRegionProvince" runat="server" OnItemDataBound="rptRegionProvince_ItemDataBound" OnItemCommand="rptRegionProvince_ItemCommand">
                    <ItemTemplate>
                        <tr style="background: #fff;">
                            <td>
                                <asp:DropDownList ID="ddlProvince" runat="server" Width="100%"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="grid-delete"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tr>
                <td></td>
                <td style="vertical-align: middle">
                    <asp:LinkButton ID="btnAddRegionProvince" runat="server" data-ajax="false" class="ui-btn ui-btn-inline toolbar-btn toolbar-btn-orange"
                        Width="100%" Style="padding: 10px 4px;" OnClick="btnAddRegionProvince_Click">
                                <i class="fa fa-plus" aria-hidden="true"></i>ADD
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
