namespace upc_r2.Exports;

internal static class Events
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_EventNextPeek", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_EventNextPeek(IntPtr inContext, IntPtr outEvent)
    {
        //Log.Verbose(nameof(UPC_EventNextPeek), new object[] { inContext, outEvent });
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_EventNextPoll", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_EventNextPoll(IntPtr inContext, IntPtr outEvent)
    {
        //Log.Verbose(nameof(UPC_EventNextPoll), new object[] { inContext, outEvent });
        return (int)UPC_Result.UPC_Result_NotFound;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_EventRegisterHandler", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_EventRegisterHandler(IntPtr inContext, uint inType, IntPtr inHandler, IntPtr inOptData)
    {
        Log.Verbose("[{Function}] {inContext} {inType} {inHandler} {inOptData}", nameof(UPC_EventRegisterHandler), inContext, inType, inHandler, inOptData);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        context.Events.Add(new Event((UPC_EventType)inType, inHandler, inOptData));
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_EventUnregisterHandler", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_EventUnregisterHandler(IntPtr inContext, uint inType)
    {
        Log.Verbose("[{Function}] {inContext} {inType}", nameof(UPC_EventUnregisterHandler), inContext, inType);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        context.Events.RemoveAll(ev => ev.EventType == (UPC_EventType)inType);
        return (int)UPC_Result.UPC_Result_Ok;
    }
}
