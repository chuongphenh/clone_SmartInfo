<%@ Page Title="Chi tiết tin tức" Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.News.Display" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/News/SideBarUC.ascx" TagPrefix="uc" TagName="SideBarUC" %>
<%@ Register Src="~/UI/SmartInfos/News/CampaignNewsUC.ascx" TagPrefix="uc" TagName="CampaignNewsUC" %>
<%@ Register Src="~/UI/SmartInfos/News/NewsUC.ascx" TagPrefix="uc" TagName="NewsUC" %>
<%@ Register Src="~/UI/SmartInfos/News/SpecificResultsUC.ascx" TagPrefix="uc" TagName="SpecificResultsUC" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        p {
            font-family: Tahoma;
            font-size: 12px;
        }

        .mySlides {
            display: none;
        }

        .w3-display-left {
            color: #fff !important;
            background-color: #000 !important;
            margin-top: -21%;
            margin-left: 28%;
            transform: translate(0%,-50%);
            border: none;
            display: inline-block;
            padding: 8px 16px;
            vertical-align: middle;
            overflow: hidden;
            text-decoration: none;
            text-align: center;
            cursor: pointer;
            white-space: nowrap;
        }

        .w3-display-right {
            color: #fff !important;
            background-color: #000 !important;
            margin-top: -21%;
            margin-left: 37%;
            transform: translate(0%,-50%);
            border: none;
            display: inline-block;
            padding: 8px 16px;
            vertical-align: middle;
            overflow: hidden;
            text-decoration: none;
            text-align: center;
            cursor: pointer;
            white-space: nowrap;
        }

        .w3-button:hover {
            color: #000 !important;
            background-color: #ccc !important;
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

        .description-detail {
            height: 33px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }

        .image-detail {
            height: 225px;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
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
        .news-status-title{
            font-size:13px;
        }
        .news-status-custom ul{
            display:block;
        }
        .news-status-custom ul li{
            display:inline-block;
            margin-right:15px;
            width:29%;
            margin-bottom:15px;
        }
        .news-status-custom ul li:last-child{
            margin-right: 15px!important;
            margin-bottom: 15px!important;
        }
        .news-status-custom .news-status-title{
            flex-direction: initial;
        }
        .custom-row {
            width: 100%;
            background-color: #fff;
            border-radius: 8px;
        }
    </style>
    <asp:HiddenField runat="server" ID="hidId" />
    <asp:HiddenField runat="server" ID="hidVersion" />

    <div style="margin-top: 10px;">
        <div class="body-content container">
            <section style="padding-right: 35px; padding-left: 35px" class="row">
                <div class="col-lg-9 col-md-9 blog-page custom-row">
                    <div class="md-margin-bottom-60">
                        <!--Blog Post-->
                        <div class="list-news-follow" style="float:left; padding-top: 15px; padding-bottom: 15px;">
                            <div style="min-height: 40px; display: flex; align-items: center; flex-direction: row-reverse; justify-content: end;">
                                 <asp:LinkButton ID="btnExit" runat="server" OnClick="btnExit_Click" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-right: 10px; border-radius: 4px;">
                                    <i class="fas fa-sign-out-alt" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; border-radius: 4px;" title="Thoát"></i>
                                     <span style="color:#595959;font-weight:bold;font-size:16px;">Thoát</span>
                                </asp:LinkButton>
                                <span id="spanDelete" runat="server">
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tin tức không?')" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-right: 10px; border-radius: 4px;">
                                        <i class="fa fa-trash" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px;  border-radius: 4px;" title="Xóa"></i>
                                        <span style="color:#595959;font-weight:bold;font-size:16px;">Xóa</span>
                                    </asp:LinkButton>
                                </span>
                                <span id="spanEdit" runat="server">
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-right: 10px; border-radius: 4px;">
                                        <i class="fas fa-pencil-alt" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; border-radius: 4px;" title="Sửa"></i>
                                        <span style="color:#595959;font-weight:bold;font-size:16px;">Sửa</span>
                                    </asp:LinkButton>
                                </span>
                                <asp:LinkButton ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px;  padding-right: 10px; border-radius: 4px;">
                                    <i class="fas fa-file-excel" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px;  border-radius: 4px;" title="Xuất Excel"></i>
                                    <span style="color:#595959;font-weight:bold;font-size:16px;">Xuất Excel</span>
                                </asp:LinkButton>
                            </div>
                            <div class="blog margin-bottom-40">
                                <h2 style=" border: unset; background: unset; margin-top: 0px; margin-bottom: 20px; padding-left: 0px; padding-right: 0px; color: #141ed2; font-size:20px; font-weight: 600">
                                    <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                </h2>
                                <div class="news-status news-status-custom">
                                    <ul>
                                        <li>
                                            <div class="news-status-title">
                                                <i class="fas fa-clock"></i> Ngày đăng tin từ:
                                            </div>
                                            <p>
                                                <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal>
                                            </p>
                                        </li>
                                        <li >
                                            <div class="news-status-title">
                                                <i class="fas fa-clock"></i> Đến:
                                            </div>
                                            <p>
                                                <asp:Literal ID="ltrPostingToDTG" runat="server"></asp:Literal>
                                            </p>
                                        </li>
                                        <li> 
                                            <div class="news-status-title">
                                                <i class="fas fa-newspaper"></i> Tổng số bài đăng:
                                            </div>
                                            <p>
                                                <asp:Literal ID="ltrNumberOfPublish" runat="server"></asp:Literal>
                                            </p>
                                        </li>
                                        <li> 
                                            <div class="news-status-title">
                                                <i class="fas fa-bolt"></i> Danh mục:
                                            </div>
                                            <p style="color: #1D39C4; text-decoration: underline">
                                                <asp:Literal ID="ltrCatalogID" runat="server"></asp:Literal>
                                            </p>
                                        </li>
                                        <li> 
                                            <div class="news-status-title">
                                                <i class="fa fa-list-alt"></i> Phân loại:
                                            </div>
                                            <p>
                                                <asp:Literal ID="ltrCategory" runat="server"></asp:Literal>
                                            </p>
                                        </li>
                                        <li> 
                                            <div class="news-status-title">
                                                <i class="fa fa-sort"></i> Thứ tự hiển thị:
                                            </div>
                                            <p>
                                                <asp:Literal ID="ltrDisplayOrder" runat="server"></asp:Literal>
                                            </p>
                                        </li>
                                    </ul>
                                </div>
                                <div class="news-status news-status-custom">
                                   <ul>
                                        <li>
                                            <div class="news-status-title">
                                                <i class="fa fa-hashtag"></i>Hastag:
                                            </div>
                                            <p>
                                            <asp:Literal ID="txtHastag" runat="server"></asp:Literal>
                                            </p>
                                         </li>
                                    </ul>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: left; padding-left: 0px; padding-right: 0px">
                                        <i class="fa fa-image" style="margin-right: 13px; color: #8C8C8C"></i><span style="color: #595959;font-size: 14px;">Hình ảnh</span>
                                        <span class="view-all" id="view-all-image">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                                        <span id="spanUpload" runat="server"><asp:Button ID="btnShowPopupUpload" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px"></asp:Button></span>
                                    </div>
                                </div>
                                <div class="row image-detail" style="text-align: center; margin-top: 10px; min-height: 102px" id="divImage" runat="server">
                                    <asp:Repeater ID="rptImage" runat="server" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-3 img-width" style="padding-left: 0px; margin-top: 5px; float: unset; display: -webkit-inline-box; width:150px !important">
                                                <div id="divViewDetailImage" runat="server" class="div-link">
                                                    <img id="img" runat="server" src="" style="width: 99%; height: 220px; border-radius: 8px; object-fit: cover" />
                                                </div>
                                                <asp:LinkButton ID="btnDeleteImage" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times" title="Xóa ảnh"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="news-vote-detail" style="margin-top: 15px;font-size: 14px;">
                                    <span>
                                        <i class="far fa-chart-bar" style="margin-right: 11px;"></i>
                                        Đánh giá chi tiết:
                                    </span>
                                    <span class="view-all" id="view-all">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                                    <p id="pDetail" class="description-detail" style="font-family: SF Pro Display; min-height: 33px">
                                        <asp:Literal ID="ltrContent" runat="server" />
                                    </p>
                                </div>
                            </div>
                            <uc:SpecificResultsUC ID="ucSpecificResults" runat="server" OnComplete="ucSpecificResults_Complete"/>
                            
                             <div class="table-responsive">
                                <asp:DropDownList ID="tuyenBai_tinLe" runat="server" AutoPostBack="true" OnSelectedIndexChanged="tuyenBai_tinLe_SelectedIndexChanged" style="margin: 20px 0 35px 0;">
                                    <asp:ListItem Text="Tuyến bài" Value="0" />
                                    <asp:ListItem Text="Tin lẻ" Value="1" />
                                </asp:DropDownList>
                            </div>
                            <uc:CampaignNewsUC ID="ucCampaignNews" runat="server" />
                            <uc:NewsUC ID="ucNews" runat="server" />
                            <uc:SideBarUC ID="ucSideBar" runat="server" />
                            <hr style="margin-bottom: 10px; margin-top: 10px;" />
                            <div class="row" style="margin-top: 10px; padding-bottom: 10px">
                                <div class="col-md-12" style="text-align: left; padding-left: 0px">
                                    <i class="fa fa-comments" style="margin-right: 10px; color: #8C8C8C;font-size: 14px;"></i><span style="color: #595959">Bình luận</span>
                                </div>
                            </div>
                            <uc:CommentUC ID="ucComment" runat="server" />

                        </div>
                        <!--End Blog Post-->
                    </div>
                </div>
            </section>
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

    <script>
        $(document).ready(function () {
            var imgWidth = document.getElementsByClassName('img-width');

            for (var i = 0; i < imgWidth.length; i++) {
                imgWidth[i].style.width = $('#ulSideBar').width() + "px";
            };

            $('#view-all').on('click', function () {
                if ($('#pDetail').hasClass('description-detail')) {
                    $('#pDetail').removeClass('description-detail');
                    $('#view-all').html('Thu gọn&nbsp;&nbsp;<i class="fa fa-angle-up"></i>');
                }
                else {
                    $('#pDetail').addClass('description-detail');
                    $('#view-all').html('Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i>');
                }
            });

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