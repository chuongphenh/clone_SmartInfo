<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderContent.ascx.cs"
    Inherits="SM.SmartInfo.UI.Shared.Common.HeaderContent" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<style>
    .txt-title {
        font-style: normal;
        font-weight: bold;
        font-size: 16px;
    }

    .has-search .form-control {
        padding-left: 15px;
    }

    .has-search .form-control-feedback {
        position: absolute;
        z-index: 2;
        display: block;
        width: 2.375rem;
        height: 2.375rem;
        line-height: 2.375rem;
        text-align: center;
        pointer-events: unset;
        color: #aaa;
    }

    .menu > ul > li:hover {
        border-bottom: 2px solid #fff;
        padding-bottom: 0px;
        text-decoration: unset;
    }

    .menu > ul > li > a.menu-active {
        border-bottom: 2px solid #fff;
        padding-bottom: 8px;
    }

    .dropdown-menu > li:hover {
        background: #F2F3F8;
    }

    .navbar-nav > li > .dropdown-menu {
        margin-top: 2px;
    }

    ::-webkit-scrollbar-track {
        /*-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);*/
        border-radius: 4px;
        background-color: #fff;
    }

    ::-webkit-scrollbar {
        width: 5px;
        height: 5px;
        background-color: #F5F5F5;
    }

    ::-webkit-scrollbar-thumb {
        border-radius: 10px;
        /*-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);*/
        background-color: #E8E8E8;
    }

    .Flipped, .Flipped .Content {
        transform: rotateX(180deg);
        -ms-transform: rotateX(180deg); /* IE 9 */
        -webkit-transform: rotateX(180deg); /* Safari and Chrome */
    }

    .dropdown-menu {
        left: unset;
        right: 0px;
    }

    .href-logo:hover {
        text-decoration: none;
    }
    .custom-menu-global li a i{
        color:#fff!important;
    }
     .custom-menu-global li ul li a i{
        color:#2F54EB!important;
    }
</style>

<%--TITLE--%>
<div class="row" style="display:flex;">
    <div class="col-sm-1">
        <a href="/UI/Shared/Common/Default.aspx" class="href-logo">
            <div class="head-logo" style="text-align: center;margin-top:20px;">
                <%--<img src="../../../Images/smart-infos/logo.svg" alt="Smart info" style="margin-bottom: 5px;width:90px;" />--%>
            </div>
        </a>
    </div>
    <!--/navbar-collapse-->
    <div id="divMenu" class="col-md-8" style="text-align: center; margin-top: 5px; height: 42px; flex: 8;">
        <div class="menu collapse navbar-collapse navbar-responsive-collapse" >
            <%-- overflow-x: scroll;--%> 
            <ul id="ulMenu" class="nav navbar-nav Flipped custom-menu-global" style="display: inline-flex; max-width: 100% !important;">
                <asp:Repeater ID="rptMenu1" runat="server" OnItemDataBound="rptMenu1_ItemDataBound">
                    <ItemTemplate>
                        <li class="dropdown Content">
                            <asp:HyperLink ID="hypMenu1Link" runat="server" class="menu-item dropdown-toggle" data-toggle="dropdown">
                                <div style="
                                    justify-content: center;
                                    display: flex;
                                    flex-direction: column;
                                    align-items: center;">
                                    <asp:Literal ID="ltrMenu1Icon" runat="server"></asp:Literal>
                                     <div style="display: inline-block; font-size: 14px; color: #fff; font-weight: 600; padding: 5px 0;">&nbsp;<asp:Literal ID="ltrMenu1Name" runat="server"></asp:Literal></div>
                                </div>
                                <div style="display: inline-block; font-size: 14px; color: #fff; font-weight: 600;"></div>
                            </asp:HyperLink>
                            <ul class="dropdown-menu" style="padding-left: 0px;" id="ulMenu2" runat="server">
                                <asp:Repeater ID="rptMenu2" runat="server" OnItemDataBound="rptMenu2_ItemDataBound">
                                    <ItemTemplate>
                                        <li style="width: 100%; padding: 5px; padding-bottom: 5px; padding-top: 5px;">
                                            <asp:HyperLink ID="hypMenu2Link" runat="server">
                                                <asp:Literal ID="ltrMenu2Icon" runat="server"></asp:Literal>
                                                <div style="display: inline-block; font-size: 14px; color: #595959; font-weight: 600;">&nbsp;<asp:Literal ID="ltrMenu2Name" runat="server"></asp:Literal></div>
                                            </asp:HyperLink>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
  <%--  <div class="col-sm-2" style="margin-top: 10px;">
        <div class="form-group has-search" style="float: right; position: absolute; right: 10px; top: 5px;">
            <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" class="fa fa-search form-control-feedback"></asp:LinkButton>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Style="border-radius: 20px;" placeholder="Tìm kiếm ..."></asp:TextBox>
        </div>
    </div>--%>
    <div class="col-md-3" style="flex: 2; position: relative;">
          <div class="form-group has-search custom-reposive-form form-res-custom" style="position: absolute; left: 0;   top: 65%; transform: translate(-50%, -50%);">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnSearch_Click" class="fa fa-search form-control-feedback"></asp:LinkButton>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Style="border-radius: 20px;" placeholder="Tìm kiếm ..."></asp:TextBox>
        </div>
        <li class="user-name" style="    z-index: 1;
    position: absolute;
    top: 65%;
    right: 10px;
    transform: translate(0, -50%);">

            <a href="#" style="width: auto; padding: 8px; font-size: 14px; color: #fff; text-transform: none; float: left;align-items:center;gap:5px;">
                            <svg style="float: left;" width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                <circle cx="16" cy="16" r="16" fill="#85A5FF" />
                <path d="M22 22V20.6667C22 19.1939 20.6569 18 19 18H13C11.3431 18 10 19.1939 10 20.6667V22" stroke="white" stroke-linecap="round" stroke-linejoin="round" />
                <path fill-rule="evenodd" clip-rule="evenodd" d="M16 15C17.6569 15 19 13.6569 19 12C19 10.3431 17.6569 9 16 9C14.3431 9 13 10.3431 13 12C13 13.6569 14.3431 15 16 15Z" stroke="white" stroke-linecap="round" stroke-linejoin="round" />
            </svg>
                <span class="content-setting"><asp:Literal ID="ltrUserName" runat="server"></asp:Literal></span>
                
            </a>
            <div class="panel panel-primary xn-drop-left">
                <div class="panel-heading ui-draggable-handle">
                </div>
                <div class="panel-body list-group">
                    <div class="mCSB_container">
                        <asp:HyperLink ID="lnkLogout" CssClass="list-group-item" runat="server">
                            <i class="fas fa-sign-out-alt"></i>
                            <asp:Literal runat="server" ID="lcUserControlLogout" Text="Đăng xuất" />
                        </asp:HyperLink>
                        <a runat="server" id="hrefChangePass" onclick="javascript:PopupCenter('/UI/Administrations/Users/ChangePass.aspx', '', 1000, 700);" class="list-group-item" href="#"><i class="fa fa-user"></i>Thay đổi mật khẩu</a>
                        <a onclick="javascript:PopupCenter('/UI/Shared/Common/UserDetails.aspx', '', 1000, 700);" class="list-group-item" href="#"><i class="fa fa-user"></i>Thông tin người dùng</a>
                    </div>
                </div>
            </div>
        </li>
    </div>
</div>
<%-- END TITLE--%>

<script>
    $(".dropdown").hover(function () {
        $(this).addClass("active");

        var maxHeight = Math.max.apply(null, $(".dropdown-menu").map(function () {
            return $(this).height();
        }).get());

        $("#ulMenu").css("padding-top", maxHeight);
    }, function () {
        $(this).removeClass("active");
        $("#ulMenu").css("padding-top", 0);
    });

    $("#ulMenu").css("maxWidth", $('#divMenu').width() - 60);
</script>