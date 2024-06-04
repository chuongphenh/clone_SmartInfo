<%@ Page Title="Chỉnh sửa mấu Email/Sms" Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
     Inherits="SM.SmartInfo.UI.Administrations.Emails.Edit" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidBinaryContent" />
    <asp:HiddenField ID="hidId" runat="server" />
    <asp:HiddenField ID="hidVersion" runat="server" />
    <asp:HiddenField ID="hiTransformType" runat="server" />
    <style>
               .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
            color: #000;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <div class="toolbar">
        CHỈNH SỬA MẪU EMAIL/SMS
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server"
                    Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black;  font-size: 16px;" title="Lưu"></i>
                    <span style="color:black;font-weight:700">Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black;  font-size: 16px;" title="Thoát"></i>
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
                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                </td>
                <th>
                    Tên mẫu<span class="star">*</span>
                </th>
                <td>
                    <asp:Label ID="lblName" runat="server" />
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
                    <asp:Label ID="lblProperties" runat="server" Width="200" />
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
                        ValidationExpression="(.*?)\.(doc|docx|html|xsl)$">
                    </asp:RegularExpressionValidator>
                    <asp:LinkButton ID="lbtImage" runat="server" Text="Tải file" Font-Underline="false"
                        ForeColor="Blue" Visible="false" OnClick="lbtImage_Click"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        var limitNum = 512;
        var counter = $get('counter');

        function LimitLength(mode) {
            var rtfEditor = $find("<%=txtContent.ClientID %>");
            if (mode == 2) {
                var oValue = rtfEditor.get_textArea().innerHTML.trim().length;
            }
            else { var oValue = rtfEditor.get_html(true).trim(); }

            if (oValue.length >= limitNum) {
                rtfEditor.set_html(oValue.substring(0, limitNum - 1));
            }
            counter.innerHTML = "Characters left: " + (limitNum - oValue.length);
        }

        function AttachHandlers(mode) {

            var rtfEditor = $find("<%=txtContent.ClientID %>");
            if (mode == 1) {
                rtfEditor.attachEventHandler("onkeyup", LimitLength);
                rtfEditor.attachEventHandler("onpaste", LimitLength);
                rtfEditor.attachEventHandler("onblur", LimitLength);
            }
            else {
                var textarea = rtfEditor.get_textArea();

                if (window.attachEvent) {
                    textarea.attachEvent("onkeydown", LimitLength);
                    textarea.attachEvent("onpaste", LimitLength);
                    textarea.attachEvent("onblur", LimitLength);
                }
                else {
                    textarea.addEventListener("keyup", LimitLength, true);
                    textarea.addEventListener("paste", LimitLength, true);
                    textarea.addEventListener("blur", LimitLength, true);
                }
            }
        }

        function OnClientLoad(editor, args) {
            rtfEditor = editor;
            AttachHandlers(1);
            LimitLength(1);


            editor.add_modeChange(function (sender, args) {
                var mode = sender.get_mode();
                if (mode == 1 || mode == 2) {
                    AttachHandlers(mode);
                    LimitLength(mode);
                }
            });
        }
    </script>
</asp:Content>
