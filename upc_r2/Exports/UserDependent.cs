using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace upc_r2.Exports;

internal static class UserDependent
{
    [UnmanagedCallersOnly(EntryPoint = "UPC_EmailGet", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPC_EmailGet(IntPtr inContext)
    {
        Log.Verbose(nameof(UPC_EmailGet), [inContext]);
        return Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Email);
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_IdGet", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPC_IdGet(IntPtr inContext)
    {
        Log.Verbose(nameof(UPC_IdGet), [inContext]);
        return Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.AccountId);
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_IdGet_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_IdGet_Extended(IntPtr inContext, IntPtr idptr)
    {
        Log.Verbose(nameof(UPC_IdGet_Extended), [inContext]);
        Marshal.WriteIntPtr(idptr, 0, Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.AccountId));
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_InstallLanguageGet", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPC_InstallLanguageGet(IntPtr inContext)
    {
        Log.Verbose(nameof(UPC_InstallLanguageGet), [inContext]);
        return Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Country);
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_InstallLanguageGet_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_InstallLanguageGet_Extended(IntPtr inContext, IntPtr langPtr)
    {
        Log.Verbose(nameof(UPC_InstallLanguageGet_Extended), [inContext]);
        Marshal.WriteIntPtr(langPtr, 0, Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Country));
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_NameGet", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPC_NameGet(IntPtr inContext)
    {
        Log.Verbose(nameof(UPC_NameGet), [inContext]);
        return Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Name);
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_NameGet_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_NameGet_Extended(IntPtr inContext, IntPtr nameptr)
    {
        Log.Verbose(nameof(UPC_NameGet_Extended), [inContext]);
        Marshal.WriteIntPtr(nameptr, 0, Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Name));
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_TicketGet", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPC_TicketGet(IntPtr inContext)
    {
        Log.Verbose(nameof(UPC_TicketGet), [inContext]);
        return Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Ticket);
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_TicketGet_Extended", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_TicketGet_Extended(IntPtr inContext, IntPtr ticketPtr)
    {
        Log.Verbose(nameof(UPC_TicketGet_Extended), [inContext]);
        Marshal.WriteIntPtr(ticketPtr, 0, Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Ticket));
        return (int)UPC_Result.UPC_Result_Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_UserAccountCountryGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_UserAccountCountryGet(IntPtr inContext, IntPtr outCountryCode)
    {
        Log.Verbose(nameof(UPC_UserAccountCountryGet), [inContext]);
        Marshal.WriteIntPtr(outCountryCode, 0, Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Country));
        return (int)UPC_Result.UPC_Result_Ok;
    }
}
