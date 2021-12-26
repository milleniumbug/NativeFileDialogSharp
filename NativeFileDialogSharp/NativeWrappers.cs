using System;
using System.Collections.Generic;
using System.Text;

namespace NativeFileDialogSharp.Native;

public static class Dialog
{
    private static byte[] ToUtf8(string s)
    {
        var byteCount = Encoding.UTF8.GetByteCount(s);
        var bytes = new byte[byteCount + 1];
        Encoding.UTF8.GetBytes(s.AsSpan(), bytes.AsSpan());
        return bytes;
    }

    private static unsafe Span<byte> MakeSpanFromNullTerminatedString(byte* nullTerminatedString)
    {
        int count = 0;
        var ptr = nullTerminatedString;
        while (*ptr != 0)
        {
            ptr++;
            count++;
        }

        return new Span<byte>(nullTerminatedString, count);
    }

    private static string FromUtf8(ReadOnlySpan<byte> input)
    {
        return Encoding.UTF8.GetString(input);
    }

    public static unsafe DialogResult FileOpen(string filterList = null, string defaultPath = null)
    {
        fixed (byte* filterListNts = filterList != null ? ToUtf8(filterList) : null)
        fixed (byte* defaultPathNts = defaultPath != null ? ToUtf8(defaultPath) : null)
        {
            string path = null;
            string errorMessage = null;
            var result = NativeFunctions.NFD_OpenDialog(filterListNts, defaultPathNts, out IntPtr outPathIntPtr);
            if (result == nfdresult_t.NFD_ERROR)
            {
                errorMessage = FromUtf8(MakeSpanFromNullTerminatedString(NativeFunctions.NFD_GetError()));
            }
            else if (result == nfdresult_t.NFD_OKAY)
            {
                var outPathNts = (byte*)outPathIntPtr.ToPointer();
                path = FromUtf8(MakeSpanFromNullTerminatedString(outPathNts));
                NativeFunctions.NFD_Free(outPathIntPtr);
            }

            return new DialogResult(result, path, null, errorMessage);
        }
    }
    
    public static unsafe DialogResult FileSave(string filterList = null, string defaultPath = null)
    {
        fixed (byte* filterListNts = filterList != null ? ToUtf8(filterList) : null)
        fixed (byte* defaultPathNts = defaultPath != null ? ToUtf8(defaultPath) : null)
        {
            string path = null;
            string errorMessage = null;
            var result = NativeFunctions.NFD_SaveDialog(filterListNts, defaultPathNts, out IntPtr outPathIntPtr);
            if (result == nfdresult_t.NFD_ERROR)
            {
                errorMessage = FromUtf8(MakeSpanFromNullTerminatedString(NativeFunctions.NFD_GetError()));
            }
            else if (result == nfdresult_t.NFD_OKAY)
            {
                var outPathNts = (byte*)outPathIntPtr.ToPointer();
                path = FromUtf8(MakeSpanFromNullTerminatedString(outPathNts));
                NativeFunctions.NFD_Free(outPathIntPtr);
            }

            return new DialogResult(result, path, null, errorMessage);
        }
    }
    
    public static unsafe DialogResult FolderPicker(string defaultPath = null)
    {
        fixed (byte* defaultPathNts = defaultPath != null ? ToUtf8(defaultPath) : null)
        {
            string path = null;
            string errorMessage = null;
            var result = NativeFunctions.NFD_PickFolder(defaultPathNts, out IntPtr outPathIntPtr);
            if (result == nfdresult_t.NFD_ERROR)
            {
                errorMessage = FromUtf8(MakeSpanFromNullTerminatedString(NativeFunctions.NFD_GetError()));
            }
            else if (result == nfdresult_t.NFD_OKAY)
            {
                var outPathNts = (byte*)outPathIntPtr.ToPointer();
                path = FromUtf8(MakeSpanFromNullTerminatedString(outPathNts));
                NativeFunctions.NFD_Free(outPathIntPtr);
            }

            return new DialogResult(result, path, null, errorMessage);
        }
    }
    
    public static unsafe DialogResult FileOpenMultiple(string filterList = null, string defaultPath = null)
    {
        fixed (byte* filterListNts = filterList != null ? ToUtf8(filterList) : null)
        fixed (byte* defaultPathNts = defaultPath != null ? ToUtf8(defaultPath) : null)
        {
            List<string> paths = null;
            string errorMessage = null;
            nfdpathset_t pathSet;
            var result = NativeFunctions.NFD_OpenDialogMultiple(filterListNts, defaultPathNts, &pathSet);
            if (result == nfdresult_t.NFD_ERROR)
            {
                errorMessage = FromUtf8(MakeSpanFromNullTerminatedString(NativeFunctions.NFD_GetError()));
            }
            else if (result == nfdresult_t.NFD_OKAY)
            {
                var pathCount = (int)NativeFunctions.NFD_PathSet_GetCount(&pathSet).ToUInt32();
                paths = new List<string>(pathCount);
                for (int i = 0; i < pathCount; i++)
                {
                    paths.Add(FromUtf8(MakeSpanFromNullTerminatedString(NativeFunctions.NFD_PathSet_GetPath(&pathSet, new UIntPtr((uint)i)))));
                }
                NativeFunctions.NFD_PathSet_Free(&pathSet);
            }

            return new DialogResult(result, null, paths, errorMessage);
        }
    }
}

public class DialogResult
{
    private readonly nfdresult_t result;
    
    public string Path { get; }
    
    public IReadOnlyList<string> Paths { get; }

    public bool IsError => result == nfdresult_t.NFD_ERROR;
    
    public string ErrorMessage { get; }

    public bool IsCancelled => result == nfdresult_t.NFD_CANCEL;

    public bool IsOk => result == nfdresult_t.NFD_OKAY;

    internal DialogResult(nfdresult_t result, string path, IReadOnlyList<string> paths, string errorMessage)
    {
        this.result = result;
        Path = path;
        Paths = paths;
        ErrorMessage = errorMessage;
    }
}