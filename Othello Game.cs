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
            restoreGameToolStripMenuItem.Visible = savegame.restoreshow;
        }
        public string P1;
        public string P2;

        int P1Points;
        int P2Points;

        int previousplayer = Convert.ToInt32(TileState.White); // Black starts 

        int gameboardRows = 8; 
        int gameboardCols = 8; 

        int gameboardstartY = 36; //Y coordinate of first Tile
        int gameboardstartX = 12; //X coordinate of first Tile

        PictureBox[,] gameboardPictures = new PictureBox[8, 8]; 

        int gameboardPosincrease = 102; // Increase in position for both X and Y for subsequent tiles

        int[,] gameboardTiles = new int[8, 8]; 

        int tilePosX;
        int tilePosY;
        /// <summary>
        /// Hides or shows the Information panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Create a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameboardPictures[0, 0] != null)
            {
                ResetGame();
                P1toplay.Visible = true;
                P2toplay.Visible = false;
            }
            else
            {
                DefaultName();

                BlackName.Enabled = false;
                WhiteName.Enabled = false;

                BlackCounter.Text = Convert.ToString(2);
                WhiteCounter.Text = Convert.ToString(2);

                P1 = BlackName.Text;
                P2 = WhiteName.Text;

                P1toplay.Visible = true;
                exitToolStripMenuItem.Visible = true;
                saveGameToolStripMenuItem.Visible = true;

                GameBoardTileSetUp();
            }
        }
        /// <summary>
        /// Opens the about text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }
        /// <summary>
        /// Asks to save the current game and then closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGameShout();
            this.Close();
        }
        /// <summary>
        /// Saves the curent game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGameShout();
        }
        /// <summary>
        /// Restores a previously saved game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restoreGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savegame.LoadGame();

            if (savegame.closed == true) { }
            else
            {
                if (gameboardPictures[0, 0] == null)
                {
                    newGameToolStripMenuItem_Click(sender, e);
                }

                P1 = savegame.Player1;
                P2 = savegame.Player2;
                previousplayer = savegame.PrevPlayer;

                for (int i = 0; i < gameboardRows; i++)
                {
                    for (int j = 0; j < gameboardCols; j++)
                    {
                        gameboardTiles[i, j] = savegame.LoadedTiles[i, j];
                        gameboardPictures[i, j].Image = Image.FromFile(gameboardTiles[i, j] + ".png");
                    }
                }
                PlayerTurnIcon();

                GameEngine ge = new GameEngine(gameboardTiles, gameboardPictures);
                ge.TileCount();
                P1Points = ge.P1Tile;
                P2Points = ge.P2Tile;
                BlackCounter.Text = Convert.ToString(P1Points);
                WhiteCounter.Text = Convert.ToString(P2Points);

                ge.ValidMovePossible(previousplayer);
            }
        }
        /// <summary>
        /// Sets up the Tile 2D array at game start
        /// </summary>
        internal void GameBoardTileSetUp() 
        {
            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    gameboardTiles[i, j] = Convert.ToInt32(TileState.Empty);
                }
            }
            gameboardTiles[3, 4] = Convert.ToInt32(TileState.Black);
            gameboardTiles[4, 3] = Convert.ToInt32(TileState.Black);
            gameboardTiles[3, 3] = Convert.ToInt32(TileState.White);
            gameboardTiles[4, 4] = Convert.ToInt32(TileState.White);

            GameboardPicSetUp();
        }
        /// <summary>
        /// Creates new picture boxes within the picture box 2D array
        /// </summary>
        internal void GameboardPicSetUp()
        {
            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    gameboardPictures[i, j] = new PictureBox();
                }
            }
            GameBoardTilePlacer();
        }
        /// <summary>
        /// Sets up the gameboard with picture box properties, images are detemined by the Tile 2D array
        /// </summary>
        internal void GameBoardTilePlacer() 
        {
            GameEngine ge = new GameEngine(gameboardTiles, gameboardPictures);

            for (int i = 0; i < gameboardRows; i++) 
            {
                for (int j = 0; j < gameboardCols; j++) 
                {
                    tilePosY = gameboardstartY + (gameboardPosincrease * (i)); // Current tile position X equals the first tile start position plus the increase based on number of rows and columns left
                    tilePosX = gameboardstartX + (gameboardPosincrease * (j));

                    gameboardPictures[i, j].Name = $"picturebox" + i + "," + j; 
                    gameboardPictures[i, j].Size = new Size(100, 100); 
                    gameboardPictures[i, j].Location = new Point(tilePosX, tilePosY); 
                    gameboardPictures[i, j].SizeMode = PictureBoxSizeMode.Zoom; 
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
        /// <summary>
        /// Activates when a picture box is clicked, will check the placement of the tile for a valid move, then will change the tiles on the board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileClickListener(object sender, EventArgs e) 
        {
            PlayerTurn pt = new PlayerTurn();
            GameEngine ge = new GameEngine(gameboardTiles, gameboardPictures);

            pt.playerturn(previousplayer);

            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    if (sender == gameboardPictures[i, j])
                    {
                        if (gameboardTiles[i, j] != Convert.ToInt32(TileState.Empty))
                        {
                            MessageBox.Show("You cannot play here as there is already another tile in this location.","Invalid Move!");
                            return;
                        }
                        else
                        {
                            ge.CheckSurroundings(i, j, previousplayer);

                            gameboardPictures = ge.gameboardPictures;
                            gameboardTiles = ge.gameboardTiles;

                            if (ge.validmove == false) { }
                            else
                            {
                                gameboardTiles[i, j] = pt.currentplayer;
                                gameboardPictures[i, j].Image = Image.FromFile(pt.currentplayer + ".png");

                                ge.TileCount();
                                P1Points = ge.P1Tile;
                                P2Points = ge.P2Tile;
                                BlackCounter.Text = Convert.ToString(P1Points);
                                WhiteCounter.Text = Convert.ToString(P2Points);

                                if (ge.endgame == true)
                                {
                                    WinAnnounce();
                                }

                                previousplayer = pt.currentplayer; //take current player as previous player

                                ge.ValidMovePossible(previousplayer);
                                if (ge.validmovepossible != true) 
                                {
                                    NoMoveShout();

                                    pt.playerturn(previousplayer);
                                    previousplayer = pt.currentplayer;

                                    ge.ValidMovePossible(previousplayer);
                                    if (ge.validmovepossible != true)
                                    {
                                        NoMoveShout();

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
        /// <summary>
        /// Shows the current player move image, hides the previous player move image
        /// </summary>
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
        /// <summary>
        /// Lets the users know the result of the game, initiates the replay method
        /// </summary>
        private void WinAnnounce()
        {
            if (P1Points == P2Points)
            {
                MessageBox.Show("The game ends in a draw!!");
            }
            else if (P1Points > P2Points)
            {
                MessageBox.Show(BlackName.Text + " wins the game!!");
            }
            else
            {
                MessageBox.Show(WhiteName.Text + " wins the game!!");
            }
            Replay();
        }
        /// <summary>
        /// Asks the user if they want to play again, will reset the game if yes, will close the game if no
        /// </summary>
        private void Replay()
        {
            DialogResult replaygame = MessageBox.Show("Do you wish to play again?", "New Game?", MessageBoxButtons.YesNo);
            if (replaygame == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
            else 
            { 
                this.Close();
            }
        }
        /// <summary>
        /// Will speak the players move
        /// </summary>
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
        /// <summary>
        /// Asks to save game, then resets the gameboard to the default state
        /// </summary>
        private void ResetGame()
        {
            GameEngine ge = new GameEngine(gameboardTiles, gameboardPictures);

            SaveGameShout();

            DialogResult renameplayers = MessageBox.Show("Do you wish to rename the players in this game?", "Rename players?", MessageBoxButtons.YesNo);
            if (renameplayers == DialogResult.Yes)
            {
                BlackName.Text = Interaction.InputBox("What would you like to name the 1st player?", "Name 1st Player");
                WhiteName.Text = Interaction.InputBox("What would you like to name the 2nd player?", "Name 2nd Player");
            }
            DefaultName();

            ge.ResetBoard();
            gameboardPictures = ge.gameboardPictures;
            gameboardTiles = ge.gameboardTiles;

            previousplayer = Convert.ToInt32(TileState.White);

            ge.ValidMovePossible(previousplayer);

            BlackCounter.Text = Convert.ToString(2);
            WhiteCounter.Text = Convert.ToString(2);
        }

        /// <summary>
        /// Saves the current settings when form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            savegame.SavePreset(speakToolStripMenuItem.Checked, InformationPanelMenuItem.Checked);
        }

        /// <summary>
        /// Tells the users if there are no moves available for the current player
        /// </summary>
        private void NoMoveShout()
        {
            if (previousplayer == Convert.ToInt32(TileState.Black))
            {
                MessageBox.Show("There are no available moves for " + WhiteName.Text);
            }
            else
            {
                MessageBox.Show("There are no available moves for " + BlackName.Text);
            }
        }

        /// <summary>
        /// Asks the user if they want to save their game
        /// </summary>
        private void SaveGameShout()
        {
            DialogResult savecurrentgame = MessageBox.Show("Do you wish to save this game?", "Save Game?", MessageBoxButtons.YesNo);
            if (savecurrentgame == DialogResult.Yes)
            {
                savegame.savegame(P1, P2, gameboardTiles, previousplayer);
                restoreGameToolStripMenuItem.Visible = savegame.restoreshow;
            }
        }

        /// <summary>
        /// Sets the default names if none are entered
        /// </summary>
        private void DefaultName()
        {
            if (BlackName.Text == "")
            {
                BlackName.Text = "Player 1";
            }
            if (WhiteName.Text == "")
            {
                WhiteName.Text = "Player 2";
            }
        }
    }
}