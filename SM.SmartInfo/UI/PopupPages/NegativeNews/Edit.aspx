<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPages/Common/Popup.Master" CodeBehind="Edit.aspx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.NegativeNews.Edit" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/PressAgencySelector.ascx" TagPrefix="uc" TagName="PressAgencySelector" %>
<%@ Register Src="~/UI/PopupPages/NegativeNews/NegativeNewsResearchedUC.ascx" TagPrefix="uc" TagName="NegativeNewsResearchedUC" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

        hr {
            margin-top: 15px;
            margin-bottom: 15px;
            border: 0;
        }

        span {
            font-size: 14px;
            color: #262626;
        }

        .image-detail {
            height: 95px;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }

        .icon_toolbar li a {
            margin: 0px 0px 0px 5px;
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

        .flex_timepicker-combo {
            height: 29px;
            width: 50px !important;
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
    </style>
    <asp:HiddenField runat="server" ID="hidId" />
    <asp:HiddenField runat="server" ID="hidNewsId" />
    <asp:HiddenField runat="server" ID="hidVersion" />
    <div class="toolbar" style="background: #F2F3F8; border: unset; padding: 0 35px; font-family: SF Pro Display">
        <span style="color: #595959">CHI TIẾT</span>
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server">
                    <i class="far fa-save" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Hoàn thành"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thoát"></i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="body-content" style="background: #F2F3F8">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <%--DESIGN--%>
        <div class="row" style="padding: 20px; padding-top: 30px;">
            <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px;">
                <div id="DaPhatSinh" runat="server" style="width: 100%; padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px; text-align: left">
                    <%--CONTENT--%>
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Loại sự vụ <span class="star">*</span></span>
                        </div>
                        <div class="col-md-11" style="width: 85%">
                            <asp:DropDownList ID="ddlType" runat="server" Width="100%" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tên vụ việc <span class="star">*</span></span>
                        </div>
                        <div class="col-md-11" style="width: 85%">
                            <asp:TextBox ID="txtName" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Thời gian <span class="star">*</span></span>
                        </div>
                        <div class="col-md-3" style="width: 35%">
                            <tk:DatePicker ID="dpkIncurredDTG" runat="server" DateFormat="DMY" Width="150" />
                            <tk:TimePicker runat="server" ID="dpkHourIncurredDTG" Width="50%" ShowSecond="false"></tk:TimePicker>
                        </div>
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Kênh đăng bài</span>
                        </div>
                        <div class="col-md-3" style="width: 35%">
                            <uc:PressAgencySelector runat="server" ID="ucPressAgencySelector" DataTextField="Name" IsSearchAll="true" Width="100%" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Link bài</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px;">
                            <tk:TextArea runat="server" ID="txtURL" Width="100%" TextMode="MultiLine" Rows="3" MaxLength="256" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Hình ảnh</span>
                            <span class="view-all" id="view-all-image">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                            <asp:Button ID="btnShowPopupUpload" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px"></asp:Button>
                        </div>
                    </div>
                    <div class="row image-detail" style="margin-top: 10px; min-height: 102px" id="div-image">
                        <div class="col-md-12">
                            <asp:Repeater ID="rptImage" runat="server" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-3" style="padding-left: 0px; margin-top: 5px">
                                        <div id="divViewDetailImage" runat="server" class="div-link">
                                            <img id="img" runat="server" src="" style="width: 99%; height: 95px; border-radius: 8px" />
                                        </div>
                                        <asp:LinkButton ID="btnDeleteImage" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" Style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times" title="Xóa ảnh"></i></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tiêu đề</span>
                        </div>
                        <div class="col-md-11" style="width: 85%">
                            <asp:TextBox runat="server" ID="txtTitle" Width="100%" MaxLength="256" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Mức độ ảnh hưởng</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px;">
                            <tk:TextArea runat="server" ID="txtJudged" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Cách xử lý</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtMethodHandle" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Kết quả</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtResult" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Ghi chú</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtNote" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <%--END CONTENT--%>
                </div>
                <div id="ChuaPhatSinh" visible="false" runat="server" style="width: 100%; padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px; text-align: left">
                    <%--CONTENT--%>
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Loại sự vụ <span class="star">*</span></span>
                        </div>
                        <div class="col-md-11" style="width: 85%">
                            <asp:DropDownList ID="ddlType1" runat="server" Width="100%" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tên vụ việc <span class="star">*</span></span>
                        </div>
                        <div class="col-md-11" style="width: 85%">
                            <asp:TextBox ID="txtName1" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Thời gian <span class="star">*</span></span>
                        </div>
                        <div class="col-md-5" style="width: 35%">
                            <tk:DatePicker ID="dpkIncurredDTG1" runat="server" Width="150px" DateFormat="DMY" />
                            <tk:TimePicker runat="server" ID="dpkHourIncurredDTG1" Width="50px" ShowSecond="false"></tk:TimePicker>
                        </div>
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Địa điểm</span>
                        </div>
                        <div class="col-md-3" style="width: 35%">
                            <asp:TextBox ID="txtPlace" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tổ chức</span>
                        </div>
                        <div class="col-md-11" style="width: 85%">
                            <uc:PressAgencySelector runat="server" ID="ucPressAgencySelectorChuaXayRa" Width="100%" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Hình ảnh</span>
                            <span class="view-all" id="view-all-Img">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                            <asp:Button ID="btnShowPopupUpload1" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px"></asp:Button>
                        </div>
                    </div>
                    <div class="row image-detail" style="margin-top: 10px; min-height: 102px" id="div-Img">
                        <div class="col-md-12">
                            <asp:Repeater ID="rptImg" runat="server" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-3" style="padding-left: 0px; margin-top: 5px">
                                        <div id="divViewDetailImage" runat="server" class="div-link">
                                            <img id="img" runat="server" src="" style="width: 99%; height: 95px; border-radius: 8px" />
                                        </div>
                                        <asp:LinkButton ID="btnDeleteImage" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" Style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times" title="Xóa ảnh"></i></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Kết quả xử lý</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtResultNoIncurred" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Thông tin phóng viên</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtReporterInformation" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tóm tắt nội dung</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtQuestion" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Câu hỏi chi tiết</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtQuestionDetail" Width="100%" TextMode="MultiLine" Rows="3" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <uc:NegativeNewsResearchedUC runat="server" ID="ucNegativeNewsResearched" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Hình thức phương án giải quyết</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtResolution" Width="100%" TextMode="MultiLine" Rows="5" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Nội dung phương án giải quyết</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 10px; min-height: 39px">
                            <tk:TextArea runat="server" ID="txtResolutionContent" Width="100%" TextMode="MultiLine" Rows="5" />
                        </div>
                    </div>
                    <%--END CONTENT--%>
                </div>
            </div>
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
        <%--END DESIGN--%>
    </div>

    <script>
        $(document).ready(function () {
            $('#view-all-image').on('click', function () {
                if ($('#div-image').hasClass('image-detail')) {
                    $('#div-image').removeClass('image-detail');
                    $('#view-all-image').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#div-image').addClass('image-detail');
                    $('#view-all-image').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-Img').on('click', function () {
                if ($('#div-Img').hasClass('image-detail')) {
                    $('#div-Img').removeClass('image-detail');
                    $('#view-all-Img').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#div-Img').addClass('image-detail');
                    $('#view-all-Img').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });
        });

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
</asp:Content>