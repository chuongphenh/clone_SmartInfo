<%@ Page Title="Tạo mới nước sản xuất" Language="C#"  MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Country.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="HiddenVersion" runat="server" />
    <div class="toolbar">
        TẠO MỚI NƯỚC SẢN XUẤT
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton CssClass="icon-save" ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="false">Lưu</asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server" CssClass="icon-exit" Text="Thoát"></asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <table class="tableDisplay">
            <col width="250" />
            <col width="250" />
            <col width="250" />
            <col />
            <tr>
                <th>Mã <span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtCode" MaxLength="128" runat="server" Width="220"></asp:TextBox>
                </td>
                <th>Tên <span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtName" MaxLength="128" runat="server" Width="220"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Trạng thái
                </th>
                <td>
                    <asp:DropDownList ID="ddStatus" runat="server" DataTextField="Value" DataValueField="Key" />
                </td>
                <th>Thứ tự hiển thị</th>
                <td>
                    <tk:NumericTextBox ID="numDisplayOrder" runat="server" Width="220" NumberDecimalDigit="0"></tk:NumericTextBox>
                </td>
            </tr>
            <tr>
                <th>Ghi chú
                </th>
                <td colspan="3">
                    <tk:TextArea ID="txtDescription" runat="server" Width="100%" TextMode="MultiLine"
                        Rows="4">
                    </tk:TextArea>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
