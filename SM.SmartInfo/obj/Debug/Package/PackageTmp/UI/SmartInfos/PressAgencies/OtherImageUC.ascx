<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtherImageUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.PressAgencies.OtherImageUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .custom-file-input-other-image {
        opacity: 0;
        z-index: 2;
        position: relative;
    }

    .custom-file-label-other-image {
        z-index: 1;
        padding: .375rem .75rem;
        font-weight: 400;
        line-height: 1.5;
        color: #495057;
        background-color: #fff;
        border: 1px solid #ced4da;
        border-radius: .25rem;
        margin-bottom: 10px;
        position: absolute;
        top: 85px;
        width: 490px;
    }

    .delete-other-image {
        color: #595959;
        margin-top: 4px;
        font-size: 12px;
        background: #F2F3F8;
        padding-bottom: 3px;
        padding-top: 3px;
        padding-left: 5px;
        padding-right: 5px;
        border-radius: 4px;
    }

        .delete-other-image:hover {
            color: #597EF7 !important;
            margin-top: 4px;
            font-size: 12px;
            background: #F2F3F8;
            padding-bottom: 3px;
            padding-top: 3px;
            padding-left: 5px;
            padding-right: 5px;
            border-radius: 4px;
        }

    .description-image {
        height: 30px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
        overflow: hidden;
        font-size:13px;
    }
</style>

<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidPressAgencyID" runat="server" />
<div class="row">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="headline" style="text-align: center; position: relative; top: -49px; right: 0px;">
        <asp:LinkButton ID="btnShowPopupUpload" runat="server" OnClick="btnShowPopupUpload_Click" Style="font-size: 16pt; float: right; padding-right: 5px">
            <i class="fa fa-upload" title="Upload ảnh khác"></i>
        </asp:LinkButton>
    </div>
    <asp:Repeater ID="rptImage" runat="server" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
        <ItemTemplate>
            <div class="col-md-3" style="padding-left: 0px; text-align: center">
                <div id="divViewDetailImage" runat="server" class="div-link" style="margin-bottom: 5px">
                    <img id="img" runat="server" src="" style="width: 99%; height: 95px; border-radius: 8px; object-fit: cover" />
                </div>
                <asp:Label ID="lblDescription" runat="server" class="description-image"></asp:Label>
                <asp:LinkButton ID="btnDeleteImage" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" Style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times delete-other-image" title="Xóa ảnh"></i></asp:LinkButton>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

<tk:PopupPanel runat="server" ID="popUpload" Title="Tải tài liệu" Width="500px" Height="100px" CancelButton="btnCancel">
    <PopupTemplate>
        <div class="popup-content" style="margin-top: 0px; width: 100%; height: 100%">
            <table class="tabLogin" style="width: 100%">
                <colgroup>
                    <col width="50%" />
                    <col />
                </colgroup>
                <tr style="height: 30px; font-weight: bold;">
                    <td colspan="2">Chọn tài liệu
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="custom-file mb-3">
                            <asp:FileUpload runat="server" ID="fileUpload" class="custom-file-input-other-image" Style="width: 100%; height: 28px"></asp:FileUpload>
                            <div id="errUpload" class="err" style="display: none;">
                                <ul>
                                    <li>Chỉ tải được file (Ảnh)</li>
                                </ul>
                            </div>
                            <label id="lblCustom" class="custom-file-label-other-image" for="customFile">Choose file</label>
                        </div>
                    </td>
                </tr>
                <tr style="height: 30px; font-weight: bold">
                    <td colspan="2" style="padding-top: 15px; padding-bottom: 5px;">Mô tả tài liệu
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <tk:TextArea runat="server" ID="txtDescription" Width="100%" MaxLength="512" TextMode="MultiLine" Rows="3" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="popup-toolbar" style="text-align: center">
            <asp:Button runat="server" ID="btnPopupUpload" Text="Tải lên" OnClick="btnPopupUpload_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>

<script>
    $(".custom-file-input-other-image").on("change", function (event) {
        var file = event.target.files[0];
        if (validateFile(file)) {
            var fileName = $(this).val().split("\\").pop();
            $(this).siblings(".custom-file-label-other-image").addClass("selected").html(fileName);
        }
    });

    function validateFile(file) {
        var extension = getExtension(file.name);
        if (extension != "jpg"
            && extension != "JPG"
            && extension != "jpeg"
            && extension != "JPEG"
            && extension != "png"
            && extension != "PNG") {
            $('#<%= fileUpload.ClientID %>').val("");
            setTimeout(function () {
                $('#errUpload').hide('blind', {}, 500)
            }, 15000)
            $('#errUpload').show();
            return false;
        }

        return true;
    }

    function getExtension(filename) {
        var parts = filename.split('.');
        return parts[parts.length - 1];
    }
</script>