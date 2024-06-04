<%@ Page Title="Tạo mới Đoạn đường" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Segments.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentAjax" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <div class="toolbar">
        TẠO MỚI ĐOẠN ĐƯỜNG
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton CssClass="icon-save" ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="true">Lưu</asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server" CssClass="icon-exit" Text="Thoát"></asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table class="tableDisplay">
            <col width="250" />
            <col width="250" />
            <col width="250" />
            <col />
            <tr>
                <th>Tỉnh/Thành phố
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProvince" Width="100%"
                        OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" />
                </td>
                <th>Quận/Huyện
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlDistrict" Width="100%"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <th>Đường/Phố <span class="star">*</span>
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlStreet" Width="100%" />
                </td>
                <th>Trạng thái
                </th>
                <td>
                    <asp:DropDownList ID="ddStatus" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <th>Đoạn từ <span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtSegmentFrom" MaxLength="256" runat="server" Width="100%"></asp:TextBox>
                </td>
                <th>Đoạn đến
                </th>
                <td>
                    <asp:TextBox ID="txtSegmentTo" MaxLength="256" runat="server" Width="100%"></asp:TextBox>
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