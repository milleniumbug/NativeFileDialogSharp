using System;
using System.Runtime.InteropServices;

namespace NativeFileDialogSharp.Native
{
    public struct nfdpathset_t
    {
        public IntPtr buf;
        public IntPtr indices;
        public UIntPtr count;
    }

    public enum nfdresult_t
    {
        NFD_ERROR,
        NFD_OKAY,
        NFD_CANCEL
    }

    public static class NativeFunctions
    {
        public const string LibraryName = "nfd";

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_OpenDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_OpenDialogMultiple(byte* filterList, byte* defaultPath,
            nfdpathset_t* outPaths);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_SaveDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_PickFolder(byte* defaultPath, out IntPtr outPath);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe byte* NFD_GetError();

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe UIntPtr NFD_PathSet_GetCount(nfdpathset_t* pathSet);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe byte* NFD_PathSet_GetPath(nfdpathset_t* pathSet, UIntPtr index);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe void NFD_PathSet_Free(nfdpathset_t* pathSet);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe void NFD_Dummy();

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe IntPtr NFD_Malloc(UIntPtr bytes);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe void NFD_Free(IntPtr ptr);
    }
    
    public static class NativeFunctions32
    {
        public const string LibraryName = "nfd_x86";

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_OpenDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_OpenDialogMultiple(byte* filterList, byte* defaultPath,
            nfdpathset_t* outPaths);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_SaveDialog(byte* filterList, byte* defaultPath, out IntPtr outPath);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe nfdresult_t NFD_PickFolder(byte* defaultPath, out IntPtr outPath);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe byte* NFD_GetError();

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe UIntPtr NFD_PathSet_GetCount(nfdpathset_t* pathSet);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe byte* NFD_PathSet_GetPath(nfdpathset_t* pathSet, UIntPtr index);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe void NFD_PathSet_Free(nfdpathset_t* pathSet);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe void NFD_Dummy();

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe IntPtr NFD_Malloc(UIntPtr bytes);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern unsafe void NFD_Free(IntPtr ptr);
    }
}