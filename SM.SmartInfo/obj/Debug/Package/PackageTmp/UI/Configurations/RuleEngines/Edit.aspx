<%@ Page Title="Chỉnh sửa điều kiện" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SM.SmartInfo.UI.Configurations.RuleEngines.Edit" %>

<%@ Register Assembly="SMCB" Namespace="SoftMart.Core.BRE" TagPrefix="sm" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <div class="toolbar">
        CHỈNH SỬA ĐIỀU KIỆN
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server">
                    <i class="far fa-save" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Lưu"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink runat="server" ID="hplExit">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Thoát"></i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="content table-core">
        <sm:EditRuleUC ID="ucEditRule" runat="server" />
    </div>
</asp:Content>
