using System;
using System.Security.Cryptography.X509Certificates;
using System.Speech.Synthesis;

namespace Game2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            savegame.preload();
        }

        public GameEngine gameengine = new GameEngine();

        public string P1;
        public string P2;

        public SaveGame savegame = new SaveGame();

        public bool endgame = false;
        public bool validmove;
        public bool validmovepossible;
        public bool speechenabled = false;

        int previousplayer = 1; // Black starts 

        int gameboardRows = 8; //rows in gameboard
        int gameboardCols = 8; // columns in gameboard

        int gameboardstartY = 36; //Y coordinate of first Tile
        int gameboardstartX = 12; //X coordinate of first Tile

        int gameboardPosincrease = 102; // Increase in position for both X and Y for subsequent tiles
        int tilePosX; // where to place the current tile in X
        int tilePosY; // Where to place the current tile in Y

        PictureBox[,] gameboardPictures = new PictureBox[8, 8]; //set up array of picture boxes

        int[,] gameboardTiles = new int[8, 8]; // set up array of integers

        internal void GameBoardTileSetUp() //set up the Picture array and value array
        {
            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    gameboardPictures[i, j] = new PictureBox(); //create a new picture box at position in picturebox array        
                    gameboardTiles[i, j] = Convert.ToInt32(TileState.Empty); // assign picture value to Tile state array
                }
            }

            gameboardTiles[3, 4] = 0;
            gameboardTiles[4, 3] = 0;
            gameboardTiles[3, 3] = 1;
            gameboardTiles[4, 4] = 1;

            GameBoardTilePlacer(); // call the tile placer method
        }
        internal void GameBoardTilePlacer() //Places the initial tiles
        {
            for (int i = 0; i < gameboardRows; i++) //run through the rows
            {
                for (int j = 0; j < gameboardCols; j++) //run through the columns
                {
                    tilePosY = gameboardstartY + (gameboardPosincrease * (i)); // Current tile position X equals the first tile start position plus the increase based on number of rows and columns left
                    tilePosX = gameboardstartX + (gameboardPosincrease * (j));

                    gameboardPictures[i, j].Name = $"picturebox" + tilePosX + "," + tilePosY; //assign name to specified picturebox
                    gameboardPictures[i, j].Size = new Size(100, 100); // assign size to specified picturebox
                    gameboardPictures[i, j].Location = new Point(tilePosX, tilePosY); //assign location to speciifed picturebox
                    gameboardPictures[i, j].SizeMode = PictureBoxSizeMode.Zoom; // assign zoom mode to picture box
                    gameboardPictures[i, j].Click += new EventHandler(TileClickListener);
                    switch (gameboardTiles[i, j]) // decide what tile to place with switch 
                    {
                        case 0: //black
                            {

                                gameboardPictures[i, j].Image = Image.FromFile("0.png");
                                Controls.Add(gameboardPictures[i, j]);
                                break;
                            }
                        case 1: // white
                            {
                                gameboardPictures[i, j].Image = Image.FromFile("1.png");
                                Controls.Add(gameboardPictures[i, j]);
                                break;
                            }
                        case 10: //boardpiece
                            {
                                gameboardPictures[i, j].Image = Image.FromFile("10.png");
                                Controls.Add(gameboardPictures[i, j]);
                                break;
                            }
                    }
                }
            }
            ValidMovePossible(previousplayer);
        }

        private void TileClickListener(object sender, EventArgs e) //whenever a picuture box is clicked, run this event
        {

            PlayerTurn pt = new PlayerTurn(); //new player turn instance
            var synthesizer = new SpeechSynthesizer();

            pt.playerturn(previousplayer); //send the previous players turn to playerturn method

            for (int i = 0; i < gameboardRows; i++)
            {
                for (int j = 0; j < gameboardCols; j++)
                {
                    if (sender == gameboardPictures[i, j]) //once found the correct matching picture box in the picturebox array
                    {
                        if (gameboardTiles[i, j] != 10) //if it is not an empty space
                        {
                            MessageBox.Show("You cannot play here as there is already another tile in this location.");
                            return;
                        }
                        else
                        {
                            CheckSurroundings(i, j, previousplayer); //send the picturebox information to the check surrounding method

                            if (validmove == false) { } //if checksurroundings find no valid move do nothing
                            else
                            {
                                gameboardTiles[i, j] = pt.currentplayer;// change tiles to current player colour
                                gameboardPictures[i, j].Image = Image.FromFile(pt.currentplayer + ".png"); // change to variable image based off of player

                                TileCount();

                                previousplayer = pt.currentplayer; //turn current player into previous player


                                ValidMovePossible(previousplayer);

                                if (validmovepossible == false)
                                {
                                    pt.playerturn(previousplayer);
                                    previousplayer = pt.currentplayer;

                                    ValidMovePossible(previousplayer);

                                    if (validmovepossible == false && endgame == false)
                                    {
                                        WinAnnounce();
                                    }
                                }
                            }
                            if (previousplayer == 0)
                            {
                                P1toplay.Visible = false;
                                P2toplay.Visible = true;
                            }
                            else
                            {
                                P1toplay.Visible = true;
                                P2toplay.Visible = false;
                            }
                            if (previousplayer == 1 && speechenabled == true)
                            {
                                synthesizer.SpeakAsync("It is " + textBox1.Text + "'s turn.");
                            }
                            else if (previousplayer == 0 && speechenabled == true)
                            {
                                synthesizer.SpeakAsync("It is " + textBox2.Text + "'s turn.");
                            }
                            break;
                        }
                    }
                }
            }

        }
        public void CheckSurroundings(int placedX, int placedY, int oppplayer)
        {
            ValidPlacement vp = new ValidPlacement(); // new valid placement instance
            int curplayer = 10; //default current player
            validmove = false; //valid move default is false

            if (oppplayer == 0) { curplayer = 1; } //current player is the opposite to the previous player
            else if (oppplayer == 1) { curplayer = 0; }


            int xcheck; //set default value 
            int ycheck; // set default value 

            int furtherx;
            int furthery;



            for (int i = 0; i < vp.ysurrounding.Length; ++i) //run through each possible movement around the placed tile
            {
                int lookincrease = 2; //starting increase when looking for a friendly tile

                xcheck = placedX + vp.xsurrounding[i]; //check the top left tile first and check clockwise after
                ycheck = placedY + vp.ysurrounding[i];

                if (xcheck >= 8 || xcheck < 0) { } //cancel the check if next to a corner or side
                else if (ycheck >= 8 || ycheck < 0) { }

                else if (gameboardTiles[xcheck, ycheck] == oppplayer) //check that the tile next to the placed tile is an opposition player
                {
                    for (int j = 0; j < 8; ++j)
                    {
                        furtherx = placedX + (vp.xsurrounding[i] * lookincrease); //check a tile further for a friendly tile
                        furthery = placedY + (vp.ysurrounding[i] * lookincrease);

                        if (furtherx >= 8 || furtherx < 0) { }
                        else if (furthery >= 8 || furthery < 0) { }
                        else if (gameboardTiles[furtherx, furthery] == 10)// if the further tile is an empty game board piece then break the for loop
                        {
                            break;
                        }
                        else if (gameboardTiles[furtherx, furthery] == curplayer) //if a friendly tile is found along the same line as the opposition tile
                        {
                            validmove = true;
                            changetiles(placedX, placedY, furtherx, furthery, curplayer);
                            break;
                        }
                        else
                        {
                            lookincrease++; //if no friendly tile found, increase distance to look
                        }
                    }
                }

            }

            if (validmove != true)
            {
                MessageBox.Show("This is not a valid move.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e) // Check if players named, if not default:
        {

        }

        private void helpToolStripMenuItem_Click_1(object sender, EventArgs e) // Help tab information 
        {

        }

        public void changetiles(int startx, int starty, int endx, int endy, int curplayer)
        {
            int diffx = endx - startx;
            int diffy = endy - starty;
            int xrep;
            int yrep;

            if (diffx < 0)
            {
                xrep = diffx * -1;
            }
            else
            {
                xrep = diffx;
            }

            if (diffy < 0)
            {
                yrep = diffy * -1;
            }
            else
            {
                yrep = diffy;
            }

            int rep = Math.Max(xrep, yrep);

            int xplace;
            int yplace;



            for (int i = 1; i < rep; i++)
            {
                if (diffx == 0)
                {
                    xplace = startx;
                }
                else
                {
                    if (diffx < 0)
                    {
                        xplace = endx + i;
                    }
                    else
                    {
                        xplace = endx - i;
                    }
                }

                if (diffy == 0)
                {
                    yplace = starty;
                }
                else
                {
                    if (diffy < 0)
                    {
                        yplace = endy + i;
                    }
                    else
                    {
                        yplace = endy - i;
                    }
                }
                gameboardTiles[xplace, yplace] = curplayer;
                gameboardPictures[xplace, yplace].Image = Image.FromFile(curplayer + ".png");
            }
        }
        public void TileCount()
        {
            int P1Tile = 0;
            int P2Tile = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gameboardTiles[i, j] == 0)
                    {
                        P1Tile++;
                    }
                    if (gameboardTiles[i, j] == 1)
                    {
                        P2Tile++;
                    }
                }
            }
            label1.Text = Convert.ToString(P1Tile);
            label2.Text = Convert.ToString(P2Tile);

            WinCheck(P1Tile, P2Tile);
        }
        public void WinAnnounce()
        {
            int P1Win = Convert.ToInt32(label1.Text);
            int P2Win = Convert.ToInt32(label2.Text);

            MessageBox.Show("There are no possible Moves left for either player!");

            if (P1Win == P2Win)
            {
                MessageBox.Show("The game ends in a draw!!");
                endgame = true;
                this.Close();
            }
            else if (P1Win > P2Win)
            {
                MessageBox.Show(textBox1.Text + " wins the game!!");
                endgame = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(textBox2.Text + " wins the game!!");
                endgame = true;
                this.Close();
            }
        }
        public void WinCheck(int P1Win, int P2Win)
        {
            int freespaces = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gameboardTiles[i, j] == 10)
                    {
                        freespaces++;
                    }
                }
            }
            if (freespaces > 0) { }
            else
            {
                if (P1Win == P2Win)
                {
                    MessageBox.Show("The game ends in a draw!!");
                    endgame = true;
                    this.Close();
                }
                else if (P1Win > P2Win)
                {
                    MessageBox.Show(textBox1.Text + " wins the game!!");
                    endgame = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(textBox2.Text + " wins the game!!");
                    endgame = true;
                    this.Close();
                }
            }
        }

        public void ValidMovePossible(int prevplayer)
        {
            int nextplayer;

            if (prevplayer == 0)
            {
                nextplayer = 1;
            }
            else
            {
                nextplayer = 0;
            }
            ValidPlacement vp = new ValidPlacement(); // new valid placement instance
            validmovepossible = false; //valid move default is false

            int xcheck; //set default value 
            int ycheck; // set default value 

            int furtherx;
            int furthery;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (gameboardTiles[x, y] == 10) //if a blank gameboard peice
                    {
                        gameboardPictures[x, y].Image = Image.FromFile("10.png"); // return to normal gamepiece look
                    }

                    for (int i = 0; i < vp.ysurrounding.Length; ++i)
                    {
                        int lookincrease = 2; //starting increase when looking for a friendly tile

                        xcheck = x + vp.xsurrounding[i]; //check the top left tile first and check clockwise after
                        ycheck = y + vp.ysurrounding[i];

                        if (xcheck >= 8 || xcheck < 0) { } //cancel the check if next to a corner or side
                        else if (ycheck >= 8 || ycheck < 0) { }

                        else if (gameboardTiles[xcheck, ycheck] == prevplayer) //check that the tile next to the placed tile is an opposition player
                        {
                            for (int j = 0; j < 8; ++j)
                            {
                                furtherx = x + (vp.xsurrounding[i] * lookincrease); //check a tile further for a friendly tile
                                furthery = y + (vp.ysurrounding[i] * lookincrease);

                                if (furtherx >= 8 || furtherx < 0) { }
                                else if (furthery >= 8 || furthery < 0) { }
                                else if (gameboardTiles[furtherx, furthery] == 10)// if the further tile is an empty game board piece then break the for loop
                                {
                                    break;
                                }
                                else if (gameboardTiles[x, y] == nextplayer || gameboardTiles[x, y] == prevplayer)
                                {
                                    break;
                                }
                                else if (gameboardTiles[furtherx, furthery] == nextplayer) //if a friendly tile is found along the same line as the opposition tile
                                {
                                    validmovepossible = true;
                                    gameboardPictures[x, y].Image = Image.FromFile("11.png"); //show that this is a possiible move
                                    break;
                                }
                                else
                                {
                                    lookincrease++; //if no friendly tile found, increase distance to look
                                }

                            }
                        }
                    }
                }

            }

            if (validmovepossible != true)
            {
                if (nextplayer == 0)
                {
                    MessageBox.Show("There are no available moves for " + textBox1.Text + ". Switching to " + textBox2.Text);
                }
                else
                {
                    MessageBox.Show("There are no available moves for " + textBox2.Text + ". Switching to " + textBox1.Text);
                }
            }
        }

        private void InformationPanelMenuItem_Click(object sender, EventArgs e)
        {
            if (InformationPanelMenuItem.Checked == true)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void speakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (speakToolStripMenuItem.Checked == true)
            {
                speechenabled = true;
            }
            else
            {
                speechenabled = false;
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameboardPictures[0, 0] != null)
            {
                SaveGame savegame = new SaveGame();
                DialogResult savecurrentgame = MessageBox.Show("There is a game currently ongoing! Do you wish to save this game?", "Save Game?", MessageBoxButtons.YesNo);
                //savegame.savegame(textBox1.Text, textBox2.Text, gameboardTiles, previousplayer);
            }
            else
            {
                if (textBox1.Text == "")
                {
                    textBox1.Text = "Player 1";
                }
                if (textBox2.Text == "")
                {
                    textBox2.Text = "Player 2";
                }
                textBox1.Enabled = false;
                textBox2.Enabled = false;

                label1.Text = Convert.ToString(2);
                label2.Text = Convert.ToString(2);

                P1 = textBox1.Text;
                P2 = textBox2.Text;

                P1toplay.Visible = true;
                exitToolStripMenuItem.Visible = true;
                saveGameToolStripMenuItem.Visible = true;

                GameBoardTileSetUp(); //call gameboard Tile setup method
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("O'Neillo is a take on the classic game Othello. This game should work in the exact same way you would expect Othello to.");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGame savegame = new SaveGame();
            DialogResult savecurrentgame = MessageBox.Show("Do you wish to save this game?", "Save Game?", MessageBoxButtons.YesNo);


        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult savecurrentgame = MessageBox.Show("Do you wish to save this game?", "Save Game?", MessageBoxButtons.YesNo);
            if (savecurrentgame == DialogResult.Yes)
            {
                SaveDataForm form = new SaveDataForm(P1, P2, gameboardTiles, previousplayer, savegame);

                form.Show();
            }


        }

        private void restoreGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}