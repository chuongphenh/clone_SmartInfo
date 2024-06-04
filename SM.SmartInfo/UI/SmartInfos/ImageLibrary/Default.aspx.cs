using DocumentFormat.OpenXml.Spreadsheet;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.SmartInfos.ImageLibrary
{
    public partial class Default : BasePage
    {
        private static List<int> lstItemId = new List<int>();
        private static string Year;
        private static DateTime? PostedDTG;
        private static DateTime? DateFrom;
        private static DateTime? DateTo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (string.IsNullOrEmpty(hidrefType.Value))
                    {
                        hidPage.Value = "1";
                        SetUpForm();
                        GetItemForView();
                    }
                    else
                    {
                        SetUpForm();
                        GetItemByFilter();
                    }
                }
                tvCatalogNews.SelectedNodeChanged += tvCatalogNews_SelectedNodeChanged;
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        //Start Code Update
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
        //End Code Update
        private void SetUpForm()
        {
            ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.GetListImageCatalog);
            MainController.Provider.Execute(param);

            List<ImageCatalog> listImageCatalog = param.listImageCatalog;

            List<TreeNodeItem> lstDataTree = new List<TreeNodeItem>();

            ImageLibraryParam param1 = new ImageLibraryParam(FunctionType.ImageLibrary.GetListPostedYears);
            MainController.Provider.Execute(param1);

            var PostedYearsDict = new Dictionary<int, string>();
            foreach (var year in param1.listPostedYears)
            {
                PostedYearsDict.Add(year, year.ToString());
            }

            foreach (var item in listImageCatalog)
            {
                TreeNodeItem node = new TreeNodeItem();
                node.ID = Utility.GetString(item.Id);
                node.Parent = Utility.GetString(item.ParentId);
                node.Text = item.CatalogName;
                lstDataTree.Add(node);
            }

            UIUtility.BindDicToDropDownList(ddlYearSorting, PostedYearsDict);
            ddlYearSorting.SelectedIndex = 0;
            ddlYearSorting.SelectedItem.Text = "Tất cả";
            tvCatalogNews.DataSource = lstDataTree;
            tvCatalogNews.DataBind();

            GetItemForView();
        }

        private void GetItemForView(string year = null, DateTime? postedDTG = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            ImageLibraryParam param2 = new ImageLibraryParam(FunctionType.ImageLibrary.GetListImage);
            param2.CurrentUserId = Profiles.MyProfile.EmployeeID;
            param2.year = Utility.GetNullableInt(year);
            param2.postedDTG = postedDTG;
            param2.dateFrom = dateFrom;
            param2.dateTo = dateTo;
            param2.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniSixteen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };

            MainController.Provider.Execute(param2);

            rptImage.DataSource = param2.listAttachment;
            rptImage.DataBind();

            Pager.BuildPager(param2.PagingInfo.RecordCount, SMX.smx_PageMiniSixteen, int.Parse(hidPage.Value), 5);
        }

        protected void btnCreateNewNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNodeName.Text))
                {
                    lbEmtyWarning.Text = "Chưa nhập dữ liệu";
                    lbEmtyWarning.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    ImageLibraryParam getList = new ImageLibraryParam(FunctionType.ImageLibrary.GetListImageCatalog);
                    MainController.Provider.Execute(getList);

                    var maxRefType = getList.listImageCatalog.OrderByDescending(x => x.refType).FirstOrDefault();

                    if (tvCatalogNews.SelectedNode != null && string.IsNullOrEmpty(tvCatalogNews.SelectedNode.Parent))
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.AddNewNode);
                        param.ImageCatalog = new ImageCatalog()
                        {
                            CatalogName = txtNodeName.Text,
                            CreatedDTG = DateTime.Now,
                            CreatedBy = Profiles.MyProfile.UserName,
                            CreatedUserId = Profiles.MyProfile.EmployeeID,
                            ParentId = Convert.ToInt32(tvCatalogNews.SelectedNode.ID),
                            refType = getList.listImageCatalog.First(x => x.Id == Convert.ToInt32(tvCatalogNews.SelectedNode.ID)).refType
                        };
                        MainController.Provider.Execute(param);
                    }
                    else
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.AddNewNode);
                        param.ImageCatalog = new ImageCatalog()
                        {
                            CatalogName = txtNodeName.Text,
                            CreatedDTG = DateTime.Now,
                            CreatedBy = Profiles.MyProfile.UserName,
                            CreatedUserId = Profiles.MyProfile.EmployeeID,
                            refType = maxRefType == null ? 0 : maxRefType.refType + 1
                        };
                        MainController.Provider.Execute(param);
                    }

                    popupCreate.Hide();

                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnAddNewNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvCatalogNews.SelectedNode != null && string.IsNullOrEmpty(tvCatalogNews.SelectedNode.Parent))
                {
                    lbNodeName.Text = "Tên album";
                }
                else
                {
                    lbNodeName.Text = "Tên thư mục";
                }
                popupCreate.Show();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnEditNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvCatalogNews.SelectedNode == null)
                {
                    ShowError("Vui lòng chọn mục muốn chỉnh sửa");
                }
                else
                {
                    if (string.IsNullOrEmpty(tvCatalogNews.SelectedNode.Parent))
                    {
                        lbNodeName2.Text = "Tên thư mục";
                    }
                    else
                    {
                        lbNodeName2.Text = "Tên album";
                    }

                    ImageLibraryParam checkRoot = new ImageLibraryParam(FunctionType.ImageLibrary.GetImageCatalogById);
                    checkRoot.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                    MainController.Provider.Execute(checkRoot);
                    if (string.IsNullOrEmpty(tvCatalogNews.SelectedNode.Parent) && checkRoot.ImageCatalog.refType >= 1 && checkRoot.ImageCatalog.refType <= 12)
                    {
                        ShowError("Không thể sửa thư mục gốc");
                    }
                    else
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.GetImageCatalogById);
                        param.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                        MainController.Provider.Execute(param);

                        if (param.ImageCatalog != null)
                        {
                            txtNodeNameEdit.Text = param.ImageCatalog.CatalogName;
                        }
                        else
                        {
                            Response.Redirect(Request.RawUrl);
                        }

                        popupEdit.Show();
                    }
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvCatalogNews.SelectedNode == null)
                {
                    ShowError("Vui lòng chọn mục muốn xóa");
                }
                else
                {
                    ImageLibraryParam checkRoot = new ImageLibraryParam(FunctionType.ImageLibrary.GetImageCatalogById);
                    checkRoot.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                    MainController.Provider.Execute(checkRoot);
                    if (string.IsNullOrEmpty(tvCatalogNews.SelectedNode.Parent) && checkRoot.ImageCatalog.refType >= 1 && checkRoot.ImageCatalog.refType <= 12)
                    {
                        ShowError("Không thể xóa thư mục gốc");
                    }
                    else
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.DeleteSelectedNode);
                        param.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                        MainController.Provider.Execute(param);

                        ShowMessage("Xóa thành công");
                        SetUpForm();
                        GetItemForView();
                    }
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void popupCreate_PopupClosed(object sender, EventArgs e)
        {
            popupCreate.Hide();
        }

        protected void popupEdit_PopupClosed(object sender, EventArgs e)
        {
            popupEdit.Hide();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lbNodeName2.Text))
                {
                    lbEmtyWarning2.Text = "Chưa nhập dữ liệu";
                    lbEmtyWarning2.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.EditNoteName);
                    param.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                    param.CatalogName = txtNodeNameEdit.Text;
                    MainController.Provider.Execute(param);

                    popupEdit.Hide();

                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        private void BindDataImage(HtmlImage imgUI, adm_Attachment imgData)
        {
            imgUI.Src = SMX.DefaultImage;
            if (imgData != null)
            {
                imgUI.Alt = imgData.Description != null ? imgData.Description : "Image";
                imgUI.Src = GetImageURL(imgData);
            }
            /*else
                imgUI.Src = SMX.DefaultImage;*/
        }

        private string GetImageURL(adm_Attachment image)
        {
            string url = SMX.DefaultImage;

            if (image != null && image.FileContent != null)
            {
                string imageFileName = string.Format("{0}_{1}", image.AttachmentID, image.FileName);
                string imageFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository", "ECM");
                if (!System.IO.Directory.Exists(imageFilePath))
                    System.IO.Directory.CreateDirectory(imageFilePath);
                imageFilePath = System.IO.Path.Combine(imageFilePath, imageFileName);
                if (!System.IO.File.Exists(imageFilePath))
                    System.IO.File.WriteAllBytes(imageFilePath, image.FileContent);
                url = ResolveUrl("/Repository/ECM/" + imageFileName);
            }

            return ResolveUrl(url);
        }

        protected void rptImage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeaterItem(e.Item);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void rptImage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        private void BindObjectToRepeaterItem(RepeaterItem e)
        {
            adm_Attachment item = e.DataItem as adm_Attachment;

            /*CheckBox ckb = e.FindControl("ckbImg") as CheckBox;
            ckb.Attributes["Value"] = item.AttachmentID.ToString();

            HtmlImage img = (HtmlImage)e.FindControl("img");
            BindDataImage(img, item);*/

            if (item != null)
            {
                CheckBox ckb = e.FindControl("ckbImg") as CheckBox;
                ckb.Attributes["Value"] = item.AttachmentID.ToString();

                var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { item?.AttachmentID ?? 0 })), 1000, 600);
                HtmlImage viewDetailImage = e.FindControl("img") as HtmlImage;
                viewDetailImage.Attributes.Add("ondblclick", url);
            }

            HtmlImage img = (HtmlImage)e.FindControl("img");
            BindDataImage(img, item);
        }

        protected void Pager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPage.Value = e.NewPageIndex.ToString();

                BuildViewState(rptImage.Items);

                if (tvCatalogNews.SelectedNode != null)
                {
                    GetItemByFilter(Year, PostedDTG, DateFrom, DateTo);
                }
                else
                {
                    GetItemForView(Year, PostedDTG, DateFrom, DateTo);
                }

            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void tvCatalogNews_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (tvCatalogNews.SelectedNode != null)
                {
                    ImageLibraryParam selectedNode = new ImageLibraryParam(FunctionType.ImageLibrary.GetImageCatalogById);
                    selectedNode.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                    MainController.Provider.Execute(selectedNode);

                    hidrefType.Value = selectedNode.ImageCatalog.refType.ToString();

                    hidPage.Value = "1";

                    GetItemByFilter(Year, PostedDTG, DateFrom, DateTo);
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        private void GetItemByFilter(string year = null, DateTime? postedDTG = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            ImageLibraryParam checkRoot = new ImageLibraryParam(FunctionType.ImageLibrary.GetImageCatalogById);
            checkRoot.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
            MainController.Provider.Execute(checkRoot);

            ImageLibraryParam param;

            if (string.IsNullOrEmpty(tvCatalogNews.SelectedNode.Parent) && checkRoot.ImageCatalog.refType >= 1 && checkRoot.ImageCatalog.refType <= 12)
            {
                param = new ImageLibraryParam(FunctionType.ImageLibrary.GetRootImageByFilter);
                param.CurrentUserId = Profiles.MyProfile.EmployeeID;
                param.refType = Utility.GetInt(hidrefType.Value);
            }
            else
            {

                param = new ImageLibraryParam(FunctionType.ImageLibrary.GetImageByFilter);
                param.CurrentUserId = Profiles.MyProfile.EmployeeID;
                param.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                param.refType = Utility.GetInt(hidrefType.Value);
            }

            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniSixteen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };

            param.year = Utility.GetNullableInt(year);
            param.postedDTG = postedDTG;
            param.dateFrom = dateFrom;
            param.dateTo = dateTo;

            MainController.Provider.Execute(param);

            hidRecordCount.Value = param.PagingInfo.RecordCount.ToString();

            rptImage.DataSource = param.listAttachment;
            rptImage.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniSixteen, int.Parse(hidPage.Value), 5);
        }

        protected void btnDeleteImg_Click(object sender, EventArgs e)
        {
            try
            {
                BuildViewState(rptImage.Items);
                if ((ViewState["ListItem"] as List<int>).Count > 0 && tvCatalogNews.SelectedNode == null)
                {
                    SettingParam settingParam = new SettingParam(FunctionType.Administration.Setting.GetSettingFirst);
                    MainController.Provider.Execute(settingParam);
                    var settingRecord = settingParam.Setting;
                    if ((bool)settingRecord.Status)
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.DeleteSelectedImgOriginal);
                        param.listDeleteImg = ViewState["ListItem"] as List<int>;
                        MainController.Provider.Execute(param);
                        ShowMessage("Xóa thành công");
                        ViewState.Clear();
                        SetUpForm();
                    }
                    else
                    {
                        if ((ViewState["ListItem"] as List<int>).Count > 0 && tvCatalogNews.SelectedNode == null)
                        {
                            ShowError("Không thể xóa ảnh");

                            return;
                        }
                    }
                    return;
                }


                if ((ViewState["ListItem"] as List<int>).Count > 0 && tvCatalogNews.SelectedNode != null)
                {
                    ImageLibraryParam checkRoot = new ImageLibraryParam(FunctionType.ImageLibrary.GetImageCatalogById);
                    checkRoot.Id = Convert.ToInt32(tvCatalogNews.SelectedNode.ID);
                    MainController.Provider.Execute(checkRoot);

                    if (string.IsNullOrEmpty(tvCatalogNews.SelectedNode.Parent) && checkRoot.ImageCatalog.refType >= 1 && checkRoot.ImageCatalog.refType <= 12)
                    {
                        SettingParam settingParam = new SettingParam(FunctionType.Administration.Setting.GetSettingFirst);
                        MainController.Provider.Execute(settingParam);
                        var settingRecord = settingParam.Setting;
                        if ((bool)settingRecord.Status)
                        {
                            ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.DeleteSelectedImgOriginal);
                            param.listDeleteImg = ViewState["ListItem"] as List<int>;
                            MainController.Provider.Execute(param);
                            ShowMessage("Xóa thành công");
                            if (Convert.ToInt32(hidRecordCount.Value) > 0 && Convert.ToInt32(hidPage.Value) > 1)
                            {
                                hidPage.Value = (Convert.ToInt32(hidRecordCount.Value) - (ViewState["ListItem"] as List<int>).Count) % SMX.smx_PageMiniSixteen == 0 ?
                                    ((Convert.ToInt32(hidRecordCount.Value) - (ViewState["ListItem"] as List<int>).Count) / SMX.smx_PageMiniSixteen).ToString() : hidPage.Value;
                            }

                            ViewState.Clear();

                            ShowMessage("Xóa thành công");

                            SetUpForm();

                            GetItemByFilter();
                        }
                        else
                        {
                            ShowError("Không thể xóa ảnh");
                        }
                        return;
                        //ShowError("Không thể xóa ảnh khỏi thư mục gốc");
                    }
                    else
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.DeleteSelectedImg);
                        param.CurrentUserId = Profiles.MyProfile.EmployeeID;
                        param.listDeleteImg = ViewState["ListItem"] as List<int>;
                        param.Id = Utility.GetInt(tvCatalogNews.SelectedNode.ID);
                        MainController.Provider.Execute(param);

                        if (Convert.ToInt32(hidRecordCount.Value) > 0 && Convert.ToInt32(hidPage.Value) > 1)
                        {
                            hidPage.Value = (Convert.ToInt32(hidRecordCount.Value) - (ViewState["ListItem"] as List<int>).Count) % SMX.smx_PageMiniSixteen == 0 ?
                                ((Convert.ToInt32(hidRecordCount.Value) - (ViewState["ListItem"] as List<int>).Count) / SMX.smx_PageMiniSixteen).ToString() : hidPage.Value;
                        }

                        ViewState.Clear();

                        ShowMessage("Xóa thành công");

                        SetUpForm();

                        GetItemByFilter();
                    }

                }
                else
                {
                    ShowError("Bạn chưa chọn ảnh");
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnAddImageToNode_Click(object sender, EventArgs e)
        {
            try
            {
                BuildViewState(rptImage.Items);

                if ((ViewState["ListItem"] as List<int>).Count > 0)
                {
                    ImageLibraryParam paramlb = new ImageLibraryParam(FunctionType.ImageLibrary.GetListEditableNode);
                    MainController.Provider.Execute(paramlb);

                    List<ImageCatalog> lstDl = new List<ImageCatalog>();
                    //lstDl.Add(new ImageCatalog() { Id = -1, CatalogName = "" });
                    lstDl.AddRange(paramlb.listImageCatalog);
                    dlNodeSelector.DataSource = lstDl;
                    dlNodeSelector.DataBind();

                    nodeSelector.Show();
                }
                else
                {
                    ShowError("Bạn chưa chọn ảnh");
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnViewDeletedItem_Click(object sender, EventArgs e)
        {
            GetListDeletedItem();
        }

        protected void saveToNode_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ViewState["ListItem"] as List<int>).Count > 0 && dlNodeSelector.SelectedValue != "-1")
                {
                    ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.AddImageToNode);
                    ImageLibraryParam nodeValues = new ImageLibraryParam(FunctionType.ImageLibrary.GetRefTypeById);
                    nodeValues.Id = Convert.ToInt32(dlNodeSelector.SelectedValue);
                    MainController.Provider.Execute(nodeValues);
                    param.CurrentUserId = Profiles.MyProfile.EmployeeID;
                    param.listInsertImg = ViewState["ListItem"] as List<int>;
                    param.Id = Convert.ToInt32(dlNodeSelector.SelectedValue);
                    param.refType = Convert.ToInt32(nodeValues.refType);
                    MainController.Provider.Execute(param);
                    ShowMessage("Thêm thành công");
                    ViewState.Clear();
                }
                else
                {
                    ShowError("Chưa chọn thư mục/album");
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
            nodeSelector.Hide();
            Response.Redirect(Request.RawUrl);
        }

        protected void nodeSelector_PopupClosed(object sender, EventArgs e)
        {
            nodeSelector.Hide();
        }

        protected void btnRevertDeletedItem_Click(object sender, EventArgs e)
        {
            try
            {
                BuildViewState(rptImage.Items);

                if ((ViewState["ListItem"] as List<int>).Count > 0)
                {
                    ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.RevertDeletedItem);
                    param.CurrentUserId = Profiles.MyProfile.EmployeeID;
                    param.listDeleteImg = (ViewState["ListItem"] as List<int>);
                    MainController.Provider.Execute(param);

                    ShowMessage("Khôi phục thành công");
                    ViewState.Clear();
                    GetListDeletedItem();
                }
                else
                {
                    ShowError("Vui lòng chọn mục muốn khôi phục");
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnDeletePermanently_Click(object sender, EventArgs e)
        {
            try
            {
                BuildViewState(rptImage.Items);

                if ((ViewState["ListItem"] as List<int>).Count > 0)
                {
                    ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.PermanentlyDelete);
                    param.CurrentUserId = Profiles.MyProfile.EmployeeID;
                    param.listDeleteImg = (ViewState["ListItem"] as List<int>);
                    MainController.Provider.Execute(param);

                    ShowMessage("Xóa thành công");
                    ViewState.Clear();
                    GetListDeletedItem();
                }
                else
                {
                    ShowError("Vui lòng chọn mục muốn xóa");
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        private void GetListDeletedItem()
        {
            ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.ViewDeletedItem);
            param.CurrentUserId = Profiles.MyProfile.EmployeeID;

            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniSixteen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };

            MainController.Provider.Execute(param);

            rptImage.DataSource = param.listAttachment;
            rptImage.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniSixteen, int.Parse(hidPage.Value), 5);
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            if (tvCatalogNews.SelectedNode != null)
            {
                Response.Redirect(Request.RawUrl);
            }

            SetUpForm();

            GetItemForView();
        }

        private void BuildViewState(RepeaterItemCollection collection)
        {
            if (ViewState["ListItem"] == null)
            {
                ViewState["ListItem"] = new List<int>();
            }

            foreach (RepeaterItem item in collection)
            {
                CheckBox ckb = item.FindControl("ckbImg") as CheckBox;

                if (ckb != null && ckb.Attributes["Value"] != null)
                {
                    if (ckb.Checked)
                    {
                        if (int.TryParse(ckb.Attributes["Value"], out int intValue))
                        {
                            if (!(ViewState["ListItem"] as List<int>).Contains(intValue))
                            {
                                (ViewState["ListItem"] as List<int>).Add(intValue);
                            }
                        }
                    }
                }
            }
        }

        protected void btnAdvancedFilter_Click(object sender, EventArgs e)
        {
            popupAdvancedFilter.Show();
            dptPostedDTG.SelectedDate = null;
            dptPostedDTGFrom.SelectedDate = null;
            dptPostedDTGTo.SelectedDate = null;
        }

        protected void popupAdvancedFilter_PopupClosed(object sender, EventArgs e)
        {
            popupAdvancedFilter.Hide();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            popupAdvancedFilter.Hide();
            PostedDTG = dptPostedDTG.SelectedDate;
            DateFrom = dptPostedDTGFrom.SelectedDate;
            DateTo = dptPostedDTGTo.SelectedDate;
            Year = null;
            hidPage.Value = "1";
            if (tvCatalogNews.SelectedNode != null)
            {
                ddlYearSorting.SelectedIndex = 0;

                GetItemByFilter(Year, PostedDTG, DateFrom, DateTo);
            }
            else
            {
                ddlYearSorting.SelectedIndex = 0;

                GetItemForView(Year, PostedDTG, DateFrom, DateTo);
            }
        }

        protected void ddlYearSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            Year = ddlYearSorting.SelectedValue;
            hidPage.Value = "1";
            PostedDTG = null;
            DateFrom = null;
            DateTo = null;
            if (tvCatalogNews.SelectedNode != null)
            {
                GetItemByFilter(Year, PostedDTG, DateFrom, DateTo);
            }
            else
            {
                GetItemForView(Year, PostedDTG, DateFrom, DateTo);
            }
        }
    }
}