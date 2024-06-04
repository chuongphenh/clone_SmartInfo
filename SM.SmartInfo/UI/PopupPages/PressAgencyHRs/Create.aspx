<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Title="Thêm nhóm tổ chức"
    Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.Create"
    MasterPageFile="~/UI/MasterPages/Common/Popup.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRUC.ascx" TagName="PressAgencyHRUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRAlertUC.ascx" TagName="PressAgencyHRAlertUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRHistoryUC.ascx" TagName="PressAgencyHRHistoryUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRRelativesUC.ascx" TagName="PressAgencyHRRelativesUC" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidPressAgencyID" />
    <asp:HiddenField runat="server" ID="hidPressAgencyHRID" />
      <style>
          .content-flex {
                display : flex;
                background: unset;
                margin: 10px 100px;
                align-items: center;
          }
          .content-left {
                font-size: 15px;
                flex : 2;
          }
           .content-right {
                flex : 5;
          }
           .input-style { 
               width: 100% !important;
           }
           .button-acm {
               align-items: center;
               text-align: center;
               color: white; 
               padding: 10px 20px;
               border-radius: 5%;
               font-size: 15px;
               border: 1px solid #597EF7;
                background: #597EF7;
           }
           .style-center {
                justify-content: center;
           }
            .input-style { 
               width: 100%;
           }
      </style>
  <div style="margin: 50px">
        <h3 style="text-align: center; font-size: 18px">Thêm loại tổ chức</h3>
        <div class="content-flex">
            <div class="content-left">
                 <asp:Label runat="server">Tên tổ chức:</asp:Label>
            </div>
             <div class="content-right">
                   <asp:TextBox runat="server" ID="txtAgencyName" CssClass="input-style"></asp:TextBox>
            </div>
        </div>
        <div class="content-flex">
            <div class="content-left">
               <asp:Label runat="server">Mã tổ chức:</asp:Label>
            </div>
             <div class="content-right">
                  <asp:TextBox runat="server" ID="txtAgencyCode" CssClass="input-style"></asp:TextBox>
            </div>
        </div>
        <div style="text-align: center">
            <asp:Button CssClass="button-acm" runat="server" Text="Tạo mới" ID="btnCreateAndSave" OnClick="btnCreateAndSave_Click"/>
        </div>
    </div>
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
</asp:Content>
