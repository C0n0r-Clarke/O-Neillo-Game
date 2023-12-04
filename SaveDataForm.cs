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
        }
        internal void ShowForm(bool savesfull)
        {
            SaveTitle = null;

            if (savesfull == true)
            {
                SaveFile1.Text = savetitles[0];
                SaveFile2.Text = savetitles[1];
                SaveFile3.Text = savetitles[2];
                SaveFile4.Text = savetitles[3];
                SaveFile5.Text = savetitles[4];

                this.ShowDialog();
            }
            else
            {
                namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
                if (namedsave == DialogResult.Yes)
                {
                    SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
                }
                else
                {
                    SaveTitle = DateTime.Now.ToString();
                }
            }
        }
        internal void LoadItems(List<SaveGame.GameStateObject> Gameloads)
        {
            for (int i = 0; i < savetitles.Count; i++)
            {
                comboBox1.Items.Add(savetitles[i]);
            }

            this.ShowDialog();
        }
        internal void SaveNames(string title)
        {
            savetitles.Add(title);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileSelect = 1;

            namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
            if (namedsave == DialogResult.Yes)
            {
                SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
            }
            else
            {
                SaveTitle = DateTime.Now.ToString();
            }
            savetitles.RemoveAt(SaveFileSelect - 1);
            savetitles.Insert(SaveFileSelect - 1, SaveTitle);

            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileSelect = 2;

            namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
            if (namedsave == DialogResult.Yes)
            {
                SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
            }
            else
            {
                SaveTitle = DateTime.Now.ToString();
            }
            savetitles.RemoveAt(SaveFileSelect - 1);
            savetitles.Insert(SaveFileSelect - 1, SaveTitle);

            this.Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileSelect = 3;

            namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
            if (namedsave == DialogResult.Yes)
            {
                SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
            }
            else
            {
                SaveTitle = DateTime.Now.ToString();
            }
            savetitles.RemoveAt(SaveFileSelect - 1);
            savetitles.Insert(SaveFileSelect - 1, SaveTitle);

            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileSelect = 4;

            namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
            if (namedsave == DialogResult.Yes)
            {
                SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
            }
            else
            {
                SaveTitle = DateTime.Now.ToString();
            }
            savetitles.RemoveAt(SaveFileSelect - 1);
            savetitles.Insert(SaveFileSelect - 1, SaveTitle);

            this.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileSelect = 5;

            namedsave = MessageBox.Show("Do you want to name your save?", "File save name", MessageBoxButtons.YesNo);
            if (namedsave == DialogResult.Yes)
            {
                SaveTitle = Interaction.InputBox("What would you like to name this save file?", "Name save file");
            }
            else
            {
                SaveTitle = DateTime.Now.ToString();
            }
            savetitles.RemoveAt(SaveFileSelect - 1);
            savetitles.Add(SaveTitle);

            this.Close();
        }
        private void SaveDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                userexit = true;
            }
        }
        private void Load_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(comboBox1.SelectedItem));
        }
    }
}
