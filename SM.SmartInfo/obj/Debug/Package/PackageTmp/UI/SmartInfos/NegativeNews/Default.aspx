<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.NegativeNews.Default" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/SmartInfos/NegativeNews/EditUC.ascx" TagName="EditUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/NegativeNews/DisplayUC.ascx" TagName="DisplayUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/PressAgencySelector.ascx" TagPrefix="uc" TagName="PressAgencySelector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .da-phat-sinh {
            font-size: 14px;
            color: white;
            padding: 3px;
            padding-right: 15px;
            padding-left: 15px;
            border: 1px solid #FF4D4F;
            background: #FF4D4F;
            border-radius: 100px;
            display: -webkit-inline-box;
        }

        .chua-phat-sinh {
            font-size: 13px;
            color: white;
            padding: 3px;
            padding-right: 15px;
            padding-left: 15px;
            border: 1px solid #597EF7;
            background: #597EF7;
            border-radius: 100px;
            display: -webkit-inline-box;
        }

        .fa-negative-news-done {
            padding: 2px;
            padding-left: 3px;
            padding-right: 3px;
            border: 1px solid #389E0D;
            border-radius: 50%;
            background: #389E0D;
            color: white;
            font-size: 9px;
        }

        .fa-negative-news-inprogress {
            padding: 2px;
            padding-left: 3px;
            padding-right: 3px;
            border: 1px solid #EE6400;
            border-radius: 50%;
            background: #EE6400;
            color: white;
            font-size: 9px;
        }

        ::-webkit-scrollbar {
            background: #fff;
            border-radius: 4px;
            height: 5px;
            width: 5px;
        }

        ::-webkit-scrollbar-thumb {
            background: #e8e8e8;
            border-radius: 4px;
        }

        .no-touchevents {
            overflow-y: scroll;
        }

        span {
            font-size: 13px;
        }

        .description-list {
            height: 18px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
            text-align: left;
        }

        .done {
            color: #389E0D !important;
        }

        .inprogress {
            color: #EE6400 !important;
        }

        .div-link:hover {
            cursor: pointer;
        }

        .div-active {
            background: #dcdfe4;
        }

        .a-hover:hover {
            text-decoration: unset;
        }

        .group-button {
            position: relative;
            list-style: none;
            display: inline-block;
        }

            .group-button > ul {
                position: absolute;
                left: 0;
                top: calc(100% + 10px);
                width: max-content;
                background: #fff;
                border: 1px solid #1D39C4;
                padding: 5px;
                border-radius: 3px;
                flex-direction: row;
                flex-wrap: wrap;
                max-width: calc(100vw - 400px);
                z-index:9;
            }

                .group-button > ul:after {
                    top: -11px;
                    left: 20px;
                    border: solid red;
                    content: " ";
                    height: 0;
                    width: 0;
                    position: absolute;
                    pointer-events: none;
                    border-color: transparent;
                    border-bottom-color: #1D39C4;
                    border-width: 5px;
                    margin-left: -5px;
                }

                .group-button > ul li {
                    margin-bottom: 5px;
                    list-style: none;
                    display: inline-block;
                }

                    .group-button > ul li:last-child {
                        margin-bottom: 0;
                    }

            .group-button > a {
                padding: 4px 6px 4px 8px !important;
                cursor: pointer;
                -webkit-touch-callout: none;
                -webkit-user-select: none;
                -khtml-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
            }

                .group-button > a > i {
                    padding-right: 8px;
                }
    </style>
    <div class="body-content" style="background: #F2F3F8; padding-top: 0px">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <asp:HiddenField ID="hidType" runat="server" />
        <asp:HiddenField ID="hidPage" runat="server" />
        <%--DESIGN--%>
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px;">
            <div class="row blog-page">
                <%--SIDEBAR--%>
                <div class="col-sm-5" style="text-align: left;">
                    <div class="home-col-3">
                        <div class="home-block">
                            <div class="home-block-title">
                                <div class="content-title-sv">
                                    <div class="title-top-sv">
                                        <h3 style="font-size: 16px; font-weight: 600; padding: 0px; margin: 10px 0;">
                                             <span class="list-table-title" style="border: unset; padding: unset">
                                                <asp:LinkButton CssClass="title-active" style="margin-right: 10px; color: #464457; font-family: SF Pro Display; font-size: 14px; font-weight: 600;" ID="btnSwitchAll" OnClick="btnSwitchAll_Click" runat="server">Tất cả</asp:LinkButton>
                                                <asp:LinkButton style="margin-right: 10px; color: #464457; font-family: SF Pro Display; font-size: 14px; font-weight: 600;" ID="btnSwitchNotYet" OnClick="btnSwitchNotYet_Click" runat="server">Chưa lên báo</asp:LinkButton>
                                                <asp:LinkButton style="color: #464457; font-family: SF Pro Display; font-size: 14px; font-weight: 600; margin-right: 0px" ID="btnSwitchAlready" runat="server" OnClick="btnSwitchAlready_Click">Đã lên báo</asp:LinkButton>
                                            </span>
                                        </h3>
                                    </div>
                                    <div class="title-bottom-sv">
                                        <asp:LinkButton ID="btnAddNew" CssClass="a-hover" runat="server" OnClick="btnAddNew_Click" Style="margin-top: -5px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-right: 10px; border-radius: 4px;">
                                            <i class="fa fa-plus" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; border-radius: 4px;" title="Thêm mới sự vụ"></i>
                                            <span style="color: #595959;font-weight:600;font-size:16px;">Thêm sự vụ</span>
                                        </asp:LinkButton>
                                        <li class="group-button" id="liMoreButton">
                                            <a class="a-hover" runat="server" style="margin-top: -5px">
                                                <i id="iShowButton" class="fas fa-angle-down" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="More"></i>
                                            </a>
                                            <ul id="ulSubButton">
                                                <li>
                                                    <asp:LinkButton ID="btnShowPopupUpload" CssClass="a-hover" runat="server" OnClick="btnShowPopupUpload_Click" Style="margin-top: -5px; margin-left: 5px">
                                                        <i class="fa fa-upload" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tải tài liệu sự vụ"> Import tài liệu</i>
                                                    </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="btnExportExcel" CssClass="a-hover" runat="server" OnClick="btnExportExcel_Click" Style="margin-top: -5px; margin-left: 5px">
                                                        <i class="fas fa-file-excel" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Xuất dữ liệu sự vụ"> Xuất Excel</i>
                                                    </asp:LinkButton>
                                                </li>
                                            </ul>
                                        </li>
                                        <span class="title-right" style="margin-top: 5px;">
                                            <asp:LinkButton ID="btnShowPopupSearch" OnClick="btnShowPopupSearch_Click" CssClass="a-hover" runat="server" Style="margin-top: -5px; margin-right: 5px">
                                                <i class="fa fa-search" aria-hidden="true" style="color: #595959; margin-top: -1px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tìm kiếm nâng cao"></i>
                                            </asp:LinkButton>
                                            <span class="title-right-icon">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                            <asp:DropDownList ID="ddlFilterTime" runat="server" style="border: unset" OnSelectedIndexChanged="ddlFilterTime_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        
                            <div class="home-block-content">
                                <ul class="home-notify" style="padding: 0px">
                                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                                    <ItemTemplate>
                                        <div class="div-link" id="divLink" runat="server" style="display: flex;">
                                            <div>
                                                  <div class="row" style="padding-left: 25px; padding-right: 25px; padding-top: 15px; padding-bottom: 10px; display: flex; align-items: center; height: 100%; width: 100%;">
                                                <div class="col-md-12" style="padding-left: 0px; padding-right: 0px; text-align: center">
                                                    <span style="float:left; font-size: 14px; color: black; font-weight:bold">
                                                             <%# (Container.ItemIndex + 1) + ((SM.SmartInfo.Utils.Utility.GetNullableInt(hidPage.Value) - 1) * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniEight) %>
                                                    </span>
                                                </div>
                                            </div>
                                            </div>
                                          <div style="width: 100%;">
                                                      <div class="row" style="padding-left: 25px; padding-right: 25px; padding-top: 15px; padding-bottom: 10px">
                                                <div class="col-md-12" style="padding-left: 0px; padding-right: 0px; text-align: center">
                                                    <span style="float: left">
                                                        <i id="iStatus" runat="server"></i><span style="font-size: 13px; font-weight: 600" id="spanStatus" runat="server">&nbsp;
                                                        <asp:Literal ID="ltrStatus" runat="server"></asp:Literal></span>
                                                    </span>
                                                    <span style="float: right">
                                                        <asp:Literal ID="ltrIncurredDTG" runat="server"></asp:Literal>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="row" style="padding-left: 25px; padding-right: 25px; padding-bottom: 15px">
                                                <div class="col-sm-7" style="padding-left: 0px; padding-right: 0px; text-align: center">
                                                    <span style="float: left; font-size: 14px" class="description-list">
                                                        <asp:Label ID="lblName" runat="server" Style="color: #262626"></asp:Label>
                                                    </span>
                                                </div>
                                                <div class="col-sm-5" style="padding-left: 0px; padding-right: 0px; text-align: center">
                                                    <span style="float: right; margin-top: -2px;">
                                                        <asp:Label style="padding-top: 0px; padding-bottom: 0px" ID="ltrNegativeType" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                            </div>
                                          </div>
                                    
                                        </div>
                                        <asp:LinkButton ID="btnViewDisplay" runat="server" style="display: none"><span>View Detail</span></asp:LinkButton>
                                        <div class="row" style="margin-right: 25px; margin-left: 25px;">
                                            <hr style="margin-top: 0px; margin-bottom: 0px">
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                            </div>                        
                        </div>
                    </div>
                    <div style="text-align: center; margin-top: 120px;">
                        <div class="text-center" style="display: inline-block">
                            <ul class="pagination">
                                <uc:PagerUC runat="server" id="Pager" OnPageIndexChanged="Pager_PageIndexChanged" />
                            </ul>
                        </div>
                    </div>
                </div>
                <%--END SIDEBAR--%>
                <%--CONTENT--%>
                <div class="col-sm-7" style="text-align: center; background: rgb(255, 255, 255);">
                    <uc:DisplayUC ID="ucDisplay" runat="server"></uc:DisplayUC>
                    <uc:EditUC ID="ucEdit" runat="server" Visible="false"></uc:EditUC>
                </div>
                <%--END CONTENT--%>
            </div>
        </section>
        <%--END DESIGN--%>

        <tk:PopupPanel ID="popSearch" runat="server" Title="TÌM KIẾM NÂNG CAO SỰ VỤ" Width="700" CancelButton="btnCancel">
            <PopupTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px;">SỰ VỤ
                        </h3>
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Ngày phát sinh từ</th>
                                <td>
                                    <tk:DatePicker ID="dpkFromIncurredDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                                <th>Đến</th>
                                <td>
                                    <tk:DatePicker ID="dpkToIncurredDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                            </tr>
                             <tr>
                                <th>Loại sự vụ</th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlType" runat="server" Width="100%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>Mức độ sự vụ</th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlClassification" runat="server" Width="100%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>Tình trạng</th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="100%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearch" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--<div class="col-md-6">
                        <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px;">CHI TIẾT SỰ VỤ
                        </h3>
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Thời gian từ</th>
                                <td>
                                    <tk:DatePicker ID="dpkFromCreatedDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                                <th>Đến</th>
                                <td>
                                    <tk:DatePicker ID="dpkToCreatedDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                            </tr>
                            <tr>
                                <th>Loại sự vụ</th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlType" runat="server" Width="100%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>Trạng thái xử lý</th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlStatusNegativeNews" runat="server" Width="100%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>Tổ chức đăng bài</th>
                                <td colspan="3">
                                    <uc:PressAgencySelector runat="server" ID="ucPressAgencySelector" DataTextField="Name" IsSearchAll="true" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearchNegativeNews" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>--%>
                <div class="popup-toolbar" style="text-align: center">
                    <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" class="btn btn-primary" Style="background: #434a54" />
                    <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
                </div>
            </PopupTemplate>
        </tk:PopupPanel>
    </div>

    <script>
        $('#ulSubButton').hide();

        function changeURL(newURL) {
            window.history.pushState('', 'Smart Info', newURL);
        }

        function clickViewDetail(clientID) {
            $('#' + clientID + ' span').trigger('click');
        }

        $('#liMoreButton').on('click', function (e) {
            if (e.target.className == 'fas fa-angle-up' || e.target.className == 'fas fa-angle-down') {
                e.preventDefault();
                $("#ulSubButton").toggle();

                if ($("#ulSubButton").is(":visible"))
                    $("#iShowButton").attr('class', 'fas fa-angle-up');
                else
                    $("#iShowButton").attr('class', 'fas fa-angle-down');
            }
        })
    </script>
</asp:Content>