using DllShared;
using Uplay.Uplaydll;

namespace upc_r2.Exports;

internal static class Init
{
    public static uint ProductId;
    [UnmanagedCallersOnly(EntryPoint = "UPC_Init", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_Init(uint inVersion, int productId)
    {
        Log.Verbose("[{Function}] {inVersion} {productId}", nameof(UPC_Init), inVersion, productId);
        ProductId = (uint)productId;
        LoadDll.LoadPlugins();
        InitResult result = InitResult.Success;
        Log.Verbose("[{Function}] {result}", nameof(UPC_Init), result);
        return result switch
        {
            InitResult.Success => (int)UPC_InitResult.UPC_InitResult_Ok,
            InitResult.Failure => (int)UPC_InitResult.UPC_InitResult_Failed,
            InitResult.ReconnectRequired => (int)UPC_InitResult.UPC_InitResult_DesktopInteractionRequired,
            InitResult.RestartWithGameLauncherRequired => (int)UPC_InitResult.UPC_InitResult_ExitProcessRequired,
            _ => (int)UPC_InitResult.UPC_InitResult_Failed,
        };
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_Uninit", CallConvs = [typeof(CallConvCdecl)])]
    public static void UPC_Uninit()
    {
        Log.Verbose("[{Function}]", nameof(UPC_Uninit));
        ProductId = 0;
        LoadDll.FreePlugins();
    }
}
