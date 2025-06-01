using MiniTC.Model;
using MiniTC.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniTC.View
{
    public partial class Form1 : Form
    {
        private PanelTC _activePanel;
        private PanelTC _passivePanel;
        public Form1()
        {
            InitializeComponent();

            var leftPresenter = new LeftTCPresenter(paneltc1);
            var rightPresenter= new LeftTCPresenter(paneltc2);

            // Set the presenters in the panels
            paneltc1.SetPresenter(leftPresenter);
            paneltc2.SetPresenter(rightPresenter);

            // Initialize the drives
            leftPresenter.ShowDisks();
            rightPresenter.ShowDisks();

            var mainPresenter = new MainPresenter(paneltc1, paneltc2);

            button_copy.Click += ButtonCopy_Click;

            paneltc1.Click += (s, e) =>
            {
                SetActivePanel(paneltc1);
                SetPassivePanel(paneltc2);
            };
            paneltc2.Click += (s, e) =>
            {
                SetActivePanel(paneltc2);
                SetPassivePanel(paneltc1);
            };

            SetActivePanel(paneltc1);
            SetPassivePanel(paneltc2);
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedFile = _activePanel.GetSelectedFile();

                if (string.IsNullOrEmpty(selectedFile))
                {
                    MessageBox.Show("Wybierz plik do skopiwoania.", "Nie wybrano pliku",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string destinationFolder = _passivePanel.currentPath;

                if (string.IsNullOrEmpty(destinationFolder))
                {
                    MessageBox.Show("Niewłaściwa ścieżka docelowa.", "Błąd",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the disk operations instance (you'll need to access this)
                var diskOps = new DiskOperations();
                diskOps.CopyFile(selectedFile, destinationFolder);

                // Odświeżenie panelu docelowego
                _passivePanel.RefreshFiles();

                MessageBox.Show("Pomyślnie skopiowano plik", "Sukces",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kopiowanie nie powiodło się: {ex.Message}", "Błąd",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetActivePanel(PanelTC panel)
        {
            _activePanel = panel;
            UpdateButtonText();
        }

        private void SetPassivePanel(PanelTC panel)
        {
            _passivePanel = panel;
        }

        private void UpdateButtonText()
        {
            if (_activePanel == paneltc1)
            {
                button_copy.Text = "Kopiuj >>";
            }
            else if (_activePanel == paneltc2)
            {
                button_copy.Text = "<< Kopiuj";
            }
            else
            {
                button_copy.Text = "Kopiuj";
            }
        }
    }
}
