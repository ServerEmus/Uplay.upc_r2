using System.Diagnostics;

namespace upc_r2;

internal class UPC_Context
{
    public UPC_ContextSubsystem EnabledSubsystems = UPC_ContextSubsystem.UPC_ContextSubsystem_None;
    public List<Callback> Callbacks = [];
    public List<Event> Events = [];
    public Stopwatch SW = new();
    // TODO Add valid events here. or something.
}

internal static class UPC_ContextExt
{
    private static readonly Dictionary<IntPtr, UPC_Context> pointerToCTX = [];

    public static IntPtr CreateContext(this UPC_ContextSubsystem subsystem)
    {
        FakeContext fakeContext = new();
        IntPtr ptr = fakeContext.ToIntPtr();
        pointerToCTX.Add(ptr, new()
        {
            EnabledSubsystems = subsystem
        });
        return ptr;
    }

    public static UPC_Context? GetContext(IntPtr ptr)
    {
        return pointerToCTX.GetValueOrDefault(ptr);
    }

    public static void FreeContext(IntPtr ptr)
    {
        pointerToCTX.Remove(ptr);
        Marshal.DestroyStructure<FakeContext>(ptr);
        Marshal.FreeHGlobal(ptr);
    }
}
