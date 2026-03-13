using TestApp.Debugging;

namespace TestApp;

public class TestAppConsts
{
    public const string LocalizationSourceName = "TestApp";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "d9943d72ec36481badd45681d837dd4f";
}
