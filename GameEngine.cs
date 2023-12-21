using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    public class GameEngine
    {
        public int[,] gameboardTiles;
        public PictureBox[,] gameboardPictures;

        public bool endgame = false;
        public int P1Tile = 0;
        public int P2Tile = 0;

        public int nextplayer;

        public bool validmovepossible;
        public bool validmove;

        public GameEngine(int[,] Tileboard, PictureBox[,] Pictureboard) 
        {
            gameboardTiles = Tileboard; 
            gameboardPictures = Pictureboard;
        }
        /// <summary>
        /// Checks the clicked Tiles surrounding for a valid move, then calls chnage tiles
        /// </summary>
        /// <param name="placedX">The X coordinate of the selected Tile</param>
        /// <param name="placedY">The Y coordinate of the selected Tile</param>
        /// <param name="oppplayer">The opposing player</param>
        internal void CheckSurroundings(int placedX, int placedY, int oppplayer)
        {
            ValidPlacement vp = new ValidPlacement(); 
            int curplayer = 10; //default current player
           

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
                MessageBox.Show("This is not a valid move.","Invalid Move!");
            }
        }
        /// <summary>
        /// checks before a players go if there is a move possible for them
        /// </summary>
        /// <param name="prevplayer">Previous Player</param>
        internal void ValidMovePossible(int prevplayer)
        {
            if (prevplayer == 0)
            {
                nextplayer = 1;
            }
            else
            {
                nextplayer = 0;
            }
            ValidPlacement vp = new ValidPlacement();

            int xcheck;
            int ycheck; 

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
        }
        /// <summary>
        /// Changes the Picture images based on the Tile array
        /// </summary>
        /// <param name="startx">Which X coordinate Tile was clicked</param>
        /// <param name="starty">Which Y coordinate Tile was clicked</param>
        /// <param name="endx">Which X coordinate Tile was found at the end of the line in the CheckSurrounding Method</param>
        /// <param name="endy">Which Y coordinate Tile was found at the end of the line in the CheckSurrounding Method</param>
        /// <param name="curplayer">Who the current player is</param>
        internal void changetiles(int startx, int starty, int endx, int endy, int curplayer)
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
        /// <summary>
        /// Counts how many tiles each player has
        /// </summary>
        internal void TileCount()
        {
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
            WinCheck();
        }
        /// <summary>
        /// Checks if there are any spaces left on the game board and determines if the game should end
        /// </summary>
        internal void WinCheck()
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
                endgame = true;
            }
        }
        /// <summary>
        /// Sets the board back up to the default state
        /// </summary>
        public void ResetBoard()
        {
            for(int i = 0;i < 8; i++)
            {
                for(int j = 0; j < 8;j++)
                {
                    gameboardTiles[i, j] = 10;
                    gameboardPictures[i, j].Image = Image.FromFile("10.png");

                }
            }
            gameboardTiles[3, 4] = 0;
            gameboardPictures[3, 4].Image = Image.FromFile("0.png");
            gameboardTiles[4, 3] = 0;
            gameboardPictures[4,3].Image = Image.FromFile("0.png");
            gameboardTiles[3, 3] = 1;
            gameboardPictures[3, 3].Image = Image.FromFile("1.png");
            gameboardTiles[4, 4] = 1;
            gameboardPictures[4, 4].Image = Image.FromFile("1.png");
        }

    }
}
