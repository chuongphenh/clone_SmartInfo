using SoftMart.Core.Utilities.Diagnostics;

namespace SM.SmartInfo.Service.Reporting
{
    public class DataContext : SoftMart.Core.Dao.EnterpriseService
    {
        private PLogger _logger;

        public DataContext()
            : base(Utils.ConfigUtils.ConnectionString)
        {
            var callerName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;
            _logger = new PLogger(string.Format("DataContext - {0}", callerName));
        }

        public override void Dispose()
        {
            base.Dispose();
            //_logger.Dispose();
        }

        protected override void ConfigBeforeExecute(System.Data.SqlClient.SqlCommand command)
        {
            command.CommandText = "SET ARITHABORT ON;\n" + command.CommandText;
            base.ConfigBeforeExecute(command);
        }

        //private static Monitor Monitor = new ConnectionMonitor(Utils.ConfigUtils.ConnectionString, "Application Connections", "Connections to application database");
    }
}