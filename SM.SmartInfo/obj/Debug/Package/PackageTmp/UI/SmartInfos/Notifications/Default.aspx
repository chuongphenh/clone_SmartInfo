<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.Notifications.Default" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/SmartInfos/Notifications/EditUC.ascx" TagName="EditUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/Notifications/DisplayUC.ascx" TagName="DisplayUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .div-link:hover {
            cursor: pointer;
        }

        .div-active {
            background: #dcdfe4;
        }

        .description-text {
            height: 15px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }

        .notification-description {
            max-height: 30px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }
        .custom-notify-content h4 span{
            color: #000;
        }
        .custom-notify-content h4 span:hover{
            color: #141ed2;
        }
    </style>
    <div class="body-content" style="padding-top: 0px;">
        <asp:HiddenField ID="hidPage" runat="server" />
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px;">
            <div class="row blog-page">
                <%--SIDEBAR--%>
                <div class="col-sm-5" style="text-align: left;">
                    <div class="home-col-3">
                        <div class="home-block box-notifi">
                            <div class="home-block-title bg-home-block-title">
                                <h3 style="font-size:18px;">
                                    <span class="title-right" style="margin-top: -5px">
                                        <asp:LinkButton ID="btnShowPopupSearch" OnClick="btnShowPopupSearch_Click" CssClass="a-hover" runat="server">
                                            <i class="fa fa-search" aria-hidden="true" style="color: #141ed2;font-size: 16px; padding-right: 10px;" title="Tìm kiếm nâng cao"></i>
                                        </asp:LinkButton>
                                        <span class="title-right-icon">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                        <asp:DropDownList ID="ddlFilterTime" runat="server" style="border: unset" OnSelectedIndexChanged="ddlFilterTime_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </span>
                                    Thông báo
                                </h3>
                            </div>
                            <%--CONTENT--%>
                            <div class="home-block-content">
                                <ul class="home-notify" style="padding: 0px">
                                    <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <div class="div-link" id="divLink" runat="server">
                                                <li style="margin-left: 10px; margin-right: 10px; display: flex; align-items: center">
                                                    <span style="float:left; font-size: 14px; color: black; font-weight:bold; flex: 1; text-align: center;">
                                                          <%# (Container.ItemIndex + 1) + ((SM.SmartInfo.Utils.Utility.GetNullableInt(hidPage.Value) - 1) * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniTen) %>
                         
                                                    </span>
                                                        <asp:HiddenField ID="NotificationId" runat="server" />
                                    
                                                        <div id="divDoDTG" runat="server"><asp:Label style="font-size: 13px; flex: 2; color: #979797" ID="ltrDoDTG" runat="server"></asp:Label></div>
                                      
                                                        <div class="custom-notify-content home-notify-content" style="flex: 10; margin-right: 15px;">
                                                            <h4 class="no-margin"><asp:Label CssClass="notification-description" ID="ltrContent" runat="server"></asp:Label></h4>
                                                            <p style="color: #000" class="description-text"><asp:Literal ID="ltrNote" runat="server"></asp:Literal></p>
                                                    </div>
                                                      <div class="notify-note" style="color: #000; flex: 2; display: flex;">Ghi nhớ <span style="margin-left: 5px" class="circle-note">&nbsp;</span></div>
                                                </li>
                                            </div>
                                            <asp:LinkButton ID="btnViewDisplay" runat="server" style="display: none"><span>View Detail</span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <%--END CONTENT--%>
                        </div>
                    </div>
                    <div style="text-align: center; margin-top: 60px;">
                        <div class="text-center" style="display: inline-block">
                            <ul class="pagination">
                                <uc:PagerUC runat="server" id="Pager" OnPageIndexChanged="ucPager_PageIndexChanged" />
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

        <tk:PopupPanel ID="popSearch" runat="server" Title="TÌM KIẾM NÂNG CAO THÔNG BÁO" Width="800" CancelButton="btnCancel" OnPopupClosed="popSearch_PopupClosed">
            <PopupTemplate>
                <table class="table" style="width: 100%; margin-bottom: 0px">
                    <colgroup>
                        <col width="150" />
                        <col />
                        <col width="150" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>Thời gian diễn ra sự kiện từ</th>
                        <td>
                            <tk:DatePicker ID="dpkFromDoDTG" runat="server" Width="100%" DateFormat="DMY" />
                        </td>
                        <th>Đến</th>
                        <td>
                            <tk:DatePicker ID="dpkToDoDTG" runat="server" Width="100%" DateFormat="DMY" />
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
                <div class="popup-toolbar" style="text-align: center">
                    <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" OnClick="btnSearch_Click" class="btn btn-primary" Style="background: #434a54" />
                    <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
                </div>
            </PopupTemplate>
        </tk:PopupPanel>
    </div>

    <script>
        function changeURL(newURL) {
            window.history.pushState('', 'Smart Info', newURL);
        }

        function clickViewDetail(clientID) {
            $('#' + clientID + ' span').trigger('click');
        }
    </script>
</asp:Content>