<%@ Page Title="Tạo mới phân loại" Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.Administrations.Categories.AddNew" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidBinaryContent" />
    <asp:HiddenField ID="hidId" runat="server" />
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
    </style>
    <div class="toolbar">
        THÊM MỚI PHÂN LOẠI
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server"
                    Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"></i>
                    <span style="color:black;font-weight:700">Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black;font-size: 16px;" title="Thoát"></i>
                    <span style="color:black;font-weight:700">Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table class="tableDisplay" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <th>Tên phân loại<span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="256" Width="200" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
