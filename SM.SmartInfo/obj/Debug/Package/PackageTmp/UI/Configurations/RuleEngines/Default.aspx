<%@ Page Title="Danh sách điều kiện thực hiện" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Configurations.RuleEngines.Default" %>

<%@ Register Assembly="SMCB" Namespace="SoftMart.Core.BRE" TagPrefix="re" %>
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
    <asp:HiddenField runat="server" ID="hidStatus" />
    <div class="toolbar">
        DANH SÁCH ĐIỀU KIỆN THỰC HIỆN
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="hplAddNew" runat="server">
                    <i class="fa fa-plus" aria-hidden="true" style="color: black; font-size: 16px;" title="Tạo mới"></i>
                    <span>Tạo mới</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div class="table-container">
            <re:ListRuleUC ID="ucListRule" runat="server" />
        </div>
    </div>
</asp:Content>
