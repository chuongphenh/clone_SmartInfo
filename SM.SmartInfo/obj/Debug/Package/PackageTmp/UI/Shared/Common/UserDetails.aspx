<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MasterPages/Common/Popup.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="SM.SmartInfo.UI.Shared.Common.UserDetails" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/Administrations/Users/UserDetailUC.ascx" TagPrefix="uc1" TagName="UserDetailUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:HiddenField runat="server" ID="hidId" />

    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="toolbar">
        THÔNG TIN NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="lnkExit" runat="server" OnClientClick='window.close();'> <i class="fas fa-sign-out-alt" aria-hidden="true" style="margin-top: 4px; font-size: 16pt; color:black" title="Thoát"></i> </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Text="Sửa" Style="float: right; margin-left: 5px"> 
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Sửa"> Sửa</i>
                </asp:HyperLink>
            </li>

        </ul>
    </div>

    <div style="margin-top: 50px;">
        <div class="body-content">
            <div style="background: #FFF; border: 1px solid #D9D9D9;">
                <uc1:UserDetailUC runat="server" ID="ucUserDetail" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#fileInput').change(function () {
                var file = $(this)[0].files[0];
                var formData = new FormData();
                formData.append('file', file);
                var hidIdValue = $('[id$="hidId"]').val();
                formData.append('hidId', hidIdValue);
                $.ajax({
                    url: 'UploadAjax.ashx', 
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        $('[id$="UserImage"]').attr('src', response);
                    },
                    error: function () {
                        alert('Đã xảy ra lỗi khi tải lên ảnh.');
                    }
                });
            });
        });
    </script>

</asp:Content>
