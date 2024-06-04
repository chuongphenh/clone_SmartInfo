using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.BIZ.Administration
{
    class TownBiz : SystemParameterCRUDBaseBiz
    {
        public TownBiz() : base(SMX.Features.smx_Town) { }

        public override void SetupAddNewForm(SystemParameterParam param)
        {
            SystemParameterDao daoProcess = new SystemParameterDao();
            param.Provinces = daoProcess.GetAllActiveSystemParametersByFeatureID(SMX.Features.smx_Province);
        }

        public override void ValidateItem(SystemParameterParam param)
        {
            base.ValidateItem(param);

            SystemParameter item = param.SystemParameter;
            List<string> lstMsg = new List<string>();

            if (item.Ext1i == null)
                lstMsg.Add("Chưa chọn quận huyện");

            //Display message
            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }

        public override void SetupEditForm(SystemParameterParam param)
        {
            SetupAddNewForm(param);
        }
    }
}
