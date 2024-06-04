<%@ Page Title="Chi tiết cơ cấu tổ chức" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Organizations.Display" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="OrgTreeViewUC.ascx" TagName="OrgTreeView" TagPrefix="uc" %>
<%@ Register Src="OrganizationEmployeeUC.ascx" TagName="OrganizationEmployee" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
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
                    .custom-sidebar-menu .flex_tree-ul li{
                font-weight: bold;
                font-size: 14px;
                background: #e0f0ff;
                margin-bottom: 4px;
                padding: 7px 5px!important;
            }
            .custom-sidebar-menu .flex_tree-cover{
                background: #cbcbcb4f;
                border-radius: 5px;
                padding:5px;
                border-radius:5px;
            }
            .custom-sidebar-menu .flex_tree-cover li ul{
                background: #fff;
                padding:0;
            }
            .custom-sidebar-menu .flex_tree-cover li ul li{
                background: #fff;
                padding-left:10px!important;
                border-bottom:1px solid #ddd;
            }
            .custom-sidebar-menu .flex_tree-cover li ul li:last-child{
                border-bottom:none;
            }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hidVersion" runat="server" />
            <asp:HiddenField ID="hidDefaultFlowID" runat="server" />
            <asp:HiddenField ID="hidID" runat="server" />
            <div class="toolbar">
                THÔNG TIN TỔ CHỨC
                <ul class="icon_toolbar">
                    <li>
                        <asp:HyperLink runat="server" ID="lnkAddNew" Visible="false">
                            <i class="fa fa-plus" aria-hidden="true" style="color: black; font-size: 16px;" title="Tạo mới"> </i>
                            <span style="color:black;font-weight:700">Tạo</span>
                        </asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="lnkEdit" runat="server" Visible="false">
                            <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; font-size: 16px;" title="Sửa"> </i>
                            <span style="color:black;font-weight:700">Sửa</span>
                        </asp:HyperLink>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                            OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tổ chức không?')" Visible="false">
                            <i class="fa fa-trash" aria-hidden="true" style="color:black; font-size: 16px;" title="Xóa"> </i>
                            <span style="color:black;font-weight:700">Xóa</span>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div class="content" style="background:transparent">
                <div class="err">
                    <uc:ErrorMessage ID="ucErr" runat="server" />
                </div>
                <div style="width: 30%; float: left;padding-left:10px;margin-top:20px;">
                    <div class="custom-sidebar-menu">
                        <uc:OrgTreeView runat="server" ID="ucOrgTreeView" />
                    </div>

                </div>
                <div style="width: 70%; float: left; position: fixed; top: 140px; left: 31%; background-color: White;
                    height: 100%; padding-bottom:110px">
                    <asp:Panel ID="pnlEditInfo" runat="server" ScrollBars="Auto" Height="700px" Width="98%">
                        <table class="tableDisplay">
                            <colgroup>
                                <col width="200px" />
                                <col width="" />
                            </colgroup>
                            <tr>
                                <th>Đơn vị trực thuộc
                                </th>
                                <td>
                                    <asp:Label ID="lblLevelInfo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Mã đơn vị
                                </th>
                                <td>
                                    <asp:Label ID="lblCode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Tên đơn vị
                                </th>
                                <td>
                                    <asp:Label ID="lblName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Mã chi nhánh
                                </th>
                                <td>
                                    <asp:Label ID="lblOffice" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Loại phòng ban
                                </th>
                                <td>
                                    <asp:Label ID="lblType" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Luật phòng
                                </th>
                                <td>
                                    <asp:Label ID="lblRule" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Luật chia chuyên viên
                                </th>
                                <td>
                                    <asp:Label ID="lblDispatchEmployeeRule" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Miền
                                </th>
                                <td>
                                    <asp:Label ID="lblZoneName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Tỉnh/ Thành phố
                                </th>
                                <td>
                                    <asp:Label ID="lblProvince" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Cấp tổ chức của đơn vị
                                </th>
                                <td>
                                    <asp:Label ID="lblCommitteeName" runat="server" />
                                </td>
                            </tr>

                            <tr>
                                <th>Địa chỉ
                                </th>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Email đại diện (cách nhau bởi dấu ;)
                                </th>
                                <td>
                                    <asp:Label ID="lblBranchEmail" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Mô tả
                                </th>
                                <td>
                                    <asp:Label ID="lblDescription" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <%-- tab control--%>
                        <tk:TabStrip ID="TabStrip1" runat="server" ContainerPanel="pnlMainTab" SelectedIndex="0">
                            <tk:TabStripItem style="font-size:14px;" Text="Danh sách Chuyên viên" Value="tabEmp" />
                            <tk:TabStripItem style="font-size:14px;" Text="Danh sách Quản lý" Value="tabMng" />
                        </tk:TabStrip>
                        <asp:Panel ID="pnlMainTab" runat="server" CssClass="tab-content">
                            <asp:Panel ID="tabEmp" runat="server">
                                <uc:OrganizationEmployee ID="ucOrganizationEmployee" runat="server" DataTextField="Name"
                                    DataValueField="EmployeeID" AllowEdit="false" Width="350" />
                            </asp:Panel>
                            <asp:Panel ID="tabMng" runat="server">
                                <uc:OrganizationEmployee ID="ucOrganizationManager" runat="server" DataTextField="Name"
                                    DataValueField="EmployeeID" AllowEdit="false" Width="350" />
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>