// See https://aka.ms/new-console-template for more information

using NativeFileDialogSharp.Native;

var result = Dialog.FileOpenMultiple();

Console.WriteLine($"Path: {result.Path}, IsError {result.IsError}, IsOk {result.IsOk}, IsCancelled {result.IsCancelled}, ErrorMessage {result.ErrorMessage}");
if (result.Paths != null)
{
    Console.WriteLine("Paths");
    Console.WriteLine(string.Join("\n", result.Paths));
}