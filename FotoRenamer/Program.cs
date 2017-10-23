using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args.Length > 0 ? args[0] : ".";

            var files = Directory.GetFiles(path,"*.*", args.Length > 0? SearchOption.TopDirectoryOnly: SearchOption.AllDirectories);
            foreach (var filePath in files)
            {
                var prefix = "";
                var ext = Path.GetExtension(filePath)?.ToLower();
                var date = File.GetLastWriteTime(filePath);
                switch (ext)
                {
                    case ".jpg":
                    case ".png":
                    case ".jpeg":
                        prefix = "img_";
                        break;
                    case ".avi":
                    case ".mp4":
                    case ".mov":
                        prefix = "video_";
                        break;
                    default:
                        continue;
                }
                var i = 0;
                string newName;
                do
                {
                    newName = Path.Combine(path, $"{prefix}{date:yyyyMMdd_HHmmss}{(i == 0 ? "" : $"_{i}")}{ext}");
                    i++;
                } while (File.Exists(newName));

                File.Move(filePath, newName);

                //File.SetLastWriteTime(newName, date);
            }
        }
    }
}
