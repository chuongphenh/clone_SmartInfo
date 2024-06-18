<%@ Page Title="Tạo mới thông tin kế hoạch" Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.Administrations.Plans.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register TagPrefix="uc" Namespace="SM.SmartInfo.UI.UserControls" Assembly="SM.smartinfo" %>
<%@ Register Src="~/UI/UserControls/TargetSelectorUC.ascx" TagName="Target" TagPrefix="uc" %>
<%@ Register Src="TargetPlanUC.ascx" TagName="TargetPlan" TagPrefix="uc" %>
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
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="HiddenVersion" runat="server" />
    <div class="toolbar">
        TẠO MỚI THÔNG TIN KẾ HOẠCH
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click"
                    Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"> </i>
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
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div style="width: 100%;  background-color: White; height: 100%; padding-bottom: 110px; padding-left: 10px; padding-top: 10px;">
            <asp:Panel ID="pnlEditInfo" runat="server" ScrollBars="Auto" Height="700px" Width="98%">
                <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
                    <colgroup>
                        <col width="200px" />
                        <col width="" />
                    </colgroup>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Mã kế hoạch <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtPlanCode" runat="server" MaxLength="64" Width="350" />
                        </td>
                    </tr>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Tên kế hoạch <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtPlanName" runat="server" MaxLength="64" Width="350px" />
                        </td>
                    </tr>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Ngày bắt đầu
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                             <tk:DatePicker ID="rdpStartDate" runat="server" Width="99%" DateFormat="DMY" />
                        </td>
                    </tr>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Ngày kết thúc
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                             <tk:DatePicker ID="rdpEndDate" runat="server" Width="99%" DateFormat="DMY" />
                        </td>
                    </tr>
                     <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Chu kỳ báo cáo<span id="span1" runat="server" class="star" visible="false">*</span>
                        </td>
                    </tr>
                   <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtReportCycle" runat="server" MaxLength="255" Width="350" />
                        </td>
                    </tr>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Loại chu kỳ báo cáo <span id="spanRequireRule" runat="server" class="star" visible="false">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DropDownList ID="rcbReportCycleType" runat="server" AppendDataBoundItems="true" DataTextField="Value" DataValueField="Key" DropDownWidth="500" Width="350px" />
                        </td>
                    </tr>
                </table>
             <uc:TargetPlan ID="ucTarget" runat="server" DataTextField="Name"
                DataValueField="TargetID" AllowEdit="true" Width="350" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>
