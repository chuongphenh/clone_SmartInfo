<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentUC.ascx.cs" Inherits="SM.SmartInfo.UI.UserControls.Attachment.AttachmentUC" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:HiddenField ID="hidRefID" runat="server" />
<asp:HiddenField ID="hidRefType" runat="server" />
<asp:HiddenField ID="hidAllowEdit" runat="server" />

<style>
    .flex_popup-content {
        max-height: 250px;
    }

    .custom-file-input {
        opacity: 0;
        z-index: 2;
        position: relative;
    }

    .custom-file-label {
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
</style>

<div class="toolbar">
    DANH SÁCH TÀI LIỆU
    <ul class="icon_toolbar">
        <li>
            <asp:LinkButton runat="server" ID="btnShowUpload" OnClick="btnShowUpload_Click"><i class="fa fa-upload new-button" title="Tải lên" ></i> </asp:LinkButton>
        </li>
    </ul>
</div>
<div class="row content">
    <asp:DataGrid ID="grdData" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="false"
        AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid-main" OnItemDataBound="grdData_ItemDataBound"
        OnItemCommand="grdData_ItemCommand">
        <HeaderStyle CssClass="grid-header" HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemStyle CssClass="grid-item-even" HorizontalAlign="Left" />
        <AlternatingItemStyle CssClass="grid-item-odd" />
        <Columns>
            <asp:TemplateColumn HeaderText="Tên tài liệu" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidAttachmentID" />
                    <asp:Literal runat="server" ID="ltrName" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Ngày đưa lên" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="ltrCreatedDTG" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Người đưa lên">
                <ItemTemplate>
                    <asp:Literal runat="server" ID="ltrCreatedBy" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Tải về" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblDownLoad" runat="server">
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Tải lên" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="btnUpLoad" runat="server" CommandName="Upload"><i class="fa fa-lg fa-cloud-upload" aria-hidden="true"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Xóa" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="grid-delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
    <tk:PopupPanel runat="server" ID="popUpload" Title="Tải tài liệu" Width="500px" Height="100px"
        CancelButton="btnCancel" OnPopupClosed="popUpload_PopupClosed">
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
                                <asp:FileUpload runat="server" ID="fileUpload" class="custom-file-input" Style="width: 100%"></asp:FileUpload>
                                <div id="errUpload" class="err" style="display: none;">
                                    <ul>
                                        <li>Chỉ tải được file (Ảnh)</li>
                                    </ul>
                                </div>
                                <label id="lblCustom" class="custom-file-label" for="customFile">Choose file</label>
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
</div>

<script>
    $("#<%= fileUpload.ClientID %>").height($("#lblCustom").height() + 10);

    $(".custom-file-input").on("change", function (event) {
        var file = event.target.files[0];
        if (validateFile(file)) {
            var fileName = $(this).val().split("\\").pop();
            $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
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