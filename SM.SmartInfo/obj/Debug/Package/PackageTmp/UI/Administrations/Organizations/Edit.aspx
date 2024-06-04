<%@ Page Title="Chỉnh sửa thông tin tổ chức" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Organizations.Edit" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/EmployeeSelectorUC.ascx" TagName="Employee" TagPrefix="uc" %>
<%@ Register Src="OrganizationEmployeeUC.ascx" TagName="OrganizationEmployee" TagPrefix="uc" %>
<%@ Register Src="OrgTreeViewUC.ascx" TagName="OrgTreeView" TagPrefix="uc" %>
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
    <div class="toolbar">
        CHỈNH SỬA THÔNG TIN TỔ CHỨC
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" Visible="false"> 
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;font-weight:700" title="Lưu"> </i>
                    <span style="color:black;font-weight:700">Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; font-size: 16px;font-weight:700" title="Thoát"> </i>
                     <span style="color:black;font-weight:700">Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content" style="background:transparent">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:HiddenField ID="hidVersion" runat="server" />
                <asp:HiddenField ID="hidOldParentID" runat="server" />
                <asp:HiddenField ID="hidNewParentID" runat="server" />
                <div class="err">
                    <uc:ErrorMessage ID="ucErr" runat="server" />
                </div>
                <div style="width: 30%; float: left; padding-left:10px;margin-top:20px;">
                    <div class="custom-sidebar-menu">
                        <uc:OrgTreeView runat="server" ID="ucOrgTreeView" />
                    </div>
                    
                </div>
                <div style="width: 70%; float: left; position: fixed; top: 140px; left: 31%; background-color: White;
                    height: 100%; padding-bottom:110px">
                    <asp:Panel ID="pnlEditInfo" runat="server" ScrollBars="Auto" Height="100%" Width="98%">
                        <table class="tableDisplay">
                            <colgroup>
                                <col width="200px" />
                                <col width="" />
                                <tr>
                                    <th>
                                        Thay đổi đơn vị trực thuộc mới
                                    </th>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkChangeParent" Text="Chọn đơn vị trực thuộc bằng cách click vào cây bên trái" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Đơn vị trực thuộc mới
                                    </th>
                                    <td>
                                        <asp:Label ID="lblLevelInfo" runat="server">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Đơn vị trực thuộc hiện tại
                                    </th>
                                    <td>
                                        <asp:Label ID="lblCurrentLevelInfo" runat="server">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Mã đơn vị <span class="star">*</span>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtCode" runat="server" MaxLength="64" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Tên đơn vị <span class="star">*</span>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" AutoPostBack="true" MaxLength="255" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Mã chi nhánh
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtOffice" runat="server" MaxLength="256"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Loại phòng ban <span id="span1" runat="server" class="star" visible="false">*</span>
                                    </th>
                                    <td>
                                        <asp:DropDownList ID="dropOrganizationType" runat="server" DataTextField="Value"
                                            DataValueField="Key" DropDownWidth="500" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Luật chia phòng <span id="spanRequireRule" runat="server" class="star" visible="false">
                                            *</span>
                                    </th>
                                    <td>
                                        <asp:DropDownList ID="rcbRule" runat="server" AppendDataBoundItems="true" DataTextField="Name"
                                            DataValueField="RuleID" DropDownWidth="500" Width="350px" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Luật chia chuyên viên
                                    </th>
                                    <td>
                                        <asp:DropDownList ID="rcbDispatchEmployeeRule" runat="server" DataTextField="Name"
                                            DataValueField="RuleID" AppendDataBoundItems="true" Width="350px" DropDownWidth="500" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Miền
                                    </th>
                                    <td>
                                        <asp:DropDownList ID="ddlZone" runat="server" AppendDataBoundItems="true" DataTextField="Name"
                                            DataValueField="SystemParameterID" Width="350" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChange" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Tỉnh/ Thành phố
                                    </th>
                                    <td>
                                        <asp:DropDownList ID="ddlProvince" runat="server" AppendDataBoundItems="true" DataTextField="Name"
                                            DataValueField="SystemParameterID" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Cấp tổ chức của đơn vị
                                    </th>
                                    <td>
                                        <asp:DropDownList ID="ddlCommittee" runat="server" AppendDataBoundItems="true" DataTextField="Name"
                                            DataValueField="CommitteeID" Width="350" />
                                    </td>
                                </tr>                                
                                <tr>
                                    <th>
                                        Địa chỉ
                                    </th>
                                    <td>
                                        <tk:TextArea ID="txtAddress" runat="server" Rows="1" MaxLength="255" TextMode="MultiLine"
                                            Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Email đại diện (cách nhau bởi dấu ;)
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtBranchEmail" runat="server" MaxLength="255" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        Mô tả
                                    </th>
                                    <td>
                                        <tk:TextArea ID="txtDescription" runat="server" Rows="1" MaxLength="255" TextMode="MultiLine"
                                            Width="350" />
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                        <%-- tab control--%>
                        <tk:TabStrip ID="TabStrip1" runat="server" ContainerPanel="pnlMainTab" SelectedIndex="0">
                            <tk:TabStripItem Text="Danh sách chuyên viên" Value="tabEmp" />
                            <tk:TabStripItem Text="Danh sách Quản lý" Value="tabMng" />
                        </tk:TabStrip>
                        <asp:Panel ID="pnlMainTab" runat="server" CssClass="tab-content">
                            <asp:Panel ID="tabEmp" runat="server">
                                <uc:OrganizationEmployee ID="ucOrganizationEmployee" runat="server" DataTextField="Name"
                                    DataValueField="EmployeeID" AllowEdit="true" Width="350" />                                
                            </asp:Panel>
                            <asp:Panel ID="tabMng" runat="server">
                                <uc:OrganizationEmployee ID="ucOrganizationManager" runat="server" DataTextField="Name"
                                    DataValueField="EmployeeID" AllowEdit="true" Width="350" />
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>