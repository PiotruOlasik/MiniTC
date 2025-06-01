using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    public class DiskOperations
    {
        public List<string> GetDisks()
        {
            string[] drives = Directory.GetLogicalDrives();
            return drives.ToList();
        }

        public List<string> GetDirectory(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            return directories.ToList();
        }

        public void CopyFile(string sourceFilePath, string destinationFolderPath)
        {
            try
            {
                string fileName = Path.GetFileName(sourceFilePath);
                string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

                File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd przy kopiowaniu plików: {ex.Message}");
            }
        }

        public List<string> GetFiles(string path)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                return files.ToList();
            }
            catch (Exception ex)
            {
                return new List<string>(); // Zwraca pustą listę w przypadku błędu
            }
        }
    }
}

