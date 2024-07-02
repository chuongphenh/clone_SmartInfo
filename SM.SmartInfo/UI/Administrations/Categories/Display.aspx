<%@ Page Title="Chi tiết chỉ tiêu" Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
 Inherits="SM.SmartInfo.UI.Administrations.Categories.Display" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="hidBinaryContent" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <asp:HiddenField ID="hidStatus" runat="server" />
    <asp:HiddenField ID="hiTransformType" runat="server" />
    <div class="toolbar">
        CHI TIẾT CHỈ TIÊU
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Visible="false">
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16px;" title="Sửa"> </i>
                    <span style="color:black;font-weight:700">Sửa</span>
                </asp:HyperLink>
            </li>
<%--            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa chỉ tiêu không?')"
                    Visible="false">
                    <i class="fa fa-trash" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16px;" title="Xóa"> </i>
                    <span style="color:black;font-weight:700">Xóa</span>
                </asp:LinkButton>
            </li>--%>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16px;" title="Thoát"> </i>
                    <span style="color:black;font-weight:700">Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table cellpadding="0" cellspacing="0" class="tableDisplay" width="100%">
            <tr>
                <th>
                    Tên chỉ tiêu
                </th>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
