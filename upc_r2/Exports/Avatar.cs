using DllShared;
using StbImageSharp;
using Uplay.Uplaydll;

namespace upc_r2.Exports;

internal static class Avatar
{
    static readonly Dictionary<IntPtr, int> PtrToSize = [];

    [UnmanagedCallersOnly(EntryPoint = "UPC_AvatarFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_AvatarFree(IntPtr inContext, IntPtr inImageRGBA)
    {
        Log.Verbose("[{Function}] {inContext} {inImageRGBA}", nameof(UPC_AvatarFree), inContext, inImageRGBA);
        if (inImageRGBA == IntPtr.Zero)
            return 0;
        Marshal.FreeHGlobal(inImageRGBA);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_AvatarGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_AvatarGet(IntPtr inContext, IntPtr inOptUserIdUtf8, uint inSize, IntPtr outImageRGBA, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inImageRGBA} {inSize} {outImageRGBA} {inCallback} {inCallbackData}", nameof(UPC_AvatarGet), inContext, inOptUserIdUtf8, inSize, outImageRGBA, inCallback, inCallbackData);
        if (string.IsNullOrEmpty(UPC_Json.Instance.AvatarsPath))
        {
            Main.GlobalContext.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -1;
        }
        string? accountid = Marshal.PtrToStringAnsi(inOptUserIdUtf8);
        if (string.IsNullOrEmpty(accountid))
        {
            Main.GlobalContext.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -1;
        }
        Uplay.Uplaydll.AvatarSize size = (Uplay.Uplaydll.AvatarSize)inSize;
        string sizeStr = size switch
        {
            Uplay.Uplaydll.AvatarSize._64 => "64",
            Uplay.Uplaydll.AvatarSize._128 => "128",
            Uplay.Uplaydll.AvatarSize._256 => "256",
            _ => "64",
        };
        string path = Path.Combine(UPC_Json.Instance.AvatarsPath, $"{accountid}_{sizeStr}.png");
        if (!File.Exists(path))
        {
            Main.GlobalContext.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -1;
        }
        using var stream = File.OpenRead(path);
        ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        byte[] data = image.Data;
        // Convert rgba to bgra
        for (int i = 0; i < image.Width * image.Width; ++i)
        {
            byte r = data[i * 4];
            byte g = data[i * 4 + 1];
            byte b = data[i * 4 + 2];
            byte a = data[i * 4 + 3];


            data[i * 4] = b;
            data[i * 4 + 1] = g;
            data[i * 4 + 2] = r;
            data[i * 4 + 3] = a;
        }
        Marshal.Copy(data, 0, outImageRGBA, data.Length);
        Main.GlobalContext.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_Ok));
        return 0;
    }


    [UnmanagedCallersOnly(EntryPoint = "UPC_BlacklistAdd", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_BlacklistAdd(IntPtr inContext, IntPtr inUserIdUtf8, IntPtr inOptCallback, IntPtr inOptCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inUserIdUtf8} {inOptCallback} {inOptCallbackData}", nameof(UPC_BlacklistAdd), inContext, inUserIdUtf8, inOptCallback, inOptCallbackData);
        Main.GlobalContext.Callbacks.Add(new(inOptCallback, inOptCallbackData, (int)UPC_Result.UPC_Result_Ok));
        return 0;
    }


    [UnmanagedCallersOnly(EntryPoint = "UPC_BlacklistHas", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_BlacklistHas(IntPtr inContext, IntPtr inUserIdUtf8)
    {
        Log.Verbose("[{Function}] {inContext} {inUserIdUtf8}", nameof(UPC_BlacklistHas), inContext, inUserIdUtf8);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_BlacklistHas_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_BlacklistHas_Extended(IntPtr inContext, IntPtr inUserIdUtf8, IntPtr isBlackListed)
    {
        Log.Verbose("[{Function}] {inContext} {inUserIdUtf8} {isBlackListed}", nameof(UPC_BlacklistHas_Extended), inContext, inUserIdUtf8, isBlackListed);
        Marshal.WriteByte(isBlackListed, 0);
        return 0;
    }
}
