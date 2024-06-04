<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.PressAgencies.Default" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/EditUC.ascx" TagName="EditUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/DisplayUC.ascx" TagName="DisplayUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .col-md-10 > p {
            margin-bottom: 5px;
        }

        tab1 {
            padding-left: 10px;
        }

        .negative {
            color: white;
            padding: 0 10px;
            border: 1px solid red;
            border-radius: 50px;
            background: red;
            font-size: 13px;
        }

        .medium {
            color: black;
            padding: 0 10px;
            border: 1px solid orange;
            border-radius: 50px;
            background: orange;
            font-size: 13px;
        }

        .positive {
            color: white;
            padding: 0 10px;
            border: 1px solid limegreen;
            border-radius: 50px;
            background: limegreen;
            font-size: 13px;
        }

        table.table > thead > tr > th {
            text-align: center;
        }

        table.table > tbody > tr > td.center {
            text-align: center;
        }

        .holder_default {
            border: 3px dashed #ccc;
        }

            .holder_default.hover {
                border: 3px dashed #0c0 !important;
            }

        .hidden {
            visibility: hidden;
        }

        .visible {
            visibility: visible;
        }

        .custom-file-input {
            opacity: 0;
            z-index: 2;
            position: absolute;
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
            width: 100%;
        }

        .div-link:hover {
            cursor: pointer;
        }

        .div-active {
            background: #dcdfe4;
            color: #1D39C4;
        }

            .div-active > li > #text-right > .notify-employee {
                color: #1D39C4;
            }

                .div-active > li > #text-right > .notify-employee > span {
                    background: #1D39C4;
                }

                .div-active > li > #text-right > .notify-employee > .fa-user {
                    color: white;
                }

            .div-active > li > #text-right > h2 > a {
                color: #1D39C4 !important;
                font-weight: 600 !important;
            }

            .div-active > li > #text-right > h2 > span {
                color: #1D39C4 !important;
                font-weight: 600 !important;
            }

            .div-active > li > #text-right > p {
                color: #1D39C4 !important;
            }

        .address-detail {
            height: 40px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }
    </style>
    <div class="body-content" style="padding-top: 0px;">
        <asp:HiddenField ID="hidPage" runat="server" />
        <asp:HiddenField ID="hidType" runat="server" />
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px;">
            <div class="row blog-page">
                <!-- Left Sidebar -->
        	    <div class="col-sm-4">
                    <div class="home-col-3" style="height: unset;">
                        <div class="home-block">
                            <div class="home-block-title">
                                <div style="margin-bottom: 5px">
                                    <div class="top-tc" style="margin-bottom:10px;">
                                        <div class="form-group has-search custom-reposive-form" style="width:100%;display: inline-block;position: relative;">
                                                <asp:LinkButton ID="btnTextSearch" runat="server" OnClick="btnTextSearch_Click" class="fa fa-search form-control-feedback"></asp:LinkButton>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Style="border-radius: 20px;width:100%;" placeholder="Tìm kiếm ..."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="bottom-tc">
                                        <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="background: #F2F3F8; padding-bottom: 7px; padding-left: 10px; padding-right: 10px; border-radius: 4px;padding-top:5px;">
                                            <i class="fa fa-plus" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; " title="Tạo"></i>
                                            <span style="color: #595959;font-weight:600;font-size:16px;">Tạo</span>
                                        </asp:LinkButton>
                                        <span class="title-right">
                                            <asp:LinkButton ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Style="margin-left: 5px;background: #F2F3F8; padding-bottom: 5px; padding-left: 0px; padding-right: 10px; border-radius: 4px;">
                                                <i class="fas fa-file-excel" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 0px; border-radius: 4px;" title="Xuất Excel"></i>
                                                <span style="color: #595959;font-weight:600;font-size:16px;">Xuất Excel</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnSearchPressAgencyHR" OnClick="btnSearchPressAgencyHR_Click" CssClass="a-hover" runat="server" Style=" margin-left: 5px;background: #F2F3F8; padding-bottom: 5px; padding-left: 0px; padding-right: 10px; border-radius: 4px;">
                                                <i class="fas fa-user-friends" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 0px; border-radius: 4px;" title="Tìm kiếm nhân sự"></i>
                                                <span style="color: #595959;font-weight:600;font-size:16px;">Tìm kiếm</span>
                                            </asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnShowPopupSearch" OnClick="btnShowPopupSearch_Click" CssClass="a-hover" runat="server" Style="margin-top: -5px; margin-left: 5px">
                                                <i class="fa fa-search" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tìm kiếm nâng cao"></i>
                                            </asp:LinkButton>--%>
                                        </span>
                                    </div>
                                </div>
                                <h3 style="font-family: SF Pro Display; font-size: 16px; font-weight: 600; padding: 0px; margin: 0px;">
                                    <span class="list-table-title" style="border: unset; padding: unset">
                                        <asp:Repeater ID="rptPressAgencyType" runat="server" OnItemDataBound="rptPressAgencyType_ItemDataBound" OnItemCommand="rptPressAgencyType_ItemCommand">
                                            <ItemTemplate>
                                                <asp:LinkButton style="margin-bottom: 10px; margin-right: 10px; color: #464457; font-family: SF Pro Display; font-size: 14px; font-weight: 600;" ID="btnSwitchPressAgentType" runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </span>
                                </h3>
                            </div>
                            <div class="home-block-content">
                                <ul class="home-notify" style="padding: 0px">
                                    <asp:Repeater ID="rptPressAgencies" runat="server" OnItemDataBound="rptPressAgencies_ItemDataBound" OnItemCommand="rptPressAgencies_ItemCommand">
                                        <ItemTemplate>
                                            <!--Blog Post-->
                                            <div class="div-link" id="divLink" runat="server">
                                                <li class="row blog blog-medium margin-bottom-40" style="padding-right: 20px; padding-left: 20px;">
                                                    <div class="col-md-1 display-order" style="text-align: center; height: 68px">
                                                        <span style="margin-right: 5px; position: absolute; top: 35%">
                                                            <%# (Container.ItemIndex + 1) + ((SM.SmartInfo.Utils.Utility.GetNullableInt(hidPage.Value) - 1) * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniTen) %>
                                                        </span>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <img id="img" runat="server" class="img-responsive image-ava" src="" alt="" style="width: 100%; object-fit: contain; display: inline-block">
                                                    </div>
                                                    <div id="text-right" class="col-md-8" style="padding-right: 0px">
                                                        <div class="notify-employee">
                                                            <asp:Literal ID="ltrCountHR" runat="server"></asp:Literal> nhân sự
                                                            <span>
                                                                <i class="fa fa-user"></i>
                                                            </span>                                                       
                                                        </div>
                                                        <h2 style="font-weight: normal;font-size: 14px;line-height: 22px;background: transparent;margin: 0;border: none;padding: 0;color: #262626;">
                                                            <span style="color: #434a54; font-family: SF Pro Display">
                                                                <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                                            </span>
                                                        </h2>
                                                        <p class="address-detail" style="font-size:13px; color:#8C8C8C; margin-top: 5px"><asp:Literal ID="ltrAddress" runat="server"></asp:Literal></p>
                                                    </div>    
                                                </li>   
                                            </div>
                                            <asp:LinkButton ID="btnViewDisplay" runat="server" style="display: none"><span>View Detail</span></asp:LinkButton>
                                            <!--End Blog Post-->
                                        </ItemTemplate>
                                </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div style="text-align: center; margin-top: 20px;">
                        <div class="text-center" style="display: inline-block">
                            <ul class="pagination">
                                <uc:PagerUC runat="server" id="Pager" OnPageIndexChanged="ucPager_PageIndexChanged" />
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End Left Sidebar -->
                <div class="col-sm-8" style="padding-left: 0px; padding-right: 0px">
                    <uc:DisplayUC ID="ucDisplay" runat="server"></uc:DisplayUC>
                    <uc:EditUC ID="ucEdit" runat="server" Visible="false"></uc:EditUC>
                </div>
            </div><!--/row-->
        </section>

        <tk:PopupPanel ID="popSearch" runat="server" Title="TÌM KIẾM NÂNG CAO TỔ CHỨC" Width="1200" CancelButton="btnCancel">
            <PopupTemplate>
                <div class="press-agency-bottom">
                    <div class="list-table-title">
                        <a class="title-active" href="#" id="popupThongTinChung" onclick="showTabPopup(0)">Thông tin chung</a>
                        <a href="#" id="popupDanhSachLienHe" onclick="showTabPopup(1)">Danh sách liên hệ</a>
                        <a href="#" id="popupLichSuThayDoiNhanSu" onclick="showTabPopup(2)">Lịch sử thay đổi nhân sự</a>
                        <a href="#" id="popupLichSuHopTacGapGo" onclick="showTabPopup(3)">Lịch sử hợp tác gặp gỡ</a>
                        <a href="#" id="popupQuanHeGiuaCacCoQuanBaoChi" onclick="showTabPopup(4)">Quan hệ giữa các tổ chức</a>
                        <a href="#" id="popupAnhKhac" onclick="showTabPopup(5)">Ảnh khác</a>
                    </div>
                    <div class="list-table-content" id="divPopupThongTinChung" style="padding: 0px">
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Ngày thành lập từ
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkFromEstablishedDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                                <th>Đến
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkToEstablishedDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearchPA" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="list-table-content" id="divPopupDanhSachLienHe" style="display: none; padding: 0px">
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Thái độ
                                </th>
                                <td colspan="3">
                                    <asp:DropDownList runat="server" ID="ddlAttitude" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <th>Ngày sinh từ
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkFromDOB" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                                <th>Đến
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkToDOB" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearchPAHR" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                        <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px;">LỊCH SỬ GẶP MẶT
                        </h3>
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Ngày gặp mặt từ
                                </th>
                                <td>
                                    <tk:DatePicker runat="server" ID="dpkFromPAHRHistoryMeetDTG" Width="100%" DateFormat="DMY" />
                                </td>
                                <th>Đến
                                </th>
                                <td>
                                    <tk:DatePicker runat="server" ID="dpkToPAHRHistoryMeetDTG" Width="100%" DateFormat="DMY" />
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearchPAHRHistory" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                        <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px;">THÔNG TIN GIA ĐÌNH
                        </h3>
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Ngày sinh từ
                                </th>
                                <td>
                                    <tk:DatePicker runat="server" ID="dpkFromPAHRRelativesDOB" Width="100%" DateFormat="DMY" />
                                </td>
                                <th>Đến
                                </th>
                                <td>
                                    <tk:DatePicker runat="server" ID="dpkToPAHRRelativesDOB" Width="100%" DateFormat="DMY" />
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearchPAHRRelatives" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="list-table-content" id="divPopupLichSuThayDoiNhanSu" style="display: none; padding: 0px">
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Ngày thay đổi từ
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkFromChangeDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                                <th>Đến
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkToChangeDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextPASearchHistory" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="list-table-content" id="divPopupLichSuHopTacGapGo" style="display: none; padding: 0px">
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Ngày ký HĐ từ
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkFromContractDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                                <th>Đến
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkToContractDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                            </tr>
                            <tr>
                                <th>Thời gian gặp mặt từ
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkFromMeetDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                                <th>Đến
                                </th>
                                <td>
                                    <tk:DatePicker ID="dpkToMeetDTG" runat="server" Width="100%" DateFormat="DMY"></tk:DatePicker>
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearchPAMeeting" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="list-table-content" id="divPopupQuanHeGiuaCacCoQuanBaoChi" style="display: none; padding: 0px">
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td>
                                    <tk:TextArea ID="txtTextSearchPARelationship" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="list-table-content" id="divPopupAnhKhac" style="display: none; padding: 0px">
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td>
                                    <tk:TextArea ID="txtTextSearchPAOtherImage" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="popup-toolbar" style="text-align: center">
                    <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" class="btn btn-primary" Style="background: #434a54" />
                    <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
                </div>
            </PopupTemplate>
        </tk:PopupPanel>
    </div>

    <script>
        $(".image-ava").height($("#text-right").height() - 6);

        $(".display-order").height($("#text-right").height() - 6);

        $(".flex_popup-content-fixedsize").css("maxHeight", ($(window).height() - 100) + "px");

        function showTabPopup(index) {
            if (index == 0) {
                $('#popupThongTinChung').attr("class", "title-active");
                $('#popupDanhSachLienHe').attr("class", "");
                $('#popupLichSuThayDoiNhanSu').attr("class", "");
                $('#popupLichSuHopTacGapGo').attr("class", "");
                $('#popupQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
                $('#popupAnhKhac').attr("class", "");

                $('#divPopupThongTinChung').show();
                $('#divPopupDanhSachLienHe').hide();
                $('#divPopupLichSuThayDoiNhanSu').hide();
                $('#divPopupLichSuHopTacGapGo').hide();
                $('#divPopupQuanHeGiuaCacCoQuanBaoChi').hide();
                $('#divPopupAnhKhac').hide();
            }

            if (index == 1) {
                $('#popupThongTinChung').attr("class", "");
                $('#popupDanhSachLienHe').attr("class", "title-active");
                $('#popupLichSuThayDoiNhanSu').attr("class", "");
                $('#popupLichSuHopTacGapGo').attr("class", "");
                $('#popupQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
                $('#popupAnhKhac').attr("class", "");

                $('#divPopupThongTinChung').hide();
                $('#divPopupDanhSachLienHe').show();
                $('#divPopupLichSuThayDoiNhanSu').hide();
                $('#divPopupLichSuHopTacGapGo').hide();
                $('#divPopupQuanHeGiuaCacCoQuanBaoChi').hide();
                $('#divPopupAnhKhac').hide();
            }

            if (index == 2) {
                $('#popupThongTinChung').attr("class", "");
                $('#popupDanhSachLienHe').attr("class", "");
                $('#popupLichSuThayDoiNhanSu').attr("class", "title-active");
                $('#popupLichSuHopTacGapGo').attr("class", "");
                $('#popupQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
                $('#popupAnhKhac').attr("class", "");

                $('#divPopupThongTinChung').hide();
                $('#divPopupDanhSachLienHe').hide();
                $('#divPopupLichSuThayDoiNhanSu').show();
                $('#divPopupLichSuHopTacGapGo').hide();
                $('#divPopupQuanHeGiuaCacCoQuanBaoChi').hide();
                $('#divPopupAnhKhac').hide();
            }

            if (index == 3) {
                $('#popupThongTinChung').attr("class", "");
                $('#popupDanhSachLienHe').attr("class", "");
                $('#popupLichSuThayDoiNhanSu').attr("class", "");
                $('#popupLichSuHopTacGapGo').attr("class", "title-active");
                $('#popupQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
                $('#popupAnhKhac').attr("class", "");

                $('#divPopupThongTinChung').hide();
                $('#divPopupDanhSachLienHe').hide();
                $('#divPopupLichSuThayDoiNhanSu').hide();
                $('#divPopupLichSuHopTacGapGo').show();
                $('#divPopupQuanHeGiuaCacCoQuanBaoChi').hide();
                $('#divPopupAnhKhac').hide();
            }

            if (index == 4) {
                $('#popupThongTinChung').attr("class", "");
                $('#popupDanhSachLienHe').attr("class", "");
                $('#popupLichSuThayDoiNhanSu').attr("class", "");
                $('#popupLichSuHopTacGapGo').attr("class", "");
                $('#popupQuanHeGiuaCacCoQuanBaoChi').attr("class", "title-active");
                $('#popupAnhKhac').attr("class", "");

                $('#divPopupThongTinChung').hide();
                $('#divPopupDanhSachLienHe').hide();
                $('#divPopupLichSuThayDoiNhanSu').hide();
                $('#divPopupLichSuHopTacGapGo').hide();
                $('#divPopupQuanHeGiuaCacCoQuanBaoChi').show();
                $('#divPopupAnhKhac').hide();
            }

            if (index == 5) {
                $('#popupThongTinChung').attr("class", "");
                $('#popupDanhSachLienHe').attr("class", "");
                $('#popupLichSuThayDoiNhanSu').attr("class", "");
                $('#popupLichSuHopTacGapGo').attr("class", "");
                $('#popupQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
                $('#popupAnhKhac').attr("class", "title-active");

                $('#divPopupThongTinChung').hide();
                $('#divPopupDanhSachLienHe').hide();
                $('#divPopupLichSuThayDoiNhanSu').hide();
                $('#divPopupLichSuHopTacGapGo').hide();
                $('#divPopupQuanHeGiuaCacCoQuanBaoChi').hide();
                $('#divPopupAnhKhac').show();
            }

            var keepscroll = window.setTimeout(function () {
                var cs = document.cookie ? document.cookie.split(';') : [];
                var i = 0, cslen = cs.length;
                for (; i < cs.length; i++) {
                    var c = cs[i].split('=');
                    if (c[0].trim() == "keepscroll") {
                        window.scrollTo(0, parseInt(c[1]));
                        break;
                    }
                }
                window.clearTimeout(keepscroll);
                keepscroll = null;
            }, 0);
        }

        function changeURL(newURL) {
            window.history.pushState('', 'Smart Info', newURL);
        }

        function clickViewDetail(clientID) {
            $('#' + clientID + ' span').trigger('click');
        }
    </script>
</asp:Content>