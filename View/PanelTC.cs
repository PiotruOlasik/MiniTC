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
        }

        public void SetPresenter(LeftTCPresenter presenter)
        {
            _presenter = presenter;
          //  _presenter.LoadDisks();
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

        public void SetDirectories (string currentPath)
        {
            this.currentPath = currentPath;

            listBox_Folders.Items.Clear();
            listBox_Folders.Items.AddRange(Directory.GetDirectories(currentPath));

            
        }



        private void comboBox_Drive_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Poinformuj prezentera o zmianie ścieżki
            _presenter?.OnDriveChanged(comboBox_Drive.SelectedItem?.ToString());
        }

    }
}
