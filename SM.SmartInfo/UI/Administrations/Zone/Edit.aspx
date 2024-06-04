<%@ Page Title="Sửa miền" Language="C#"  MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Zone.Edit" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="HiddenVersion" runat="server" />
    <div class="toolbar">
        SỬA MIỀN
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Lưu"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Thoát"></i>
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
                <th>Mã <span class="star">*</span>
                </th>
                <td>
                    <asp:Label ID="txtCode" MaxLength="128" runat="server" Width="200px"></asp:Label>
                </td>
                <th>Tên <span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtName" MaxLength="128" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Trạng thái
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddStatus" runat="server" DataTextField="Value" DataValueField="Key" />
                </td>
            </tr>
            <tr>
                <th>Ghi chú
                </th>
                <td colspan="3">
                    <tk:TextArea ID="txtDescription" Font-Size="Small" runat="server" Width="100%" TextMode="MultiLine"
                        Rows="4">
                    </tk:TextArea>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
