<%@ Page Title="Sửa nhóm người dùng" Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.Administrations.Roles.Edit" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <div class="toolbar">
        SỬA NHÓM NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"> </i>
                    <span style="color:black;font-weight:700">Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; font-size: 16px;" title="Thoát"></i>
                    <span style="color:black;font-weight:700">Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="content">
        <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
            <col width="20%" />
            <col width="30%" />
            <col width="30%" />
            <col />
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Tên vai trò <span class="star">*</span>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:TextBox runat="server" ID="txtRoleName" Width="99%" MaxLength="256" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Trạng thái
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="99%" DataValueField="Key" DataTextField="Value"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
             <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Mô tả
                </td>
                 <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <tk:TextArea runat="server" ID="txtDescription" TextMode="MultiLine" Rows="3" Width="99%"
                        MaxLength="256" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
