using Microsoft.VisualBasic;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Speech.Synthesis;

namespace Game2
{
    public partial class Form1 : Form
    {
        public SaveGame savegame = new SaveGame();

        public Form1()
        {
            InitializeComponent();

            savegame.PreLoad();

            speakToolStripMenuItem.Checked = savegame.speechstart;
            InformationPanelMenuItem.Checked = savegame.infopanlestart;
            InfoPanel.Visible = savegame.infopanlestart;
        }

        public string P1;
        public string P2;

        int P1Points;
        int P2Points;

        int previousplayer = Convert.ToInt32(TileState.White); // Black starts 

        int gameboardRows = 8; //rows in gameboard
        int gameboardCols = 8; // columns in gameboard

        int gameboardstartY = 36; //Y coordinate of first Tile
        int gameboardstartX = 12; //X coordinate of first Tile

        PictureBox[,] gameboardPictures = new PictureBox[8, 8]; //set up array of picture boxes

        int gameboardPosincrease = 102; // Increase in position for both X and Y for subsequent tiles

        int[,] gameboardTiles = new int[8, 8]; // set up array of integers

        int tilePosX;
        int tilePosY;
        internal void GameBoardTileSetUp() //set up the Picture array and value array
        {
            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    gameboardTiles[i, j] = Convert.ToInt32(TileState.Empty); // assign picture value to Tile state array
                }
            }
            gameboardTiles[3, 4] = Convert.ToInt32(TileState.Black);
            gameboardTiles[4, 3] = Convert.ToInt32(TileState.Black);
            gameboardTiles[3, 3] = Convert.ToInt32(TileState.White);
            gameboardTiles[4, 4] = Convert.ToInt32(TileState.White);

            GameboardPicSetUp();
        }
        internal void GameboardPicSetUp()
        {
            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    gameboardPictures[i, j] = new PictureBox(); // assign picture value to Tile state array
                }
            }
            GameBoardTilePlacer();
        }
        internal void GameBoardTilePlacer() //Places the initial tiles
        {
            GameEngine ge = new GameEngine(gameboardTiles, gameboardPictures);

            for (int i = 0; i < gameboardRows; i++) //run through the rows
            {
                for (int j = 0; j < gameboardCols; j++) //run through the columns
                {
                    tilePosY = gameboardstartY + (gameboardPosincrease * (i)); // Current tile position X equals the first tile start position plus the increase based on number of rows and columns left
                    tilePosX = gameboardstartX + (gameboardPosincrease * (j));

                    gameboardPictures[i, j].Name = $"picturebox" + i + "," + j; //assign name to specified picturebox
                    gameboardPictures[i, j].Size = new Size(100, 100); // assign size to specified picturebox
                    gameboardPictures[i, j].Location = new Point(tilePosX, tilePosY); //assign location to speciifed picturebox
                    gameboardPictures[i, j].SizeMode = PictureBoxSizeMode.Zoom; // assign zoom mode to picture box
                    gameboardPictures[i, j].Click += new EventHandler(TileClickListener);
                    switch (gameboardTiles[i, j]) // decide what tile to place with switch 
                    {
                        case 0: //black
                            {

                                gameboardPictures[i, j].Image = Image.FromFile(Convert.ToInt32(TileState.Black) + ".png");
                                Controls.Add(gameboardPictures[i, j]);
                                break;
                            }
                        case 1: // white
                            {
                                gameboardPictures[i, j].Image = Image.FromFile(Convert.ToInt32(TileState.White) + ".png");
                                Controls.Add(gameboardPictures[i, j]);
                                break;
                            }
                        case 10: //boardpiece
                            {
                                gameboardPictures[i, j].Image = Image.FromFile(Convert.ToInt32(TileState.Empty) + ".png");
                                Controls.Add(gameboardPictures[i, j]);
                                break;
                            }
                    }
                }
            }
            ge.ValidMovePossible(previousplayer);
        }
        private void TileClickListener(object sender, EventArgs e) //whenever a picuture box is clicked, run this event
        {

            PlayerTurn pt = new PlayerTurn(); //new player turn instance
            GameEngine ge = new GameEngine(gameboardTiles, gameboardPictures);

            pt.playerturn(previousplayer); //send the previous players turn to playerturn method

            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    if (sender == gameboardPictures[i, j]) //once found the correct matching picture box in the picturebox array
                    {
                        if (gameboardTiles[i, j] != Convert.ToInt32(TileState.Empty)) //if it is not an empty space
                        {
                            MessageBox.Show("You cannot play here as there is already another tile in this location.");
                            return;
                        }
                        else
                        {
                            ge.CheckSurroundings(i, j, previousplayer); //send the picturebox information to the check surrounding method

                            gameboardPictures = ge.gameboardPictures;
                            gameboardTiles = ge.gameboardTiles;

                            if (ge.validmove == false) { } //if checksurroundings find no valid move do nothing
                            else
                            {
                                gameboardTiles[i, j] = pt.currentplayer;// change tiles to current player colour
                                gameboardPictures[i, j].Image = Image.FromFile(pt.currentplayer + ".png"); // change to variable image based off of player

                                ge.TileCount();
                                P1Points = ge.P1Tile;
                                P2Points = ge.P2Tile;
                                BlackCounter.Text = Convert.ToString(P1Points);
                                WhiteCounter.Text = Convert.ToString(P2Points);

                                if (ge.endgame == true)
                                {
                                    WinAnnounce();
                                }

                                previousplayer = pt.currentplayer; //turn current player into previous player

                                ge.ValidMovePossible(previousplayer);
                                if (ge.validmovepossible != true)
                                {
                                    if (ge.nextplayer == Convert.ToInt32(TileState.Black))
                                    {
                                        MessageBox.Show("There are no available moves for " + BlackName.Text + ". Switching to " + WhiteName.Text);
                                    }
                                    else
                                    {
                                        MessageBox.Show("There are no available moves for " + WhiteName.Text + ". Switching to " + BlackName.Text);
                                    }
                                }

                                if (ge.validmovepossible == false)
                                {
                                    pt.playerturn(previousplayer);
                                    previousplayer = pt.currentplayer;

                                    ge.ValidMovePossible(previousplayer);
                                    if (ge.validmovepossible != true)
                                    {
                                        if (ge.nextplayer == Convert.ToInt32(TileState.Black))
                                        {
                                            MessageBox.Show("There are no available moves for " + BlackName.Text);
                                        }
                                        else
                                        {
                                            MessageBox.Show("There are no available moves for " + WhiteName.Text);
                                        }
                                    }

                                    if (ge.validmovepossible == false && ge.endgame == false)
                                    {
                                        WinAnnounce();
                                    }
                                }
                            }
                            PlayerTurnIcon();
                            MoveSpeech();
                            break;
                        }
                    }
                }
            }

        }
        private void PlayerTurnIcon()
        {
            if (previousplayer == Convert.ToInt32(TileState.Black))
            {
                P1toplay.Visible = false;
                P2toplay.Visible = true;
            }
            else
            {
                P1toplay.Visible = true;
                P2toplay.Visible = false;
            }
        }
        private void WinAnnounce()
        {
            if (P1Points == P2Points)
            {
                MessageBox.Show("The game ends in a draw!!");
                this.Close();
            }
            else if (P1Points > P2Points)
            {
                MessageBox.Show(BlackName.Text + " wins the game!!");
                this.Close();
            }
            else
            {
                MessageBox.Show(WhiteName.Text + " wins the game!!");
                this.Close();
            }
        }
        private void MoveSpeech()
        {
            var synthesizer = new SpeechSynthesizer();

            if (previousplayer == Convert.ToInt32(TileState.White) && speakToolStripMenuItem.Checked == true)
            {
                synthesizer.SpeakAsync("It is " + BlackName.Text + "'s turn.");
            }
            else if (previousplayer == Convert.ToInt32(TileState.Black) && speakToolStripMenuItem.Checked == true)
            {
                synthesizer.SpeakAsync("It is " + WhiteName.Text + "'s turn.");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void InformationPanelMenuItem_Click(object sender, EventArgs e)
        {
            if (InformationPanelMenuItem.Checked == true)
            {
                InfoPanel.Visible = true;
            }
            else
            {
                InfoPanel.Visible = false;
            }
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameboardPictures[0, 0] != null)
            {
                ResetGame();
            }
            else
            {
                if (BlackName.Text == "")
                {
                    BlackName.Text = "Player 1";
                }
                if (WhiteName.Text == "")
                {
                    WhiteName.Text = "Player 2";
                }
                BlackName.Enabled = false;
                WhiteName.Enabled = false;

                BlackCounter.Text = Convert.ToString(2);
                WhiteCounter.Text = Convert.ToString(2);

                P1 = BlackName.Text;
                P2 = WhiteName.Text;

                P1toplay.Visible = true;
                exitToolStripMenuItem.Visible = true;
                saveGameToolStripMenuItem.Visible = true;
                restoreGameToolStripMenuItem.Visible = true;

                GameBoardTileSetUp();
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult savecurrentgame = MessageBox.Show("Do you wish to save this game?", "Save Game?", MessageBoxButtons.YesNo);
            if (savecurrentgame == DialogResult.Yes)
            {
                savegame.savegame(P1, P2, gameboardTiles, previousplayer);
            }

            this.Close();
        }
        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult savecurrentgame = MessageBox.Show("Do you wish to save this game?", "Save Game?", MessageBoxButtons.YesNo);
            if (savecurrentgame == DialogResult.Yes)
            {
                savegame.savegame(P1, P2, gameboardTiles, previousplayer);
            }
        }
        private void restoreGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savegame.LoadGame();

            P1 = savegame.Player1;
            P2 = savegame.Player2;
            previousplayer = savegame.PrevPlayer;

            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    gameboardTiles[i, j] = savegame.LoadedTiles[i, j];
                    gameboardPictures[i,j].Image = Image.FromFile(gameboardTiles[i,j]+".png");
                }
            }
            GameEngine ge = new GameEngine(gameboardTiles,gameboardPictures);
            ge.ValidMovePossible(previousplayer);
        }
        private void ResetGame()
        {
            GameEngine ge = new GameEngine(gameboardTiles, gameboardPictures);

            DialogResult savecurrentgame = MessageBox.Show("Do you wish to save this game?", "Save Game?", MessageBoxButtons.YesNo);
            if (savecurrentgame == DialogResult.Yes)
            {
                savegame.savegame(P1, P2, gameboardTiles, previousplayer);
            }

            DialogResult renameplayers = MessageBox.Show("Do you wish to rename the players in this game?", "Rename players?", MessageBoxButtons.YesNo);
            if (renameplayers == DialogResult.Yes)
            {
                BlackName.Text = Interaction.InputBox("What would you like to name the 1st player?", "Name 1st Player");
                WhiteName.Text = Interaction.InputBox("What would you like to name the 2nd player?", "Name 2nd Player");
            }

            ge.ResetBoard();
            gameboardPictures = ge.gameboardPictures;
            gameboardTiles = ge.gameboardTiles;

            previousplayer = Convert.ToInt32(TileState.White);

            ge.ValidMovePossible(previousplayer);

            BlackCounter.Text = Convert.ToString(2);
            WhiteCounter.Text = Convert.ToString(2);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            savegame.SavePreset(speakToolStripMenuItem.Checked, InformationPanelMenuItem.Checked);
        }
    }
}