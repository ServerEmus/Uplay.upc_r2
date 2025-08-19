namespace upc_r2.Exports;

internal static partial class Export
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_UserGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_UserGet(IntPtr inContext, IntPtr inOptUserIdUtf8, IntPtr outUser, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inOptUserIdUtf8} {outUser} {inCallback} {inCallbackData}", nameof(UPC_UserGet), inContext, inOptUserIdUtf8, outUser, inCallback, inCallbackData);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        context.Callbacks.Add(new(inCallback, inCallbackData, 0));

        UPC_User user = new()
        {
            idUtf8 = UPC_Json.Instance.Account.AccountId,
            nameUtf8 = UPC_Json.Instance.Account.Name,
            relationship = Uplay.Uplaydll.Relationship.None,
            presence = new()
            {
                onlineStatus = Uplay.Uplaydll.OnlineStatusV2.OnlineStatusOnline,
                multiplayerSize = 1,
                multiplayerMaxSize = 1,
                detailsUtf8 = $"Playing {ProductId}",
                multiplayerId = Guid.NewGuid().ToString(),
                multiplayerInternalData = [],
                multiplayerJoinable = 0, // Disable joinable lobbies
                titleId = ProductId,
                titleNameUtf8 = $"Product {ProductId}"
            }
        };
        var impl = UPC_UserImpl.BuildFrom(user);
        Marshal.WriteIntPtr(outUser, impl.ToIntPtr());
        return 0x10000;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_UserFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_UserFree(IntPtr inContext, IntPtr inUser)
    {
        Log.Verbose("[{Function}] {inContext} {inUser}", nameof(UPC_UserFree), inContext, inUser);
        if (inUser == IntPtr.Zero)
            return 0;
        var user = Marshal.PtrToStructure<UPC_UserImpl>(inUser);
        UPC_UserImpl.Free(user);
        Marshal.DestroyStructure<UPC_UserImpl>(inUser);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_UserPlayedWithAdd", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_UserPlayedWithAdd(IntPtr inContext, IntPtr inUserIdUtf8List, uint inListLength)
    {
        Log.Verbose("[{Function}] {inContext} {inUserIdUtf8List} {inListLength}", nameof(UPC_UserPlayedWithAdd), inContext, inUserIdUtf8List, inListLength);
        return 0;
    }


    [UnmanagedCallersOnly(EntryPoint = "UPC_UserPlayedWithAdd_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_UserPlayedWithAdd_Extended(IntPtr inContext, IntPtr inUserIdUtf8List, uint inListLength, IntPtr unk1, IntPtr unk2)
    {
        Log.Verbose("[{Function}] {inContext} {inUserIdUtf8List} {inListLength} {unk1} {unk2}", nameof(UPC_UserPlayedWithAdd_Extended), inContext, inUserIdUtf8List, inListLength, unk1, unk2);
        return 0;
    }
}
