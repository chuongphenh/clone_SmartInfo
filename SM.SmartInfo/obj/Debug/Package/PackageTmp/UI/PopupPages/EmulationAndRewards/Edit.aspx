<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPages/Common/Popup.Master" CodeBehind="Edit.aspx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.EmulationAndRewards.Edit" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        hr {
            margin-top: 15px;
            margin-bottom: 15px;
            border: 0;
        }

        span {
            font-size: 14px;
            color: #262626;
        }

        .div-display {
            border-radius: 25px;
            padding-bottom: 5px;
            padding-top: 5px;
            padding-right: 20px !important;
            padding-left: 20px !important;
            background: #f2f3f8 !important;
        }

        .div-link:hover {
            cursor: pointer;
        }
    </style>
    <asp:HiddenField runat="server" ID="hidSubjectID" />
    <div class="toolbar" style="background: #F2F3F8; border: unset; padding: 0 35px; font-family: SF Pro Display">
        <span style="color: #595959">CHI TIẾT</span>
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server">
                    <i class="far fa-save" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Hoàn thành"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: white; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thoát"></i>
                </asp:HyperLink>
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
                <div style="width: 100%; padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px; text-align: left">
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Mã</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:Label ID="ltrCode" runat="server" Style="font-size: 14px"></asp:Label>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Tên</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:TextBox ID="txtName" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Đơn vị</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:TextBox ID="txtUnit" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Email</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:TextBox ID="txtEmail" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-1" style="width: 15%">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Số điện thoại</span>
                        </div>
                        <div class="col-md-11 div-display" style="width: 85%">
                            <asp:TextBox ID="txtMobile" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Danh sách lần khen thưởng</span>
                            <div style="width: 100%;">
                                <div class="new-grid">
                                    <asp:DataGrid ID="grdData" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="false"
                                        AllowCustomPaging="true" AutoGenerateColumns="false" OnItemDataBound="grdData_ItemDataBound" GridLines="None">
                                        <HeaderStyle CssClass="grid-header" />
                                        <ItemStyle CssClass="grid-even" />
                                        <AlternatingItemStyle CssClass="grid-odd" />
                                        <FooterStyle CssClass="grid-footer" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="STT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# (Container.DataSetIndex + 1) %>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Danh hiệu" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Đơn vị khen thưởng" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltrEmulationAndRewardUnit" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Thời gian" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltrRewardedDTG" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <span style="color: #262626; font-size: 14px; font-weight: 600;">Danh sách tài liệu đi kèm</span>
                            <div class="row content new-grid" style="padding: 0px; width: auto; margin-left: 10px; margin-right: 10px">
                                <asp:DataGrid ID="grdAtt" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="false"
                                    AllowCustomPaging="true" AutoGenerateColumns="false" OnItemDataBound="grdAtt_ItemDataBound" GridLines="None">
                                    <HeaderStyle CssClass="grid-header" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle CssClass="grid-even" />
                                    <AlternatingItemStyle CssClass="grid-odd" />
                                    <FooterStyle CssClass="grid-footer" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Loại tài liệu" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidAttachmentID" />
                                                <asp:Literal runat="server" ID="ltrDocumentName" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tên tài liệu" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal runat="server" ID="ltrName" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Người đưa lên" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal runat="server" ID="ltrCreatedBy" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Ngày đưa lên" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal runat="server" ID="ltrCreatedDTG" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tải về" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDownLoad" runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--END DESIGN--%>
    </div>
</asp:Content>