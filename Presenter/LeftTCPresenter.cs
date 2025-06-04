using MiniTC.Model;
using MiniTC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MiniTC.Presenter
{
    public class LeftTCPresenter
    {
        private View.PanelTC _view;
        private DiskOperations _diskOps = new DiskOperations();

        public LeftTCPresenter(View.PanelTC view)
        {
            _view = view;
        }

        public void ShowDisks()
        {
            try
            {
                var disks = _diskOps.GetDisks();
                _view.SetDisks(disks);
                _view.PopulateDriveComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania dysków: {ex.Message}", "Błąd",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowDirectory(string path)
        {
            try
            {
                var contents = _diskOps.GetDirectoryContents(path);
                _view.SetDirectories(path, contents);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Dostęp do tego folderu jest zabroniony.", "Odmowa dostępu",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Nie znaleziono folderu.", "Folder nie istnieje",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dostępu do folderu: {ex.Message}", "Błąd",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnDriveChanged(string selectedDrive)
        {
            if (!string.IsNullOrEmpty(selectedDrive))
            {
                ShowDirectory(selectedDrive);
            }
        }

        public void OnFolderSelected(string folderPath)
        {
            try
            {
                string newPath;

                if (folderPath == "..")
                {
                    // Przejdź do folderu nadrzędnego
                    DirectoryInfo parent = Directory.GetParent(_view.currentPath);
                    newPath = parent?.FullName ?? _view.currentPath;
                }
                else
                {
                    // Sprawdź czy to jest folder
                    if (Directory.Exists(folderPath))
                    {
                        newPath = folderPath;
                    }
                    else
                    {
                        // To jest plik, nie nawiguj
                        return;
                    }
                }

                ShowDirectory(newPath);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Dostęp do tego folderu jest zabroniony.", "Odmowa dostępu",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Nie znaleziono folderu.", "Folder nie istnieje",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dostępu do folderu: {ex.Message}", "Błąd",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshCurrentDirectory()
        {
            if (!string.IsNullOrEmpty(_view.currentPath))
            {
                ShowDirectory(_view.currentPath);
            }
        }
    }
}