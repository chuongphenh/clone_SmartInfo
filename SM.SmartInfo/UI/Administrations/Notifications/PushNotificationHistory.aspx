<%@ Page Title="Lịch sử thông báo" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="PushNotificationHistory.aspx.cs" Inherits="SM.SmartInfo.UI.Administration.Notifications.PushNotificationHistory" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .icon_toolbar li a {
            background: #F2F3F8;
            padding: 5px 10px;
            border-radius: 4px;
            font-weight: bold;
        }

            .icon_toolbar li a i {
                padding-top: 0;
            }
    </style>
    <asp:HiddenField ID="hdID" runat="server" />
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="toolbar">
        LỊCH SỬ THÔNG BÁO
        <%--<ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="lnkExit" runat="server" OnClientClick='window.close();'> <i class="fas fa-sign-out-alt" aria-hidden="true" style="margin-top: 4px; font-size: 16pt;" title="Thoát"></i> </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Text="Sửa" Style="float: right; margin-left: 5px"> 
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Sửa"> Sửa</i>
                </asp:HyperLink>
            </li>
        </ul>--%>
    </div>

    <div style="margin-top: 50px;">
        <div class="body-content">
            <div style="background: #FFF; border: 1px solid #D9D9D9;">
                <div class="list-table-content" style="padding-top: 30px; padding-bottom: 40px">
                    <div class="err">
                        <uc:ErrorMessage ID="ErrorMessage1" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <%--<h3>Vai trò</h3>--%>
                            <div class="new-grid" style="width: 100%; height: 100%; overflow: scroll"">
                                <%--<asp:DataGrid ID="grdRole" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="false"
                                    AllowCustomPaging="false" AutoGenerateColumns="false" GridLines="None"
                                    BackColor="White" OnItemDataBound="grdRole_ItemDataBound">
                                    <HeaderStyle CssClass="grid-header" />
                                    <ItemStyle CssClass="grid-even" />
                                    <AlternatingItemStyle CssClass="grid-odd" />
                                    <FooterStyle CssClass="grid-footer" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="#">
                                            <HeaderStyle Width="60px" Font-Bold="true" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hiRoleID" runat="server" />
                                                <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" Checked='<%# Eval("IsSelect") %>'></asp:CheckBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Name" HeaderText="Tên chức năng">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Description" HeaderText="Mô tả">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="10" />
                                </asp:DataGrid>--%>
                            <%--    <asp:DataGrid ID="grdNotificationPushHistory" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="true"
                                    AllowCustomPaging="false" AutoGenerateColumns="false" GridLines="None" BackColor="White"
                                    OnItemDataBound="grdRole_ItemDataBound" Width="100%" HorizontalAlign="Left" BorderStyle="Solid" BorderWidth="1px">
                                    <HeaderStyle CssClass="grid-header" />
                                    <ItemStyle CssClass="grid-even" />
                                    <AlternatingItemStyle CssClass="grid-odd" />
                                    <FooterStyle CssClass="grid-footer" />
                                    <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="10" />
                                    <Columns>
                                       
                                        <asp:BoundColumn DataField="ID" HeaderText="ID">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Content" HeaderText="Content">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CreatedDTG" HeaderText="Created Date Time" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CreatedBy" HeaderText="Created By">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="RefData" HeaderText="Ref Data">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Title" HeaderText="Title">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="EmployeeID" HeaderText="Employee ID">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="IsRead" HeaderText="Is Read">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Status" HeaderText="Status">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ClientMessageID" HeaderText="Client Message ID">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Error" HeaderText="Error">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DeviceID" HeaderText="Device ID">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TransactionId" HeaderText="Transaction ID">
                                            <HeaderStyle Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>--%>
                                <asp:DataGrid ID="grdNotificationPushHistory" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="true" PageSize="20"
                                    AllowCustomPaging="false" AutoGenerateColumns="false" GridLines="None" BackColor="White"
                                    OnItemDataBound="grdRole_ItemDataBound" Width="100%" HorizontalAlign="Left" BorderStyle="Solid" BorderWidth="1px"  OnPageIndexChanged="grdNotificationPushHistory_PageIndexChanged">
                                    <HeaderStyle CssClass="grid-header" />
                                    <ItemStyle CssClass="grid-even" />
                                    <AlternatingItemStyle CssClass="grid-odd" />
                                    <FooterStyle CssClass="grid-footer" />
                                    <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="20" />
                                    <Columns>
                                        <asp:BoundColumn ItemStyle-Width="3%" DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Font-Bold="true"/>
                                        <asp:BoundColumn ItemStyle-Width="20%" DataField="Content" HeaderText="Content" ItemStyle-HorizontalAlign="Left"   HeaderStyle-Font-Bold="true"/>
                                        <%--<asp:BoundColumn DataField="RefData" HeaderText="Ref Data" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" />--%>
                                        <asp:BoundColumn ItemStyle-Width="20%"  DataField="Title" HeaderText="Title" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" />
                                        <%--<asp:BoundColumn DataField="IsRead" HeaderText="Is Read" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Font-Bold="true"/>--%>
                                        <asp:BoundColumn ItemStyle-Width="3%"  DataField="Status" HeaderText="Status Code" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Font-Bold="true"/>
                                        <asp:BoundColumn ItemStyle-Width="15%"  DataField="ClientMessageID" HeaderText="Client Message ID" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Font-Bold="true"/>
                                        <asp:BoundColumn ItemStyle-Width="10%"  DataField="Error" HeaderText="Error" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Font-Bold="true"/>
                                        <asp:BoundColumn ItemStyle-Width="10%"  DataField="DeviceID" HeaderText="FCM Token" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"/>
                                        <asp:BoundColumn ItemStyle-Width="3%"  DataField="EmployeeID" HeaderText="Employee ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" />
                                        <asp:BoundColumn ItemStyle-Width="5%"  DataField="CreatedDTG" HeaderText="Created Date Time" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Font-Bold="true"/>
                                        <asp:BoundColumn ItemStyle-Width="3%"  DataField="CreatedBy" HeaderText="Created By" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" />
                                        <%--<asp:BoundColumn DataField="TransactionId" HeaderText="Transaction ID" ItemStyle-HorizontalAlign="Left"  HeaderStyle-Font-Bold="true"/>--%>
                                    </Columns>
                                </asp:DataGrid>


                                </>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
