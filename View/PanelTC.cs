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
        private LeftTCPresenter _leftPresenter;

        public PanelTC()
        {
            InitializeComponent();
            comboBox_Drive.DropDown += ComboBox_Drive_DropDown;
            comboBox_Drive.SelectedIndexChanged += comboBox_Drive_SelectedIndexChanged;
            listBox_Folders.DoubleClick += listBox_Folders_DoubleClick;
        }

        public void SetPresenter(LeftTCPresenter presenter)
        {
            _leftPresenter = presenter;
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

            // Dodaj opcję ".." jeśli nie jesteśmy w katalogu głównym
            if (!IsRootDirectory(currentPath))
            {
                listBox_Folders.Items.Add("..");
            }

            // Rozdziel katalogi i pliki
            var directories = contents.Where(item => Directory.Exists(item)).ToList();
            var files = contents.Where(item => File.Exists(item)).ToList();

            // Dodaj prefiks <D> do katalogów
            foreach (string directory in directories)
            {
                string displayName = $"<D> {Path.GetFileName(directory)}";
                listBox_Folders.Items.Add(displayName);
            }

            // Dodaj pliki po katalogach
            foreach (string file in files)
            {
                string displayName = Path.GetFileName(file);
                listBox_Folders.Items.Add(displayName);
            }

            // Zaktualizuj wyświetlanie ścieżki
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
            // Odśwież dostępne dyski gdy lista rozwijana jest otwierana
            _leftPresenter?.ShowDisks();
        }

        private void comboBox_Drive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDrive = comboBox_Drive.SelectedItem?.ToString();
            _leftPresenter?.OnDriveChanged(selectedDrive);
        }

        private void listBox_Folders_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_Folders.SelectedItem != null)
            {
                string selectedItem = listBox_Folders.SelectedItem.ToString();

                if (selectedItem == "..")
                {
                    _leftPresenter?.OnFolderSelected("..");
                    return;
                }

                // Usuń prefiks <D> i pobierz rzeczywistą ścieżkę
                string actualName;
                if (selectedItem.StartsWith("<D> "))
                {
                    actualName = selectedItem.Substring(4); // Usuń "<D> "
                }
                else
                {
                    actualName = selectedItem;
                }

                // Znajdź pełną ścieżkę z currentPathContent
                string fullPath = currentPathContent?.FirstOrDefault(path =>
                    Path.GetFileName(path) == actualName);

                if (!string.IsNullOrEmpty(fullPath))
                {
                    _leftPresenter?.OnFolderSelected(fullPath);
                }
            }
        }

        public string GetSelectedFile()
        {
            if (listBox_Folders.SelectedItem != null)
            {
                string selectedItem = listBox_Folders.SelectedItem.ToString();

                // Pomiń katalog nadrzędny i katalogi
                if (selectedItem == ".." || selectedItem.StartsWith("<D> "))
                {
                    return null;
                }

                // Pobierz rzeczywistą nazwę pliku
                string fileName = selectedItem;

                // Znajdź pełną ścieżkę z currentPathContent
                string fullPath = currentPathContent?.FirstOrDefault(path =>
                    Path.GetFileName(path) == fileName);

                // Sprawdź czy to jest plik
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
                _leftPresenter?.ShowDirectory(currentPath);
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