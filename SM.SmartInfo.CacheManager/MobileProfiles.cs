using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftMart.Core.Utilities.Profiles;

namespace SM.SmartInfo.CacheManager
{
    public class MobileProfiles
    {
        public static ProfileManager MyProfileManager { get; set; } = new ProfileManager();

        public static UserProfile MyProfile
        {
            get
            {
                return (UserProfile)MyProfileManager.MyProfile;
            }
        }

        public static void SetMyProfile(UserProfile profile)
        {
            MyProfileManager.SetMyProfile(profile);
        }

        public class ProfileManager
        {
            public virtual IUserProfile MyProfile
            {
                get
                {
                    return SoftMart.Core.Utilities.Profiles.Profiles.MyProfile;

                }
            }

            public virtual void SetMyProfile(UserProfile profile)
            {
                SoftMart.Core.Utilities.Profiles.Profiles.SetMyProfile(profile);
            }
        }

    }
}
