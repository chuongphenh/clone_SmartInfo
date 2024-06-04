using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.CommonList;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class UserDetails : SoftMart.Core.Security.UnsecuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UserProfile profile = Profiles.MyProfile;
                    lnkEdit.NavigateUrl = string.Format("~/UI/Administrations/Users/Edit.aspx?ID={0}", (Profiles.MyProfile.EmployeeID).ToString());
                    if (profile != null)
                    {
                        ucUserDetail.BinData(Profiles.MyProfile.EmployeeID, null, null, null, true);
                        hidId.Value = (Profiles.MyProfile.EmployeeID).ToString();
                    }
                }
            }
            catch (SMXException)
            {
                ucErr.ShowError(Messages.ItemNotExisted);
            }

        }
    }
}