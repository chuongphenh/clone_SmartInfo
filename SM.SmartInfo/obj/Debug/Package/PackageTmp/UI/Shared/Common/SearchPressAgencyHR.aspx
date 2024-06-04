<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPressAgencyHR.aspx.cs" Inherits="SM.SmartInfo.UI.Shared.Common.SearchPressAgencyHR"
    MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .view-all {
            font-size: 14px;
            color: white;
            padding: 3px;
            padding-right: 15px;
            padding-left: 15px;
            border: 1px solid #597EF7;
            background: #597EF7;
            border-radius: 100px;
        }

            .view-all:hover {
                cursor: pointer;
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
    <asp:HiddenField ID="hidPage" runat="server" />
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="body-content" style="background: #F2F3F8">
        <%--DESIGN--%>
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px">
            <div class="row">
                <div class="col-sm-4">
                    <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px;">THÔNG TIN NHÂN SỰ
                    </h3>
                    <table class="table" style="width: 100%; margin-bottom: 0px">
                        <colgroup>
                            <col width="80" />
                            <col />
                            <col width="80" />
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
                                <tk:TextArea ID="txtTextSearchPAHR" runat="server" Width="100%" TextMode="MultiLine" Rows="1"></tk:TextArea>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-4">
                    <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px;">LỊCH SỬ GẶP MẶT
                    </h3>
                    <table class="table" style="width: 100%; margin-bottom: 0px">
                        <colgroup>
                            <col width="80" />
                            <col />
                            <col width="80" />
                            <col />
                        </colgroup>
                        <tr>
                            <th>Ngày gặp từ
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
                </div>
                <div class="col-sm-4">
                    <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px;">THÔNG TIN GIA ĐÌNH
                    </h3>
                    <table class="table" style="width: 100%; margin-bottom: 0px">
                        <colgroup>
                            <col width="80" />
                            <col />
                            <col width="80" />
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
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align: center">
                    <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click">
                        <span class="view-all" id="view-all-image">Tìm kiếm&nbsp;&nbsp;<i class="fa fa-search"></i></span>
                    </asp:LinkButton>
                </div>
            </div>
            <hr />
            <div class="home-block">
                <ul class="press-agency-hr" style="max-height: unset; overflow: unset">
                    <asp:Repeater ID="rptHR" runat="server" OnItemDataBound="rptHR_ItemDataBound">
                        <ItemTemplate>
                            <li class="active-li">
                                <div class="div-link" id="divLink" runat="server">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <a id="linkItem" runat="server" href="#">
                                                <img id="img" runat="server" class="img-responsive" src="" alt="" style="height: 60px; width: 60px; border-radius: 50%; object-fit: cover">
                                            </a>
                                        </div>
                                        <div class="col-sm-8">
                                            <p class="p-title" style="margin: 15px 0 0">
                                                <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                                            </p>
                                            <p style="color: #1D39C4">
                                                <asp:Literal ID="ltrPosition" runat="server"></asp:Literal>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Tổ chức</div>
                                        <div class="col-sm-8 description-detail">
                                            <asp:Literal ID="ltrPressAgencyName" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Tuổi</div>
                                        <div class="col-sm-8 description-detail">
                                            <asp:Literal ID="ltrAge" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Ngày sinh</div>
                                        <div class="col-sm-8 description-detail">
                                            <asp:Literal ID="ltrDOB" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Điện thoại</div>
                                        <div class="col-sm-8 description-detail">
                                            <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Email</div>
                                        <div class="col-sm-8 description-detail">
                                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Địa chỉ</div>
                                        <div class="col-sm-8 description-address">
                                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Thái độ</div>
                                        <div class="col-sm-8 description-detail">
                                            <asp:Label ID="ltrAttitude" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">Ghi nhớ</div>
                                        <div class="col-sm-8 description-detail">
                                            <span class="circle-note">&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
    <div class="row">
        <div class="col-sm-12" style="text-align: center">
            <div style="text-align: center; margin-top: 20px;">
                <div class="text-center" style="display: inline-block">
                    <ul class="pagination">
                        <uc:PagerUC runat="server" ID="Pager" OnPageIndexChanged="ucPager_PageIndexChanged" />
                    </ul>
                </div>
            </div>
        </div>
    </div>
    </section>
    </div>
    <%--END DESIGN--%>
</asp:Content>