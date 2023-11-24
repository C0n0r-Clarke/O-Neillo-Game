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
            SaveFile1 = new Button();
            SaveFile2 = new Button();
            SaveFile3 = new Button();
            SaveFile4 = new Button();
            SaveFile5 = new Button();
            button1 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // SaveFile1
            // 
            SaveFile1.Location = new Point(12, 12);
            SaveFile1.Name = "SaveFile1";
            SaveFile1.Size = new Size(491, 83);
            SaveFile1.TabIndex = 0;
            SaveFile1.Text = "Save File 1";
            SaveFile1.UseVisualStyleBackColor = true;
            SaveFile1.Click += button1_Click;
            // 
            // SaveFile2
            // 
            SaveFile2.Location = new Point(12, 100);
            SaveFile2.Name = "SaveFile2";
            SaveFile2.Size = new Size(491, 83);
            SaveFile2.TabIndex = 1;
            SaveFile2.Text = "Save File 2";
            SaveFile2.UseVisualStyleBackColor = true;
            SaveFile2.Click += button2_Click;
            // 
            // SaveFile3
            // 
            SaveFile3.Location = new Point(12, 188);
            SaveFile3.Name = "SaveFile3";
            SaveFile3.Size = new Size(491, 83);
            SaveFile3.TabIndex = 2;
            SaveFile3.Text = "Save File 3";
            SaveFile3.UseVisualStyleBackColor = true;
            SaveFile3.Click += button3_Click;
            // 
            // SaveFile4
            // 
            SaveFile4.Location = new Point(12, 276);
            SaveFile4.Name = "SaveFile4";
            SaveFile4.Size = new Size(491, 83);
            SaveFile4.TabIndex = 3;
            SaveFile4.Text = "Save File 4";
            SaveFile4.UseVisualStyleBackColor = true;
            SaveFile4.Click += button4_Click;
            // 
            // SaveFile5
            // 
            SaveFile5.Location = new Point(12, 364);
            SaveFile5.Name = "SaveFile5";
            SaveFile5.Size = new Size(491, 83);
            SaveFile5.TabIndex = 4;
            SaveFile5.Text = "Save File 5";
            SaveFile5.UseVisualStyleBackColor = true;
            SaveFile5.Click += button5_Click;
            // 
            // button1
            // 
            button1.Location = new Point(615, 404);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 5;
            button1.Text = "Close";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(574, 188);
            label1.MaximumSize = new Size(200, 200);
            label1.Name = "label1";
            label1.Size = new Size(198, 75);
            label1.TabIndex = 6;
            label1.Text = "Max save files reached! Please select a save file to overwrite.";
            // 
            // SaveDataForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(SaveFile5);
            Controls.Add(SaveFile4);
            Controls.Add(SaveFile3);
            Controls.Add(SaveFile2);
            Controls.Add(SaveFile1);
            Name = "SaveDataForm";
            Text = "SaveDataForm";
            FormClosing += SaveDataForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SaveFile1;
        private Button SaveFile2;
        private Button SaveFile3;
        private Button SaveFile4;
        private Button SaveFile5;
        private Button button1;
        private Label label1;
    }
}