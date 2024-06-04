<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.NegativeNews.EditUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/NegativeNews/NewsResearchedUC.ascx" TagName="NewsResearchedUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/NegativeNews/ListNagativeNewsUC.ascx" TagName="ListNagativeNewsUC" TagPrefix="uc" %>

<style>
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

    .image-detail {
        height: 95px;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 1;
        overflow: hidden;
    }

    .flex_timepicker-combo {
        height: 29px;
    }

    .fa-times {
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

        .fa-times:hover {
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

    .div-link:hover {
        cursor: pointer;
    }

    .btnSaveContinue {
        float: right;
        margin-top: -5px;
        padding-right: 15px;
        padding-left: 15px;
        padding-bottom: 2px;
        padding-top: 0px;
        border: 1px solid #597EF7;
        background: #597EF7;
        border-radius: 100px;
        color: white;
        font-size: 10px;
        text-align: center;
    }

        .btnSaveContinue:hover {
            text-decoration: unset;
            color: white;
        }
</style>

<div style="position: fixed; right: 10px; top: 50%; opacity: 1; z-index: 9">
    <asp:LinkButton ID="btnSaveContinues" class="btnSaveContinue" OnClick="btnSaveContinues_Click" runat="server">
        <i class="far fa-save" style="color: white; margin-top: 4px; font-size: 16px; border-radius: 4px;"></i>
        <span style="font-size: 10px; display: inherit;">&nbsp; Lưu & Tiếp tục</span>
    </asp:LinkButton>
</div>
<asp:HiddenField ID="hidNewsID" runat="server" />
<div style="width: 100%; padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px; text-align: left">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <%--CONTENT--%>
    <div class="row">
        <div class="col-md-8" style="padding-left: 0px;font-size: 16px;">
            Tên sự vụ <span class="star">*</span>
        </div>
        <div class="col-md-4" style="padding-right: 0px; padding-bottom: 5px">
            <asp:LinkButton ID="btnExit" runat="server" OnClick="btnExit_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: #F2F3F8; padding-bottom: 5px; padding-right: 10px; border-radius: 4px;">
                <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px;  border-radius: 4px;" title="Thoát"></i>
                <span style="color: #595959;font-weight:600;font-size:16px;">Thoát</span>
            </asp:LinkButton>
            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: #F2F3F8; padding-bottom: 5px; padding-right: 10px; border-radius: 4px;">
                <i class="far fa-save" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; border-radius: 4px;" title="Lưu"></i>
                <span style="color: #595959;font-weight:600;font-size:16px;">Lưu</span>
            </asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="padding-left: 0px">
            <asp:TextBox ID="txtName" runat="server" placeholder="Tên sự vụ" Width="100%" MaxLength="256"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-md-10" style="padding-left: 0px; margin-top: 10px;font-size: 16px;">
            Ngày phát sinh <span class="star">*</span>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="padding-left: 0px; margin-top: 10px">
            <tk:DatePicker ID="dpkIncurredDTG" runat="server" Width="130px" DateFormat="DMY" />
            <tk:TimePicker runat="server" ID="dpkHourIncurredDTG" Width="60px" ShowSecond="false" Height="28px"></tk:TimePicker>
            <hr style="margin-bottom: 0px; margin-top: 10px" />
        </div>
    </div>
    <div class="row" style="margin-top: 20px;">
        <div class="col-md-4" style="text-align: left; padding-left: 0px;">
            <i class="fa fa-clock" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Loại sự vụ <span class="star">*</span></span>
        </div>
        <div class="col-md-4" style="text-align: left;font-size: 16px;">
            <i class="fa fa-link" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Mức độ sự vụ <span class="star">*</span></span>
        </div>
        <div class="col-md-4" style="text-align: left;font-size: 16px;">
            <i class="fa fa-tags" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Tình trạng <span class="star">*</span></span>
        </div>
    </div>
    <div class="row" style="margin-top: 5px">
        <div class="col-md-4" style="text-align: left; padding-left: 0">
            <asp:DropDownList ID="ddlNegativeType" runat="server" Width="100%"></asp:DropDownList>
            <hr style="margin-bottom: 0px; margin-top: 10px" />
        </div>

        <div class="col-md-4" style="text-align: left; padding-left: 10px">
            <asp:DropDownList ID="ddlClassification" runat="server" Width="100%"></asp:DropDownList>
            <hr style="margin-bottom: 0px; margin-top: 10px" />
        </div>
        <div class="col-md-4" style="text-align: left; padding-left: 10px">
            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%"></asp:DropDownList>
            <hr style="margin-bottom: 0px; margin-top: 10px" />
        </div>
    </div>
    <div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fa fa-image" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Hình ảnh</span>
            <span class="view-all" id="view-all-image">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
            <asp:Button ID="btnShowPopupUpload" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px"></asp:Button>
        </div>
    </div>
    <div class="row image-detail" style="margin: 35px 0; min-height: 102px" id="divImage" runat="server">
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
    </div>
   <%-- <div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="far fa-chart-bar" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Đánh giá chi tiết <span class="star">*</span></span>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <tk:TextArea ID="txtRatedLevel" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
        </div>
    </div>--%>
    <div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fas fa-user-friends" style="margin-right: 10px; color: #8C8C8C"></i>
            <asp:Label ID="lblTitlePressAgency" runat="server" Style="color: #595959;font-size: 16px;" Text="Thông tin cá nhân, tổ chức liên quan <span class='star'>*</span>"></asp:Label>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <tk:TextArea ID="txtPressAgency" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></tk:TextArea>
        </div>
    </div>
    <%--<div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="far fa-file-alt" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Đề xuất phương án xử lý <span class="star">*</span></span>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <tk:TextArea ID="txtResolution" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
        </div>
    </div>
    <div class="row" style="margin-top: 15px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fa fa-check-circle" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Phê duyệt phương án xử lý <span class="star">*</span></span>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <tk:TextArea ID="txtResolutionContent" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
        </div>
    </div>--%>
    <%--<uc:NewsResearchedUC ID="ucNewsResearched" runat="server" />--%>
    <div class="row" style="margin-top: 25px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fas fa-thumbtack" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Thông tin tổng quan <span class="star">*</span></span>
        </div>
    </div>
    <div class="row" style="margin-top: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <tk:TextArea ID="txtConcluded" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></tk:TextArea>
        </div>
    </div>
    <%--<uc:ListNagativeNewsUC ID="ucListNagativeNews" runat="server" />--%>
    <%--END CONTENT--%>
    <hr style="margin-bottom: 10px; margin-top: 10px;" />
    <div class="row" style="margin-top: 10px; padding-bottom: 10px" id="divComment" runat="server">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fa fa-comments" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size: 16px;">Bình luận</span>
        </div>
    </div>
    <uc:CommentUC ID="ucComment" runat="server" />
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
                    <td colspan="2">
                        Chọn tài liệu
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="custom-file mb-3">
                            <asp:FileUpload runat="server" ID="fileUpload" class="custom-file-input" Style="width: 100%"></asp:FileUpload>
                            <div id="errUpload" class="err" style="display: none;">
                                <ul>
                                    <li>Chỉ tải được file (Ảnh và .pdf)</li>
                                </ul>
                            </div>
                            <label id="lblCustom" class="custom-file-label" for="customFile">Choose file</label>
                        </div>
                    </td>
                </tr>
                <tr style="height: 30px; font-weight: bold">
                    <td colspan="2" style="padding-top: 15px; padding-bottom: 5px;">
                        Mô tả tài liệu
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
    $(document).ready(function () {
        $('#view-all-image').on('click', function () {
            if ($('#<%= divImage.ClientID %>').hasClass('image-detail')) {
                $('#<%= divImage.ClientID %>').removeClass('image-detail');
                $('#view-all-image').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
            }
            else {
                $('#<%= divImage.ClientID %>').addClass('image-detail');
                $('#view-all-image').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
            }
        });
    });

   <%-- $('#<%= ddlNegativeType.ClientID %>').on('change', function () {
        if (this.value === "1")
            $('#<%= lblTitlePressAgency.ClientID %>').html('Cơ quan báo chí liên hệ <span class="star">*</span>');
        else
            $('#<%= lblTitlePressAgency.ClientID %>').html('Các báo đăng tải <span class="star">*</span>');
    });--%>

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
            && extension != "pdf"
            && extension != "PDF"
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
