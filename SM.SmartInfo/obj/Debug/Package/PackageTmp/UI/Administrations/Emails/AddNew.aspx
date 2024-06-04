<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.Administrations.Emails.AddNew" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidBinaryContent" />
    <asp:HiddenField ID="hidId" runat="server" />
                <style>
                .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <div class="toolbar">
        THÊM MỚI SỬA MẪU EMAIL/SMS
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server"
                    Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"></i>
                    <span style="color:black;font-weight:700">Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server" >
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
                <th>
                    Mã mẫu<span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" MaxLength="256" Width="200"></asp:TextBox>
                </td>
                <th>
                    Tên mẫu<span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="256" Width="200" />
                </td>
            </tr>
            <tr>
                <th>
                    Loại mẫu<span class="star">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="ddTemplateType" runat="server" DataTextField="Value" DataValueField="Key"
                        Width="200">
                    </asp:DropDownList>
                </td>
                <th>
                    Cách thức sinh<span class="star">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="ddTransformType" AutoPostBack="true" OnSelectedIndexChanged="ddTransformType_OnSelectedIndexChanged"
                        runat="server" DataTextField="Value" DataValueField="Key" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    Trạng thái
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddStatus" runat="server" DataTextField="Value" DataValueField="Key"
                        Width="200" />
                </td>
            </tr>
            <tr>
                <th>
                    Thuộc tính
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtProperties" runat="server" MaxLength="512" Width="200" />
                </td>
            </tr>
            <tr>
                <th>
                    Tần suất gửi
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTriggerType" OnSelectedIndexChanged="ddlTriggerType_OnSelectedIndexChanged" AutoPostBack="true" />
                </td>
                <th>
                    Thời điểm gửi
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTriggerTime" /> (<asp:Label runat="server" ID="lblTriggerTimeNote" Font-Italic="true" />)
                </td>
            </tr>
            <tr>
                <th>
                    Tiêu đề
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtSubject" runat="server" Width="300" MaxLength="256" />
                </td>
            </tr>
            <tr>
                <th>
                    Nội dung
                </th>
                <td colspan="3">
                    <tk:TextArea ID="txtContent" Visible="false" runat="server" Width="100%" TextMode="MultiLine"
                        Rows="7"></tk:TextArea>
                    <asp:FileUpload ID="fileUpload" runat="server" Width="300" />
                    <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="fileUpload"
                        Display="None" ForeColor="Red" ErrorMessage="Chỉ cho phép upload file word or html"
                        ValidationExpression="(.*?)\.(doc|docx|html|xsl)$"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
