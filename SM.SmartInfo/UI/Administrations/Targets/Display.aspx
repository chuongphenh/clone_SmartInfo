<%@ Page Title="Chi tiết mẫu Email/Sms" Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
 Inherits="SM.SmartInfo.UI.Administrations.Targets.Display" %>

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
        CHI TIẾT MẪU EMAIL/SMS
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Visible="false">
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16px;" title="Sửa"> </i>
                    <span style="color:black;font-weight:700">Sửa</span>
                </asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa mẫu email/sms không?')"
                    Visible="false">
                    <i class="fa fa-trash" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16px;" title="Xóa"> </i>
                    <span style="color:black;font-weight:700">Xóa</span>
                </asp:LinkButton>
            </li>
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
                    Mã mẫu
                </th>
                <td>
                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                </td>
                <th>
                    Tên mẫu
                </th>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    Loại mẫu
                </th>
                <td>
                    <asp:Label ID="lblTemplateType" runat="server"></asp:Label>
                </td>
                <th>
                    Cách thức sinh
                </th>
                <td>
                    <asp:Label ID="lblTransformType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    Trạng thái
                </th>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblStatus" />
                </td>
            </tr>
            <tr>
                <th>
                    Thuộc tính
                </th>
                <td colspan="3">
                    <asp:Label ID="lblProperties" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    Tần suất gửi
                </th>
                <td>
                    <asp:Label runat="server" ID="lblTriggerType" />
                </td>
                <th>
                    Thời điểm gửi
                </th>
                <td>
                    <asp:Label runat="server" ID="lblTriggerTime" />
                </td>
            </tr>
            <tr>
                <th>
                    Tiêu đề
                </th>
                <td colspan="3">
                    <asp:Label ID="lblSubject" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    Nội dung
                </th>
                <td colspan="3">
                    <tk:MultiLineLabel ID="lblContent" runat="server" Visible="false"></tk:MultiLineLabel>
                    <asp:LinkButton ID="lbtImage" runat="server" Text="Tải file" Font-Underline="false"
                        ForeColor="Blue" OnClick="lbtImage_Click" Visible="false"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
