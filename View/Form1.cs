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

            // Stwórz oddzielne presentery dla każdego panelu
            var leftPresenter = new TCPresenter(paneltc1);
            var rightPresenter = new TCPresenter(paneltc2);

            paneltc1.SetPresenter(leftPresenter);
            paneltc2.SetPresenter(rightPresenter);

            leftPresenter.ShowDisks();
            rightPresenter.ShowDisks();

            var mainPresenter = new MainPresenter(paneltc1, paneltc2);

            button_copy.Click += ButtonCopy_Click;

            // Obsługa kliknięć paneli - ustawianie aktywnego i pasywnego panelu
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

            // Domyślnie ustaw lewy panel jako aktywny
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
                    MessageBox.Show("Wybierz plik do skopiowania.", "Nie wybrano pliku",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string destinationFolder = _passivePanel.currentPath;

                if (string.IsNullOrEmpty(destinationFolder))
                {
                    MessageBox.Show("Nieprawidłowa ścieżka docelowa.", "Błąd",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Pobierz instancję operacji na dysku
                var diskOps = new DiskOperations();
                diskOps.CopyFile(selectedFile, destinationFolder);

                // Odśwież panel docelowy
                _passivePanel.RefreshFiles();

                MessageBox.Show("Plik został pomyślnie skopiowany.", "Sukces",
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

            // Opcjonalnie: wizualne oznaczenie aktywnego panelu
            paneltc1.BackColor = (panel == paneltc1) ? Color.LightBlue : SystemColors.Control;
            paneltc2.BackColor = (panel == paneltc2) ? Color.LightBlue : SystemColors.Control;

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