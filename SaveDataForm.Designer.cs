namespace Game2
{
    partial class SaveDataForm
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
            SaveLabel = new Label();
            FileCombo = new ComboBox();
            Load = new Button();
            Save = new Button();
            Close = new Button();
            SuspendLayout();
            // 
            // SaveLabel
            // 
            SaveLabel.AutoSize = true;
            SaveLabel.Location = new Point(308, 62);
            SaveLabel.MaximumSize = new Size(200, 200);
            SaveLabel.Name = "SaveLabel";
            SaveLabel.Size = new Size(198, 75);
            SaveLabel.TabIndex = 6;
            SaveLabel.Text = "Max save files reached! Please select a save file to overwrite.";
            // 
            // FileCombo
            // 
            FileCombo.FormattingEnabled = true;
            FileCombo.Location = new Point(38, 204);
            FileCombo.Name = "FileCombo";
            FileCombo.Size = new Size(719, 33);
            FileCombo.TabIndex = 7;
            // 
            // Load
            // 
            Load.Location = new Point(524, 304);
            Load.Name = "Load";
            Load.Size = new Size(112, 34);
            Load.TabIndex = 8;
            Load.Text = "Load";
            Load.UseVisualStyleBackColor = true;
            Load.Click += Load_Click;
            // 
            // Save
            // 
            Save.Location = new Point(183, 304);
            Save.Name = "Save";
            Save.Size = new Size(112, 34);
            Save.TabIndex = 9;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // Close
            // 
            Close.Location = new Point(354, 389);
            Close.Name = "Close";
            Close.Size = new Size(112, 34);
            Close.TabIndex = 10;
            Close.Text = "Close";
            Close.UseVisualStyleBackColor = true;
            Close.Click += Close_Click;
            // 
            // SaveDataForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Close);
            Controls.Add(Save);
            Controls.Add(Load);
            Controls.Add(FileCombo);
            Controls.Add(SaveLabel);
            MaximumSize = new Size(822, 506);
            MinimumSize = new Size(822, 506);
            Name = "SaveDataForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SaveDataForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label SaveLabel;
        private ComboBox FileCombo;
        private Button Load;
        private Button Save;
        private Button Close;
    }
}