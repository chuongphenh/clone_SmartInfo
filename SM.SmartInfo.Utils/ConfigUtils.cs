using SM.SmartInfo.SharedComponent.Constants;
using System;
using System.Configuration;
using System.IO;

namespace SM.SmartInfo.Utils
{
    public static class ConfigUtils
    {
        private static string _repository = null;

        public static string Repository
        {
            get
            {
                if (_repository == null)
                {
                    string configRepository = ConfigurationManager.AppSettings.Get(SMX.smx_Repository);
                    if (string.IsNullOrWhiteSpace(configRepository))
                        throw new ConfigurationErrorsException("Chưa config folder Repository");

                    DirectoryInfo dicInfo = new DirectoryInfo(configRepository);
                    if (dicInfo.Exists)
                        _repository = dicInfo.FullName;
                    else
                        throw new ConfigurationErrorsException("Folder Repository không tồn tại: " + configRepository);
                }
                return _repository;
            }
        }

        public static string BPSConfigFolder
        {
            get
            {
                string strValue = GetAndCheckConfig("BPSConfigFolder");
                return strValue;
            }
        }

        public static string GenTemporaryFilePath(string fileName)
        {
            string filePath = Path.Combine(TemporaryFolder, fileName);//...\Templates\template.***
            FileInfo fileInfo = new FileInfo(filePath);

            return fileInfo.FullName;
        }

        public static string TemplateFolder
        {
            get
            {
                string strValue = GetAndCheckConfig("TemplateFolder");
                return strValue;
            }
        }

        private static string _temporary = null;

        public static string TemporaryFolder
        {
            get
            {
                if (_temporary == null)
                {
                    _temporary = Path.Combine(Repository, SMX.smx_RepositoryTemporary);

                    if (!Directory.Exists(_temporary))
                        Directory.CreateDirectory(_temporary);
                }
                return _temporary;
            }
        }

        public static string DynamicReportFolder
        {
            get
            {
                string strValue = GetAndCheckConfig("DynamicReportFolder");
                return strValue;
            }
        }

        public static System.Data.IsolationLevel IsolationLevel
        {
            get
            {
                string value = ConfigurationManager.AppSettings.Get("IsolationLevel");

                System.Data.IsolationLevel level;
                if (!Enum.TryParse(value, out level))
                    level = System.Data.IsolationLevel.ReadCommitted;

                return level;
            }
        }

        private static int? _maxUploadSize = null;

        public static int MaxUploadSize
        {
            get
            {
                if (_maxUploadSize == null)
                {
                    int? configVal = Utility.GetNullableInt(GetConfig("MaxUploadSize"));
                    if (configVal == null || configVal <= 0)
                        configVal = 15;

                    _maxUploadSize = configVal.Value * 1024 * 1024;
                }

                return _maxUploadSize.Value;
            }
        }

        private static int? _fixImageWidth = null;

        public static int FixImageWidth
        {
            get
            {
                if (_fixImageWidth == null)
                {
                    int? configVal = Utility.GetNullableInt(GetConfig("FixImageWidth"));
                    if (configVal == null || configVal.Value < 0)
                        configVal = 0; // set 0 => giu nguyen size

                    _fixImageWidth = configVal;
                }

                return _fixImageWidth.Value;
            }
        }

        public static string GetAndCheckConfig(string key)
        {
            string value = GetConfig(key);
            if (string.IsNullOrWhiteSpace(value))
                throw new ConfigurationErrorsException("Chưa config: " + key);

            return value;
        }

        public static string GetConfig(string key)
        {
            string value = ConfigurationManager.AppSettings.Get(key);

            return value;
        }

        #region Email
        private static string _emailHost = null;

        public static string EmailHost
        {
            get
            {
                if (_emailHost == null)
                    _emailHost = System.Configuration.ConfigurationManager.AppSettings["MailHost"];

                return _emailHost;
            }
        }

        public static int EmailPort
        {
            get
            {
                string strPort = System.Configuration.ConfigurationManager.AppSettings["MailPort"];
                int port = 0;
                int.TryParse(strPort, out port);
                return port;
            }
        }

        private static string _emailUsername = null;

        public static string EmailUserName
        {
            get
            {
                if (_emailUsername == null)
                    _emailUsername = System.Configuration.ConfigurationManager.AppSettings["MailUserName"];
                try
                {
                    return Utility.DecryptCode64Bit(_emailUsername);
                }
                catch (Exception)
                {
                    return _emailUsername;
                }
            }
        }

        private static string _emailFrom = null;

        public static string EmailFrom
        {
            get
            {
                if (_emailFrom == null)
                {
                    try
                    {
                        _emailFrom = System.Configuration.ConfigurationManager.AppSettings["MailFrom"];
                    }
                    catch { _emailFrom = string.Empty; }
                }
                try
                {
                    return Utility.DecryptCode64Bit(_emailFrom);
                }
                catch (Exception)
                {
                    return _emailFrom;
                }
            }
        }

        private static string _emailPassword = null;

        public static string EmailPassword
        {
            get
            {
                if (_emailPassword == null)
                    _emailPassword = System.Configuration.ConfigurationManager.AppSettings["MailPassword"];
                try
                {
                    return Utility.DecryptCode64Bit(_emailPassword);
                }
                catch (Exception)
                {
                    return _emailPassword;
                }
            }
        }

        public static bool EmailEnableSSL
        {
            get
            {
                string value = System.Configuration.ConfigurationManager.AppSettings["MailSSL"];
                return "true".Equals(value, StringComparison.OrdinalIgnoreCase);
            }
        }
        #endregion

        private static string _connectionString = null;
        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    var cnn = ConfigurationManager.ConnectionStrings["ApplicationDatabase"].ConnectionString;
                    _connectionString = cnn;
                    try
                    {
                        _connectionString = Utility.DecryptCode64Bit(cnn);
                    }
                    catch (Exception)
                    {
                        _connectionString = cnn;
                    }
                }

                return _connectionString;
            }
        }
    }
}