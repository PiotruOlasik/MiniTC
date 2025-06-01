using MiniTC.Presenter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace MiniTC.View
{
    public partial class PanelTC : UserControl, IPanelTC
    {
        private LeftTCPresenter _presenter;

        public PanelTC()
        {
            InitializeComponent();
            comboBox_Drive.SelectedIndexChanged += comboBox_Drive_SelectedIndexChanged;
            listBox_Folders.DoubleClick += listBox_Folders_DoubleClick;
        }

        public void SetPresenter(LeftTCPresenter presenter)
        {
            _presenter = presenter;
        }

        public string currentPath { get; private set; }
        public List<string> currentPathContent { get; private set; }
        public List<string> drives { get; private set; } = new List<string>();

        public void SetDisks(List<string> drives)
        {
            this.drives = drives;

            comboBox_Drive.Items.Clear();
            comboBox_Drive.Items.AddRange(drives.ToArray());

            if (comboBox_Drive.Items.Count > 0)
                comboBox_Drive.SelectedIndex = 0;
        }

        public void SetDirectories(string currentPath, List<string> contents)
        {
            this.currentPath = currentPath;
            this.currentPathContent = contents;
            listBox_Folders.Items.Clear();

            // Add parent directory option if not at root
            if (Directory.GetParent(currentPath) != null)
            {
                listBox_Folders.Items.Add("..");
            }

            // Add directories and files with visual indicators
            foreach (string item in contents)
            {
                string displayName;
                if (Directory.Exists(item))
                {
                    displayName = $"[DIR] {Path.GetFileName(item)}";
                }
                else if (File.Exists(item))
                {
                    displayName = $"      {Path.GetFileName(item)}";
                }
                else
                {
                    displayName = Path.GetFileName(item);
                }

                listBox_Folders.Items.Add(displayName);
            }

            // Update path display
            richTextBox_Path.Text = currentPath;
        }

        private void listBox_Folders_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_Folders.SelectedItem != null)
            {
                string selectedItem = listBox_Folders.SelectedItem.ToString();

                if (selectedItem == "..")
                {
                    _presenter?.OnFolderSelected("..");
                    return;
                }

                // Remove the [DIR] prefix and get the actual path
                string actualName;
                if (selectedItem.StartsWith("[DIR] "))
                {
                    actualName = selectedItem.Substring(6); // Remove "[DIR] "
                }
                else
                {
                    actualName = selectedItem.Trim(); // Remove leading spaces for files
                }

                // Find the full path from currentPathContent
                string fullPath = currentPathContent?.FirstOrDefault(path =>
                    Path.GetFileName(path) == actualName);

                if (!string.IsNullOrEmpty(fullPath))
                {
                    _presenter?.OnFolderSelected(fullPath);
                }
            }
        }

        private void comboBox_Drive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter?.OnDriveChanged(comboBox_Drive.SelectedItem?.ToString());
        }

        public string GetSelectedFile()
        {
            if (listBox_Folders.SelectedItem != null)
            {
                string selectedItem = listBox_Folders.SelectedItem.ToString();

                // Skip parent directory and directories
                if (selectedItem == ".." || selectedItem.StartsWith("[DIR] "))
                {
                    return null;
                }

                // Get the actual filename (remove leading spaces)
                string fileName = selectedItem.Trim();

                // Find the full path from currentPathContent
                string fullPath = currentPathContent?.FirstOrDefault(path =>
                    Path.GetFileName(path) == fileName);

                // Verify it's a file
                if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                {
                    return fullPath;
                }
            }
            return null;
        }

        public void RefreshFiles()
        {
            if (!string.IsNullOrEmpty(currentPath))
            {
                _presenter?.ShowDirectory(currentPath);
            }
        }
    }
}