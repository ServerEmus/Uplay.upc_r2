namespace upc_r2.Exports;

internal static partial class Export
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_AchievementImageFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_AchievementImageFree(IntPtr inContext, IntPtr inImageRGBA)
    {
        Log.Verbose("[{Function}] {inContext} {inImageRGBA}", nameof(UPC_AchievementImageFree), inContext, inImageRGBA);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_AchievementImageGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_AchievementImageGet(IntPtr inContext, uint inId, IntPtr outImageRGBA, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inId} {outImageRGBA} {inCallback} {inCallbackData}", nameof(UPC_AchievementImageGet), inContext, inId, outImageRGBA, inCallback, inCallbackData);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_CommunicationError));
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_AchievementListFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_AchievementListFree(IntPtr inContext, IntPtr inAchievementList)
    {
        Log.Verbose("[{Function}] {inContext} {inImageRGBA}", nameof(UPC_AchievementListFree), inContext, inAchievementList);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_AchievementListGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_AchievementListGet(IntPtr inContext, IntPtr inOptUserIdUtf8, uint inFilter, IntPtr outAchievementList, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inOptUserIdUtf8} {inFilter} {outAchievementList} {inCallback} {inCallbackData}", nameof(UPC_AchievementListGet), inContext, inOptUserIdUtf8, inFilter, outAchievementList, inCallback, inCallbackData);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_CommunicationError));
        WriteOutList(outAchievementList, 0, IntPtr.Zero);
        return 2000;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_AchievementUnlock", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_AchievementUnlock(IntPtr inContext, uint inId, IntPtr inOptCallback, IntPtr inOptCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inId} {inCallback} {inCallbackData}", nameof(UPC_AchievementUnlock), inContext, inId, inOptCallback, inOptCallbackData);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        context.Callbacks.Add(new(inOptCallback, inOptCallbackData, (int)UPC_Result.UPC_Result_CommunicationError));
        return 0;
    }
}
