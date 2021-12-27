using NativeFileDialogSharp;
using System;

namespace NativeFileDialogSharpSandboxNetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintResult(Dialog.FileOpenMultiple("pdf", null));
            PrintResult(Dialog.FileOpen(null));
            PrintResult(Dialog.FileSave(null));
            PrintResult(Dialog.FolderPicker(null));
        }

        static void PrintResult(DialogResult result)
        {
            Console.WriteLine($"Path: {result.Path}, IsError {result.IsError}, IsOk {result.IsOk}, IsCancelled {result.IsCancelled}, ErrorMessage {result.ErrorMessage}");
            if (result.Paths != null)
            {
                Console.WriteLine("Paths");
                Console.WriteLine(string.Join("\n", result.Paths));
            }
        }
    }
}
