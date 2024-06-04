using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.DAO.Administration;

namespace SM.SmartInfo.BIZ.Administration
{
    class DistrictBiz : SystemParameterCRUDBaseBiz
    {
        public DistrictBiz() : base(SMX.Features.smx_District) { }

        public override void SetupAddNewForm(SystemParameterParam param)
        {
            SystemParameterDao daoProcess = new SystemParameterDao();
            param.Provinces = daoProcess.GetAllActiveSystemParametersByFeatureID(SMX.Features.smx_Province);
        }

        public override void SetupEditForm(SystemParameterParam param)
        {
            //Get item
            //base.SetupEditForm(param);

            //Get ComboBox data
            SetupAddNewForm(param);
        }
    }
}
