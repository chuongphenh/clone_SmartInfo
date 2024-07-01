<%--<%@ Page Title="Chỉnh sửa thông tin kế hoạch" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Plans.Edit" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/EmployeeSelectorUC.ascx" TagName="Employee" TagPrefix="uc" %>
<%@ Register Src="TargetPlanUC.ascx" TagName="TargetPlan" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .icon_toolbar li a {
            background: #F2F3F8;
            padding: 5px 10px;
            border-radius: 4px;
            font-weight: bold;
        }

            .icon_toolbar li a i {
                padding-top: 0;
            }

        .custom-sidebar-menu .flex_tree-ul li {
            font-weight: bold;
            font-size: 14px;
            background: #e0f0ff;
            margin-bottom: 4px;
            padding: 7px 5px !important;
        }

        .custom-sidebar-menu .flex_tree-cover {
            background: #cbcbcb4f;
            border-radius: 5px;
            padding: 5px;
            border-radius: 5px;
        }

            .custom-sidebar-menu .flex_tree-cover li ul {
                background: #fff;
                padding: 0;
            }

                .custom-sidebar-menu .flex_tree-cover li ul li {
                    background: #fff;
                    padding-left: 10px !important;
                    border-bottom: 1px solid #ddd;
                }

                    .custom-sidebar-menu .flex_tree-cover li ul li:last-child {
                        border-bottom: none;
                    }
    </style>
    <div class="toolbar">
        CHỈNH SỬA THÔNG TIN KẾ HOẠCH
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
    <div class="content" style="background: transparent">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:HiddenField ID="hidVersion" runat="server" />
                <asp:HiddenField ID="hidOldParentID" runat="server" />
                <asp:HiddenField ID="hidNewParentID" runat="server" />
                <div class="err">
                    <uc:ErrorMessage ID="ucErr" runat="server" />
                </div>
                <div style="background-color: White; height: 100%; padding-bottom: 110px">
                    <asp:Panel ID="pnlEditInfo" runat="server" ScrollBars="Auto" Height="100%" Width="98%">
                        <table class="tableDisplay">
                            <colgroup>
                                <col width="200px" />
                                <col width="" />
                                <tr>
                                    <th>Mã kế hoạch <span class="star">*</span>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtPlanCode" runat="server" MaxLength="64" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>Tên kế hoạch <span class="star">*</span>
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtPlanName" runat="server" AutoPostBack="true" MaxLength="255" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>Ngày bắt đầu
                                    </th>
                                    <td>
                                        <tk:DatePicker ID="rdpStartDate" runat="server" Width="99%" DateFormat="DMY" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>Ngày kết thúc
                                    </th>
                                    <td>
                                        <tk:DatePicker ID="rdpEndDate" runat="server" Width="99%" DateFormat="DMY" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>Chu kỳ báo cáo
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtReportCycle" runat="server" MaxLength="255" Width="350" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>Loại chu kỳ báo cáo
                                    </th>
                                    <td>
                                         <asp:DropDownList ID="rcbReportCycleType" runat="server" AppendDataBoundItems="true" DataTextField="Value" DataValueField="Key" DropDownWidth="500" Width="350px" />
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                       <uc:TargetPlan ID="ucTarget" runat="server" DataTextField="Name"
    DataValueField="TargetID" AllowEdit="true" Width="350" />
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>--%>
