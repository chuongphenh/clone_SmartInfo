<%@ Page Title="Tạo mới Đường/Phố/Đại lộ" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Street.AddNew" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls"
    TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="HiddenVersion" runat="server" />
    <div class="toolbar">
        TẠO MỚI ĐƯỜNG/PHỐ/ĐẠI LỘ
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server" Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Lưu"> Lưu</i>
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
        <table cellpadding="0" cellspacing="0" class="tableDisplay" width="100%">
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
                <th>Tỉnh/Thành phố
                </th>
                <td align="left">
                    <asp:DropDownList runat="server" ID="ddlProvince" Width="220" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"
                        AutoPostBack="true" />
                </td>
                <th>Quận/Huyện
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlDistrict" Width="220" />
                </td>
            </tr>
            <tr>
                <th>Trạng thái
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddStatus" runat="server" DataTextField="Value" DataValueField="Key"
                        Width="220" />
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
