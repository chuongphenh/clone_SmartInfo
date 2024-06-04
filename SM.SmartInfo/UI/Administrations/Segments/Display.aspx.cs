using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.Administrations.Segments
{
    public partial class Display : BasePage, ISMFormDisplay<SystemParameter>
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    LoadData();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItems();
                Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
        #endregion

        #region Common
        public void SetupForm()
        {
            lnkExit.NavigateUrl = string.Format(PageURL.Default);
            hidID.Value = GetParamId();
            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, hidID.Value);
        }

        public void LoadData()
        {
            int? itemID = Utility.GetNullableInt(hidID.Value);

            var param = new SystemParameterParam(FunctionType.Administration.Segment.LoadDataEditSegment);
            param.SystemParameter = new SystemParameter { SystemParameterID = itemID };
            MainController.Provider.Execute(param);

            BindObjectToForm(param.SystemParameter);
        }

        public void DeleteItems()
        {
            var item = new SystemParameter();
            item.SystemParameterID = Utility.GetNullableInt(hidID.Value);
            item.Version = Utility.GetNullableInt(hidVersion.Value);
            var param = new SystemParameterParam(FunctionType.Administration.Segment.DeleteItemsSegment);
            param.SystemParameters = new List<SystemParameter>() { item };
            MainController.Provider.Execute(param);
        }

        public void BindObjectToForm(SystemParameter item)
        {
            if (item == null)
                return;

            //bind System
            hidVersion.Value = Utility.GetString(item.Version);
            hidID.Value = Utility.GetString(item.SystemParameterID);
            ltrCode.Text = item.Code;
            ltrName.Text = item.Name;
            ltrStatus.Text = Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
            ltrDescription.Text = UIUtility.ConvertBreakLine2Html(item.Description);
            ltrSegmentFrom.Text = item.Ext1;
            ltrSegmentTo.Text = item.Ext2;

            SystemParameter street = GlobalCache.GetSystemParameterByID(item.Ext1i);
            if(street != null)
            {
                ltrStreet.Text = street.Name;

                SystemParameter district = GlobalCache.GetSystemParameterByID(street.Ext1i);
                if(district != null)
                {
                    ltrDistrict.Text = district.Name;
                    ltrProvince.Text = GlobalCache.GetNameByID(district.Ext1i);
                }
            }
        }
        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkEdit   , FunctionCode.EDIT     },
                    { btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}