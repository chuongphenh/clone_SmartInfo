<%@ Page Title="Chỉnh sửa chỉ tiêu" Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
     Inherits="SM.SmartInfo.UI.Administrations.Targets.Edit" %>

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
        CHỈNH SỬA CHỈ TIÊU
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
                    Mã chỉ tiêu<span class="star">*</span>
                </th>
                <td>
                    <asp:Label ID="lblCode" runat="server"></asp:Label>
                </td>
                <th>
                    Tên chỉ tiêu<span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtName" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    Kết quả yêu cầu
                </th>
                <td>
                   <tk:TextArea ID="txtDescription"  runat="server" Width="100%" TextMode="MultiLine"
                        Rows="1"></tk:TextArea>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        var limitNum = 512;
        var counter = $get('counter');

        function LimitLength(mode) {
            var rtfEditor = $find("<%=txtDescription.ClientID %>");
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

            var rtfEditor = $find("<%=txtDescription.ClientID %>");
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
