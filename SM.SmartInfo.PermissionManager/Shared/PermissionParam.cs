using SM.SmartInfo.SharedComponent.Entities;
using System.Collections.Generic;

namespace SM.SmartInfo.PermissionManager.Shared
{
    public class PermissionParam
    {
        public PermissionParam() { }
        public PermissionParam(PermissionType type)
        {
            Type = type;
        }

        // input - base
        public Feature Feature { get; set; }
        public List<string> FunctionCodes { get; set; }
        public PermissionType Type { get; private set; }

        public int? ItemID { get; set; } // dung cho check permission cua item

        // output
        public List<FunctionRight> FunctionRights { get; set; }

        // view data permission
        public ViewDataPermissionInfo ViewDataPermission { get; set; }
    }

    public enum PermissionType
    {
        AccessPage = 1,
        AccessView = 2,
        AccessItem = 3
    }
}
