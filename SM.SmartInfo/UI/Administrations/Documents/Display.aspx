<%@ Page Title="Chi tiết cơ cấu tổ chức" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Plans.Display" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hidVersion" runat="server" />
            <asp:HiddenField ID="hidDefaultFlowID" runat="server" />
            <asp:HiddenField ID="hidID" runat="server" />
            <div class="toolbar">
                THÔNG TIN KẾ HOẠCH
                <ul class="icon_toolbar">
                    <li>
                        <asp:HyperLink ID="lnkEdit" runat="server" Visible="false">
             <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16px;" title="Sửa"> </i>
             <span style="color:black;font-weight:700">Sửa</span>
                        </asp:HyperLink>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                            OnClientClick="return confirm('Bạn có chắc chắn muốn xóa kế hoạch không?')"
                            Visible="false">
             <i class="fa fa-trash" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16px;" title="Xóa"> </i>
             <span style="color:black;font-weight:700">Xóa</span>
                        </asp:LinkButton>
                    </li>
                    <li>
                        <asp:HyperLink ID="lnkExit" runat="server">
             <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16px;" title="Thoát"> </i>
             <span style="color:black;font-weight:700">Thoát</span>
                        </asp:HyperLink>
                    </li>
                </ul>
            </div>
            <div class="content" style="background: transparent">
                <div class="err">
                    <uc:ErrorMessage ID="ucErr" runat="server" />
                </div>

                <div style="background-color: White; height: 100%; padding-bottom: 110px">
                    <asp:Panel ID="pnlEditInfo" runat="server" ScrollBars="Auto" Height="700px" Width="98%">
                        <table class="tableDisplay">
                            <colgroup>
                                <col width="200px" />
                                <col width="" />
                            </colgroup>
                            <tr>
                                <th>Mã kế hoạch
                                </th>
                                <td>
                                    <asp:Label ID="lblPlanCode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Tên kế hoạch 
                                </th>
                                <td>
                                    <asp:Label ID="lblPlanName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Ngày bắt đầu
                                </th>
                                <td>
                                    <asp:Label ID="lblStartDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Ngày kết thúc
                                </th>
                                <td>
                                    <asp:Label ID="lblEndDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Chu kỳ báo cáo
                                </th>
                                <td>
                                    <asp:Label ID="lblReportCycle" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>Loại chu kỳ báo cáo
                                </th>
                                <td>
                                    <asp:Label ID="lblReportCycleType" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <uc:TargetPlan ID="ucTarget" runat="server" DataTextField="Name"
                            DataValueField="TargetID" AllowEdit="false" Width="350" />

                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
