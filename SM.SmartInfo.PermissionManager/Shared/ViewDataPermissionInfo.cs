using System.Collections.Generic;

namespace SM.SmartInfo.PermissionManager.Shared
{
    public class ViewDataPermissionInfo
    {
        public string BizTable { get; set; }
        public string BizColumn { get; set; }
        public string TemporaryViewQuery { get; set; }

        public List<ViewDataPermissionParam> Params { get; set; }

        public void AddParam(string name, object val)
        {
            if (Params == null)
                Params = new List<ViewDataPermissionParam>();

            Params.Add(new ViewDataPermissionParam() { Name = name, Value = val });
        }
    }

    public class ViewDataPermissionParam
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
