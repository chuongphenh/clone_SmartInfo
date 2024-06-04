<%@ Page Title="Thêm mới điều kiện" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" Inherits="SM.SmartInfo.UI.Configurations.RuleEngines.AddNew" %>

<%@ Register Assembly="SMCB" Namespace="SoftMart.Core.BRE" TagPrefix="sm" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
            color:#000;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <asp:HiddenField ID="hidID" runat="server" />
    <div class="toolbar">
        THÊM MỚI ĐIỀU KIỆN
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"></i>
                    <span>Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink runat="server" ID="hplExit">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black;font-size: 16px;" title="Thoát"></i>
                    <span>Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="content">
        <sm:EditRuleUC ID="ucEditRule" runat="server" />
    </div>
</asp:Content>
