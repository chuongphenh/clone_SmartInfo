<%@ Page Title="Cấu hình các ngày đặc biệt" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SM.SmartInfo.UI.Administration.Anniversarys.Edit" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <style>
       .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
            color: #000;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <div class="toolbar">
        CẤU HÌNH CÁC NGÀY KỶ NIỆM, TRUYỀN THỐNG
        <ul class="icon_toolbar">

            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="true">
                    <i class="far fa-save" aria-hidden="true" style=" font-size: 16px;" title="Lưu"></i>
                    <span style="color: black;font-weight:600;font-size:16px;"> Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="font-size: 16px;" title="Thoát"></i>
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
                <td align="right" style="height: 30px; font-weight: bold;">Mã : 
                </td>
                <td>
                    <asp:Literal ID="ltrCode" runat="server"></asp:Literal>
                </td>
                <td align="right" style="height: 30px; font-weight: bold;">Tên <span class="star">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtName" MaxLength="256" runat="server" Width="220"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 30px; font-weight: bold;">Ngày kỷ niệm, truyền thống
                </td>
                <td>
                    <tk:DatePicker ID="dtpExt4" runat="server" Width="99%" DateFormat="DMY" />
                </td>
                <td align="right" style="height: 30px; font-weight: bold;">Trạng thái 
                </td>
                <td>
                    <asp:DropDownList ID="ddStatus" runat="server" Width="220" DataTextField="Value" DataValueField="Key" />
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 30px; font-weight: bold;">Nội dung
                </td>
                <td>
                    <tk:TextArea ID="txtDescription" MaxLength="512" Font-Size="Small" runat="server" Width="100%" TextMode="MultiLine"
                        Rows="3">
                    </tk:TextArea>
                </td>
                <td align="right" style="height: 30px; font-weight: bold;">Loại thông báo
                </td>
                <td>
                    <asp:DropDownList ID="ddlExt1i" runat="server" Width="220" DataTextField="Value" DataValueField="Key" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
