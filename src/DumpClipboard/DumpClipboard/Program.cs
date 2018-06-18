using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DumpClipboard
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string suppliedFilename = null;
            if (args.Length > 0)
            {
                suppliedFilename = args[0];
            }
            if (Clipboard.ContainsImage())
            {
                string path = suppliedFilename ?? GetAvailablePath(".png");
                Image img = Clipboard.GetImage();
                img.Save(path);
            }
            if (Clipboard.ContainsText())
            {
                string path = suppliedFilename ?? GetAvailablePath(".txt");
                File.WriteAllText(path, Clipboard.GetText());
            }
            if (Clipboard.ContainsData(DataFormats.Rtf))
            {
                string path = suppliedFilename ?? GetAvailablePath(".rtf");
                File.WriteAllText(path, (string)Clipboard.GetData(DataFormats.Rtf));
            }
            if (Clipboard.ContainsData(DataFormats.CommaSeparatedValue))
            {
                string path = suppliedFilename ?? GetAvailablePath(".csv");
                File.WriteAllText(path, (string)Clipboard.GetData(DataFormats.CommaSeparatedValue));
            }
        }

        public static string GetAvailablePath(string extension)
        {
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string baseFilename = "ClipboardContents";
            string tentativePath = Path.Combine(directoryPath, baseFilename + extension);
            int i = 1;
            while (File.Exists(tentativePath))
            {
                tentativePath = Path.Combine(directoryPath, baseFilename + $"_{i++:D3}" + extension);
            }
            return tentativePath;
        }
    }
}
