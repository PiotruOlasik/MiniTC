using MiniTC.Presenter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public string currentPath { get; private set;}
        public List<string> currentPathContent { get; private set; }
        public List<string> drives { get; private set; } = new List<string>();

        public void SetDisks(List<string> drives)
        {
            this.drives = drives;

            comboBox_Drive.Items.Clear();
            comboBox_Drive.Items.AddRange(drives.ToArray());

            if (comboBox_Drive.Items.Count > 0)
                comboBox_Drive.SelectedIndex = 0; // automatycznie wybierz pierwszy dysk
        }

        public void SetDirectories(string currentPath, List<string> directories)
        {
            this.currentPath = currentPath;
            listBox_Folders.Items.Clear();

            if (Directory.GetParent(currentPath) != null)
            {
                listBox_Folders.Items.Add("..");
            }

            listBox_Folders.Items.AddRange(directories.ToArray());

            // Odświeżenie pokazanje ścieżki
            richTextBox_Path.Text = currentPath;
        }

        private void listBox_Folders_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_Folders.SelectedItem != null)
            {
                string selectedFolder = listBox_Folders.SelectedItem.ToString();
                _presenter?.OnFolderSelected(selectedFolder);
            }
        }


        private void comboBox_Drive_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Poinformuj prezentera o zmianie ścieżki
            _presenter?.OnDriveChanged(comboBox_Drive.SelectedItem?.ToString());
        }

        public string GetSelectedFile()
        {
            if (listBox_Folders.SelectedItem != null)
            {
                string selectedItem = listBox_Folders.SelectedItem.ToString();

                // Sprawdzenie czy to jest plik, a nie ścieżka
                if (File.Exists(selectedItem))
                {
                    return selectedItem;
                }
            }
            return null; 
        }

        public void RefreshFiles()
        {
            if (!string.IsNullOrEmpty(currentPath))
            {
                SetDirectories(currentPath);
            }
        }

    }
}
