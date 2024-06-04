<%@ Page EnableEventValidation="false"  Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" CodeBehind="ConfigRight.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Setting.ConfigRight" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/EmployeeSelectorUC.ascx" TagName="Employee" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentAjax" runat="server">
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
    </style>
    <div class="toolbar">
        PHÂN QUYỀN TRUY CẬP
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px; font-weight:700" title="Lưu"></i>
                    <span style="color:black;font-weight:700">Lưu</span>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <asp:Panel ID="Panel1" runat="server">
            <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
                <col width="200" />
                <col width="250" />
                <col width="100" />
                <col />
                <tr >
                    <td align="right" style="font-weight: bold;  font-size:14px;  padding-right: 10px;">Chọn loại
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" style="font-size:13px;" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td id="thUser" runat="server" visible="false" align="right">Người dùng
                    </td>
                    <td id="tdUser" runat="server" visible="false" align="left">
                        <uc:Employee ID="ucEmployee" runat="server" DataTextField="Name" AutoPostBack="true"
                            OnSelectedIndexChanged="ucEmployee_SelectedIndexChanged" IsSearchAll="true" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="height: 30px; font-weight: bold;padding-right: 10px;font-size:14px;">Nhóm chức năng
                    </td>
                    <td>
                        <tk:ComboTreeView ID="cbvFeature" runat="server" Width="220" OnSelectedNodeChanged="cbvFeature_SelectedNodeChanged"
                            AutoPostBack="true" DropDownWidth="400" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField runat="server" ID="hidDynamicColumn" />
        <div runat="server" id="divGrid" style="margin-top: 30px">
        </div>
    </div>
</asp:Content>
