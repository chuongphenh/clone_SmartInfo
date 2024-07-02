using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.EntityInfos
{
    public class CategoryInfo : Category
    {
        public string CategoryDescription { get; set; }
        #region Properties used for approve process flow (committee)

        //public string IsOrgManager { get { return !string.IsNullOrEmpty(OrganizationOfManagerName) ? "Có" : string.Empty; } }

        #endregion
    }
}
