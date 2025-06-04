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
                MessageBox.Show("Dostęp do tego folderu jest zabroniony.", "Zabroniony dostęp",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Nie znaleziono folderu.", "Nie znaleziono folderu",
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
                    // Navigate to parent directory
                    DirectoryInfo parent = Directory.GetParent(_view.currentPath);
                    newPath = parent?.FullName ?? _view.currentPath;
                }
                else
                {
                    // Check if it's a directory
                    if (Directory.Exists(folderPath))
                    {
                        newPath = folderPath;
                    }
                    else
                    {
                        // It's a file, don't navigate
                        return;
                    }
                }

                ShowDirectory(newPath);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied to this folder.", "Access Denied",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Directory not found.", "Directory Not Found",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accessing directory: {ex.Message}", "Error",
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