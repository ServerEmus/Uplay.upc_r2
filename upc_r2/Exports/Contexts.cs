namespace upc_r2.Exports;

internal static partial class Export
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_ContextCreate", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPC_ContextCreate(uint inVersion, IntPtr inOptSetting)
    {
        Log.Verbose("[{Function}] {inVersion} {inOptSetting}", nameof(UPC_ContextCreate), inVersion, inOptSetting);
        UPC_ContextSettings contextSettings = new()
        {
            subsystems = UPC_ContextSubsystem.UPC_ContextSubsystem_None
        };
        if (inOptSetting != IntPtr.Zero)
        {
            contextSettings = Marshal.PtrToStructure<UPC_ContextSettings>(inOptSetting);
        }
        Log.Verbose("[{Function}] Subsystems: {subsystems}", nameof(UPC_ContextCreate), contextSettings.subsystems);
        IntPtr context = UPC_ContextExt.CreateContext(contextSettings.subsystems);
        Log.Verbose("[{Function}] Context Pointer: {context}", nameof(UPC_ContextCreate), context);
        return context;

    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_ContextFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_ContextFree(IntPtr inContext)
    {
        Log.Verbose("[{Function}] {inContext}", nameof(UPC_ContextFree), inContext);
        UPC_ContextExt.FreeContext(inContext);
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_Update", CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe int UPC_Update(IntPtr inContext)
    {
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return 0;
        //Internal waiting update for reason
        context.SW.Stop();
        if (context.SW.ElapsedTicks <= UPC_Json.Instance.BasicLog.WaitBetweenUpdate)
        {
            context.SW.Start();
            return 0;
        }
        if (UPC_Json.Instance.BasicLog.LogUpdate)
            Log.Verbose("[{Function}] ElapsedTicks: {ElapsedTicks} WaitBetweebUpdate: {WaitBetweebUpdate}", nameof(UPC_Update), context.SW.ElapsedTicks, UPC_Json.Instance.BasicLog.WaitBetweenUpdate);
        context.SW.Restart();
        if (UPC_Json.Instance.BasicLog.LogUpdate)
            Log.Verbose("[{Function}] Current Callbacks Count: {Count}", nameof(UPC_Update), context.Callbacks.Count);
        foreach (var cb in context.Callbacks)
        {
            if (cb.fun != IntPtr.Zero)
            {
                Log.Verbose("[{Function}] Callback run with: {fun} {result} {data}", nameof(UPC_Update), cb.fun, cb.Result, cb.context_data);
                delegate* unmanaged<int, void*, void> @Callback = (delegate* unmanaged<int, void*, void>)cb.fun;
                Log.Verbose("[{Function}] {fun} is about to be called!", nameof(UPC_Update), cb.fun);
                @Callback(cb.Result, (void*)cb.context_data);
            }
        }
        context.Callbacks.Clear();
        Log.Verbose("[{Function}] Cleared Callbacks", nameof(UPC_Update));
        // TODO: Call Events. Somehow
        /*
         * Rsp Response = new();
        foreach (var ev in GlobalContext.Events)
        {
            if (ev.Handler != IntPtr.Zero)
            {
                delegate* unmanaged<void*, void*, void> @delegate = (delegate* unmanaged<void*, void*, void>)ev.Handler;
                if (Response.OverlayRsp != null && Response.OverlayRsp.OverlayStateChangedPush != null)
                {
                    if (Response.OverlayRsp.OverlayStateChangedPush.State == OverlayState.Showing && ev.EventType == UPC_EventType.UPC_Event_OverlayShown)
                    {
                        var ptr = Marshal.AllocHGlobal(sizeof(IntPtr));
                        Marshal.StructureToPtr(UPC_EventType.UPC_Event_OverlayShown, ptr, false);
                        @delegate((void*)ptr, (void*)ev.OptData);
                    }
                    if (Response.OverlayRsp.OverlayStateChangedPush.State == OverlayState.Hidden && ev.EventType == UPC_EventType.UPC_Event_OverlayHidden)
                    {
                        var ptr = Marshal.AllocHGlobal(sizeof(IntPtr));
                        Marshal.StructureToPtr(UPC_EventType.UPC_Event_OverlayHidden, ptr, false);
                        @delegate((void*)ptr, (void*)ev.OptData);
                    }
                }
            }
        }
        GlobalContext.Events.Clear();
        */
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_Cancel", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_Cancel(IntPtr inContext, IntPtr inHandler)
    {
        Log.Verbose("[{Function}] {inContext} {inHandler}", nameof(UPC_Cancel), inContext, inHandler);
        return (int)UPC_Result.UPC_Result_Ok;
    }
}
