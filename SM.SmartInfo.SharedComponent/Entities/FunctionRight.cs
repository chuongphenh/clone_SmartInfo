using SoftMart.Core.Dao;
using SoftMart.Core.Security.Entity;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class FunctionRight : BaseEntity, IFunctionRight
    {
        [PropertyEntity("FeatureID", false, false)]
        public int FeatureID { get; set; }

        [PropertyEntity("FunctionID", false, false)]
        public int FunctionID { get; set; }

        [PropertyEntity("FeatureFunctionID", false, false)]
        public int FeatureFunctionID { get; set; }

        [PropertyEntity("FunctionCode", false, false)]
        public string FunctionCode { get; set; }

        [PropertyEntity("URL", false, false)]
        public string URL { get; set; }

        [PropertyEntity("RuleID", false, false)]
        public int? RuleID { get; set; }

        [PropertyEntity("EmployeeID", false, false)]
        public int? EmployeeID { get; set; }

        public FunctionRight()
            : base(string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", FeatureID, FunctionID, URL);
        }
    }
}
