using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class RightParam : BaseParam
    {
        public RightParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public string EmployeeOrRoleID { get; set; }
        public bool IsEmployee { get; set; }
        public string Url { get; set; }
        public List<Right> Rights { get; set; }
        public List<BuildRightConfig> BuildRightConfigs { get; set; }
        public Right Right { get; set; }
        public int? FeatureId { get; set; }
        public List<string> RoleIDs { get; set; }
        public List<int> FeatureIDs { get; set; }
        public List<Feature> Features { get; set; }
        public List<FeatureFunction> FeatureFunctions { get; set; }
        public List<Function> Functions { get; set; }
        public List<Role> Roles { get; set; }
    }
}
