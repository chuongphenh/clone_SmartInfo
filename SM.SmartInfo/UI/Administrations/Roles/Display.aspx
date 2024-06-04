<%@ Page Title="Chi tiết nhóm người dùng" Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.Administrations.Roles.Display" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <asp:HiddenField ID="hidStatus" runat="server" />
    <div class="toolbar">
        CHI TIẾT NHÓM NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="hypEdit" runat="server" Visible="false">
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16pt;" title="Sửa"> Sửa</i>
                </asp:HyperLink>
            </li>
             <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa nhóm người dùng không?')" Visible="false">
                    <i class="fa fa-trash" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Xóa"> Xóa</i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Thoát"> Thoát</i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
            <col width="20%" />
            <col width="10%" />
            <col width="20%" />
            <col />
            <tr style="height:40px;   line-height: 1.34; border-color: #ebedf0; border-style: solid;">
                <td></td>
                <td style="text-align:left;font-weight:bold">Tên vai trò
                </td>
                <td>
                    <asp:Literal ID="ltrRoleName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr style="height:40px;   line-height: 1.34; border-color: #ebedf0; border-style: solid;">
                <td></td>
                <td style="text-align:left;font-weight:bold">Trạng thái
                </td>
                <td>
                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr style="height:40px;   line-height: 1.34; border-color: #ebedf0; border-style: solid;">
                <td></td>
                <td style="text-align:left;font-weight:bold">Mô tả
                </td>
                <td>
                    <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
