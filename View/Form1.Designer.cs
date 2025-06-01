namespace MiniTC.View
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            paneltc1 = new PanelTC();
            paneltc2 = new PanelTC();
            button_copy = new Button();
            SuspendLayout();
            // 
            // paneltc1
            // 
            paneltc1.Location = new Point(29, 12);
            paneltc1.Name = "paneltc1";
            paneltc1.Size = new Size(480, 616);
            paneltc1.TabIndex = 0;
            // 
            // paneltc2
            // 
            paneltc2.Location = new Point(504, 12);
            paneltc2.Name = "paneltc2";
            paneltc2.Size = new Size(480, 616);
            paneltc2.TabIndex = 1;
            // 
            // button_copy
            // 
            button_copy.Location = new Point(470, 646);
            button_copy.Name = "button_copy";
            button_copy.Size = new Size(75, 23);
            button_copy.TabIndex = 2;
            button_copy.Text = "button1";
            button_copy.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 699);
            Controls.Add(button_copy);
            Controls.Add(paneltc2);
            Controls.Add(paneltc1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private PanelTC paneltc1;
        private PanelTC paneltc2;
        private Button button_copy;
    }
}