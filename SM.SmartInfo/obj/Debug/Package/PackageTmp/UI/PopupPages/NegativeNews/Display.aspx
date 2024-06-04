<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPages/Common/Popup.Master" CodeBehind="Display.aspx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.NegativeNews.Display" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
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

        .description-detail {
            height: 39px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
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

        .div-display {
            border-radius: 25px;
            padding-bottom: 5px;
            padding-top: 5px;
            padding-right: 20px !important;
            padding-left: 20px !important;
            background: #f2f3f8 !important;
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
    </style>
    <asp:HiddenField runat="server" ID="hidId" />
    <asp:HiddenField runat="server" ID="hidNewsId" />
    <asp:HiddenField runat="server" ID="hidVersion" />
    <div class="toolbar" style="background: #F2F3F8; border: unset; padding: 0 35px; font-family: SF Pro Display">
        <span style="color: #595959">CHI TIẾT</span>
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnExportWord" runat="server" OnClick="btnExportWord_Click">
                    <i class="fas fa-file-word" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Xuất phiếu trình chi tiết sự vụ"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Text="Sửa"> 
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Sửa"></i>
                </asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnFinish" OnClick="btnFinish_Click" runat="server">
                    <i class="fa fa-check" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Hoàn thành"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lnkExit" runat="server" OnClientClick='window.close();'>
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thoát"></i>
                </asp:LinkButton>
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
                <div id="DaPhatSinh" visible="false" runat="server" style="width: 100%; padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px; text-align: left">
                    <%--CONTENT--%>
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Loại sự vụ</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:Label ID="ltrType" runat="server" Style="font-size: 14px"></asp:Label>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tên vụ việc</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:Label ID="ltrName" runat="server" Style="font-size: 14px"></asp:Label>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Thời gian</span>
                        </div>
                        <div class="col-md-3 div-display" style="width: 35%">
                            <asp:Label ID="ltrIncurredDTG" runat="server" />
                        </div>
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Kênh đăng bài</span>
                        </div>
                        <div class="col-md-3 div-display" style="width: 35%%">
                            <asp:Label runat="server" ID="ltrPressAgencyID" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Link bài</span>
                            <span class="view-all" id="view-all-URL">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px;">
                            <asp:Label runat="server" ID="ltrURL" class="description-detail" />
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
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:Label runat="server" ID="ltrTitle" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Mức độ ảnh hưởng</span>
                            <span class="view-all" id="view-all-Judged">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px;">
                            <asp:Label runat="server" ID="ltrJudged" class="description-detail" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Cách xử lý</span>
                            <span class="view-all" id="view-all-MethodHandle">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrMethodHandle" class="description-detail" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Kết quả</span>
                            <span class="view-all" id="view-all-Result">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrResult" class="description-detail" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Ghi chú</span>
                            <span class="view-all" id="view-all-Note">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrNote" class="description-detail" />
                        </div>
                    </div>
                    <%--END CONTENT--%>
                </div>
                <div id="ChuaPhatSinh" visible="false" runat="server" style="width: 100%; padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px; text-align: left">
                    <%--CONTENT--%>
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Loại sự vụ</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:Label ID="ltrType1" runat="server" Style="font-size: 14px"></asp:Label>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tên vụ việc</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:Label ID="ltrName1" runat="server" Style="font-size: 14px"></asp:Label>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Thời gian</span>
                        </div>
                        <div class="col-md-5 div-display" style="width: 20%">
                            <asp:Label ID="ltrIncurredDTG1" runat="server" />
                        </div>
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Địa điểm</span>
                        </div>
                        <div class="col-md-3 div-display" style="width: 50%">
                            <asp:Label runat="server" ID="ltrPlace" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tổ chức</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:Label runat="server" ID="ltrPressAgencyIDNoIncurred" />
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
                            <span class="view-all" id="view-all-ResultNoIncurred">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrResultNoIncurred" class="description-detail" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Thông tin phóng viên</span>
                            <span class="view-all" id="view-all-ReporterInformation">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrReporterInformation" class="description-detail" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tóm tắt nội dung</span>
                            <span class="view-all" id="view-all-Question">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrQuestion" class="description-detail" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Câu hỏi chi tiết</span>
                            <span class="view-all" id="view-all-QuestionDetail">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrQuestionDetail" class="description-detail" />
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
                            <span class="view-all" id="view-all-Resolution">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrResolution" class="description-detail" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Nội dung phương án giải quyết</span>
                            <span class="view-all" id="view-all-ResolutionContent">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 div-display" style="margin-top: 10px; min-height: 39px">
                            <asp:Label runat="server" ID="ltrResolutionContent" class="description-detail" />
                        </div>
                    </div>
                    <%--END CONTENT--%>
                </div>
            </div>
        </div>
        <%--END DESIGN--%>
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

            $('#view-all-Judged').on('click', function () {
                if ($('#<%= ltrJudged.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrJudged.ClientID %>').removeClass('description-detail');
                    $('#view-all-Judged').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrJudged.ClientID %>').addClass('description-detail');
                    $('#view-all-Judged').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-MethodHandle').on('click', function () {
                if ($('#<%= ltrMethodHandle.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrMethodHandle.ClientID %>').removeClass('description-detail');
                    $('#view-all-MethodHandle').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrMethodHandle.ClientID %>').addClass('description-detail');
                    $('#view-all-MethodHandle').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-Result').on('click', function () {
                if ($('#<%= ltrResult.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrResult.ClientID %>').removeClass('description-detail');
                    $('#view-all-Result').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrResult.ClientID %>').addClass('description-detail');
                    $('#view-all-Result').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-ReporterInformation').on('click', function () {
                if ($('#<%= ltrReporterInformation.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrReporterInformation.ClientID %>').removeClass('description-detail');
                    $('#view-all-ReporterInformation').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrReporterInformation.ClientID %>').addClass('description-detail');
                    $('#view-all-ReporterInformation').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-Question').on('click', function () {
                if ($('#<%= ltrQuestion.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrQuestion.ClientID %>').removeClass('description-detail');
                    $('#view-all-Question').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrQuestion.ClientID %>').addClass('description-detail');
                    $('#view-all-Question').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-QuestionDetail').on('click', function () {
                if ($('#<%= ltrQuestionDetail.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrQuestionDetail.ClientID %>').removeClass('description-detail');
                    $('#view-all-QuestionDetail').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrQuestionDetail.ClientID %>').addClass('description-detail');
                    $('#view-all-QuestionDetail').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-ResultNoIncurred').on('click', function () {
                if ($('#<%= ltrResultNoIncurred.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrResultNoIncurred.ClientID %>').removeClass('description-detail');
                    $('#view-all-ResultNoIncurred').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrResultNoIncurred.ClientID %>').addClass('description-detail');
                    $('#view-all-ResultNoIncurred').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-Resolution').on('click', function () {
                if ($('#<%= ltrResolution.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrResolution.ClientID %>').removeClass('description-detail');
                    $('#view-all-Resolution').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrResolution.ClientID %>').addClass('description-detail');
                    $('#view-all-Resolution').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-ResolutionContent').on('click', function () {
                if ($('#<%= ltrResolutionContent.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrResolutionContent.ClientID %>').removeClass('description-detail');
                    $('#view-all-ResolutionContent').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrResolutionContent.ClientID %>').addClass('description-detail');
                    $('#view-all-ResolutionContent').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
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

            $('#view-all-URL').on('click', function () {
                if ($('#<%= ltrURL.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrURL.ClientID %>').removeClass('description-detail');
                    $('#view-all-URL').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrURL.ClientID %>').addClass('description-detail');
                    $('#view-all-URL').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

            $('#view-all-Note').on('click', function () {
                if ($('#<%= ltrNote.ClientID %>').hasClass('description-detail')) {
                    $('#<%= ltrNote.ClientID %>').removeClass('description-detail');
                    $('#view-all-Note').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#<%= ltrNote.ClientID %>').addClass('description-detail');
                    $('#view-all-Note').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
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