<%@ Page Title="Chi tiết điều kiện thực hiện" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Configurations.RuleEngines.Display" %>

<%@ Register Assembly="SMCB" Namespace="SoftMart.Core.BRE" TagPrefix="sm" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidRuleID" runat="server" />
    <div class="toolbar">
        CHI TIẾT ĐIỀU KIỆN THỰC HIỆN
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="hplEdit" runat="server">
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16pt;" title="Sửa"></i>
                </asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink runat="server" ID="hplExit">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Thoát"></i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="err">
        <uc:ErrorMessage ID="ucError" runat="server" />
    </div>
    <div class="content table-core">
        <sm:DisplayRuleUC ID="ucDisplay" runat="server" />
    </div>
</asp:Content>
