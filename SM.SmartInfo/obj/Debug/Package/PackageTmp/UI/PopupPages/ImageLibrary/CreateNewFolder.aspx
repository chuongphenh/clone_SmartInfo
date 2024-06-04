<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateNewFolder.aspx.cs" Title="Thêm thư mục mục"
    Inherits="SM.SmartInfo.UI.PopupPages.ImageLibrary.CreateNewFolder"
    MasterPageFile="~/UI/MasterPages/Common/Popup.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidPressAgencyID" />
    <asp:HiddenField runat="server" ID="hidPressAgencyHRID" />
      <style>
          .content-flex {
                display : flex;
                background: unset;
                margin: 10px 30px;
                align-items: center;
          }
          .content-left {
                font-size: 15px;
                flex : 1;
          }
           .content-right {
                flex : 4;
          }
           .input-style { 
               width: 100%;
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
      </style>
  <div style="margin: 50px">
        <h3 style="text-align: center; font-size: 18px">Thêm thư mục/Album ảnh</h3>
        <div class="content-flex">
            <div class="content-left">
                 <asp:Label runat="server" ID="lbTxtBox"></asp:Label>
            </div>
             <div class="content-right">
                   <asp:TextBox runat="server" ID="txtFolderName" CssClass="input-style"></asp:TextBox>
            </div>
            <div>
                <asp:label runat="server" ID="errMessage" style="color:red"></asp:label>
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