using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;

namespace Game2
{
    public partial class SaveDataForm : Form
    {
        List<string> savetitles = new List<string>();

        public int LoadFile;
        public bool userexit;
        public DialogResult namedsave;
        public string SaveTitle;

        public int SaveFileSelect;
        public SaveDataForm()
        {
            InitializeComponent();

            FileCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ShowInTaskbar = false;
            this.ControlBox = false;
            this.Text = null;
        }
        /// <summary>
        /// Determines whether the user wants to save or load a game, then shows the form if save are full or there is more than 1 file to load
        /// </summary>
        /// <param name="savesfull">Tells whether there are 5 saved games</param>
        /// <param name="function">Tells whether the user wishes to Save or Load a game</param>
        internal void ShowForm(bool savesfull, int function)
        {
            SaveText.Visible = false;
            userexit = false;
            SaveTitle = null;

            if (function == 1)
            {
                Save.Visible = false;
                Load.Visible = true;

                if (savetitles.Count == 1) { LoadFile = 0; }
                else
                {
                    LoadItems();
                    this.ShowDialog();
                }
            }
            else
            {
                Save.Visible = true;
                Load.Visible = false;

                if (savesfull == true)
                {
                    SaveText.Visible = true;
                    LoadItems();
                    this.ShowDialog();
                }
                else
                {
                    SaveName();
                    if (userexit == true) { } 
                    else 
                    { 
                    savetitles.Add(SaveTitle);
                    }
                }
            }
        }
        /// <summary>
        /// Loads the saved game titles into a combo box, for the user to select from
        /// </summary>
        internal void LoadItems()
        {
            FileCombo.Items.Clear();

            for (int i = 0; i < savetitles.Count; i++)
            {
                FileCombo.Items.Add(savetitles[i]);
            }
        }
        /// <summary>
        /// Saves the saved game titles to a list
        /// </summary>
        /// <param name="title">The title of the saved game</param>
        internal void SaveNames(string title)
        {
            savetitles.Add(title);
        }
        /// <summary>
        /// Determines what game has been selected to load, when load button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, EventArgs e)
        {
            if (FileCombo.SelectedItem == null) { }
            else
            {
                LoadFile = FileCombo.SelectedIndex;
                this.Close();
            }
        }
        /// <summary>
        /// Determines what saved game file to overwrite when save button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            if (FileCombo.SelectedItem == null) { }
            else
            {
                SaveFileSelect = FileCombo.SelectedIndex;

                SaveName();

                savetitles.RemoveAt(SaveFileSelect);
                savetitles.Insert(SaveFileSelect, SaveTitle);

                this.Close();
            }
        }
        /// <summary>
        /// Asks the user if they want to name their save, if no default to the current date and time
        /// </summary>
        private void SaveName()
        {
            namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
            if (namedsave == DialogResult.Yes)
            {
                WhatName();
            }
            else
            {
                SaveTitle = DateTime.Now.ToString();
            }
        }
        /// <summary>
        /// Asks the user what they would like to name their save, if no name is entered cancel the save
        /// </summary>
        private void WhatName()
        {
            SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
            if (SaveTitle == String.Empty)
            {
                userexit = true;
            }
        }
        /// <summary>
        /// Closes the form when the user clicks close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, EventArgs e)
        {
            userexit = true;
            this.Close();
        }
    }
}
