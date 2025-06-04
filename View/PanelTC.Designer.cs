namespace MiniTC.View
{
    partial class PanelTC
    {
        /// <summary> 
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox_Drive = new ComboBox();
            richTextBox_Path = new RichTextBox();
            listBox_Folders = new ListBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // comboBox_Drive
            // 
            comboBox_Drive.FormattingEnabled = true;
            comboBox_Drive.Location = new Point(272, 85);
            comboBox_Drive.Name = "comboBox_Drive";
            comboBox_Drive.Size = new Size(121, 23);
            comboBox_Drive.TabIndex = 0;
            comboBox_Drive.SelectedIndexChanged += comboBox_Drive_SelectedIndexChanged;
            // 
            // richTextBox_Path
            // 
            richTextBox_Path.Location = new Point(170, 37);
            richTextBox_Path.Name = "richTextBox_Path";
            richTextBox_Path.Size = new Size(223, 25);
            richTextBox_Path.TabIndex = 1;
            richTextBox_Path.Text = "";
            // 
            // listBox_Folders
            // 
            listBox_Folders.FormattingEnabled = true;
            listBox_Folders.ItemHeight = 15;
            listBox_Folders.Location = new Point(72, 135);
            listBox_Folders.Name = "listBox_Folders";
            listBox_Folders.Size = new Size(321, 454);
            listBox_Folders.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(119, 40);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 3;
            label1.Text = "Ścieżka";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(228, 88);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 4;
            label2.Text = "Dysk";
            // 
            // PanelTC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBox_Folders);
            Controls.Add(richTextBox_Path);
            Controls.Add(comboBox_Drive);
            Name = "PanelTC";
            Size = new Size(480, 616);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox_Drive;
        private RichTextBox richTextBox_Path;
        public ListBox listBox_Folders;
        private Label label1;
        private Label label2;
    }
}
