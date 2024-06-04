
namespace SM.SmartInfo.PermissionManager.UserAuthentication
{
    class MembershipHelper
    {
        public static IMembershipService GetMembershipService(int? authorizationType)
        {
            if (authorizationType == SharedComponent.Constants.SMX.LoginAuthorization_Local)
                return new SQLMembershipService();

            return new ADMembershipService();
        }

        public static IFormsAuthenticationService FormService
        {
            get
            {
                return new FormsAuthenticationService();
            }
        }

        public static string GetRealUserName(string userName)
        {
            string validUserName = userName; // userName is domain\name, validUserName = name
            if (!string.IsNullOrWhiteSpace(userName) && userName.Contains("\\"))
                validUserName = userName.Substring(userName.LastIndexOf('\\') + 1);

            return validUserName;
        }

        public static string GetPasswordHash(string userName, string password)
        {
            //return Utils.Utility.Encrypt(userName.ToLower(), password);
            return Utils.SHA.GenerateSHA512String(password);
        }
    }
}
