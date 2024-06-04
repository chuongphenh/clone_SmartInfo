<%@ Page Title="Cấu hình thông báo" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" Inherits="SM.SmartInfo.UI.Administration.Notifications.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
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
    </style>
    <div class="toolbar">
        CẤU HÌNH THÔNG BÁO
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"> </i>
                    <span style="color: black;font-weight:600;font-size:16px;"> Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; font-size: 16px;" title="Thoát"> </i>
                    <span style="color: black;font-weight:600;font-size:16px;">Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
            <col width="250" />
            <col width="250" />
            <col width="250" />
            <col />
            <tr>
                <td align="right" style="height: 30px; font-weight: bold;">Mã <span class="star">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtCode" MaxLength="128" runat="server" Width="220"></asp:TextBox>
                </td>
                <td align="right" style="height: 30px; font-weight: bold;">Tên <span class="star">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtName" MaxLength="256" runat="server" Width="220"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 30px; font-weight: bold;">Loại thông báo
                </td>
                <td>
                    <asp:DropDownList ID="ddlExt1i" runat="server" Width="220" DataTextField="Value" DataValueField="Key" />
                </td>
                <td align="right" style="height: 30px; font-weight: bold;">Trạng thái 
                </td>
                <td>
                    <asp:DropDownList ID="ddStatus" runat="server" Width="220" DataTextField="Value" DataValueField="Key" />
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 30px; font-weight: bold;">Số lần gửi thông báo
                </td>
                <td colspan="3">
                    <tk:NumericTextBox ID="numExt2i" runat="server" Width="220px" AllowThousandDigit="false" NumberDecimalDigit="0"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
