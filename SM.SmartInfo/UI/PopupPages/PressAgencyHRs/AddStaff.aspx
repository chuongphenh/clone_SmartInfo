<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.AddStaff" 
    MasterPageFile="~/UI/MasterPages/Common/Popup.Master" Title="Thêm nhân sự vào tổ chức"%>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/EditUC.ascx" TagName="EditUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/DisplayUC.ascx" TagName="DisplayUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidType" runat="server" />
    <style>
        ul.press-agency-hr > li {
            width: 32.5%;
        }

        ul.press-agency-hr {
            max-height: unset;
            padding: 0px 0px 0px 30px;
        }

        .content-address {
            height: 20px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }

        .active-li > .div-link > .row > .col-sm-8 > .p-title {
            font-size: 14px;
            font-weight: 600;
            color: #1D39C4;
        }

        .active-li > .div-link > .row > .col-sm-4 {
            font-size: 13px;
            font-weight: 600;
        }

        .description-detail {
            font-size: 13px;
            height: 18px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }

        .description-address {
            font-size: 13px;
            height: 35px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }

        .div-link:hover {
            cursor: pointer;
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
    </style>
    <div class="body-content" style="padding-top: 0px;">
        <asp:HiddenField  runat="server" ID="hidPage"/>
        <div class="home-col-3">
                        <div class="home-block">
                            <div class="home-block-title">
                                <div style="margin-bottom: 5px">
                                    <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="margin-top: -5px; margin-left: 5px">
                                        <i class="fa fa-plus" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thêm mới tổ chức"> Tạo mới</i>
                                    </asp:LinkButton>
                                    <span class="title-right" style="margin-top: 5px">
                                        <asp:LinkButton ID="btnSearchPressAgencyHR" OnClick="btnSearchPressAgencyHR_Click" CssClass="a-hover" runat="server" Style="margin-top: -5px; margin-left: 5px">
                                            <i class="fas fa-user-friends" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tìm kiếm nhân sự"> Tìm kiếm</i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnShowPopupSearch" OnClick="btnShowPopupSearch_Click" CssClass="a-hover" runat="server" Style="margin-top: -5px; margin-left: 5px">
                                            <i class="fa fa-search" aria-hidden="true" style="color: #595959; margin-top: 3px; font-size: 16px; background: #F2F3F8; padding-bottom: 6px; padding-top: 6px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tìm kiếm nâng cao"></i>
                                        </asp:LinkButton>
                                    </span>
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
                                                        <p class="address-detail" style="font-size:12px; color:#8C8C8C; margin-top: 5px"><asp:Literal ID="ltrAddress" runat="server"></asp:Literal></p>
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
    </div>
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
</asp:Content>