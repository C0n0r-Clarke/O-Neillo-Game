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
        public bool userexit = false;
        public DialogResult namedsave;
        public string P1;
        public string P2;
        public int[,] Digitarray;
        public int Prevplayer;
        public string SaveTitle;
        public SaveGame SaveGame;

        public string button;
        public int SaveFileSelect;

        public SaveDataForm()
        {
            InitializeComponent();

            FileCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ShowInTaskbar = false;
            this.ControlBox = false;
            this.Text = null;
        }
        internal void ShowForm(bool savesfull, int function)
        {
            SaveTitle = null;

            if (function == 0)
            {
                Save.Visible = true;
                Load.Visible = false;
            }
            else
            {
                Save.Visible = false;
                Load.Visible = true;
            }

            if (savesfull == true)
            {
                LoadItems();
                this.ShowDialog();
            }
            else
            {
                namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
                if (namedsave == DialogResult.Yes)
                {
                    SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
                    if(this.DialogResult == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
                else
                {
                    SaveTitle = DateTime.Now.ToString();
                }
            }
        }
        internal void LoadItems()
        {
            FileCombo.Items.Clear();

            for (int i = 0; i < savetitles.Count; i++)
            {
                FileCombo.Items.Add(savetitles[i]);
            }
        }
        internal void SaveNames(string title)
        {
            savetitles.Add(title);
        }
        private void Load_Click(object sender, EventArgs e)
        {
            if (FileCombo.SelectedItem == null) { }
            else
            {
                LoadFile = FileCombo.SelectedIndex;
                this.Close();
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            if (FileCombo.SelectedItem == null) { }
            else
            {
                SaveFileSelect = FileCombo.SelectedIndex;

                namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
                if (namedsave == DialogResult.Yes)
                {
                    SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
                }
                else
                {
                    SaveTitle = DateTime.Now.ToString();
                }

                savetitles.RemoveAt(SaveFileSelect);
                savetitles.Insert(SaveFileSelect, SaveTitle);

                this.Close();
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
