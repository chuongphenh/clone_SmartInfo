<%@ Page Title="Cập nhật tin tức" Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.News.Edit" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/News/SideBarUC.ascx" TagPrefix="uc" TagName="SideBarUC" %>
<%@ Register Src="~/UI/SmartInfos/News/CampaignNewsUC.ascx" TagPrefix="uc" TagName="CampaignNewsUC" %>
<%@ Register Src="~/UI/SmartInfos/News/NewsUC.ascx" TagPrefix="uc" TagName="NewsUC" %>
<%@ Register Src="~/UI/SmartInfos/News/SpecificResultsUC.ascx" TagPrefix="uc" TagName="SpecificResultsUC" %>
<%@ Register Src="~/UI/SmartInfos/NegativeNews/ListNagativeNewsUC.ascx" TagName="ListNagativeNewsUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/CatalogNewsSelectorTreeUC.ascx" TagPrefix="uc" TagName="CatalogNewsSelectorTreeUC" %>

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

        .hidButton {
            visibility: hidden;
        }
        /* Định dạng danh sách hashtag */
        #hashtagList {
            background-color: #f5f5f5;
            border: 1px solid #ccc;
            max-height: 150px;
            overflow-y: auto;
            display: none;
        }

        /* Định dạng mục trong danh sách */
        #listItems li {
            margin-bottom: 0;
            list-style-type: none;
            padding: 5px;
            cursor: pointer;
            border-bottom: 1px solid #ccc;
        }

            /* Màu khi di chuột vào mục */
            #listItems li:hover {
                background-color: #e0e0e0;
            }

        .custom-row {
            width: 100%;
            background-color: #fff;
            border-radius: 8px;
        }

        .table th, .table td {
            min-width: 50px;
            word-break: break-word !important;
        }

        .justify {
            font-size: 13px;
            text-align: justify;
        }
    </style>
    <asp:HiddenField runat="server" ID="hidId" />
    <asp:HiddenField runat="server" ID="hidVersion" />
    <asp:HiddenField runat="server" ID="hidCroppedImage" />
    <asp:HiddenField runat="server" ID="hidEditable" />

    <div style="margin-top: 10px;">
        <div class="body-content container">
            <section style="padding-right: 35px; padding-left: 35px" class="custom-section-news">
                <div class="col-lg-9 col-md-9 blog-page custom-row">
                    <div class="md-margin-bottom-60">
                        <div style="position: fixed; right: 10px; top: 50%; opacity: 1; z-index: 9">
                            <asp:LinkButton ID="btnSaveContinues" class="btnSaveContinue" OnClick="btnSaveContinues_Click" runat="server">
                                <i class="far fa-save" style="color: white; margin-top: 4px; font-size: 16px; border-radius: 4px;"></i>
                                <span style="font-size: 10px; display: inherit;">&nbsp; Lưu & Tiếp tục</span>
                            </asp:LinkButton>
                        </div>
                        <!--Blog Post-->
                        <div class="list-news-follow" style="float: left; padding-top: 15px; padding-bottom: 15px;">
                            <div class="blog margin-bottom-40">
                                <div class="row">
                                    <div class="col-md-7" style="padding-left: 0px; color: #595959; font-size: 14px;">
                                        Tên tin <span class="star">*</span>
                                    </div>
                                    <div class="col-md-5" style="padding-right: 0px; padding-bottom: 5px">
                                        <asp:LinkButton ID="btnExit" runat="server" OnClick="btnExit_Click" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                                            <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; " title="Thoát"></i>
                                            <span style="color:#595959;font-weight:bold;font-size:16px;">Thoát</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                                            <i class="far fa-save" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px;" title="Lưu"></i>
                                            <span style="color:#595959;font-weight:bold;font-size:16px;">Lưu</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="padding-left: 0px">
                                        <asp:TextBox ID="txtName" runat="server" placeholder="Tên tin" Width="100%" MaxLength="256"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="row" style="margin: 20px 0 10px 0;">
                                    <div class="col-md-4" style="padding-left: 0px">
                                        <div class="news-status-title">
                                            <i class="fas fa-clock"></i>Ngày đăng tin từ: <span class="star">*</span>
                                        </div>
                                        <p>
                                            <tk:DatePicker ID="dpkPostingFromDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                        </p>
                                    </div>
                                    <div class="col-md-4" style="padding-left: 0px">
                                        <div class="news-status-title">
                                            <i class="fas fa-clock"></i>Đến: <span class="star">*</span>
                                        </div>
                                        <p>
                                            <tk:DatePicker ID="dpkPostingToDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                        </p>
                                    </div>
                                    <div class="col-md-4" style="padding-left: 0px">
                                        <div class="news-status-title">
                                            <i class="fas fa-newspaper"></i>Tổng số bài đăng:
                                        </div>
                                        <p>
                                            <tk:NumericTextBox ID="numNumberOfPublish" runat="server" Width="100%" AllowThousandDigit="false" NumberDecimalDigit="0"></tk:NumericTextBox>
                                        </p>
                                    </div>

                                </div>

                                <div class="row" style="margin: 10px 0">
                                    <div class="col-md-4" style="padding-left: 0px">
                                        <div class="news-status-title">
                                            <i class="fas fa-bolt"></i>Danh mục:
                                        </div>
                                        <p style="color: #1D39C4; text-decoration: underline">
                                            <uc:CatalogNewsSelectorTreeUC runat="server" ID="ucCatalogNewsSelectorTree" Width="100%" />
                                        </p>
                                    </div>
                                    <div class="col-md-4" style="padding-left: 0px">
                                        <div class="news-status-title">
                                            <i class="fa fa-list-alt"></i>Phân loại:
                                        </div>
                                        <p style="color: #1D39C4; text-decoration: underline">
                                            <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" DataTextField="Value" DataValueField="Key" />
                                        </p>
                                    </div>
                                    <div class="col-md-4" style="padding-left: 0px">
                                        <div class="news-status-title">
                                            <i class="fa fa-sort"></i>Thứ tự hiển thị:
                                        </div>
                                        <p style="color: #1D39C4; text-decoration: underline">
                                            <tk:NumericTextBox ID="numDisplayOrder" runat="server" Width="100%" AllowThousandDigit="false" NumberDecimalDigit="0"></tk:NumericTextBox>
                                        </p>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4" style="padding-left: 0px">
                                        <div class="news-status-title">
                                            <i class="fa fa-hashtag"></i>Hastag:
                                        </div>
                                        <asp:TextBox ID="txtHastag" runat="server" placeholder="Hastag" Width="100%" MaxLength="256" autocomplete="off"></asp:TextBox>
                                        <div id="hashtagList" class="">
                                            <ul id="listItems">
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="news-status" style="margin-top: 15px">
                                    <%--<ul>
                                            <li>
                                           
                                            </li>
                                            <li>
                                           
                                            </li>
                                            <li> 
                                           
                                            </li>
                                            <li> 
                                         
                                            </li>
                                            <li> 
                                           
                                            </li>
                                            <li> 
                                          
                                            </li>
                                    </ul>--%>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: left; padding-left: 0px; padding-right: 0px">
                                        <i class="fa fa-image" style="margin-right: 13px; color: #8C8C8C"></i><span style="color: #595959">Hình ảnh</span>
                                        <span class="view-all" id="view-all-image">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                                        <asp:Button ID="btnShowPopupUpload" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px"></asp:Button>
                                    </div>
                                </div>
                                <div class="row image-detail" style="text-align: center; margin-top: 10px; min-height: 102px" id="divImage" runat="server">
                                    <asp:Repeater ID="rptImage" runat="server" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-2 img-width" style="padding-left: 0px; margin-top: 5px; float: unset; display: -webkit-inline-box;">
                                                <div id="divViewDetailImage" runat="server" class="div-link">
                                                    <img id="img" runat="server" src="" style="width: 99%; height: 220px; border-radius: 8px; object-fit: cover" />
                                                </div>
                                                <asp:LinkButton ID="btnDeleteImage" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" Style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times" title="Xóa ảnh"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="news-vote-detail" style="margin-top: 15px; font-size: 14px;">
                                    <span style="margin-bottom: 10px;">
                                        <i class="far fa-chart-bar" style="margin-right: 11px;"></i>
                                        Đánh giá chi tiết:
                                    </span>
                                    <tk:TextArea ID="txtContent" runat="server" Width="100%" TextMode="MultiLine" Rows="6" MaxLength="1024"></tk:TextArea>
                                </div>
                            </div>
                            <uc:SpecificResultsUC ID="ucSpecificResults" runat="server" OnComplete="ucSpecificResults_Complete" />
                            <hr style="margin-bottom: 10px; margin-top: 10px;" />
                            <div class="row" style="margin-top: 10px; padding-bottom: 10px">
                                <div class="col-md-12" style="text-align: left; padding-left: 0px">
                                    <i class="fa fa-comments" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959; font-size: 14px;">Bình luận</span>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:DropDownList ID="tuyenBai_tinLe" runat="server" AutoPostBack="true" OnSelectedIndexChanged="tuyenBai_tinLe_SelectedIndexChanged" Style="margin: 20px 0 35px 0;">
                                    <asp:ListItem Text="Tuyến bài" Value="0" />
                                    <asp:ListItem Text="Tin lẻ" Value="1" />
                                </asp:DropDownList>
                            </div>
                            <uc:CampaignNewsUC ID="ucCampaignNews" runat="server" />
                            <uc:NewsUC ID="ucNews" runat="server" />
                            <uc:SideBarUC ID="ucSideBar" runat="server" />
                            <uc:CommentUC ID="ucComment" runat="server" />

                        </div>
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
    <div class="modal fade" id="imageModal" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="imageModalLabel">Edit Image</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="image-container">
                        <img id="editable-Image" alt="Image" style="z-index: 2">
                        <div id="crop-area" style="z-index: 3"></div>
                    </div>
                    <canvas id="hidden-canvas"></canvas>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button id="btnSaveCrop" class="btn btn-secondary">Lưu ảnh</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var txtHastag = $("#<%= txtHastag.ClientID %>");
            var hashtagList = $("#hashtagList");
            var listItems = $("#listItems");

            function fetchData(searchValue) {
                $.ajax({
                    type: "GET",
                    url: "TagAJax.ashx",
                    data: { searchValue: searchValue },
                    success: function (data) {
                        listItems.empty();
                        $.each(data, function (index, value) {
                            var listItem = $("<li>" + value + "</li>");
                            listItem.on('click', function () {
                                txtHastag.val(value);
                                hashtagList.hide();
                            });
                            listItems.append(listItem);
                        });
                        hashtagList.show();
                    }
                });
            }

            txtHastag.on('click', function (event) {
                event.stopPropagation();
                hashtagList.show();
                // Gọi hàm fetchData khi click vào txtHastag
                fetchData(txtHastag.val());
            });

            $(document).on('click', function (event) {
                if (!txtHastag.is(event.target) && !hashtagList.is(event.target) && hashtagList.has(event.target).length === 0) {
                    hashtagList.hide();
                }
            });

            txtHastag.on('keyup', function () {
                var searchValue = $(this).val();
                // Gọi hàm fetchData khi thay đổi giá trị trong txtHastag
                fetchData(searchValue);
            });
        });
        <%-- $(document).ready(function () {
            var imgWidth = document.getElementsByClassName('img-width');

            for (var i = 0; i < imgWidth.length; i++) {
                imgWidth[i].style.width = $('#ulSideBar').width() + "px";
            };

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
        });--%>

        <%--window.onload = function () {
            var x = document.getElementById('<%= filesUpload.ClientID%>/*');
            x.setAttribute("value", "");
            console.log(x);
        }

        document.getElementById('<%= filesUpload.ClientID%>/*').addEventListener('change', function (e) {
            var hidEditable = document.getElementById('<%= hidEditable.ClientID%>/*');
            hidEditable.value = 'true';
            document.getElementById('<%= btnTrigger.ClientID%>/*').click();
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
