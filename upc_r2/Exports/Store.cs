namespace upc_r2.Exports;

internal static partial class Export
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreCheckout", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreCheckout(IntPtr inContext, uint inId)
    {
        Log.Verbose("[{Function}] {inContext} {inId}", nameof(UPC_StoreCheckout), inContext, inId);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreIsEnabled", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreIsEnabled(IntPtr inContext)
    {
        Log.Verbose("[{Function}] {inContext{", nameof(UPC_StoreIsEnabled), inContext);
        return 1;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreIsEnabled_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreIsEnabled_Extended(IntPtr inContext, IntPtr outIsEnabled)
    {
        Log.Verbose("[{Function}] {inContext} {outIsEnabled}", nameof(UPC_StoreIsEnabled_Extended), inContext);
        Marshal.WriteInt32(outIsEnabled, 0, 1);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreLanguageSet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreLanguageSet(IntPtr inContext, IntPtr inLanguageCountryCode)
    {
        Log.Verbose("[{Function}] {inContext} {inAddonId} {inOptCallback} {inOptCallbackData}", nameof(UPC_StoreLanguageSet), inContext, inLanguageCountryCode);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorePartnerGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorePartnerGet(IntPtr inContext)
    {
        Log.Verbose("[{Function}] {inContext}", nameof(UPC_StorePartnerGet), inContext);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorePartnerGet_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorePartnerGet_Extended(IntPtr inContext, IntPtr outPartner)
    {
        Log.Verbose("[{Function}] {inContext} {outPartner}", nameof(UPC_StorePartnerGet_Extended), inContext, outPartner);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreProductDetailsShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreProductDetailsShow(IntPtr inContext, uint inId)
    {
        Log.Verbose("[{Function}] {inContext} {inId}", nameof(UPC_StoreProductDetailsShow), inContext, inId);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreProductListFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreProductListFree(IntPtr inContext, IntPtr inProductList)
    {
        Log.Verbose("[{Function}] {inContext} {inProductList}", nameof(UPC_StoreProductListFree), inContext, inProductList);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreProductListGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreProductListGet(IntPtr inContext, IntPtr outProductList, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {outProductList} {inCallback} {inCallbackData}", nameof(UPC_StoreProductListGet), inContext, outProductList, inCallback, inCallbackData);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StoreProductsShow", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StoreProductsShow(IntPtr inContext, IntPtr inTagsList)
    {
        Log.Verbose("[{Function}] {inContext} {inTagsList}", nameof(UPC_StoreProductsShow), inContext, inTagsList);
        return 0;
    }
}
