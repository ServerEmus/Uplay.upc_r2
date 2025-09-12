namespace upc_r2.Exports;

internal static partial class Export
{
    static readonly Dictionary<int, string> PtrToFilePath = [];

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorageFileListGet", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorageFileListGet(IntPtr inContext, IntPtr outStorageFileList)
    {
        Log.Verbose("[{Function}] {inContext} {outStorageFileList}", nameof(UPC_StorageFileListGet), inContext, outStorageFileList);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        List<UPC_StorageFile> storageFiles = [];
        if (string.IsNullOrEmpty(UPC_Json.Instance.Save.Path))
        {
            UPC_Json.Instance.Save.Path = "saves";
            UPC_Json.SaveToJson();
        }
            
        Log.Verbose("[{Function}] Save Path: {Path}", nameof(UPC_StorageFileListGet), UPC_Json.Instance.Save.Path);
        if (!Directory.Exists(UPC_Json.Instance.Save.Path))
            Directory.CreateDirectory(UPC_Json.Instance.Save.Path);
        var files = Directory.GetFiles(UPC_Json.Instance.Save.Path);
        foreach (var item in files)
        {
            if (string.IsNullOrEmpty(item))
                continue;

            FileInfo info = new(item);
            UPC_StorageFile storageFile = new()
            {
                fileNameUtf8 = info.Name,
                legacyNameUtf8 = info.Name,
                size = (uint)info.Length,
                lastModifiedMs = (ulong)(info.LastWriteTime - new DateTime(1970, 1, 1)).TotalMilliseconds
            };
            storageFiles.Add(storageFile);
        }
        Log.Verbose("[{Function}] storageFiles Count: {Count}", nameof(UPC_StorageFileListGet), storageFiles.Count);
        if (storageFiles.Count == 0)
            return -6;
        WriteOutList(outStorageFileList, storageFiles);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorageFileListFree", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorageFileListFree(IntPtr inContext, IntPtr inStorageFileList)
    {
        Log.Verbose("[{Function}] {inContext} {inStorageFileList}", nameof(UPC_StorageFileListFree), inContext, inStorageFileList);
        if (inStorageFileList == IntPtr.Zero)
            return -0xd;
        FreeList(inStorageFileList);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorageFileOpen", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorageFileOpen(IntPtr inContext, IntPtr inFileNameUtf8, uint inFlags, IntPtr outHandle)
    {
        Log.Verbose("[{Function}] {inContext} {inFileNameUtf8} {inFlags} {outHandle}", nameof(UPC_StorageFileOpen), inContext, inFileNameUtf8, inFlags, outHandle);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        string? filename = Marshal.PtrToStringUTF8(inFileNameUtf8);
        if (filename == null)
            return (int)UPC_Result.UPC_Result_CommunicationError;
        string file = string.Empty;
        if (UPC_Json.Instance.Save.UseProductIdInName)
            file = Path.Combine(UPC_Json.Instance.Save.Path, ProductId.ToString(), filename);
        else
            file = Path.Combine(UPC_Json.Instance.Save.Path, filename);
        if (!Directory.Exists(Path.GetDirectoryName(file)))
            Directory.CreateDirectory(Path.GetDirectoryName(file)!);
        if (!File.Exists(file))
            File.Create(file).Close();
        int ptr = Random.Shared.Next();
        PtrToFilePath.Add(ptr, file);
        Log.Verbose("[{Function}] File Handler: {handler}!", nameof(UPC_StorageFileOpen), ptr);
        Marshal.WriteInt32(outHandle, 0, ptr);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorageFileRead", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorageFileRead(IntPtr inContext, int inHandle, int inBytesToRead, uint inBytesReadOffset, IntPtr outData, IntPtr outBytesRead, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inHandle} {inBytesToRead} {inBytesReadOffset} {outData} {outBytesRead} {inCallback} {inCallbackData}", nameof(UPC_StorageFileRead), inContext, inHandle, inBytesToRead, inBytesReadOffset, outData, outBytesRead, inCallback, inCallbackData);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        if (inHandle == 0)
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -13;
        }
        if (!PtrToFilePath.TryGetValue(inHandle, out string? path))
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -13;
        }
        if (path == null)
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -13;
        }
        if (inBytesToRead <= 0)
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -13;
        }

        var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        stream.Seek(0, SeekOrigin.Begin);
        if (stream.Length < inBytesReadOffset)
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_EOF));
            return -13;
        }
        var buff = new byte[inBytesToRead];
        var readed = stream.Read(buff, (int)inBytesReadOffset, inBytesToRead);
        stream.Close();
        Log.Verbose("[{Function}] bytes readed: {Readed} must read: {MustRead}", nameof(UPC_StorageFileRead), readed, inBytesToRead);
        if (readed < 0)
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_EOF));
            return -13;
        }
        Marshal.WriteInt32(outBytesRead, readed);
        Marshal.Copy(buff, 0, outData, buff.Length);
        context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_Ok));
        Log.Verbose("[{Function}] Write Done!", nameof(UPC_StorageFileRead));
        return 0x10000;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorageFileWrite", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorageFileWrite(IntPtr inContext, int inHandle, IntPtr inData, int inSize, IntPtr inCallback, IntPtr inCallbackData)
    {
        Log.Verbose("[{Function}] {inContext} {inHandle} {inData} {inSize} {inCallback} {inCallbackData}", nameof(UPC_StorageFileWrite), inContext, inHandle, inData, inSize, inCallback, inCallbackData);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        if (!PtrToFilePath.TryGetValue(inHandle, out string? path))
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -13;
        }
        if (path == null)
        {
            context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_FailedPrecondition));
            return -13;
        }
        var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        var buff = new byte[inSize];
        Marshal.Copy(inData, buff, 0, inSize);
        stream.Write(buff);
        stream.Flush(true);
        stream.Close();
        context.Callbacks.Add(new(inCallback, inCallbackData, (int)UPC_Result.UPC_Result_Ok));
        Log.Verbose("[{Function}] Write Done!", nameof(UPC_StorageFileWrite));
        return 0x10000;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorageFileClose", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorageFileClose(IntPtr inContext, int inHandle)
    {
        Log.Verbose("[{Function}] {inContext} {inHandle}", nameof(UPC_StorageFileClose), inContext, inHandle);
        PtrToFilePath.Remove(inHandle);
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPC_StorageFileDelete", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPC_StorageFileDelete(IntPtr inContext, IntPtr inFileNameUtf8)
    {
        Log.Verbose("[{Function}] {inContext} {inFileNameUtf8}", nameof(UPC_StorageFileDelete), inContext, inFileNameUtf8);
        UPC_Context? context = UPC_ContextExt.GetContext(inContext);
        if (context == null)
            return (int)UPC_Result.UPC_Result_InternalError;
        if (UPC_Json.Instance.Save.EnableFileDelete)
        {
            string? fileName = Marshal.PtrToStringUTF8(inFileNameUtf8);
            if (string.IsNullOrEmpty(fileName))
                return 0;
            string file = string.Empty;
            if (UPC_Json.Instance.Save.UseProductIdInName)
                file = Path.Combine(UPC_Json.Instance.Save.Path, ProductId.ToString(), fileName);
            else
                file = Path.Combine(UPC_Json.Instance.Save.Path, fileName);
            if (string.IsNullOrEmpty(file))
                return 0;
            File.Delete(file);
        }
        return 0;
    }
}
