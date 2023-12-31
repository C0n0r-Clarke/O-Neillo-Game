﻿namespace Game2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            startGameToolStripMenuItem = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            saveGameToolStripMenuItem = new ToolStripMenuItem();
            restoreGameToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            InformationPanelMenuItem = new ToolStripMenuItem();
            speakToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            BlackName = new TextBox();
            WhiteName = new TextBox();
            InfoPanel = new Panel();
            P2toplay = new PictureBox();
            P1toplay = new PictureBox();
            WhiteCounter = new Label();
            BlackCounter = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            menuStrip1.SuspendLayout();
            InfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)P2toplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)P1toplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { startGameToolStripMenuItem, settingsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(854, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // startGameToolStripMenuItem
            // 
            startGameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, saveGameToolStripMenuItem, restoreGameToolStripMenuItem, exitToolStripMenuItem });
            startGameToolStripMenuItem.Name = "startGameToolStripMenuItem";
            startGameToolStripMenuItem.Size = new Size(74, 29);
            startGameToolStripMenuItem.Text = "Game";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.Size = new Size(270, 34);
            newGameToolStripMenuItem.Text = "New Game";
            newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            // 
            // saveGameToolStripMenuItem
            // 
            saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            saveGameToolStripMenuItem.Size = new Size(270, 34);
            saveGameToolStripMenuItem.Text = "Save Game";
            saveGameToolStripMenuItem.Visible = false;
            saveGameToolStripMenuItem.Click += saveGameToolStripMenuItem_Click;
            // 
            // restoreGameToolStripMenuItem
            // 
            restoreGameToolStripMenuItem.Name = "restoreGameToolStripMenuItem";
            restoreGameToolStripMenuItem.Size = new Size(270, 34);
            restoreGameToolStripMenuItem.Text = "Restore Game";
            restoreGameToolStripMenuItem.Visible = false;
            restoreGameToolStripMenuItem.Click += restoreGameToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(270, 34);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Visible = false;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { InformationPanelMenuItem, speakToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(92, 29);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // InformationPanelMenuItem
            // 
            InformationPanelMenuItem.Checked = true;
            InformationPanelMenuItem.CheckOnClick = true;
            InformationPanelMenuItem.CheckState = CheckState.Checked;
            InformationPanelMenuItem.Name = "InformationPanelMenuItem";
            InformationPanelMenuItem.Size = new Size(254, 34);
            InformationPanelMenuItem.Text = "Information Panel";
            InformationPanelMenuItem.Click += InformationPanelMenuItem_Click;
            // 
            // speakToolStripMenuItem
            // 
            speakToolStripMenuItem.CheckOnClick = true;
            speakToolStripMenuItem.Name = "speakToolStripMenuItem";
            speakToolStripMenuItem.Size = new Size(254, 34);
            speakToolStripMenuItem.Text = "Speak";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(65, 29);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(164, 34);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // BlackName
            // 
            BlackName.Location = new Point(205, 48);
            BlackName.Name = "BlackName";
            BlackName.Size = new Size(157, 31);
            BlackName.TabIndex = 1;
            // 
            // WhiteName
            // 
            WhiteName.Location = new Point(657, 48);
            WhiteName.Name = "WhiteName";
            WhiteName.Size = new Size(150, 31);
            WhiteName.TabIndex = 2;
            // 
            // InfoPanel
            // 
            InfoPanel.BackColor = Color.FromArgb(255, 128, 128);
            InfoPanel.Controls.Add(P2toplay);
            InfoPanel.Controls.Add(P1toplay);
            InfoPanel.Controls.Add(WhiteCounter);
            InfoPanel.Controls.Add(BlackCounter);
            InfoPanel.Controls.Add(WhiteName);
            InfoPanel.Controls.Add(pictureBox2);
            InfoPanel.Controls.Add(BlackName);
            InfoPanel.Controls.Add(pictureBox1);
            InfoPanel.Location = new Point(0, 888);
            InfoPanel.Name = "InfoPanel";
            InfoPanel.Size = new Size(874, 83);
            InfoPanel.TabIndex = 3;
            // 
            // P2toplay
            // 
            P2toplay.ErrorImage = (Image)resources.GetObject("P2toplay.ErrorImage");
            P2toplay.Image = (Image)resources.GetObject("P2toplay.Image");
            P2toplay.Location = new Point(657, 3);
            P2toplay.Name = "P2toplay";
            P2toplay.Size = new Size(157, 39);
            P2toplay.SizeMode = PictureBoxSizeMode.Zoom;
            P2toplay.TabIndex = 5;
            P2toplay.TabStop = false;
            P2toplay.Visible = false;
            // 
            // P1toplay
            // 
            P1toplay.ErrorImage = (Image)resources.GetObject("P1toplay.ErrorImage");
            P1toplay.Image = (Image)resources.GetObject("P1toplay.Image");
            P1toplay.Location = new Point(205, 3);
            P1toplay.Name = "P1toplay";
            P1toplay.Size = new Size(157, 39);
            P1toplay.SizeMode = PictureBoxSizeMode.Zoom;
            P1toplay.TabIndex = 4;
            P1toplay.TabStop = false;
            P1toplay.Visible = false;
            // 
            // WhiteCounter
            // 
            WhiteCounter.AutoSize = true;
            WhiteCounter.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            WhiteCounter.Location = new Point(459, 16);
            WhiteCounter.Name = "WhiteCounter";
            WhiteCounter.Size = new Size(45, 54);
            WhiteCounter.TabIndex = 1;
            WhiteCounter.Text = "0";
            // 
            // BlackCounter
            // 
            BlackCounter.AutoSize = true;
            BlackCounter.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            BlackCounter.Location = new Point(12, 16);
            BlackCounter.Name = "BlackCounter";
            BlackCounter.Size = new Size(45, 54);
            BlackCounter.TabIndex = 0;
            BlackCounter.Text = "0";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(525, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(151, 83);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(74, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(157, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 192);
            ClientSize = new Size(854, 970);
            Controls.Add(InfoPanel);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(880, 1030);
            MinimumSize = new Size(880, 1030);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "O'Neillo Game v1.0";
            FormClosing += Form1_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            InfoPanel.ResumeLayout(false);
            InfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)P2toplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)P1toplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem startGameToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private TextBox BlackName;
        private TextBox WhiteName;
        private Panel InfoPanel;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Label WhiteCounter;
        private Label BlackCounter;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem InformationPanelMenuItem;
        private PictureBox P2toplay;
        private PictureBox P1toplay;
        private ToolStripMenuItem speakToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem saveGameToolStripMenuItem;
        private ToolStripMenuItem restoreGameToolStripMenuItem;
    }
}