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
        public DialogResult namedsave;
        public string P1;
        public string P2;
        public int[,] Digitarray;
        public int Prevplayer;
        public string SaveTitle;
        public SaveGame SaveGame;

        public SaveDataForm(string p1, string p2, int[,] digitarray, int prevplayer, SaveGame saveGame)
        {
            InitializeComponent();

            P1 = p1;
            P2 = p2;
            Digitarray = digitarray;
            Prevplayer = prevplayer;
            SaveGame = saveGame;

        }

        public int SaveFileSelect;

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

            label1.Text = SaveTitle;

            SaveGame.savegame(P1, P2, Digitarray, Prevplayer, SaveFileSelect);

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

            label2.Text = SaveTitle;

            SaveGame.savegame(P1, P2, Digitarray, Prevplayer, SaveFileSelect);

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

            label3.Text = SaveTitle;

            SaveGame.savegame(P1, P2, Digitarray, Prevplayer, SaveFileSelect);

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

            label4.Text = SaveTitle;

            SaveGame.savegame(P1, P2, Digitarray, Prevplayer, SaveFileSelect);

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

            label5.Text = SaveTitle;

            SaveGame.savegame(P1, P2, Digitarray, Prevplayer, SaveFileSelect);

            this.Close();
        }
    }
}
