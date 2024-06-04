<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    Inherits="SM.SmartInfo.UI.SmartInfos.News.Default" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/News/SideBarTreeViewUC.ascx" TagPrefix="uc" TagName="SideBarTreeViewUC" %>
<%@ Register Src="~/UI/UserControls/PressAgencySelector.ascx" TagPrefix="uc" TagName="PressAgencySelector" %>
<%@ Register Src="~/UI/UserControls/CatalogNewsSelectorTreeUC.ascx" TagPrefix="uc" TagName="CatalogNewsSelectorTreeUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .content-description {
            height: 100px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 6;
            overflow: hidden;
        }

        .h3-header {
            height: 36px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }

        .news-description {
            height: 78px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 3;
            overflow: hidden;
        }
        .flex_tree-ul li{
            font-size:13px;
        }
                    .custom-sidebar-menu .flex_tree-ul li{
                font-weight: bold;
                font-size: 14px;
                background: #e0f0ff;
                margin-bottom: 4px;
                padding: 7px 5px!important;
            }
            .custom-sidebar-menu .flex_tree-cover{
                background: #cbcbcb4f;
                border-radius: 5px;
                padding:5px;
                border-radius:5px;
            }
            .custom-sidebar-menu .flex_tree-cover li ul{
                background: #fff;
                padding:0;
            }
            .custom-sidebar-menu .flex_tree-cover li ul li{
                background: #fff;
                padding-left:10px!important;
                border-bottom:1px solid #ddd;
            }
            .custom-sidebar-menu .flex_tree-cover li ul li:last-child{
                border-bottom:none;
            }
    </style>
    <asp:HiddenField ID="hidPage" runat="server" />
    <script>
         <!-- OWL -->
        $(document).ready(function() {
            $('#slider .owl-carousel').owlCarousel({
                loop:true,
                margin: 0,
                nav:false,
                items: 1,
                autoplayTimeout: 4000,
                autoplay:true
            })
        });
    </script>
    <div class="body-content" style="padding-top: 0px;">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px; padding-bottom: 25px">
             <div class="row" style="margin-bottom: 20px; margin-top: 10px;">
                <div id="slider">
                    <div class="owl-carousel owl-theme">
                       <asp:Repeater ID="rptDataTop" runat="server" OnItemDataBound="rptDataTop_ItemDataBound">
                          <ItemTemplate>
                              <div class="col-sm-12 hot-news" style="margin-bottom: 20px;">
                                  <div class="hot-news-item" style="min-height: unset; width: 100%">
                                      <img style=" width: 100%;
                                                      height: 500px;
                                                      object-fit: cover;" id="imgRpt" runat="server" class="img-responsive hover-effect" alt="" />
                                      <div class="hot-news-item-content">
                                          <div class="hot-news-item-text" style="padding-bottom: 20px;">
                                              <p class="news-date" style="font-size: 20px;" >
                                                  <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal>
                                              </p>
                                              <h3 style="max-height: unset; margin-bottom: -8px">
                                                  <asp:HyperLink style="font-size: 32px; line-height: 36px;" runat="server" ID="hplName"></asp:HyperLink>
                                              </h3>
                                          </div>                                            
                                      </div>
                                  </div>
                              </div>                      
                          </ItemTemplate>
                      </asp:Repeater>
                    </div>
                </div>
             </div>
            <%-- <div class="row" style="margin-bottom: 50px;"> --%>
        	   <%--  <div class="col-sm-6"> --%>
            <%--         <!-- Clients Block--> --%>
            <%--         <div class="hot-news"> --%>
            <%--               <div class="hot-news-item" style="width: 100%"> --%>
            <%--                   <img id="img" runat="server" class="img-responsive hover-effect" alt="" style="object-fit: cover" /> --%>
            <%--                   <div class="hot-news-item-content"> --%>
            <%--                       <div class="hot-news-item-text"> --%>
            <%--                           <p class="news-date"> --%>
            <%--                               <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal> --%>
            <%--                           </p> --%>
            <%--                           <h3 style="margin-bottom: -8px; max-height: unset"> --%>
            <%--                               <asp:HyperLink runat="server" ID="hplName"></asp:HyperLink> --%>
            <%--                           </h3> --%>
            <%--                       </div>                                             --%>
            <%--                   </div> --%>
            <%--               </div> --%>
            <%--           </div> --%>
            <%--         <!-- End Clients Block--> --%>
            <%--     </div> --%>
        	   <%--  <div class="col-sm-6"> --%>
            <%--         <!-- Clients Block--> --%>
            <%--         <div class="row"> --%>
            <%--             --%>
            <%--         </div> --%>
            <%--         <!-- End Clients Block--> --%>
            <%--     </div> --%>
            <%-- </div><!--/row-->  --%>
            <div class="row">
                <div class="col-sm-3">
                    <h3 style="font-size:16px; font-weight: 700; margin-bottom:20px; font-family: SF Pro Display; color: #000000;">
                        Tin tức
                    </h3>
                    <div class="custom-sidebar-menu">
                        <uc:SideBarTreeViewUC ID="ucSideBarTreeView" runat="server" OnSelectedNodeChanged="ucSideBarTreeView_SelectedNodeChanged" />
                    </div>
                </div>
                <div class="col-sm-9 custom-link-focus">
                    <h3 style="font-size:16px; font-weight: 600; margin-bottom:20px; font-family: SF Pro Display; color: #000000;">
                        <span style="opacity: 0">Tin tức</span>
                        <asp:LinkButton ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: white; padding-bottom: 5px;padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                            <i class="fas fa-file-excel" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Xuất dữ liệu tin tức"></i>
                            <span style="color: #595959;font-weight:600;font-size:16px;">Export</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnShowPopupUpload" runat="server" OnClick="btnShowPopupUpload_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: white; padding-bottom: 5px;  padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                            <i class="fa fa-upload" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tải tài liệu tin tức"></i>
                            <span style="color: #595959;font-weight:600;font-size:16px;">Import</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnShowPopupSearch" runat="server" OnClick="btnShowPopupSearch_Click" Style="float: right; margin-top: -5px; margin-left: 5px; padding-bottom: 5px;background: white; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                            <i class="fa fa-search" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Tìm kiếm nâng cao"></i>
                            <span style="color: #595959;font-weight:600;font-size:16px;">Tìm kiếm nâng cao</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: white; padding-bottom: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                            <i class="fas fa-plus" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thêm mới"></i>
                            <span style="color: #595959;font-weight:600;font-size:16px;">Thêm mới</span>
                        </asp:LinkButton>
                    </h3>
                    <div class="list-news">
                        <ul>
                            <asp:Repeater ID="rptDataBelow" runat="server" OnItemDataBound="rptDataBelow_ItemDataBound">
                                <ItemTemplate>
                                    <li class="news-detail">
                                        <img id="imgRpt" runat="server" src="" alt="" style="object-fit: contain" />
                                        <div class="list-news-content">
                                            <h4>
                                                <asp:HyperLink runat="server" ID="hplName"></asp:HyperLink>
                                            </h4>
                                            <p class="news-date">
                                                <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal>
                                            </p>
                                            <p class="news-description">
                                                <asp:Label ID="ltrContent" runat="server"></asp:Label>
                                            </p>
                                            <asp:HyperLink runat="server" ID="hypReadMore" Text="Xem tiếp"></asp:HyperLink>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div style="text-align: center;">
                            <div class="text-center" style="display: inline-block">
                                <ul class="pagination">
                                    <uc:PagerUC runat="server" id="Pager" OnPageIndexChanged="ucPager_PageIndexChanged" />
                                </ul>                                                            
                            </div>
                        </div>
                    </div>
                </div>
                <%-- <div class="col-sm-3"> --%>
                <%--     <h3 style="font-size:16px; font-weight: 700; margin-bottom:20px; font-family: SF Pro Display; color: #000000;"> --%>
                <%--         Tin theo dõi --%>
                <%--     </h3> --%>
                <%--     <div class="list-news-follow"> --%>
                <%--         <ul> --%>
                <%--             <asp:Repeater ID="rptDataFollow" runat="server" OnItemDataBound="rptDataFollow_ItemDataBound"> --%>
                <%--                 <ItemTemplate> --%>
                <%--                     <li class="news-follow" style="border-bottom: 1px solid #E5E5E5; padding-bottom: 10px"> --%>
                <%--                         <asp:HyperLink class="news-follow-text" ID="hypName" runat="server" style="margin-bottom: 5px"></asp:HyperLink> --%>
                <%--                         <p> --%>
                <%--                             <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal> --%>
                <%--                         </p> --%>
                <%--                         <asp:HyperLink ID="hypLink" runat="server"> --%>
                <%--                             <img id="imgRpt" runat="server" src="" alt="" style="height: 220px; object-fit: cover" /> --%>
                <%--                         </asp:HyperLink> --%>
                <%--                     </li> --%>
                <%--                 </ItemTemplate> --%>
                <%--             </asp:Repeater> --%>
                <%--         </ul> --%>
                <%--     </div> --%>
                <%-- </div> --%>
            </div>
        </section>

        <tk:PopupPanel ID="popSearch" runat="server" Title="TÌM KIẾM NÂNG CAO TIN TỨC" Width="1200" CancelButton="btnCancel">
            <PopupTemplate>
                <div class="row">
                    <div class="col-md-6">
                        <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px; display: none">TIN TỨC
                        </h3>
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Ngày đăng tin từ</th>
                                <td>
                                    <tk:DatePicker ID="dpkPostingFromDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                                <th>Đến</th>
                                <td>
                                    <tk:DatePicker ID="dpkPostingToDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <th>Tổng số bài đăng</th>
                                <td colspan="3">
                                    <tk:NumericTextBox ID="numNumberOfPublish" runat="server" Width="100%" AllowThousandDigit="true" NumberDecimalDigit="0"></tk:NumericTextBox>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <th>Danh mục tin</th>
                                <td colspan="3">
                                    <uc:CatalogNewsSelectorTreeUC runat="server" ID="ucCatalogNewsSelectorTree" Width="100%" />
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
                    <div class="col-md-6">
                        <h3 style="font-family: SF Pro Display; font-size: 14px; font-weight: 600; padding: 7px; margin: 0px; display: none">KẾT QUẢ CỤ THỂ
                        </h3>
                        <table class="table" style="width: 100%; margin-bottom: 0px">
                            <colgroup>
                                <col width="150" />
                                <col />
                                <col width="150" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>Phương tiện</th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlType" runat="server" Width="100%" MaxLength="256"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>Tuyến bài</th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlCampaignID" runat="server" Width="100%"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <th>Kênh truyền thông</th>
                                <td colspan="3">
                                    <uc:PressAgencySelector runat="server" Width="100%" ID="ucPressAgencySelector" DataTextField="Name" IsSearchAll="true" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <th>Ngày phát hành từ</th>
                                <td>
                                    <tk:DatePicker ID="dpkFromPublishDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                                <th>Đến</th>
                                <td>
                                    <tk:DatePicker ID="dpkToPublishDTG" runat="server" Width="100%" DateFormat="DMY" />
                                </td>
                            </tr>
                            <tr>
                                <th>Từ khóa
                                </th>
                                <td colspan="3">
                                    <tk:TextArea ID="txtTextSearchSpecificResults" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></tk:TextArea>
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
</asp:Content>