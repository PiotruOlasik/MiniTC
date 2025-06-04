using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiniTC.Model
{
    public class DiskOperations
    {
        public List<string> GetDisks()
        {
            var availableDrives = new List<string>();

            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo drive in drives)
                {
                    // Tylko dyski gotowe do użycia
                    if (drive.IsReady)
                    {
                        availableDrives.Add(drive.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                // Błąd podczas pobierania listy dysków - zwracamy pustą listę
            }

            return availableDrives;
        }

        public List<string> GetDirectory(string path)
        {
            try
            {
                string[] directories = Directory.GetDirectories(path);
                return directories.ToList();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public List<string> GetDirectoryContents(string path)
        {
            try
            {
                var contents = new List<string>();

                // Najpierw dodaj foldery
                string[] directories = Directory.GetDirectories(path);
                contents.AddRange(directories);

                // Następnie dodaj pliki
                string[] files = Directory.GetFiles(path);
                contents.AddRange(files);

                return contents;
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
            catch (DirectoryNotFoundException)
            {
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public void CopyFile(string sourceFilePath, string destinationFolderPath)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Nie znaleziono pliku źródłowego: {sourceFilePath}");
                }

                if (!Directory.Exists(destinationFolderPath))
                {
                    throw new DirectoryNotFoundException($"Nie znaleziono folderu docelowego: {destinationFolderPath}");
                }

                string fileName = Path.GetFileName(sourceFilePath);
                string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

                if (File.Exists(destinationFilePath))
                {
                    File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                }
                else
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception($"Odmowa dostępu: {ex.Message}");
            }
            catch (IOException ex)
            {
                throw new Exception($"Błąd I/O podczas kopiowania: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas kopiowania pliku: {ex.Message}");
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
                return new List<string>();
            }
        }

        public bool IsDirectory(string path)
        {
            try
            {
                return Directory.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        public bool IsFile(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        public string GetParentDirectory(string path)
        {
            try
            {
                return Path.GetDirectoryName(path);
            }
            catch
            {
                return null;
            }
        }
    }
}