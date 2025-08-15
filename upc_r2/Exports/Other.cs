namespace upc_r2.Exports;

internal static class Other
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_CPUScoreGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_CPUScoreGet(IntPtr inContext, IntPtr outScore)
    {
        Log.Verbose("[{Function}] {inContext} {outScore}", nameof(UPC_CPUScoreGet), inContext, outScore);
        Marshal.WriteInt32(outScore, 0x1000);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_GPUScoreGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_GPUScoreGet(IntPtr inContext, IntPtr outScore, IntPtr outConfidenceLevel)
    {
        Log.Verbose("[{Function}] {inContext} {outScore} {outConfidenceLevel}", nameof(UPC_GPUScoreGet), inContext, outScore, outConfidenceLevel);
        Marshal.WriteInt32(outScore, 0x1000);
        Marshal.WriteInt64(outConfidenceLevel, (long)0.1f);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_ApplicationIdGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_ApplicationIdGet(IntPtr inContext, IntPtr outAppId)
    {
        Log.Verbose("[{Function}] {inContext} {outAppId}", nameof(UPC_ApplicationIdGet), inContext, outAppId);
        Marshal.WriteIntPtr(outAppId, Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Others.ApplicationId));
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_RichPresenceSet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_RichPresenceSet(IntPtr inContext, uint inId, IntPtr inOptTokenList)
    {
        Log.Verbose("[{Function}] {inContext} {inId} {inOptTokenList}", nameof(UPC_RichPresenceSet), inContext, inId, inOptTokenList);
        // TODO: parse list to UPC_RichPresenceToken.
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_RichPresenceSet_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_RichPresenceSet_Extended(IntPtr inContext, uint inId, IntPtr inOptTokenList, IntPtr unk1, IntPtr unk2)
    {
        Log.Verbose("[{Function}] {inContext} {inId} {inOptTokenList} {unk1} {unk2}", nameof(UPC_RichPresenceSet_Extended), inContext, inId, inOptTokenList, unk1, unk2);
        // TODO: parse list to UPC_RichPresenceToken.
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_LaunchApp", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_LaunchApp(IntPtr inContext, uint inProductId, IntPtr MustBeZero)
    {
        Log.Verbose("[{Function}] {inContext} {inProductId} {MustBeZero}", nameof(UPC_LaunchApp), inContext, inProductId, MustBeZero);
        return 1;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_ErrorToString", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPC_ErrorToString(int error)
    {
        string switch_ret = "get";
        switch_ret = error switch
        {
            -14 => "Unavailable",
            -13 => "Failed precondition",
            -11 => "Operation aborted",
            -10 => "Internal error",
            -9 => "Unauthorized action",
            -8 => "Limit reached",
            -7 => "End of file",
            -6 => "Not found",
            -5 => "Memory error",
            -4 => "Communication error",
            -3 => "Uninitialized subsystem",
            -2 => "Invalid arguments",
            -1 => "Declined",
            _ => "Unknown error",
        };
        var ret = Marshal.StringToHGlobalAnsi(switch_ret);
        Log.Verbose("[{Function}] {error} {switch_ret}", nameof(UPC_ErrorToString), error, switch_ret);
        return ret;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_IsCrossBootAllowed", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_IsCrossBootAllowed(IntPtr inContext, uint inProductId, IntPtr outIsCrossBootAllowed, IntPtr unk1, IntPtr unk2)
    {
        Log.Verbose("[{Function}] {inContext} {inProductId} {outIsCrossBootAllowed} {unk1} {unk2}", nameof(UPC_IsCrossBootAllowed), inContext, inProductId, outIsCrossBootAllowed, unk1, unk2);
        Marshal.WriteInt32(outIsCrossBootAllowed, 0, Convert.ToInt32(UPC_Json.Instance.Others.EnableCrossBoot));
        return 0;
    }
}
