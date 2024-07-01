using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Entities;

using System.Web.UI.WebControls;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.UI.UserControls;
using SM.SmartInfo.SharedComponent.Params;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.EntityInfos;
using System.Web.UI;
using SoftMart.Core.UIControls;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.SmartInfo.UI.Administrations.Documents
{
    public abstract class DocumentsBase : BasePage
    {
        protected string TruncateParentPath(string breadCrumb, string name)
        {
            string parentPath = string.Empty;

            int nameLength = name.Length + 2;
            int breadLength = breadCrumb.Length;
            int parentLength = breadLength - nameLength;

            if (parentLength > 0)
                parentPath = breadCrumb.Substring(0, parentLength);

            return parentPath;
        }

        protected int? GetSelectedOrganizationZoneID(int organizationID)
        {
            CommonParam param = new CommonParam(FunctionType.Common.GetZoneIDByOrganizationID);
            param.Organization = new Organization()
            {
                OrganizationID = organizationID
            };

            MainController.Provider.Execute(param);

            return param.Organization.ZoneID;
        }

        protected void ValidateEmployeeIsInOtherOrganization(int employeeID, int? organizationID)
        {
            // if organizationID = 0, it means in case adding organization
            // else, it means in case update organization
            OrganizationEmployee orgEmp = new OrganizationEmployee();
            orgEmp.EmployeeID = employeeID;
            orgEmp.OrganizationID = organizationID;

            OrganizationParam param = new OrganizationParam(FunctionType.Administration.Organization.ValidateEmployeeIsInOtherOrganization);
            param.OrganizationEmployee = orgEmp;

            MainController.Provider.Execute(param);
        }
    }
}