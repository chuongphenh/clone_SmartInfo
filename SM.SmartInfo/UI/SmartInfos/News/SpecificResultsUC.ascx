<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpecificResultsUC.ascx.cs" Inherits="SM.SmartInfo.UI.SmartInfos.News.SpecificResultsUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/PressAgencySelector.ascx" TagPrefix="uc" TagName="PressAgencySelector" %>

<style>
    .table > tbody > tr {
        height: 50px;
    }

    .table th, .table td {
        min-width: 50px; 
        word-wrap: break-word; 
    }

    .table > tbody > tr > td {
        vertical-align: middle;
        font-size: 13px
    }

    .table > thead > tr {
        height: 50px;
    }

        .table > thead > tr > th {
            color: #595959;
            background: transparent !important;
            font-size: 14px;
            font-weight: 600;
            text-align: center;
            vertical-align: middle;
        }

    .center {
        font-size: 14px;
        text-align: center;
    }

    .aspNetDisabled {
        background: unset !important;
        color: darkgray !important;
        border-color: unset !important;
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

    .view-all {
        font-size: 12px;
        color: white;
        float: right;
        padding: 1px;
        padding-right: 15px;
        padding-left: 15px;
        border: 1px solid #597EF7;
        background: #597EF7;
        border-radius: 100px;
    }

        .view-all:hover {
            cursor: pointer;
        }
</style>

<asp:HiddenField ID="hidPage" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidNewsID" runat="server" />
<div class="row">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="row blog blog-medium">
        <div class="col-md-12 table-responsive" style="padding-left: 0px; padding-right: 0px">
            <table class="table" style="border: 1px solid #D9D9D9; box-sizing: border-box;">
                <colgroup>
                    <col style="width: 3%" />
                    <col style="width: 15%" />
                    <col style="width: 15%" />
                    <col style="width: 15%" />
                    <col style="width: 29%"/>
                    <col style="width: 15%" />
                    <col style="width: 8%"/>
                </colgroup>
                <thead>
                    <tr>
                        <th colspan="4" style="font-size: 16px; color: #262626; text-align: left; padding-left: 25px;">Kết quả cụ thể</th>
                        <th></th>
                        <th></th>
                        <th id="thAddNew" runat="server" style="text-align: right; padding-right: 25px;">
                            <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="font-size: 16pt; margin-left: 10px"><i class="fa fa-plus"></i></asp:LinkButton>
                        </th>
                    </tr>
                    <tr>
                        <th style="width: 3%">#</th>
                        <th style="text-align: left; width: 15%">Phương tiện</th>
                        <th style="text-align: left; width: 15%">Tuyến bài</th>
                        <th style="text-align: left; width: 15%">Kênh</th>
                        <th style="text-align: left" width: 32%">Tiêu đề</th>
                        <th style="width: 15%">Ngày đăng</th>
                        <th id="thEdit" runat="server" style="width: 8%"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPositiveNews" runat="server" OnItemDataBound="rptPositiveNews_ItemDataBound" OnItemCommand="rptPositiveNews_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="center">
                                    <%# (Container.ItemIndex + 1) + ((SM.SmartInfo.Utils.Utility.GetNullableInt(hidPage.Value) - 1) * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniSize) %>
                                    <asp:HiddenField ID="hidPositiveNewsID" runat="server" />
                                    <asp:HiddenField ID="hidType" runat="server" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server" Width="100%" Visible="false"></asp:DropDownList>
                                    <asp:Literal ID="ltrType" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="ltrCampaign" runat="server"></asp:Literal></td>
                                <asp:HiddenField ID="hidCampaignID" runat="server" />
                                <asp:HiddenField ID="hidBrief" runat="server" />
                                <asp:HiddenField ID="hidUrl" runat="server" />
                                <td>
                                    <asp:Literal ID="ltrPressAgencryName" runat="server"></asp:Literal></td>
                                <asp:HiddenField ID="hidPressAgencryID" runat="server" />
                                <td>
                                    <asp:LinkButton ID="btnTitle" runat="server"></asp:LinkButton>
                                    <asp:HiddenField ID="hidTitle" runat="server" />
                                </td>
                                <td class="center">
                                    <asp:Literal ID="ltrPublishDTG" runat="server"></asp:Literal></td>
                                <tk:DatePicker ID="dpkPublishDTG" runat="server" Width="100%" DateFormat="DMY" Visible="false" />
                                <td id="tdEdit" runat="server" class="center">
                                    <asp:LinkButton ID="btnEdit" runat="server" Style="margin-right: 5px"><i class="fas fa-pencil-alt" title="Sửa"></i></asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash" title="Xóa"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <asp:LinkButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click"><i class="fa fa-angle-left"></i></asp:LinkButton>
                            <span style="padding-right: 20px; padding-left: 20px">Trang <%= hidPage.Value %></span>
                            <asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click"><i class="fa fa-angle-right"></i></asp:LinkButton>
                        </td>
                        <td></td>
                        <td></td>
                        <td id="footerEdit" runat="server"></td>
                        <td colspan="2" style="text-align: right" class="center"><span style="font-size: 13px; color: #262626"></span></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>

<tk:PopupPanel ID="popEdit" runat="server" Title="THÊM MỚI/CẬP NHẬT KẾT QUẢ" Width="850" CancelButton="btnCancel" OnPopupClosed="popEdit_PopupClosed">
    <PopupTemplate>
        <table class="table" style="width: 100%; margin-bottom: 0px">
            <colgroup>
                <col width="200" />
                <col />
            </colgroup>
            <tr>
                <th>Phương tiện</th>
                <td>
                    <asp:HiddenField ID="hidPositiveNewsID" runat="server"></asp:HiddenField>
                    <asp:DropDownList ID="ddlType" runat="server" Width="100%" MaxLength="256"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Tuyến bài
                </th>
                <td>
                    <asp:DropDownList ID="ddlCampaignID" runat="server" Width="100%"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Kênh truyền thông
                </th>
                <td>
                    <uc:PressAgencySelector runat="server" Width="100%" ID="ucPressAgencySelector" DataTextField="Name" IsSearchAll="true" />
                </td>
            </tr>
            <tr>
                <th>Tiêu đề <span class="star">*</span>
                </th>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Ngày phát hành
                </th>
                <td>
                    <tk:DatePicker ID="dpkPublishDTG" runat="server" Width="100%" DateFormat="DMY" />
                </td>
            </tr>
            <tr>
                <th>Tóm tắt
                </th>
                <td>
                    <tk:TextArea ID="txtBrief" runat="server" Width="100%" TextMode="MultiLine" Rows="3" />
                </td>
            </tr>
            <tr>
                <th>Link
                </th>
                <td>
                    <asp:TextBox ID="txtUrl" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr id="trImage" runat="server">
                <th>Hình ảnh
                    <asp:Button ID="btnShowPopupUpload" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px"></asp:Button>
                </th>
                <td>
                    <asp:Repeater ID="rptImage" runat="server" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
                        <ItemTemplate>
                            <div class="col-md-3" style="padding-left: 0px; margin-top: 5px">
                                <div id="divViewDetailImage" runat="server" class="div-link">
                                    <img id="img" runat="server" src="" style="width: 99%; height: 95px; border-radius: 8px; object-fit: cover" />
                                </div>
                                <asp:LinkButton ID="btnDeleteImage" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" Style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times" title="Xóa ảnh"></i></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <div class="popup-toolbar" style="text-align: center">
            <asp:Button runat="server" ID="btnSave" Text="Lưu" OnClick="btnSave_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>

<tk:PopupPanel runat="server" ID="popUpload" Title="Tải tài liệu" Width="500px" Height="100px" CancelButton="btnCancelUpload">
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
            <asp:Button runat="server" ID="btnCancelUpload" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>

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
