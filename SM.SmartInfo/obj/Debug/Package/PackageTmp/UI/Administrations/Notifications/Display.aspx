<%@ Page Title="Cấu hình thông báo" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="SM.SmartInfo.UI.Administration.Notifications.Display" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <div class="toolbar">
        CẤU HÌNH THÔNG BÁO
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server">
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Sửa"> Sửa</i>
                </asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa bản ghi này không?')">
                    <i class="fa fa-trash" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Xóa"> Xóa</i>
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
            <col width="250" />
            <col width="250" />
            <col width="250" />
            <col />
            <tr>
                <td style="height: 40px; text-align: right; font-weight: bold">Mã: &nbsp;
                </td>
                <td>
                    <asp:Literal ID="ltrCode" runat="server"></asp:Literal>
                </td>
                <td style="height: 40px; text-align: right; font-weight: bold">Tên: &nbsp;
                </td>
                <td>
                    <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="height: 40px; text-align: right; font-weight: bold">Loại thông báo: &nbsp;
                </td>
                <td>
                    <asp:Literal ID="ltrExt1i" runat="server" />
                </td>
                <td style="height: 40px; text-align: right; font-weight: bold">Trạng thái: &nbsp;
                </td>
                <td>
                    <asp:Literal ID="ltrStatus" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 40px; text-align: right; font-weight: bold">Số lần gửi thông báo: &nbsp;
                </td>
                <td colspan="3">
                    <asp:Literal ID="ltrExt2i" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
