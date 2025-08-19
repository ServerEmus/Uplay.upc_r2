namespace upc_r2.Exports;

internal static partial class Export
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_ShowBrowserUrl", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_ShowBrowserUrl(IntPtr inContext, IntPtr inBrowserUrlUtf8)
    {
        Log.Verbose("[{Function}] {inContext} {inBrowserUrlUtf8}", nameof(UPC_ShowBrowserUrl), inContext, inBrowserUrlUtf8);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayBrowserUrlShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayBrowserUrlShow(IntPtr inContext, IntPtr inBrowserUrlUtf8, IntPtr inOptCallback, IntPtr inOptCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inBrowserUrlUtf8} {inOptCallback} {inOptCallbackData}", nameof(UPC_OverlayBrowserUrlShow), inContext, inBrowserUrlUtf8, inOptCallback, inOptCallbackData);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayFriendInvitationShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayFriendInvitationShow(IntPtr inContext, IntPtr inOptIdListUtf8, uint inOptIdListLength)
    {
        Log.Verbose("[{Function}] {inContext} {inOptIdListUtf8} {inOptIdListLength}", nameof(UPC_OverlayFriendInvitationShow), inContext, inOptIdListUtf8, inOptIdListLength);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayFriendInvitationShow_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayFriendInvitationShow_Extended(IntPtr inContext, IntPtr inOptIdListUtf8, uint inOptIdListLength, IntPtr unk1, IntPtr unk2)
    {
        Log.Verbose("[{Function}] {inContext} {inOptIdListUtf8} {inOptIdListLength} {unk1} {unk2}", nameof(UPC_OverlayFriendInvitationShow_Extended), inContext, inOptIdListUtf8, inOptIdListLength, unk1, unk2);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayFriendSelectionFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayFriendSelectionFree(IntPtr inContext, IntPtr inSelectedFriends)
    {
        Log.Verbose("[{Function}] {inContext} {inSelectedFriends}", nameof(UPC_OverlayFriendSelectionFree), inContext, inSelectedFriends);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayFriendSelectionShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayFriendSelectionShow(IntPtr inContext, IntPtr inIdListUtf8, uint inIdListLength, IntPtr outSelectedFriends, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inIdListUtf8} {inIdListLength} {outSelectedFriends} {inCallback} {inCallbackData}", nameof(UPC_OverlayFriendSelectionShow), inContext, inIdListUtf8, inIdListLength, outSelectedFriends, inCallback, inCallbackData);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayMicroAppShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayMicroAppShow(IntPtr inContext, IntPtr inOptMicroAppParamList, IntPtr inOptCallback, IntPtr inOptCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inOptMicroAppParamList} {inOptCallback} {inOptCallbackData}", nameof(UPC_OverlayMicroAppShow), inContext, inOptMicroAppParamList, inOptCallback, inOptCallbackData);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayNotificationShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayNotificationShow(IntPtr inContext, uint inId)
    {
        Log.Verbose("[{Function}] {inContext} {inId}", nameof(UPC_OverlayNotificationShow), inContext, inId);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayNotificationShow_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayNotificationShow_Extended(IntPtr inContext, uint inId, IntPtr unk1, IntPtr unk2)
    {
        Log.Verbose("[{Function}] {inContext} {inId} {unk1} {unk2}", nameof(UPC_OverlayNotificationShow_Extended), inContext, inId, unk1, unk2);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_OverlayShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_OverlayShow(IntPtr inContext, uint inSection, IntPtr inOptCallback, IntPtr inOptCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inSection} {inOptCallback} {inOptCallbackData}", nameof(UPC_OverlayShow), inContext, inSection, inOptCallback, inOptCallbackData);
        return 0;
    }
}
