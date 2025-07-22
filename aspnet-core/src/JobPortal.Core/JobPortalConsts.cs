using JobPortal.Debugging;

namespace JobPortal
{
    public class JobPortalConsts
    {
        public const string LocalizationSourceName = "JobPortal";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "8019ae93d994461cb4d88655bf702eaf";
    }
}
