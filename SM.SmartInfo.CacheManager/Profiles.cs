
namespace SM.SmartInfo.CacheManager
{
    public class Profiles
    {
        public static UserProfile MyProfile
        {
            get
            {
                return (UserProfile)SoftMart.Core.Utilities.Profiles.Profiles.MyProfile;
            }
        }

        public static void SetMyProfile(UserProfile profile)
        {
            SoftMart.Core.Utilities.Profiles.Profiles.SetMyProfile(profile);
        }
    }
}
