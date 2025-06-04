using MiniTC.Presenter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace MiniTC.View
{
    public partial class PanelTC : UserControl, IPanelTC
    {
        private LeftTCPresenter _presenter;

        public PanelTC()
        {
            InitializeComponent();
            comboBox_Drive.DropDown += ComboBox_Drive_DropDown;
            comboBox_Drive.SelectedIndexChanged += comboBox_Drive_SelectedIndexChanged;
            listBox_Folders.DoubleClick += listBox_Folders_DoubleClick;
            listBox_Folders.Click += listBox_Folders_Click;
        }

        public void SetPresenter(LeftTCPresenter presenter)
        {
            _presenter = presenter;
        }

        public string currentPath { get; private set; } = "";
        public List<string> currentPathContent { get; private set; } = new List<string>();
        public List<string> drives { get; private set; } = new List<string>();

        public void SetDisks(List<string> drives)
        {
            this.drives = drives;
        }

        public void SetDirectories(string currentPath, List<string> contents)
        {
            this.currentPath = currentPath;
            this.currentPathContent = contents;
            listBox_Folders.Items.Clear();

            if (!IsRootDirectory(currentPath))
            {
                listBox_Folders.Items.Add("..");
            }

            // Separate directories and files
            var directories = contents.Where(item => Directory.Exists(item)).ToList();
            var files = contents.Where(item => File.Exists(item)).ToList();

            // DOdanie prefuksu <D>
            foreach (string directory in directories)
            {
                string displayName = $"<D> {Path.GetFileName(directory)}";
                listBox_Folders.Items.Add(displayName);
            }

            // Add files after directories
            foreach (string file in files)
            {
                string displayName = Path.GetFileName(file);
                listBox_Folders.Items.Add(displayName);
            }

            // Update path display
            richTextBox_Path.Text = currentPath;
        }

        private bool IsRootDirectory(string path)
        {
            try
            {
                return Directory.GetParent(path) == null;
            }
            catch
            {
                return true;
            }
        }

        private void ComboBox_Drive_DropDown(object sender, EventArgs e)
        {
            // Refresh available drives when dropdown is opened
            _presenter?.ShowDisks();
        }

        private void comboBox_Drive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter?.OnDriveChanged(comboBox_Drive.SelectedItem?.ToString());
        }

        private void listBox_Folders_Click(object sender, EventArgs e)
        {
            // Handle single click for selection
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

                // Remove the <D> prefix and get the actual path
                string actualName;
                if (selectedItem.StartsWith("<D> "))
                {
                    actualName = selectedItem.Substring(4); // Remove "<D> "
                }
                else
                {
                    actualName = selectedItem;
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

        public string GetSelectedFile()
        {
            if (listBox_Folders.SelectedItem != null)
            {
                string selectedItem = listBox_Folders.SelectedItem.ToString();

                // Skip parent directory and directories
                if (selectedItem == ".." || selectedItem.StartsWith("<D> "))
                {
                    return null;
                }

                // Get the actual filename
                string fileName = selectedItem;

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

        public void PopulateDriveComboBox()
        {
            comboBox_Drive.Items.Clear();
            comboBox_Drive.Items.AddRange(drives.ToArray());

            if (comboBox_Drive.Items.Count > 0)
                comboBox_Drive.SelectedIndex = 0;
        }
    }
}