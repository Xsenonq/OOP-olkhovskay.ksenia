using System;
using System.IO;

namespace lab30vN
{
    public class FileHelper
    {
        public string GetExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty");

            return Path.GetExtension(fileName);
        }

        public bool IsImageFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            string ext = Path.GetExtension(fileName).ToLower();

            return ext == ".jpg" ||
                   ext == ".jpeg" ||
                   ext == ".png" ||
                   ext == ".gif" ||
                   ext == ".bmp";
        }
    }
}
