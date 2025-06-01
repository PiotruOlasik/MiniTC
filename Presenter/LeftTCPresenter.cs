using MiniTC.Model;
using MiniTC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            var disks = _diskOps.GetDisks();
            _view.SetDisks(disks);
        }

        public void ShowDirectory(string path)
        {
            var directory = _diskOps.GetDirectory(path);
            _view.SetDirectories(path, directory);
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
                    newPath = folderPath;
                }

                ShowDirectory(newPath);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied to this folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accessing directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
