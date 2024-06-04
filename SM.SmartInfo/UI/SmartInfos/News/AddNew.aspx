<%@ Page Title="Tạo mới tin tức" Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.News.AddNew" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
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

        #listItems li {
            margin-bottom: 0;
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
            height: 95px;
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

        .custom-section-news {
            display: flex;
            gap: 30px;
        }

        .custom-row {
            width: 100%;
            background-color: #fff;
            border-radius: 8px;
        }

            .custom-row .table-responsive {
                padding: 20px !important;
            }

        @media screen and (max-width: 1299px) {
            .custom-section-news {
                flex-direction: column;
            }
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
    </style>
    <div style="margin-top: 10px;">
        <div class="body-content container" style="height: 100%">
            <section style="padding-right: 35px; padding-left: 35px" class="custom-section-news">
                <div class="col-lg-9 col-md-9 blog-page custom-row" style="">
                    <div class="md-margin-bottom-60">
                        <div style="position: fixed; right: 10px; top: 50%; opacity: 1; z-index: 9">
                            <asp:LinkButton ID="btnSaveContinues" class="btnSaveContinue" OnClick="btnSaveContinues_Click" runat="server">
                                <i class="far fa-save" style="color: white; margin-top: 4px; font-size: 16px; border-radius: 4px;"></i>
                                <span style="font-size: 10px; display: inherit;">&nbsp; Lưu & Tiếp tục</span>
                            </asp:LinkButton>
                        </div>
                        <!--Blog Post-->
                        <div class="list-news-follow" style="float: left; padding-top: 15px; padding-bottom: 15px; margin-bottom: 30px;">
                            <div class="blog margin-bottom-40">
                                <div class="row" style="display: flex; align-items: center; margin-bottom: 10px;">
                                    <div class="col-md-8" style="padding-left: 0px; color: #595959">
                                        Tên tin <span class="star">*</span>
                                    </div>
                                    <div class="col-md-3" style="padding-right: 0px; padding-bottom: 5px; display: flex; flex-direction: row-reverse; width: 100%">
                                        <asp:LinkButton ID="btnExit" runat="server" OnClick="btnExit_Click" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                                            <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thoát"></i>
                                            <span style="color: #595959;font-weight:600;font-size:16px;">Thoát</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Style="float: right; margin-top: -10px; margin-left: 5px; background: #F2F3F8; padding-bottom: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                                            <i class="far fa-save" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Lưu"></i>
                                            <span style="color: #595959;font-weight:600;font-size:16px;">Lưu</span>
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
                                <div class="row" style="margin: 10px 0;">
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
                                    <ul>
                                        <li></li>
                                        <li></li>
                                        <li></li>
                                        <li></li>
                                        <li></li>
                                        <li></li>
                                    </ul>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: left; padding-left: 0px; padding-right: 0px">
                                        <i class="fa fa-image" style="margin-right: 13px; color: #8C8C8C"></i><span style="color: #595959">Hình ảnh</span>
                                        <%--<div>
                                            <div class="container">
                                           <asp:FileUpload ID="filesUpload" runat="server" AllowMultiple="true" ClientIDMode="Static"/>
                                           <asp:Button ID="btnTrigger" OnClick="btnTrigger_Click" ClientIDMode="Static" runat="server" CssClass="hidButton"/>
                                        </div>--%>
                                    </div>
                                    <span class="view-all" id="view-all-image">Xem tất cả&nbsp;&nbsp;<i class="fa fa-angle-down"></i></span>
                                    <asp:Button ID="btnShowPopupUpload" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px"></asp:Button>
                                </div>
                            </div>
                            <div class="row image-detail" style="margin-top: 10px; min-height: 102px" id="divImage" runat="server">
                            </div>
                            <div class="news-vote-detail" style="margin-top: 15px;">
                                <span style="margin-bottom: 10px;">
                                    <i class="far fa-chart-bar" style="margin-right: 11px;"></i>
                                    Đánh giá chi tiết:
                                </span>
                                <tk:TextArea ID="txtContent" runat="server" Width="100%" TextMode="MultiLine" Rows="5" MaxLength="1024"></tk:TextArea>
                            </div>
                        </div>
                        <uc:SpecificResultsUC ID="ucSpecificResults" runat="server" />
                        <div class="table-responsive">
                            <asp:DropDownList ID="tuyenBai_tinLe" runat="server" AutoPostBack="true" OnSelectedIndexChanged="tuyenBai_tinLe_SelectedIndexChanged" Style="margin: -10px 0 -10px 0;">
                                <asp:ListItem Text="Tuyến bài" Value="0" />
                                <asp:ListItem Text="Tin lẻ" Value="1" />
                            </asp:DropDownList>
                        </div>
                        <uc:CampaignNewsUC ID="ucCampaignNews" runat="server" />
                        <uc:NewsUC ID="ucNews" runat="server" />
                        <uc:SideBarUC ID="ucSideBar" runat="server" />
                    </div>
                    <!--End Blog Post-->
                </div>
            </section>
        </div>
    </div>
    <script type="text/javascript">
        <%--document.getElementById('*/<%= filesUpload.ClientID%>/*').addEventListener('change', function (e) {
            document.getElementById('*/<%= btnTrigger.ClientID%>/*').click();
        });--%>
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
    </script>
</asp:Content>
