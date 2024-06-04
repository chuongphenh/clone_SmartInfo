
namespace SM.SmartInfo.PermissionManager.Shared
{
    class RuleEngineInfo
    {
        public string Condition { get; set; }
        public string ViewName { get; set; }
        public string PrimaryKey { get; set; }
        public int? RuleID { get; set; }
        public int? CaseID { get; set; }
    }
}
